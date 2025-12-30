using GestionDesMedicaments.Classes;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GestionDesMedicaments
{
    public class GestionClientsForm : Form
    {
        private SidebarControl sidebar;
        private Panel panelContent;
        private ListView listViewClients;
        private ListView listViewFactures;
        private TextBox txtRecherche;
        private TextBox txtHiddenId, txtNomUtilisateur, txtMotDePasse, txtNom, txtPrenom, txtAdresse, txtTelephone, txtEmail;
        private TextBox txtMontantPaiement;
        private ComboBox comboBoxModePaiement;
        private Button btnAjouter, btnModifier, btnSupprimer, btnNouveau, btnEnregistrerPaiement;
        private Label lblTitre;
        private int selectedClientId = 0;

        public GestionClientsForm()
        {
            InitializeComponent();
            LoadClients();
            this.WindowState = FormWindowState.Maximized;
        }

        private void InitializeComponent()
        {
            this.sidebar = new SidebarControl();
            this.panelContent = new Panel();
            // var panelContainer = new Panel(); // Non nécessaire car panelContent est le conteneur principal du contenu
            this.listViewClients = new ListView();
            this.listViewFactures = new ListView();
            this.txtRecherche = new TextBox();
            this.txtHiddenId = new TextBox();
            this.txtNomUtilisateur = new TextBox();
            this.txtMotDePasse = new TextBox();
            this.txtNom = new TextBox();
            this.txtPrenom = new TextBox();
            this.txtAdresse = new TextBox();
            this.txtTelephone = new TextBox();
            this.txtEmail = new TextBox();
            this.txtMontantPaiement = new TextBox();
            this.comboBoxModePaiement = new ComboBox();
            this.btnAjouter = new Button();
            this.btnModifier = new Button();
            this.btnSupprimer = new Button();
            this.btnNouveau = new Button();
            this.btnEnregistrerPaiement = new Button();
            this.lblTitre = new Label();
            
            // --- Configuration du Formulaire Principal ---
            this.SuspendLayout();
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.Text = "👥 Gestion des Clients";

            // --- Sidebar ---
            this.sidebar.Dock = DockStyle.Left;
            this.sidebar.SetActiveButton("clients");
            this.sidebar.MedicamentsClicked += Sidebar_MedicamentsClicked;
            this.sidebar.CommandesClicked += Sidebar_CommandesClicked;
            this.sidebar.ClientsClicked += Sidebar_ClientsClicked;
            this.sidebar.DashboardClicked += Sidebar_DashboardClicked;
            this.sidebar.RefreshClicked += Sidebar_RefreshClicked;
            this.sidebar.DeconnexionClicked += Sidebar_DeconnexionClicked;
            
            // --- Panel Content ---
            // Ce panel prend tout l'espace restant à droite de la Sidebar
            this.panelContent.Dock = DockStyle.Fill;
            this.panelContent.BackColor = Color.FromArgb(245, 247, 250);
            this.panelContent.Padding = new Padding(20); // Marge interne
            this.panelContent.AutoScroll = true; // Permet de faire défiler si le contenu dépasse

            // --- Titre ---
            this.lblTitre.Text = "👥 Gestion des Clients";
            this.lblTitre.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            this.lblTitre.ForeColor = Color.FromArgb(50, 50, 60);
            this.lblTitre.Location = new Point(20, 20); // Respecte le Padding du panelContent
            this.lblTitre.AutoSize = true;

            // --- Recherche ---
            var lblRecherche = new Label
            {
                Text = "🔍 Rechercher:",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 100, 120),
                Location = new Point(20, 95),
                AutoSize = true
            };
            this.txtRecherche.Location = new Point(140, 92);
            this.txtRecherche.Size = new Size(400, 28);
            this.txtRecherche.Font = new Font("Segoe UI", 10F);
            this.txtRecherche.BorderStyle = BorderStyle.FixedSingle;
            this.txtRecherche.TextChanged += TxtRecherche_TextChanged;

            // --- Labels pour les ListViews ---
            var lblClients = new Label
            {
                Text = "📋 Liste des Clients",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 60),
                Location = new Point(20, 140), // Nouvelle position après la barre de recherche
                AutoSize = true
            };
            var lblFactures = new Label
            {
                Text = "📄 Factures du Client",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 60),
                Location = new Point(640, 140), // Position initiale qui sera ajustée
                AutoSize = true
            };

            // --- ListView Clients ---
            this.listViewClients.Location = new Point(20, 170);
            this.listViewClients.Size = new Size(600, 300); // Taille initiale
            this.listViewClients.View = View.Details;
            this.listViewClients.FullRowSelect = true;
            this.listViewClients.GridLines = true;
            this.listViewClients.BorderStyle = BorderStyle.FixedSingle; // Mieux que None
            this.listViewClients.BackColor = Color.White;
            this.listViewClients.Font = new Font("Segoe UI", 9F);
            this.listViewClients.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.listViewClients.MultiSelect = false;
            this.listViewClients.SelectedIndexChanged += ListViewClients_SelectedIndexChanged;
            this.listViewClients.DoubleClick += ListViewClients_DoubleClick;
            this.listViewClients.Columns.Add("ID", 60);
            this.listViewClients.Columns.Add("Nom", 150);
            this.listViewClients.Columns.Add("Prénom", 150);
            this.listViewClients.Columns.Add("Téléphone", 120);
            this.listViewClients.Columns.Add("Email", 200);

            // --- ListView Factures ---
            this.listViewFactures.Location = new Point(640, 170);
            this.listViewFactures.Size = new Size(580, 300); // Taille initiale
            this.listViewFactures.View = View.Details;
            this.listViewFactures.FullRowSelect = true;
            this.listViewFactures.GridLines = true;
            this.listViewFactures.BorderStyle = BorderStyle.FixedSingle; // Mieux que None
            this.listViewFactures.BackColor = Color.White;
            this.listViewFactures.Font = new Font("Segoe UI", 9F);
            this.listViewFactures.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.listViewFactures.Columns.Add("ID Facture", 80);
            this.listViewFactures.Columns.Add("Date", 120);
            this.listViewFactures.Columns.Add("Total", 100);
            this.listViewFactures.Columns.Add("Payé", 100);
            this.listViewFactures.Columns.Add("Restant", 100);
            this.listViewFactures.Columns.Add("Statut", 120);

            // --- Panel Formulaire Client ---
            var panelFormulaire = new Panel
            {
                Location = new Point(20, 500), // Nouvelle position Y ajustée
                Size = new Size(600, 350),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle, 
                Padding = new Padding(20)
            };

            var lblFormTitre = new Label
            {
                Text = "📝 Formulaire Client",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 60),
                Location = new Point(20, 20),
                AutoSize = true
            };

            this.txtHiddenId.Visible = false;

            int y = 60;
            int gap = 35;

            var lblUser = new Label { Text = "Nom d'utilisateur *", Location = new Point(20, y), Size = new Size(150, 23), Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            this.txtNomUtilisateur.Location = new Point(180, y);
            this.txtNomUtilisateur.Size = new Size(200, 25);
            y += gap;

            var lblPwd = new Label { Text = "Mot de passe *", Location = new Point(20, y), Size = new Size(150, 23), Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            this.txtMotDePasse.Location = new Point(180, y);
            this.txtMotDePasse.Size = new Size(200, 25);
            this.txtMotDePasse.UseSystemPasswordChar = true;
            y += gap;

            var lblN = new Label { Text = "Nom", Location = new Point(20, y), Size = new Size(150, 23), Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            this.txtNom.Location = new Point(180, y);
            this.txtNom.Size = new Size(200, 25);
            y += gap;

            var lblP = new Label { Text = "Prénom", Location = new Point(20, y), Size = new Size(150, 23), Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            this.txtPrenom.Location = new Point(180, y);
            this.txtPrenom.Size = new Size(200, 25);
            y += gap;

            var lblAdr = new Label { Text = "Adresse", Location = new Point(20, y), Size = new Size(150, 23), Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            this.txtAdresse.Location = new Point(180, y);
            this.txtAdresse.Size = new Size(380, 25);
            y += gap;

            var lblTel = new Label { Text = "Téléphone", Location = new Point(20, y), Size = new Size(150, 23), Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            this.txtTelephone.Location = new Point(180, y);
            this.txtTelephone.Size = new Size(200, 25);
            y += gap;

            var lblMail = new Label { Text = "Email", Location = new Point(20, y), Size = new Size(150, 23), Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            this.txtEmail.Location = new Point(180, y);
            this.txtEmail.Size = new Size(200, 25);
            y += 50;

            this.btnAjouter.Text = "➕ Ajouter";
            this.btnAjouter.Location = new Point(20, y);
            this.btnAjouter.Size = new Size(110, 35);
            this.btnAjouter.BackColor = Color.FromArgb(255, 140, 0);
            this.btnAjouter.ForeColor = Color.White;
            this.btnAjouter.FlatStyle = FlatStyle.Flat;
            this.btnAjouter.FlatAppearance.BorderSize = 0;
            this.btnAjouter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnAjouter.Click += BtnAjouter_Click;

            this.btnModifier.Text = "✏️ Modifier";
            this.btnModifier.Location = new Point(140, y);
            this.btnModifier.Size = new Size(110, 35);
            this.btnModifier.BackColor = Color.FromArgb(60, 160, 60);
            this.btnModifier.ForeColor = Color.White;
            this.btnModifier.FlatStyle = FlatStyle.Flat;
            this.btnModifier.FlatAppearance.BorderSize = 0;
            this.btnModifier.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnModifier.Click += BtnModifier_Click;

            this.btnSupprimer.Text = "❌ Supprimer";
            this.btnSupprimer.Location = new Point(260, y);
            this.btnSupprimer.Size = new Size(110, 35);
            this.btnSupprimer.BackColor = Color.FromArgb(200, 50, 50);
            this.btnSupprimer.ForeColor = Color.White;
            this.btnSupprimer.FlatStyle = FlatStyle.Flat;
            this.btnSupprimer.FlatAppearance.BorderSize = 0;
            this.btnSupprimer.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnSupprimer.Click += BtnSupprimer_Click;

            this.btnNouveau.Text = "🆕 Nouveau";
            this.btnNouveau.Location = new Point(380, y);
            this.btnNouveau.Size = new Size(110, 35);
            this.btnNouveau.BackColor = Color.Gray;
            this.btnNouveau.ForeColor = Color.White;
            this.btnNouveau.FlatStyle = FlatStyle.Flat;
            this.btnNouveau.FlatAppearance.BorderSize = 0;
            this.btnNouveau.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnNouveau.Click += BtnNouveau_Click;

            panelFormulaire.Controls.AddRange(new Control[] {
                lblFormTitre, txtHiddenId, lblUser, txtNomUtilisateur, lblPwd, txtMotDePasse,
                lblN, txtNom, lblP, txtPrenom, lblAdr, txtAdresse, lblTel, txtTelephone,
                lblMail, txtEmail, btnAjouter, btnModifier, btnSupprimer, btnNouveau
            });

            // --- Panel Paiement ---
            var panelPaiement = new Panel
            {
                Location = new Point(640, 500), // Nouvelle position Y ajustée. Sera ajustée par AjusterTailles
                Size = new Size(580, 350),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(20)
            };

            var lblPaiementTitre = new Label
            {
                Text = "💳 Gestion des Paiements",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 60),
                Location = new Point(20, 20),
                AutoSize = true
            };

            var lblMontant = new Label
            {
                Text = "Montant:",
                Location = new Point(20, 280),
                Size = new Size(100, 23),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            this.txtMontantPaiement.Location = new Point(130, 277);
            this.txtMontantPaiement.Size = new Size(150, 25);
            this.txtMontantPaiement.Font = new Font("Segoe UI", 9F);

            var lblMode = new Label
            {
                Text = "Mode:",
                Location = new Point(300, 280),
                Size = new Size(80, 23),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            this.comboBoxModePaiement.Location = new Point(390, 277);
            this.comboBoxModePaiement.Size = new Size(150, 25);
            this.comboBoxModePaiement.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxModePaiement.Items.AddRange(new string[] { "Espèces", "Carte bancaire", "Chèque", "Virement" });
            this.comboBoxModePaiement.SelectedIndex = 0;
            this.comboBoxModePaiement.Font = new Font("Segoe UI", 9F);

            this.btnEnregistrerPaiement.Text = "✅ Enregistrer Paiement";
            this.btnEnregistrerPaiement.Location = new Point(20, 310);
            this.btnEnregistrerPaiement.Size = new Size(200, 35);
            this.btnEnregistrerPaiement.BackColor = Color.FromArgb(255, 140, 0);
            this.btnEnregistrerPaiement.ForeColor = Color.White;
            this.btnEnregistrerPaiement.FlatStyle = FlatStyle.Flat;
            this.btnEnregistrerPaiement.FlatAppearance.BorderSize = 0;
            this.btnEnregistrerPaiement.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnEnregistrerPaiement.Click += BtnEnregistrerPaiement_Click;

            panelPaiement.Controls.AddRange(new Control[] {
                lblPaiementTitre, lblMontant, txtMontantPaiement, lblMode, comboBoxModePaiement, btnEnregistrerPaiement
            });

            // --- Ajout des contrôles au Panel Content ---
            this.panelContent.Controls.Add(this.lblTitre);
            this.panelContent.Controls.Add(lblRecherche);
            this.panelContent.Controls.Add(this.txtRecherche);
            this.panelContent.Controls.Add(lblClients);
            this.panelContent.Controls.Add(this.listViewClients);
            this.panelContent.Controls.Add(lblFactures);
            this.panelContent.Controls.Add(this.listViewFactures);
            this.panelContent.Controls.Add(panelFormulaire);
            this.panelContent.Controls.Add(panelPaiement);

            // --- Ajout des contrôles principaux au Formulaire ---
            this.Controls.Add(this.panelContent); // Content en premier pour qu'il prenne tout le reste
            this.Controls.Add(this.sidebar); // Sidebar en dernier pour qu'il soit ancré à gauche

            this.ResumeLayout(false);

            // Gestionnaires d'événements pour le redimensionnement
            this.Load += (s, e) => AjusterTailles();
            this.Resize += (s, e) => AjusterTailles(); // Important pour les ajustements dynamiques
        }

        private void AjusterTailles()
        {
            // La largeur du contenu est la largeur du panelContent moins le padding (40 au total)
            int contentWidth = this.panelContent.ClientSize.Width - 40;
            int topOffset = 200; // AUGMENTÉ de 170 à 200 pour plus d'espace sous les titres
            int gap = 20; // Espacement entre les panneaux/listes
            
            if (contentWidth > 0)
            {
                // Calcule la largeur de chaque moitié (listes et panneaux)
                int listPanelWidth = (contentWidth - gap) / 2;
                if (listPanelWidth < 300) listPanelWidth = contentWidth; // Bascule en mode simple colonne si trop étroit

                // 1. Ajustement des listes
                this.listViewClients.Location = new Point(20, topOffset);
                this.listViewClients.Size = new Size(listPanelWidth, 300);

                this.listViewFactures.Location = new Point(20 + listPanelWidth + gap, topOffset);
                this.listViewFactures.Size = new Size(listPanelWidth, 300);
                
                // Ajustement des labels de titre des listes
                var lblClients = this.panelContent.Controls.OfType<Label>().FirstOrDefault(l => l.Text.Contains("Clients"));
                if (lblClients != null) lblClients.Location = new Point(20, topOffset - 40); // Plus d'espace au-dessus (était -30)
                
                var lblFactures = this.panelContent.Controls.OfType<Label>().FirstOrDefault(l => l.Text.Contains("Factures"));
                if (lblFactures != null) lblFactures.Location = new Point(20 + listPanelWidth + gap, topOffset - 40);

                // 2. Ajustement des panneaux de formulaire et paiement
                int formY = topOffset + 300 + gap + 30; // 300 (hauteur liste) + gap + 30 (espace)

                var panelFormulaire = this.panelContent.Controls.OfType<Panel>().FirstOrDefault(p => p.Controls.Contains(txtNomUtilisateur));
                if (panelFormulaire != null)
                {
                    panelFormulaire.Location = new Point(20, formY);
                    panelFormulaire.Size = new Size(listPanelWidth, 350);
                }

                var panelPaiement = this.panelContent.Controls.OfType<Panel>().FirstOrDefault(p => p.Controls.Contains(txtMontantPaiement));
                if (panelPaiement != null)
                {
                    panelPaiement.Location = new Point(20 + listPanelWidth + gap, formY);
                    panelPaiement.Size = new Size(listPanelWidth, 350);
                }
                
                // 3. Ajustement de la recherche (pour centrage si besoin)
                int searchX = this.lblTitre.Location.X + this.lblTitre.Width + 20;
                this.txtRecherche.Location = new Point(20, 92);
                this.txtRecherche.Size = new Size(listPanelWidth + listPanelWidth + gap - 120, 28); // S'étend sur toute la largeur
                var lblRecherche = this.panelContent.Controls.OfType<Label>().FirstOrDefault(l => l.Text.Contains("Rechercher"));
                if (lblRecherche != null) lblRecherche.Location = new Point(20, 95);
            }
        }

        private void LoadClients(string filter = "")
        {
            this.listViewClients.Items.Clear();
            var clients = Client.GetAll(filter);

            foreach (var client in clients)
            {
                var item = new ListViewItem(client.Id.ToString());
                item.SubItems.Add(client.Nom ?? "");
                item.SubItems.Add(client.Prenom ?? "");
                item.SubItems.Add(client.Telephone ?? "");
                item.SubItems.Add(client.Email ?? "");
                item.Tag = client;
                this.listViewClients.Items.Add(item);
            }
        }

        private void ListViewClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewClients.SelectedItems.Count > 0)
            {
                var client = listViewClients.SelectedItems[0].Tag as Client;
                if (client != null)
                {
                    selectedClientId = client.Id;
                    LoadClientToForm(client);
                    ChargerFactures(client.Id);
                }
            }
        }

        private void ListViewClients_DoubleClick(object sender, EventArgs e)
        {
            if (listViewClients.SelectedItems.Count > 0)
            {
                var client = listViewClients.SelectedItems[0].Tag as Client;
                if (client != null) LoadClientToForm(client);
            }
        }

        private void TxtRecherche_TextChanged(object sender, EventArgs e)
        {
            LoadClients(txtRecherche.Text.Trim());
        }

        private void LoadClientToForm(Client c)
        {
            txtHiddenId.Text = c.Id.ToString();
            txtNomUtilisateur.Text = c.NomUtilisateur;
            txtMotDePasse.Text = c.MotDePasse;
            txtNom.Text = c.Nom;
            txtPrenom.Text = c.Prenom;
            txtAdresse.Text = c.Adresse;
            txtTelephone.Text = c.Telephone;
            txtEmail.Text = c.Email;
        }

        private void ClearForm()
        {
            selectedClientId = 0;
            txtHiddenId.Text = "";
            txtNomUtilisateur.Text = "";
            txtMotDePasse.Text = "";
            txtNom.Text = "";
            txtPrenom.Text = "";
            txtAdresse.Text = "";
            txtTelephone.Text = "";
            txtEmail.Text = "";
            listViewFactures.Items.Clear();
        }

        private void ChargerFactures(int clientId)
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string commandeUserColumn = Database.GetExistingColumn(conn, "Commande", "id_utilisateur", "id_client");
                    this.listViewFactures.Items.Clear();

                    string checkTable = @"SELECT COUNT(*) 
                                             FROM INFORMATION_SCHEMA.TABLES 
                                             WHERE TABLE_NAME = 'Paiement'";
                    SqlCommand cmdCheck = new SqlCommand(checkTable, conn);
                    int tableExists = Convert.ToInt32(cmdCheck.ExecuteScalar());

                    string query;
                    if (tableExists > 0)
                    {
                        query = $@"SELECT 
                                        f.id_facture,
                                        f.id_commande,
                                        f.date_facture,
                                        f.total,
                                        ISNULL(SUM(p.montant), 0) as MontantPaye,
                                        (f.total - ISNULL(SUM(p.montant), 0)) as MontantRestant,
                                        f.statut,
                                        c.date_commande as DateCommande
                                        FROM Facture f
                                        INNER JOIN Commande c ON f.id_commande = c.id_commande
                                        LEFT JOIN Paiement p ON f.id_facture = p.id_facture
                                        WHERE c.{commandeUserColumn} = @ClientId
                                        GROUP BY f.id_facture, f.id_commande, f.date_facture, f.total, f.statut, c.date_commande
                                        ORDER BY f.date_facture DESC";
                    }
                    else
                    {
                        query = $@"SELECT 
                                        f.id_facture,
                                        f.id_commande,
                                        f.date_facture,
                                        f.total,
                                        0 as MontantPaye,
                                        f.total as MontantRestant,
                                        f.statut,
                                        c.date_commande as DateCommande
                                        FROM Facture f
                                        INNER JOIN Commande c ON f.id_commande = c.id_commande
                                        WHERE c.{commandeUserColumn} = @ClientId
                                        ORDER BY f.date_facture DESC";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ClientId", clientId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new ListViewItem(reader["id_facture"].ToString());
                                item.SubItems.Add(Convert.ToDateTime(reader["date_facture"]).ToString("dd/MM/yyyy"));
                                item.SubItems.Add(Convert.ToDecimal(reader["total"]).ToString("C2"));
                                item.SubItems.Add(Convert.ToDecimal(reader["MontantPaye"]).ToString("C2"));
                                item.SubItems.Add(Convert.ToDecimal(reader["MontantRestant"]).ToString("C2"));
                                item.SubItems.Add(reader["statut"].ToString());
                                item.Tag = Convert.ToInt32(reader["id_facture"]);
                                this.listViewFactures.Items.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur chargement factures: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAjouter_Click(object sender, EventArgs e)
        {
            var c = new Client
            {
                NomUtilisateur = txtNomUtilisateur.Text.Trim(),
                MotDePasse = txtMotDePasse.Text.Trim(),
                Nom = txtNom.Text.Trim(),
                Prenom = txtPrenom.Text.Trim(),
                Adresse = txtAdresse.Text.Trim(),
                Telephone = txtTelephone.Text.Trim(),
                Email = txtEmail.Text.Trim()
            };

            if (c.Ajouter())
            {
                MessageBox.Show("Client ajouté !");
                LoadClients();
                ClearForm();
            }
            else MessageBox.Show("Erreur lors de l'ajout !");
        }

        private void BtnModifier_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtHiddenId.Text, out int id)) { MessageBox.Show("Sélectionnez un client !"); return; }
            var c = new Client
            {
                Id = id,
                NomUtilisateur = txtNomUtilisateur.Text.Trim(),
                MotDePasse = txtMotDePasse.Text.Trim(),
                Nom = txtNom.Text.Trim(),
                Prenom = txtPrenom.Text.Trim(),
                Adresse = txtAdresse.Text.Trim(),
                Telephone = txtTelephone.Text.Trim(),
                Email = txtEmail.Text.Trim()
            };

            if (c.Modifier())
            {
                MessageBox.Show("Client modifié !");
                LoadClients();
                ClearForm();
            }
            else MessageBox.Show("Erreur lors de la modification !");
        }

        private void BtnSupprimer_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtHiddenId.Text, out int id)) { MessageBox.Show("Sélectionnez un client !"); return; }
            if (MessageBox.Show("Voulez-vous supprimer ce client ?", "Confirmer", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (Client.Supprimer(id))
                {
                    MessageBox.Show("Client supprimé !");
                    LoadClients();
                    ClearForm();
                }
                else MessageBox.Show("Erreur lors de la suppression !");
            }
        }

        private void BtnNouveau_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void BtnEnregistrerPaiement_Click(object sender, EventArgs e)
        {
            if (listViewFactures.SelectedItems.Count == 0)
            {
                MessageBox.Show("Sélectionnez une facture !", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                int idFacture = (int)listViewFactures.SelectedItems[0].Tag;
                decimal montant = decimal.TryParse(txtMontantPaiement.Text, out var m) ? m : 0;
                string modePaiement = comboBoxModePaiement.SelectedItem?.ToString() ?? "Espèces";

                if (montant <= 0)
                {
                    MessageBox.Show("Montant invalide !", "Erreur",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();

                    string checkTable = @"SELECT COUNT(*) 
                                             FROM INFORMATION_SCHEMA.TABLES 
                                             WHERE TABLE_NAME = 'Paiement'";
                    SqlCommand cmdCheck = new SqlCommand(checkTable, conn);
                    int tableExists = Convert.ToInt32(cmdCheck.ExecuteScalar());

                    if (tableExists > 0)
                    {
                        string queryPaiement = @"INSERT INTO Paiement (id_facture, montant, date_paiement, mode_paiement)
                                                   VALUES (@id_facture, @montant, GETDATE(), @mode_paiement)";
                        SqlCommand cmdPaiement = new SqlCommand(queryPaiement, conn);
                        cmdPaiement.Parameters.AddWithValue("@id_facture", idFacture);
                        cmdPaiement.Parameters.AddWithValue("@montant", montant);
                        cmdPaiement.Parameters.AddWithValue("@mode_paiement", modePaiement);
                        cmdPaiement.ExecuteNonQuery();

                        string queryTotalPaye = @"SELECT ISNULL(SUM(montant), 0) 
                                                     FROM Paiement 
                                                     WHERE id_facture = @id_facture";
                        SqlCommand cmdTotal = new SqlCommand(queryTotalPaye, conn);
                        cmdTotal.Parameters.AddWithValue("@id_facture", idFacture);
                        decimal totalPaye = Convert.ToDecimal(cmdTotal.ExecuteScalar());

                        string queryTotalFacture = @"SELECT total FROM Facture WHERE id_facture = @id_facture";
                        SqlCommand cmdTotalFacture = new SqlCommand(queryTotalFacture, conn);
                        cmdTotalFacture.Parameters.AddWithValue("@id_facture", idFacture);
                        decimal totalFacture = Convert.ToDecimal(cmdTotalFacture.ExecuteScalar());
                        decimal montantRestant = totalFacture - totalPaye;

                        string nouveauStatut = totalPaye >= totalFacture ? "Payée" : "Partiellement payée";
                        string queryUpdate = @"UPDATE Facture SET statut = @statut WHERE id_facture = @id_facture";
                        SqlCommand cmdUpdate = new SqlCommand(queryUpdate, conn);
                        cmdUpdate.Parameters.AddWithValue("@statut", nouveauStatut);
                        cmdUpdate.Parameters.AddWithValue("@id_facture", idFacture);
                        cmdUpdate.ExecuteNonQuery();

                        string message = $"Paiement de {montant:C2} enregistré !\n\n" +
                                         $"Total payé: {totalPaye:C2} / {totalFacture:C2}\n" +
                                         $"Montant restant: {montantRestant:C2}";
                        MessageBox.Show(message, "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        txtMontantPaiement.Clear();
                        if (selectedClientId > 0)
                            ChargerFactures(selectedClientId);
                    }
                    else
                    {
                        MessageBox.Show("La table Paiement n'existe pas dans la base de données.\n" +
                            "Veuillez créer la table avec les colonnes: id_paiement, id_facture, montant, date_paiement, mode_paiement",
                            "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur enregistrement paiement: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Événements Sidebar
        private void Sidebar_MedicamentsClicked(object sender, EventArgs e) { OuvrirForm(new GestionMedicamentsForm()); }
        private void Sidebar_CommandesClicked(object sender, EventArgs e) { OuvrirForm(new GestionCommandesForm()); }
        private void Sidebar_ClientsClicked(object sender, EventArgs e) { }
        private void Sidebar_DashboardClicked(object sender, EventArgs e) { OuvrirForm(new DashboardPharmacien()); }
        private void Sidebar_RefreshClicked(object sender, EventArgs e) { LoadClients(); }
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