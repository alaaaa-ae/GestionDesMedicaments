using GestionDesMedicaments.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using GestionDesMedicaments.Clients;


namespace GestionDesMedicaments
{
    public partial class GestionCommandesForm : Form
    {
        private PrintDocument printDoc = new PrintDocument();
        private string printText = "";

        public GestionCommandesForm()
        {
            InitializeComponent();
            ConfigurerDataGridViews();
            ChargerCommandes();
            printDoc.PrintPage += PrintDoc_PrintPage;
        }

        private void GestionCommandesForm_Load(object sender, EventArgs e)
        {
            dtpTo.Value = DateTime.Today;
            dtpFrom.Value = DateTime.Today.AddDays(-7);
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

                    string query = @"
                        SELECT 
                            c.id_commande,
                            u.nom + ' ' + u.prenom as Utilisateur,
                            c.date_commande,
                            ISNULL(f.total, 0) as total,
                            c.statut
                        FROM Commande c
                        INNER JOIN Utilisateur u ON c.id_utilisateur = u.id_utilisateur
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
                    string query = @"
                        SELECT 
                            c.id_commande,
                            u.nom + ' ' + u.prenom as Utilisateur,
                            c.date_commande,
                            ISNULL(f.total, 0) as total,
                            c.statut
                        FROM Commande c
                        INNER JOIN Utilisateur u ON c.id_utilisateur = u.id_utilisateur
                        LEFT JOIN Facture f ON c.id_commande = f.id_commande
                        WHERE c.date_commande BETWEEN @From AND @To
                    ";

                    if (!string.IsNullOrWhiteSpace(txtClient.Text))
                    {
                        query += " AND (u.nom + ' ' + u.prenom) LIKE @Client";
                    }

                    query += " ORDER BY c.date_commande DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@From", dtpFrom.Value.Date);
                    cmd.Parameters.AddWithValue("@To", dtpTo.Value.Date.AddDays(1).AddSeconds(-1));

                    if (!string.IsNullOrWhiteSpace(txtClient.Text))
                        cmd.Parameters.AddWithValue("@Client", $"%{txtClient.Text.Trim()}%");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    DataGridViewCommandes.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la recherche: {ex.Message}");
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
                MessageBox.Show("Sélectionnez une commande à imprimer.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            object val = DataGridViewCommandes.SelectedRows[0].Cells["id_commande"].Value;
            if (val == DBNull.Value) return;
            int idCommande = Convert.ToInt32(val);

            try
            {
                StringBuilder sb = new StringBuilder();

                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmdHeader = new SqlCommand(@"
                        SELECT c.id_commande, u.nom + ' ' + u.prenom as Utilisateur, c.date_commande, c.statut, ISNULL(f.total,0) as total, c.id_utilisateur
                        FROM Commande c
                        INNER JOIN Utilisateur u ON c.id_utilisateur = u.id_utilisateur
                        LEFT JOIN Facture f ON c.id_commande = f.id_commande
                        WHERE c.id_commande = @Id", conn);
                    cmdHeader.Parameters.AddWithValue("@Id", idCommande);
                    using (SqlDataReader r = cmdHeader.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            sb.AppendLine($"Commande N°: {r["id_commande"]}");
                            sb.AppendLine($"Client: {r["Utilisateur"]}");
                            sb.AppendLine($"Date: {Convert.ToDateTime(r["date_commande"]).ToString("g")}");
                            sb.AppendLine($"Statut: {r["statut"]}");
                            sb.AppendLine($"Total: {r["total"]}");
                            sb.AppendLine(new string('-', 50));
                        }
                    }

                    SqlCommand cmdLines = new SqlCommand(@"
                        SELECT m.nom as Medicament, lc.quantite, COALESCE(lc.prix_unitaire, m.prix_vente) as prix_unitaire, (lc.quantite*COALESCE(lc.prix_unitaire,m.prix_vente)) as SousTotal
                        FROM LigneCommande lc
                        INNER JOIN Medicament m ON lc.id_medicament = m.id_medicament
                        WHERE lc.id_commande = @Id", conn);
                    cmdLines.Parameters.AddWithValue("@Id", idCommande);
                    using (SqlDataReader r2 = cmdLines.ExecuteReader())
                    {
                        sb.AppendLine($"{"Produit",-40} {"QTE",4} {"PU",10} {"ST",12}");
                        sb.AppendLine(new string('-', 80));
                        while (r2.Read())
                        {
                            string prod = r2["Medicament"].ToString();
                            int q = Convert.ToInt32(r2["quantite"]);
                            decimal pu = Convert.ToDecimal(r2["prix_unitaire"]);
                            decimal st = Convert.ToDecimal(r2["SousTotal"]);
                            sb.AppendLine($"{prod,-40} {q,4} {pu,10:C} {st,12:C}");
                        }
                    }
                }

                printText = sb.ToString();
                PrintPreviewDialog preview = new PrintPreviewDialog();
                preview.Document = printDoc;
                preview.Width = 800;
                preview.Height = 600;
                preview.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'impression: {ex.Message}");
            }
        }

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(printText, new System.Drawing.Font("Courier New", 10), System.Drawing.Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top);
        }

        private void btnStats_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        SELECT COUNT(*) as Nb, ISNULL(SUM(f.total),0) as TotalVentes
                        FROM Commande c
                        LEFT JOIN Facture f ON c.id_commande = f.id_commande
                        WHERE c.date_commande BETWEEN @From AND @To", conn);
                    cmd.Parameters.AddWithValue("@From", dtpFrom.Value.Date);
                    cmd.Parameters.AddWithValue("@To", dtpTo.Value.Date.AddDays(1).AddSeconds(-1));
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            int nb = Convert.ToInt32(r["Nb"]);
                            decimal total = Convert.ToDecimal(r["TotalVentes"]);
                            MessageBox.Show($"Période: {dtpFrom.Value.Date:d} → {dtpTo.Value.Date:d}\nNombre de commandes: {nb}\nTotal ventes: {total:C}",
                                "Statistiques", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur statistiques: {ex.Message}");
            }
        }
    }
}
