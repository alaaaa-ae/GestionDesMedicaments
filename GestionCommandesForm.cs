using GestionDesMedicaments.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GestionDesMedicaments.Clients;

namespace GestionDesMedicaments
{
    public class GestionCommandesForm : Form
    {
        private PrintDocument printDoc = new PrintDocument();
        private string printText = "";
        private string commandeUserColumn = "id_utilisateur";
        private SidebarControl sidebar;
        private Panel panelContent;
        private ListView listViewCommandes;
        private ListView listViewDetails;
        private TextBox txtClient;
        private DateTimePicker dtpDate;
        private Button btnNouveau, btnModifier, btnSupprimer, btnImprimer, btnStats, btnRechercher, btnReinitialiser;
        private Label lblTitre;
        private ComboBox comboBoxStatut;
        private int selectedCommandeId = -1;

        public GestionCommandesForm()
        {
            InitializeComponent();
            InitialiserColonnes();
            ChargerCommandes();
            printDoc.PrintPage += PrintDoc_PrintPage;
            this.WindowState = FormWindowState.Maximized;
        }

        private void InitializeComponent()
        {
            this.sidebar = new SidebarControl();
            this.panelContent = new Panel();
            this.listViewCommandes = new ListView();
            this.listViewDetails = new ListView();
            this.txtClient = new TextBox();
            this.dtpDate = new DateTimePicker();
            this.btnNouveau = new Button();
            this.btnModifier = new Button();
            this.btnSupprimer = new Button();
            this.btnImprimer = new Button();
            this.btnStats = new Button();
            this.btnRechercher = new Button();
            this.btnReinitialiser = new Button();
            this.lblTitre = new Label();
            this.comboBoxStatut = new ComboBox();

            this.SuspendLayout();

            // Sidebar
            this.sidebar.Dock = DockStyle.Left;
            this.sidebar.SetActiveButton("commandes");
            this.sidebar.MedicamentsClicked += Sidebar_MedicamentsClicked;
            this.sidebar.CommandesClicked += Sidebar_CommandesClicked;
            this.sidebar.ClientsClicked += Sidebar_ClientsClicked;
            this.sidebar.DashboardClicked += Sidebar_DashboardClicked;
            this.sidebar.RefreshClicked += Sidebar_RefreshClicked;
            this.sidebar.DeconnexionClicked += Sidebar_DeconnexionClicked;

            // Panel Content
            this.panelContent.Dock = DockStyle.Fill;
            this.panelContent.BackColor = Color.FromArgb(245, 247, 250);
            this.panelContent.Padding = new Padding(20);
            this.panelContent.AutoScroll = true;

            // Titre
            this.lblTitre.Text = "📦 Gestion des Commandes";
            this.lblTitre.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            this.lblTitre.ForeColor = Color.FromArgb(50, 50, 60);
            this.lblTitre.Location = new Point(20, 20);
            this.lblTitre.AutoSize = true;

            // Panel Recherche
            var panelRecherche = new Panel
            {
                Location = new Point(20, 80),
                Size = new Size(1200, 80),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Padding = new Padding(15)
            };

            var lblRecherche = new Label
            {
                Text = "🔍 Recherche",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 60),
                Location = new Point(15, 10),
                AutoSize = true
            };

            var lblDate = new Label
            {
                Text = "📅 Date:",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 100, 120),
                Location = new Point(15, 45),
                AutoSize = true
            };

            this.dtpDate.Location = new Point(80, 42);
            this.dtpDate.Size = new Size(150, 25);
            this.dtpDate.Format = DateTimePickerFormat.Short;
            this.dtpDate.Font = new Font("Segoe UI", 9F);
            this.dtpDate.Value = DateTime.Today;

