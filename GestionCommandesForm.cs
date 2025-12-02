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
    public partial class GestionCommandesForm : Form
    {
        private PrintDocument printDoc = new PrintDocument();
        private string printText = "";
        private string commandeUserColumn = "id_utilisateur";

        public GestionCommandesForm()
        {
            InitializeComponent();
            ConfigurerDataGridViews();
            InitialiserColonnes();
            ChargerCommandes();
            printDoc.PrintPage += PrintDoc_PrintPage;
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

        private void GestionCommandesForm_Load(object sender, EventArgs e)
        {
            dtpDate.Value = DateTime.Today;
        }

        private void ConfigurerDataGridViews()
        {
            // Commandes
            DataGridViewCommandes.AutoGenerateColumns = false;
            DataGridViewCommandes.Columns.Clear();

            DataGridViewCommandes.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { Name = "id_commande", DataPropertyName = "id_commande", HeaderText = "N° Commande", Width = 80, ReadOnly = true },
                new DataGridViewTextBoxColumn { Name = "Utilisateur", DataPropertyName = "Utilisateur", HeaderText = "Client / Utilisateur", Width = 200, ReadOnly = true },
                new DataGridViewTextBoxColumn { Name = "date_commande", DataPropertyName = "date_commande", HeaderText = "Date", Width = 140, ReadOnly = true },
                new DataGridViewTextBoxColumn { Name = "total", DataPropertyName = "total", HeaderText = "Total", Width = 100, ReadOnly = true },
                new DataGridViewComboBoxColumn { Name = "statut", DataPropertyName = "statut", HeaderText = "Statut", Width = 150, DataSource = new string[] { "En attente", "Confirmée", "Préparée", "Livrée", "Annulée" } }
            });

            // Détails
            DataGridViewDetails.AutoGenerateColumns = false;
            DataGridViewDetails.Columns.Clear();

            DataGridViewDetails.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { Name = "NomMedicament", DataPropertyName = "NomMedicament", HeaderText = "Médicament", Width = 320, ReadOnly = true },
                new DataGridViewTextBoxColumn { Name = "quantite", DataPropertyName = "quantite", HeaderText = "Quantité", Width = 80, ReadOnly = true },
                new DataGridViewTextBoxColumn { Name = "prix_unitaire", DataPropertyName = "prix_unitaire", HeaderText = "Prix Unitaire", Width = 120, ReadOnly = true },
                new DataGridViewTextBoxColumn { Name = "SousTotal", DataPropertyName = "SousTotal", HeaderText = "Sous-Total", Width = 120, ReadOnly = true }
            });
        }

        private void ChargerCommandes()
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();

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

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    DataGridViewCommandes.DataSource = dt;
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

                    // Si LigneCommande.prix_unitaire existe on l'utilise, sinon on prend Medicament.prix_vente
                    string query = @"
                        SELECT 
                            m.nom as NomMedicament,
                            lc.quantite,
                            COALESCE(lc.prix_unitaire, m.prix_vente) as prix_unitaire,
                            (lc.quantite * COALESCE(lc.prix_unitaire, m.prix_vente)) as SousTotal
                        FROM LigneCommande lc
                        INNER JOIN Medicament m ON lc.id_medicament = m.id_medicament
                        WHERE lc.id_commande = @IdCommande";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@IdCommande", idCommande);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    DataGridViewDetails.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des détails: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridViewCommandes_SelectionChanged(object sender, EventArgs e)
        {
            if (DataGridViewCommandes.SelectedRows.Count == 0) return;

            object value = DataGridViewCommandes.SelectedRows[0].Cells["id_commande"].Value;
            if (value != DBNull.Value)
            {
                int idCommande = Convert.ToInt32(value);
                ChargerDetailsCommande(idCommande);
            }
        }

        private void DataGridViewCommandes_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (DataGridViewCommandes.Columns["statut"] == null) return;

            if (e.ColumnIndex != DataGridViewCommandes.Columns["statut"].Index) return;

            object value = DataGridViewCommandes.Rows[e.RowIndex].Cells["id_commande"].Value;
            if (value != DBNull.Value)
            {
                int idCommande = Convert.ToInt32(value);
                string nouveauStatut = DataGridViewCommandes.Rows[e.RowIndex].Cells["statut"].Value?.ToString();
                if (!string.IsNullOrEmpty(nouveauStatut))
                    MettreAJourStatutCommande(idCommande, nouveauStatut);
            }
        }

        private void MettreAJourStatutCommande(int idCommande, string nouveauStatut)
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE Commande SET statut=@Statut WHERE id_commande=@IdCommande";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Statut", nouveauStatut);
                    cmd.Parameters.AddWithValue("@IdCommande", idCommande);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la mise à jour du statut: {ex.Message}");
            }
        }

        private void btnRechercher_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
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
                        WHERE 1=1
                    ";

                    // Filtre par date (optionnel)
                    query += " AND CAST(c.date_commande AS DATE) = @Date";

                    // Filtre par client (recherche dans nom et prenom séparément)
                    if (!string.IsNullOrWhiteSpace(txtClient.Text))
                    {
                        query += " AND (u.nom LIKE @Client OR u.prenom LIKE @Client OR (u.nom + ' ' + u.prenom) LIKE @Client OR u.nom_utilisateur LIKE @Client)";
                    }

                    query += " ORDER BY c.date_commande DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Date", dtpDate.Value.Date);

                    if (!string.IsNullOrWhiteSpace(txtClient.Text))
                    {
                        string searchTerm = $"%{txtClient.Text.Trim()}%";
                        cmd.Parameters.AddWithValue("@Client", searchTerm);
                    }

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    DataGridViewCommandes.DataSource = dt;

                    if (dt.Rows.Count == 0)
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

        private void btnReinitialiser_Click(object sender, EventArgs e)
        {
            dtpDate.Value = DateTime.Today;
            txtClient.Clear();
            ChargerCommandes();
        }

        private void btnRetour_Click(object sender, EventArgs e)
        {
            try
            {
                // Cherche une instance existante du DashboardPharmacien
                var dashboard = Application.OpenForms.OfType<Form>().FirstOrDefault(f => f.GetType().Name == "DashboardPharmacien");

                if (dashboard != null)
                {
                    dashboard.Show();
                    dashboard.BringToFront();
                }
                else
                {
                    // Si aucune instance n'existe, créer une nouvelle instance
                    dashboard = new DashboardPharmacien();
                    dashboard.Show();
                }

                // Ferme la fenêtre actuelle
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible d'ouvrir le Dashboard :\n" + ex.Message, "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNouveau_Click(object sender, EventArgs e)
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

        private void btnModifier_Click(object sender, EventArgs e)
        {
            if (DataGridViewCommandes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Sélectionnez une commande.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            object val = DataGridViewCommandes.SelectedRows[0].Cells["id_commande"].Value;
            if (val == DBNull.Value) return;

            int idCommande = Convert.ToInt32(val);
            using (CommandeEditForm f = new CommandeEditForm())
            {
                f.Mode = CommandeEditForm.EditMode.Edit;
                f.IdCommande = idCommande;
                if (f.ShowDialog() == DialogResult.OK)
                {
                    ChargerCommandes();
                }
            }
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if (DataGridViewCommandes.SelectedRows.Count == 0) return;
            var result = MessageBox.Show("Voulez-vous supprimer la commande sélectionnée ?", "Confirmer", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes) return;

            object val = DataGridViewCommandes.SelectedRows[0].Cells["id_commande"].Value;
            if (val == DBNull.Value) return;
            int idCommande = Convert.ToInt32(val);

            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM LigneCommande WHERE id_commande=@Id; DELETE FROM Facture WHERE id_commande=@Id; DELETE FROM Commande WHERE id_commande=@Id;", conn);
                    cmd.Parameters.AddWithValue("@Id", idCommande);
                    cmd.ExecuteNonQuery();
                    ChargerCommandes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la suppression: {ex.Message}");
            }
        }

        private void btnImprimer_Click(object sender, EventArgs e)
        {
            if (DataGridViewCommandes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Sélectionnez une commande à imprimer.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            object val = DataGridViewCommandes.SelectedRows[0].Cells["id_commande"].Value;
            if (val == DBNull.Value) return;
            int idCommande = Convert.ToInt32(val);

            try
            {
                StringBuilder sb = new StringBuilder();
                decimal totalCommande = 0;

                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();

                    // Récupération des informations de la commande
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
                    cmdHeader.Parameters.AddWithValue("@Id", idCommande);

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

                    // Récupération des lignes de commande
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
                    cmdLines.Parameters.AddWithValue("@Id", idCommande);

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

                // Afficher le dialogue d'aperçu avant impression
                PrintPreviewDialog preview = new PrintPreviewDialog();
                preview.Document = printDoc;
                preview.Width = 900;
                preview.Height = 700;
                preview.Text = "Aperçu avant impression - Commande";

                // Option d'impression directe
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

        private void btnStats_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();

                    // Statistiques globales
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
    }
}