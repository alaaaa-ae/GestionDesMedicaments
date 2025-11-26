using GestionDesMedicaments.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace GestionDesMedicaments
{
    public partial class GestionCommandesForm : Form
    {
        public GestionCommandesForm()
        {
            InitializeComponent();
            ChargerCommandes();
            ConfigurerDataGridViews();
        }

        private void ConfigurerDataGridViews()
        {
            // Commandes
            DataGridViewCommandes.AutoGenerateColumns = false;
            DataGridViewCommandes.Columns.Clear();

            DataGridViewCommandes.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { Name = "id_commande", DataPropertyName = "id_commande", HeaderText = "N° Commande", Width = 80, ReadOnly = true },
                new DataGridViewTextBoxColumn { Name = "Utilisateur", DataPropertyName = "Utilisateur", HeaderText = "Utilisateur", Width = 150, ReadOnly = true },
                new DataGridViewTextBoxColumn { Name = "date_commande", DataPropertyName = "date_commande", HeaderText = "Date", Width = 120, ReadOnly = true },
                new DataGridViewTextBoxColumn { Name = "total", DataPropertyName = "total", HeaderText = "Total", Width = 80, ReadOnly = true },
                new DataGridViewComboBoxColumn { Name = "statut", DataPropertyName = "statut", HeaderText = "Statut", Width = 120, DataSource = new string[] { "En attente", "Confirmée", "Préparée", "Livrée", "Annulée" } }
            });

            // Détails
            DataGridViewDetails.AutoGenerateColumns = false;
            DataGridViewDetails.Columns.Clear();

            DataGridViewDetails.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { Name = "NomMedicament", DataPropertyName = "NomMedicament", HeaderText = "Médicament", Width = 150, ReadOnly = true },
                new DataGridViewTextBoxColumn { Name = "quantite", DataPropertyName = "quantite", HeaderText = "Quantité", Width = 80, ReadOnly = true },
                new DataGridViewTextBoxColumn { Name = "prix_unitaire", DataPropertyName = "prix_unitaire", HeaderText = "Prix Unitaire", Width = 100, ReadOnly = true },
                new DataGridViewTextBoxColumn { Name = "SousTotal", DataPropertyName = "SousTotal", HeaderText = "Sous-Total", Width = 100, ReadOnly = true }
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

        private void ChargerDetailsCommande(int idCommande)
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();

                    string query = @"
                        SELECT 
                            m.nom as NomMedicament,
                            lc.quantite,
                            lc.prix_unitaire,
                            (lc.quantite * lc.prix_unitaire) as SousTotal
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

        private void DataGridViewCommandes_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != DataGridViewCommandes.Columns["statut"].Index) return;

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
    }
}