            var lblClient = new Label
            {
                Text = "👤 Client:",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 100, 120),
                Location = new Point(250, 45),
                AutoSize = true
            };

            this.txtClient.Location = new Point(310, 42);
            this.txtClient.Size = new Size(250, 25);
            this.txtClient.Font = new Font("Segoe UI", 9F);
            this.txtClient.BorderStyle = BorderStyle.FixedSingle;

            this.btnRechercher.Text = "🔍 Rechercher";
            this.btnRechercher.Location = new Point(580, 40);
            this.btnRechercher.Size = new Size(120, 30);
            this.btnRechercher.BackColor = Color.FromArgb(255, 140, 0);
            this.btnRechercher.ForeColor = Color.White;
            this.btnRechercher.FlatStyle = FlatStyle.Flat;
            this.btnRechercher.FlatAppearance.BorderSize = 0;
            this.btnRechercher.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnRechercher.Click += BtnRechercher_Click;

            this.btnReinitialiser.Text = "🔄 Réinitialiser";
            this.btnReinitialiser.Location = new Point(710, 40);
            this.btnReinitialiser.Size = new Size(120, 30);
            this.btnReinitialiser.BackColor = Color.FromArgb(100, 100, 100);
            this.btnReinitialiser.ForeColor = Color.White;
            this.btnReinitialiser.FlatStyle = FlatStyle.Flat;
            this.btnReinitialiser.FlatAppearance.BorderSize = 0;
            this.btnReinitialiser.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnReinitialiser.Click += BtnReinitialiser_Click;

            panelRecherche.Controls.AddRange(new Control[] { lblRecherche, lblDate, dtpDate, lblClient, txtClient, btnRechercher, btnReinitialiser });

            // ListView Commandes
            this.listViewCommandes.Location = new Point(20, 180);
            this.listViewCommandes.Size = new Size(1200, 300);
            this.listViewCommandes.View = View.Details;
            this.listViewCommandes.FullRowSelect = true;
            this.listViewCommandes.GridLines = true;
            this.listViewCommandes.BorderStyle = BorderStyle.None;
            this.listViewCommandes.BackColor = Color.White;
            this.listViewCommandes.Font = new Font("Segoe UI", 9F);
            this.listViewCommandes.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.listViewCommandes.MultiSelect = false;
            this.listViewCommandes.SelectedIndexChanged += ListViewCommandes_SelectedIndexChanged;
            this.listViewCommandes.Columns.Add("ID", 60);
            this.listViewCommandes.Columns.Add("Client", 250);
            this.listViewCommandes.Columns.Add("Date", 150);
            this.listViewCommandes.Columns.Add("Total", 120);
            this.listViewCommandes.Columns.Add("Statut", 150);

            var lblCommandes = new Label
            {
                Text = "📋 Liste des Commandes",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 60),
                Location = new Point(20, 155),
                AutoSize = true
            };

            // Panel Statut
            var panelStatut = new Panel
            {
                Location = new Point(20, 500),
                Size = new Size(400, 60),
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Padding = new Padding(15)
            };

            var lblStatut = new Label
            {
                Text = "Modifier le statut:",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 60),
                Location = new Point(15, 20),
                AutoSize = true
            };

            this.comboBoxStatut.Location = new Point(150, 17);
            this.comboBoxStatut.Size = new Size(200, 25);
            this.comboBoxStatut.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxStatut.Items.AddRange(new string[] { "En attente", "Confirmée", "Préparée", "Livrée", "Annulée" });
            this.comboBoxStatut.Font = new Font("Segoe UI", 9F);
            this.comboBoxStatut.SelectedIndexChanged += ComboBoxStatut_SelectedIndexChanged;

            panelStatut.Controls.AddRange(new Control[] { lblStatut, comboBoxStatut });

            // ListView Détails
            this.listViewDetails.Location = new Point(440, 500);
            this.listViewDetails.Size = new Size(780, 200);
            this.listViewDetails.View = View.Details;
            this.listViewDetails.FullRowSelect = true;
            this.listViewDetails.GridLines = true;
            this.listViewDetails.BorderStyle = BorderStyle.None;
            this.listViewDetails.BackColor = Color.White;
            this.listViewDetails.Font = new Font("Segoe UI", 9F);
            this.listViewDetails.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.listViewDetails.Columns.Add("Médicament", 350);
            this.listViewDetails.Columns.Add("Quantité", 100);
            this.listViewDetails.Columns.Add("Prix Unitaire", 150);
            this.listViewDetails.Columns.Add("Sous-Total", 150);

            var lblDetails = new Label
            {
                Text = "📄 Détails de la Commande",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 60),
                Location = new Point(440, 475),
                AutoSize = true
            };

            // Boutons Actions
            this.btnNouveau.Text = "➕ Nouveau";
            this.btnNouveau.Location = new Point(20, 720);
            this.btnNouveau.Size = new Size(120, 40);
            this.btnNouveau.BackColor = Color.FromArgb(255, 140, 0);
            this.btnNouveau.ForeColor = Color.White;
            this.btnNouveau.FlatStyle = FlatStyle.Flat;
            this.btnNouveau.FlatAppearance.BorderSize = 0;
            this.btnNouveau.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnNouveau.Click += BtnNouveau_Click;

            this.btnModifier.Text = "✏️ Modifier";
            this.btnModifier.Location = new Point(150, 720);
            this.btnModifier.Size = new Size(120, 40);
            this.btnModifier.BackColor = Color.FromArgb(60, 160, 60);
            this.btnModifier.ForeColor = Color.White;
            this.btnModifier.FlatStyle = FlatStyle.Flat;
            this.btnModifier.FlatAppearance.BorderSize = 0;
            this.btnModifier.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnModifier.Click += BtnModifier_Click;

            this.btnSupprimer.Text = "❌ Supprimer";
            this.btnSupprimer.Location = new Point(280, 720);
            this.btnSupprimer.Size = new Size(120, 40);
            this.btnSupprimer.BackColor = Color.FromArgb(200, 50, 50);
            this.btnSupprimer.ForeColor = Color.White;
            this.btnSupprimer.FlatStyle = FlatStyle.Flat;
            this.btnSupprimer.FlatAppearance.BorderSize = 0;
            this.btnSupprimer.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnSupprimer.Click += BtnSupprimer_Click;

            this.btnImprimer.Text = "🖨️ Imprimer";
            this.btnImprimer.Location = new Point(410, 720);
            this.btnImprimer.Size = new Size(120, 40);
            this.btnImprimer.BackColor = Color.FromArgb(100, 150, 200);
            this.btnImprimer.ForeColor = Color.White;
            this.btnImprimer.FlatStyle = FlatStyle.Flat;
            this.btnImprimer.FlatAppearance.BorderSize = 0;
            this.btnImprimer.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnImprimer.Click += BtnImprimer_Click;

            this.btnStats.Text = "📊 Statistiques";
            this.btnStats.Location = new Point(540, 720);
            this.btnStats.Size = new Size(140, 40);
            this.btnStats.BackColor = Color.FromArgb(255, 140, 0);
            this.btnStats.ForeColor = Color.White;
            this.btnStats.FlatStyle = FlatStyle.Flat;
            this.btnStats.FlatAppearance.BorderSize = 0;
            this.btnStats.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnStats.Click += BtnStats_Click;

            this.panelContent.Controls.Add(this.lblTitre);
            this.panelContent.Controls.Add(panelRecherche);
            this.panelContent.Controls.Add(lblCommandes);
            this.panelContent.Controls.Add(this.listViewCommandes);
            this.panelContent.Controls.Add(panelStatut);
            this.panelContent.Controls.Add(lblDetails);
            this.panelContent.Controls.Add(this.listViewDetails);
            this.panelContent.Controls.Add(this.btnNouveau);
            this.panelContent.Controls.Add(this.btnModifier);
            this.panelContent.Controls.Add(this.btnSupprimer);
            this.panelContent.Controls.Add(this.btnImprimer);
            this.panelContent.Controls.Add(this.btnStats);

            this.Controls.Add(this.sidebar);
            this.Controls.Add(this.panelContent);

            this.BackColor = Color.FromArgb(245, 247, 250);
            this.Text = "📦 Gestion des Commandes";
            this.ResumeLayout(false);

            this.Load += (s, e) => AjusterTailles();
        }

        private void AjusterTailles()
        {
            int contentWidth = this.panelContent.Width - 40;
            if (contentWidth > 0)
            {
                var panelRecherche = this.panelContent.Controls.OfType<Panel>().FirstOrDefault(p => p.Controls.Contains(dtpDate));
                if (panelRecherche != null) panelRecherche.Size = new Size(contentWidth, 80);
                this.listViewCommandes.Size = new Size(contentWidth, 300);
                this.listViewDetails.Size = new Size(contentWidth - 420, 200);
                this.listViewDetails.Location = new Point(440, 500);
            }
        }

        private void InitialiserColonnes()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();
                    commandeUserColumn = Database.GetExistingColumn(conn, "Commande", "id_utilisateur", "id_client");
                }
            }
            catch
            {
                commandeUserColumn = "id_utilisateur";
            }
        }

        private void ChargerCommandes()
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    this.listViewCommandes.Items.Clear();

                    string query = $@"
                        SELECT 
                            c.id_commande,
                            u.nom + ' ' + u.prenom as Utilisateur,
                            c.date_commande,
                            ISNULL(f.total, 0) as total,
                            c.statut
                        FROM Commande c
                        INNER JOIN Utilisateur u ON c.{commandeUserColumn} = u.id_utilisateur
                        LEFT JOIN Facture f ON c.id_commande = f.id_commande
                        ORDER BY c.date_commande DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new ListViewItem(reader["id_commande"].ToString());
                                item.SubItems.Add(reader["Utilisateur"].ToString());
                                item.SubItems.Add(Convert.ToDateTime(reader["date_commande"]).ToString("dd/MM/yyyy HH:mm"));
                                item.SubItems.Add(Convert.ToDecimal(reader["total"]).ToString("C2"));
                                item.SubItems.Add(reader["statut"].ToString());
                                item.Tag = Convert.ToInt32(reader["id_commande"]);
                                
                                // Colorer selon le statut
                                string statut = reader["statut"].ToString();
                                if (statut == "Confirmée") item.ForeColor = Color.Green;
                                else if (statut == "Annulée") item.ForeColor = Color.Red;
                                else if (statut == "Livrée") item.ForeColor = Color.Blue;
                                
                                this.listViewCommandes.Items.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des commandes: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChargerDetailsCommande(int idCommande)
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    this.listViewDetails.Items.Clear();

                    string query = @"
                        SELECT 
                            m.nom as NomMedicament,
                            lc.quantite,
                            COALESCE(lc.prix_unitaire, m.prix_vente) as prix_unitaire,
                            (lc.quantite * COALESCE(lc.prix_unitaire, m.prix_vente)) as SousTotal
                        FROM LigneCommande lc
                        INNER JOIN Medicament m ON lc.id_medicament = m.id_medicament
                        WHERE lc.id_commande = @IdCommande";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdCommande", idCommande);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new ListViewItem(reader["NomMedicament"].ToString());
                                item.SubItems.Add(reader["quantite"].ToString());
                                item.SubItems.Add(Convert.ToDecimal(reader["prix_unitaire"]).ToString("C2"));
                                item.SubItems.Add(Convert.ToDecimal(reader["SousTotal"]).ToString("C2"));
                                this.listViewDetails.Items.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des détails: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ListViewCommandes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewCommandes.SelectedItems.Count > 0)
            {
                selectedCommandeId = (int)listViewCommandes.SelectedItems[0].Tag;
                string statut = listViewCommandes.SelectedItems[0].SubItems[4].Text;
                int index = comboBoxStatut.Items.IndexOf(statut);
                if (index >= 0) comboBoxStatut.SelectedIndex = index;
                ChargerDetailsCommande(selectedCommandeId);
            }
        }

        private void ComboBoxStatut_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedCommandeId > 0 && comboBoxStatut.SelectedItem != null)
            {
                string nouveauStatut = comboBoxStatut.SelectedItem.ToString();
                MettreAJourStatutCommande(selectedCommandeId, nouveauStatut);
            }
        }

        private void MettreAJourStatutCommande(int idCommande, string nouveauStatut)
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();
                    
                    try
                    {
                        // Récupérer l'ancien statut pour vérifier si on doit ajuster le stock
                        string queryOldStatus = "SELECT statut FROM Commande WHERE id_commande=@IdCommande";
                        SqlCommand cmdOldStatus = new SqlCommand(queryOldStatus, conn, transaction);
                        cmdOldStatus.Parameters.AddWithValue("@IdCommande", idCommande);
                        string ancienStatut = cmdOldStatus.ExecuteScalar()?.ToString() ?? "";

                        // Mettre à jour le statut
                        string query = "UPDATE Commande SET statut=@Statut WHERE id_commande=@IdCommande";
                        SqlCommand cmd = new SqlCommand(query, conn, transaction);
                        cmd.Parameters.AddWithValue("@Statut", nouveauStatut);
                        cmd.Parameters.AddWithValue("@IdCommande", idCommande);
                        cmd.ExecuteNonQuery();

                        // Si le nouveau statut est "Confirmée" et que l'ancien statut n'était pas "Confirmée"
                        if (nouveauStatut == "Confirmée" && ancienStatut != "Confirmée")
                        {
                            // Récupérer toutes les lignes de commande pour cette commande
                            string queryLignes = @"
                                SELECT id_medicament, quantite 
                                FROM LigneCommande 
                                WHERE id_commande = @IdCommande";
                            SqlCommand cmdLignes = new SqlCommand(queryLignes, conn, transaction);
                            cmdLignes.Parameters.AddWithValue("@IdCommande", idCommande);

                            using (SqlDataReader reader = cmdLignes.ExecuteReader())
                            {
                                List<(int idMedicament, int quantite)> lignes = new List<(int, int)>();
                                while (reader.Read())
                                {
                                    lignes.Add((
                                        Convert.ToInt32(reader["id_medicament"]),
                                        Convert.ToInt32(reader["quantite"])
                                    ));
                                }
                                reader.Close();

                                // Diminuer le stock pour chaque médicament
                                foreach (var ligne in lignes)
                                {
                                    // Vérifier que le stock est suffisant
                                    string queryCheckStock = "SELECT stock FROM Medicament WHERE id_medicament = @IdMedicament";
                                    SqlCommand cmdCheckStock = new SqlCommand(queryCheckStock, conn, transaction);
                                    cmdCheckStock.Parameters.AddWithValue("@IdMedicament", ligne.idMedicament);
                                    object stockObj = cmdCheckStock.ExecuteScalar();
                                    
                                    if (stockObj != null && stockObj != DBNull.Value)
                                    {
                                        int stockActuel = Convert.ToInt32(stockObj);
                                        if (stockActuel < ligne.quantite)
                                        {
                                            transaction.Rollback();
                                            MessageBox.Show($"Stock insuffisant pour le médicament ID {ligne.idMedicament}. Stock disponible: {stockActuel}, Quantité demandée: {ligne.quantite}",
                                                "Erreur Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            ChargerCommandes();
                                            return;
                                        }

                                        // Diminuer le stock
                                        string queryUpdateStock = @"
                                            UPDATE Medicament 
                                            SET stock = stock - @Quantite 
                                            WHERE id_medicament = @IdMedicament";
                                        SqlCommand cmdUpdateStock = new SqlCommand(queryUpdateStock, conn, transaction);
                                        cmdUpdateStock.Parameters.AddWithValue("@Quantite", ligne.quantite);
                                        cmdUpdateStock.Parameters.AddWithValue("@IdMedicament", ligne.idMedicament);
                                        cmdUpdateStock.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                        // Si on annule une commande confirmée, restituer le stock
                        else if (nouveauStatut == "Annulée" && ancienStatut == "Confirmée")
                        {
                            string queryLignes = @"
                                SELECT id_medicament, quantite 
                                FROM LigneCommande 
                                WHERE id_commande = @IdCommande";
                            SqlCommand cmdLignes = new SqlCommand(queryLignes, conn, transaction);
                            cmdLignes.Parameters.AddWithValue("@IdCommande", idCommande);

                            using (SqlDataReader reader = cmdLignes.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int idMedicament = Convert.ToInt32(reader["id_medicament"]);
                                    int quantite = Convert.ToInt32(reader["quantite"]);

                                    // Restituer le stock
                                    string queryRestoreStock = @"
                                        UPDATE Medicament 
                                        SET stock = stock + @Quantite 
                                        WHERE id_medicament = @IdMedicament";
                                    SqlCommand cmdRestoreStock = new SqlCommand(queryRestoreStock, conn, transaction);
                                    cmdRestoreStock.Parameters.AddWithValue("@Quantite", quantite);
                                    cmdRestoreStock.Parameters.AddWithValue("@IdMedicament", idMedicament);
                                    cmdRestoreStock.ExecuteNonQuery();
                                }
                            }
                        }

                        transaction.Commit();
                        MessageBox.Show("Statut de la commande mis à jour avec succès.", "Succès",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ChargerCommandes();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la mise à jour du statut: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                ChargerCommandes();
            }
        }

        private void BtnRechercher_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    this.listViewCommandes.Items.Clear();

                    string query = $@"
                        SELECT 
                            c.id_commande,
                            u.nom + ' ' + u.prenom as Utilisateur,
                            c.date_commande,
                            ISNULL(f.total, 0) as total,
                            c.statut
                        FROM Commande c
                        INNER JOIN Utilisateur u ON c.{commandeUserColumn} = u.id_utilisateur
                        LEFT JOIN Facture f ON c.id_commande = f.id_commande
                        WHERE CAST(c.date_commande AS DATE) = @Date";

                    if (!string.IsNullOrWhiteSpace(txtClient.Text))
                    {
                        query += " AND (u.nom LIKE @Client OR u.prenom LIKE @Client OR (u.nom + ' ' + u.prenom) LIKE @Client OR u.nom_utilisateur LIKE @Client)";
                    }

                    query += " ORDER BY c.date_commande DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Date", dtpDate.Value.Date);
                        if (!string.IsNullOrWhiteSpace(txtClient.Text))
                        {
                            cmd.Parameters.AddWithValue("@Client", $"%{txtClient.Text.Trim()}%");
                        }

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new ListViewItem(reader["id_commande"].ToString());
                                item.SubItems.Add(reader["Utilisateur"].ToString());
                                item.SubItems.Add(Convert.ToDateTime(reader["date_commande"]).ToString("dd/MM/yyyy HH:mm"));
                                item.SubItems.Add(Convert.ToDecimal(reader["total"]).ToString("C2"));
                                item.SubItems.Add(reader["statut"].ToString());
                                item.Tag = Convert.ToInt32(reader["id_commande"]);
                                
                                string statut = reader["statut"].ToString();
                                if (statut == "Confirmée") item.ForeColor = Color.Green;
                                else if (statut == "Annulée") item.ForeColor = Color.Red;
                                else if (statut == "Livrée") item.ForeColor = Color.Blue;
                                
                                this.listViewCommandes.Items.Add(item);
                            }
                        }
                    }

                    if (listViewCommandes.Items.Count == 0)
                    {
                        MessageBox.Show("Aucune commande trouvée avec ces critères.", "Recherche",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la recherche: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnReinitialiser_Click(object sender, EventArgs e)
        {
            dtpDate.Value = DateTime.Today;
            txtClient.Clear();
            ChargerCommandes();
        }

        private void BtnNouveau_Click(object sender, EventArgs e)
        {
            try
            {
                using (CommandeEditForm f = new CommandeEditForm())
                {
                    f.Mode = CommandeEditForm.EditMode.Create;
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        ChargerCommandes();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur: {ex.Message}");
            }
        }

        private void BtnModifier_Click(object sender, EventArgs e)
        {
            if (selectedCommandeId == -1)
            {
                MessageBox.Show("Sélectionnez une commande.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (CommandeEditForm f = new CommandeEditForm())
            {
                f.Mode = CommandeEditForm.EditMode.Edit;
                f.IdCommande = selectedCommandeId;
                if (f.ShowDialog() == DialogResult.OK)
                {
                    ChargerCommandes();
                }
            }
        }

        private void BtnSupprimer_Click(object sender, EventArgs e)
        {
            if (selectedCommandeId == -1) return;
            var result = MessageBox.Show("Voulez-vous supprimer la commande sélectionnée ?", "Confirmer", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes) return;

            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM LigneCommande WHERE id_commande=@Id; DELETE FROM Facture WHERE id_commande=@Id; DELETE FROM Commande WHERE id_commande=@Id;", conn);
                    cmd.Parameters.AddWithValue("@Id", selectedCommandeId);
                    cmd.ExecuteNonQuery();
                    ChargerCommandes();
                    selectedCommandeId = -1;
                    listViewDetails.Items.Clear();
                    comboBoxStatut.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la suppression: {ex.Message}");
            }
        }

        private void BtnImprimer_Click(object sender, EventArgs e)
        {
            if (selectedCommandeId == -1)
            {
                MessageBox.Show("Sélectionnez une commande à imprimer.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                StringBuilder sb = new StringBuilder();
                decimal totalCommande = 0;

                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();

                    SqlCommand cmdHeader = new SqlCommand($@"
                        SELECT 
                            c.id_commande, 
                            u.nom + ' ' + u.prenom as Utilisateur, 
                            u.adresse,
                            u.telephone,
                            c.date_commande, 
                            c.statut, 
                            ISNULL(f.total,0) as total
                        FROM Commande c
                        INNER JOIN Utilisateur u ON c.{commandeUserColumn} = u.id_utilisateur
                        LEFT JOIN Facture f ON c.id_commande = f.id_commande
                        WHERE c.id_commande = @Id", conn);
                    cmdHeader.Parameters.AddWithValue("@Id", selectedCommandeId);

                    using (SqlDataReader r = cmdHeader.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            sb.AppendLine("═══════════════════════════════════════════════════════");
                            sb.AppendLine("              PHARMACIE - BON DE COMMANDE");
                            sb.AppendLine("═══════════════════════════════════════════════════════");
                            sb.AppendLine();
                            sb.AppendLine($"Commande N°: {r["id_commande"]}");
                            sb.AppendLine($"Date: {Convert.ToDateTime(r["date_commande"]).ToString("dd/MM/yyyy HH:mm")}");
                            sb.AppendLine($"Statut: {r["statut"]}");
                            sb.AppendLine();
                            sb.AppendLine("───────────────────────────────────────────────────────");
                            sb.AppendLine("INFORMATIONS CLIENT");
                            sb.AppendLine("───────────────────────────────────────────────────────");
                            sb.AppendLine($"Client: {r["Utilisateur"]}");
                            if (r["adresse"] != DBNull.Value && !string.IsNullOrEmpty(r["adresse"].ToString()))
                                sb.AppendLine($"Adresse: {r["adresse"]}");
                            if (r["telephone"] != DBNull.Value && !string.IsNullOrEmpty(r["telephone"].ToString()))
                                sb.AppendLine($"Téléphone: {r["telephone"]}");
                            sb.AppendLine();
                            sb.AppendLine("───────────────────────────────────────────────────────");
                            sb.AppendLine("DÉTAILS DE LA COMMANDE");
                            sb.AppendLine("───────────────────────────────────────────────────────");
                            totalCommande = Convert.ToDecimal(r["total"]);
                        }
                    }

                    SqlCommand cmdLines = new SqlCommand(@"
                        SELECT 
                            m.nom as Medicament, 
                            lc.quantite, 
                            COALESCE(lc.prix_unitaire, m.prix_vente) as prix_unitaire, 
                            (lc.quantite * COALESCE(lc.prix_unitaire, m.prix_vente)) as SousTotal
                        FROM LigneCommande lc
                        INNER JOIN Medicament m ON lc.id_medicament = m.id_medicament
                        WHERE lc.id_commande = @Id
                        ORDER BY m.nom", conn);
                    cmdLines.Parameters.AddWithValue("@Id", selectedCommandeId);

                    using (SqlDataReader r2 = cmdLines.ExecuteReader())
                    {
                        sb.AppendLine($"{"Médicament",-35} {"Qté",5} {"Prix U.",12} {"Total",12}");
                        sb.AppendLine(new string('-', 70));

                        int ligneNum = 1;
                        while (r2.Read())
                        {
                            string prod = r2["Medicament"].ToString();
                            if (prod.Length > 35) prod = prod.Substring(0, 32) + "...";

                            int q = Convert.ToInt32(r2["quantite"]);
                            decimal pu = Convert.ToDecimal(r2["prix_unitaire"]);
                            decimal st = Convert.ToDecimal(r2["SousTotal"]);

                            sb.AppendLine($"{ligneNum,2}. {prod,-33} {q,5} {pu,12:C2} {st,12:C2}");
                            ligneNum++;
                        }
                    }

                    sb.AppendLine(new string('-', 70));
                    sb.AppendLine($"{"TOTAL",42} {totalCommande,12:C2}");
                    sb.AppendLine();
                    sb.AppendLine("═══════════════════════════════════════════════════════");
                    sb.AppendLine($"Imprimé le: {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}");
                    sb.AppendLine("═══════════════════════════════════════════════════════");
                }

                printText = sb.ToString();

                PrintPreviewDialog preview = new PrintPreviewDialog();
                preview.Document = printDoc;
                preview.Width = 900;
                preview.Height = 700;
                preview.Text = "Aperçu avant impression - Commande";

                var result = MessageBox.Show(
                    "Voulez-vous voir l'aperçu avant impression ?\n\nCliquez sur Oui pour l'aperçu, Non pour imprimer directement.",
                    "Impression",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    preview.ShowDialog();
                }
                else if (result == DialogResult.No)
                {
                    PrintDialog printDialog = new PrintDialog();
                    printDialog.Document = printDoc;
                    if (printDialog.ShowDialog() == DialogResult.OK)
                    {
                        printDoc.Print();
                        MessageBox.Show("Commande imprimée avec succès !", "Succès",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'impression: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font font = new Font("Courier New", 9, FontStyle.Regular);
            Font fontBold = new Font("Courier New", 9, FontStyle.Bold);
            Brush brush = Brushes.Black;
            float yPos = e.MarginBounds.Top;
            float leftMargin = e.MarginBounds.Left;
            float lineHeight = font.GetHeight(e.Graphics);

            string[] lines = printText.Split('\n');

            foreach (string line in lines)
            {
                if (yPos + lineHeight > e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }

                Font currentFont = (line.Contains("═══") || line.Contains("───") || line.Contains("PHARMACIE"))
                    ? fontBold : font;

                e.Graphics.DrawString(line, currentFont, brush, leftMargin, yPos);
                yPos += lineHeight;
            }

            e.HasMorePages = false;
        }

        private void BtnStats_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();

                    SqlCommand cmdGlobal = new SqlCommand(@"
                        SELECT 
                            COUNT(*) as NbTotal,
                            ISNULL(SUM(f.total),0) as TotalVentes,
                            COUNT(CASE WHEN c.statut = 'Livrée' THEN 1 END) as NbLivrees,
                            COUNT(CASE WHEN c.statut = 'En attente' THEN 1 END) as NbEnAttente
                        FROM Commande c
                        LEFT JOIN Facture f ON c.id_commande = f.id_commande", conn);

                    StringBuilder stats = new StringBuilder();
                    stats.AppendLine("═══════════════════════════════════════");
                    stats.AppendLine("     STATISTIQUES DES COMMANDES");
                    stats.AppendLine("═══════════════════════════════════════");
                    stats.AppendLine();

                    using (SqlDataReader r = cmdGlobal.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            int nbTotal = Convert.ToInt32(r["NbTotal"]);
                            decimal totalVentes = Convert.ToDecimal(r["TotalVentes"]);
                            int nbLivrees = Convert.ToInt32(r["NbLivrees"]);
                            int nbEnAttente = Convert.ToInt32(r["NbEnAttente"]);

                            stats.AppendLine($"Nombre total de commandes: {nbTotal}");
                            stats.AppendLine($"Total des ventes: {totalVentes:C2}");
                            stats.AppendLine($"Commandes livrées: {nbLivrees}");
                            stats.AppendLine($"Commandes en attente: {nbEnAttente}");
                            stats.AppendLine();

                            if (nbTotal > 0)
                            {
                                decimal moyenne = totalVentes / nbTotal;
                                stats.AppendLine($"Moyenne par commande: {moyenne:C2}");
                            }
                        }
                    }

                    stats.AppendLine("═══════════════════════════════════════");

                    MessageBox.Show(stats.ToString(), "Statistiques",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du calcul des statistiques: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Événements Sidebar
        private void Sidebar_MedicamentsClicked(object sender, EventArgs e) { OuvrirForm(new GestionMedicamentsForm()); }
        private void Sidebar_CommandesClicked(object sender, EventArgs e) { }
        private void Sidebar_ClientsClicked(object sender, EventArgs e) { OuvrirForm(new GestionClientsForm()); }
        private void Sidebar_DashboardClicked(object sender, EventArgs e) { OuvrirForm(new DashboardPharmacien()); }
        private void Sidebar_RefreshClicked(object sender, EventArgs e) { ChargerCommandes(); }
        private void Sidebar_DeconnexionClicked(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Close();
        }

        private void OuvrirForm(Form form)
        {
            form.WindowState = FormWindowState.Maximized;
            form.Show();
            this.Hide();
        }
    }
}
