using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GestionDesMedicaments.Classes;

namespace GestionDesMedicaments
{
    public partial class DashboardPharmacien : Form
    {
        private string commandeUserColumn = "id_utilisateur";

        public DashboardPharmacien()
        {
            InitializeComponent();
            InitialiserColonnes();
            ChargerDonnees();
            AppliquerThemeOrange();
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

        private void ChargerDonnees()
        {
            ChargerStatistiques();
            ChargerMedicamentsStockBas();
            ChargerCommandesRecentess();
            ChargerMedicamentsPopulaires();
            VerifierAlertesStock();
            ChargerMedicamentsAlertePeremption();
        }

        private void ChargerStatistiques()
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();

                    // Chiffre d'affaires du jour - Correction avec jointure correcte
                    string queryCA = @"SELECT ISNULL(SUM(f.total), 0) 
                                     FROM Commande c
                                     INNER JOIN Facture f ON c.id_commande = f.id_commande
                                     WHERE CAST(c.date_commande AS DATE) = CAST(GETDATE() AS DATE) 
                                     AND c.statut != 'Annulée'
                                     AND f.statut != 'Annulée'";
                    SqlCommand cmdCA = new SqlCommand(queryCA, conn);
                    object resultCA = cmdCA.ExecuteScalar();
                    decimal caJournalier = resultCA != null && resultCA != DBNull.Value ? Convert.ToDecimal(resultCA) : 0;
                    lblCAJournalier.Text = $"{caJournalier:C2}";

                    // Commandes du jour - Correction
                    string queryCommandes = @"SELECT COUNT(*) 
                                            FROM Commande 
                                            WHERE CAST(date_commande AS DATE) = CAST(GETDATE() AS DATE)
                                            AND statut != 'Annulée'";
                    SqlCommand cmdCommandes = new SqlCommand(queryCommandes, conn);
                    object resultCmd = cmdCommandes.ExecuteScalar();
                    int commandesJour = resultCmd != null && resultCmd != DBNull.Value ? Convert.ToInt32(resultCmd) : 0;
                    lblCommandesJour.Text = commandesJour.ToString();

                    // Médicaments stock bas
                    string queryStockBas = @"SELECT COUNT(*) 
                                           FROM Medicament 
                                           WHERE stock <= seuil_alerte";
                    SqlCommand cmdStockBas = new SqlCommand(queryStockBas, conn);
                    object resultStock = cmdStockBas.ExecuteScalar();
                    int stockBas = resultStock != null && resultStock != DBNull.Value ? Convert.ToInt32(resultStock) : 0;
                    lblAlertesStock.Text = stockBas.ToString();

                    // Clients du mois
                    string queryClients = $@"SELECT COUNT(DISTINCT {commandeUserColumn}) 
                                          FROM Commande 
                                          WHERE MONTH(date_commande) = MONTH(GETDATE()) 
                                          AND YEAR(date_commande) = YEAR(GETDATE())";
                    SqlCommand cmdClients = new SqlCommand(queryClients, conn);
                    object resultClients = cmdClients.ExecuteScalar();
                    int clientsMois = resultClients != null && resultClients != DBNull.Value ? Convert.ToInt32(resultClients) : 0;
                    lblClientsMois.Text = clientsMois.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur chargement statistiques: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VerifierAlertesStock()
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT COUNT(*) 
                                   FROM Medicament 
                                   WHERE stock = seuil_alerte";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    object result = cmd.ExecuteScalar();
                    int count = result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;

                    if (count > 0)
                    {
                        System.Media.SystemSounds.Exclamation.Play();
                        MessageBox.Show($"⚠️ ALERTE : {count} médicament(s) ont atteint le seuil d'alerte !",
                            "Alerte Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                // Erreur silencieuse pour ne pas perturber l'utilisateur
            }
        }

        private void ChargerMedicamentsAlertePeremption()
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    // Vérifier si la colonne date_peremption existe
                    string checkColumn = @"SELECT COUNT(*) 
                                          FROM INFORMATION_SCHEMA.COLUMNS 
                                          WHERE TABLE_NAME = 'Medicament' 
                                          AND COLUMN_NAME = 'date_peremption'";
                    SqlCommand cmdCheck = new SqlCommand(checkColumn, conn);
                    int columnExists = Convert.ToInt32(cmdCheck.ExecuteScalar());

                    if (columnExists > 0)
                    {
                        string query = @"SELECT TOP 10 
                                       id_medicament, nom, stock, date_peremption,
                                       DATEDIFF(DAY, GETDATE(), date_peremption) as JoursRestants
                                       FROM Medicament 
                                       WHERE date_peremption IS NOT NULL
                                       AND date_peremption <= DATEADD(DAY, 30, GETDATE())
                                       ORDER BY date_peremption ASC";
                        SqlDataAdapter da = new SqlDataAdapter(query, conn);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridViewAlertePeremption.DataSource = dt;
                    }
                    else
                    {
                        // Si la colonne n'existe pas, créer un DataTable vide
                        DataTable dt = new DataTable();
                        dt.Columns.Add("nom", typeof(string));
                        dt.Columns.Add("JoursRestants", typeof(int));
                        dt.Rows.Add("Colonne date_peremption non disponible", 0);
                        dataGridViewAlertePeremption.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                // Erreur silencieuse
            }
        }

        private void ChargerMedicamentsStockBas()
        {
            try
            {
                var medicaments = Medicament.GetAll()
                    .Where(m => m.Stock <= m.SeuilAlerte)
                    .Select(m => new
                    {
                        m.Id,
                        m.Nom,
                        m.Stock,
                        m.SeuilAlerte,
                        Statut = m.Stock == 0 ? "Rupture" : "Stock bas"
                    })
                    .ToList();

                dataGridViewStockBas.DataSource = medicaments;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur chargement stocks bas: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChargerCommandesRecentess()
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string query = $@"SELECT TOP 10 c.id_commande, u.nom + ' ' + u.prenom as Client, 
                                   c.date_commande, c.statut, f.total
                                   FROM Commande c
                                   INNER JOIN Utilisateur u ON c.{commandeUserColumn} = u.id_utilisateur
                                   LEFT JOIN Facture f ON c.id_commande = f.id_commande
                                   ORDER BY c.date_commande DESC";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridViewCommandes.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur chargement commandes: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChargerMedicamentsPopulaires()
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT TOP 5 m.nom, SUM(lc.quantite) as QuantiteVendue
                                   FROM LigneCommande lc
                                   INNER JOIN Medicament m ON lc.id_medicament = m.id_medicament
                                   INNER JOIN Commande c ON lc.id_commande = c.id_commande
                                   WHERE c.date_commande >= DATEADD(DAY, -30, GETDATE())
                                   GROUP BY m.nom
                                   ORDER BY QuantiteVendue DESC";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridViewPopulaires.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur chargement médicaments populaires: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AppliquerThemeOrange()
        {
            Color orange = Color.FromArgb(255, 140, 0);

            // Appliquer le thème aux boutons
            var buttons = this.Controls.OfType<Button>();
            foreach (var btn in buttons)
            {
                if (btn.Name.StartsWith("btn") && btn.Name != "btnDeconnexion")
                {
                    btn.BackColor = orange;
                    btn.ForeColor = Color.White;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                }
            }

            // Appliquer le style aux DataGridViews
            if (dataGridViewStockBas != null)
            {
                dataGridViewStockBas.EnableHeadersVisualStyles = false;
                dataGridViewStockBas.ColumnHeadersDefaultCellStyle.BackColor = orange;
                dataGridViewStockBas.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dataGridViewStockBas.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            }

            if (dataGridViewCommandes != null)
            {
                dataGridViewCommandes.EnableHeadersVisualStyles = false;
                dataGridViewCommandes.ColumnHeadersDefaultCellStyle.BackColor = orange;
                dataGridViewCommandes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dataGridViewCommandes.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            }

            if (dataGridViewPopulaires != null)
            {
                dataGridViewPopulaires.EnableHeadersVisualStyles = false;
                dataGridViewPopulaires.ColumnHeadersDefaultCellStyle.BackColor = orange;
                dataGridViewPopulaires.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dataGridViewPopulaires.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            }
        }

        // Méthodes des boutons
        private void btnGestionMedicaments_Click(object sender, EventArgs e)
        {
            GestionMedicamentsForm form = new GestionMedicamentsForm();
            form.Show();
            this.Hide(); // ou form.ShowDialog() pour modal
        }


        private void btnGestionCommandes_Click(object sender, EventArgs e)
        {
            GestionCommandesForm form = new GestionCommandesForm();
            form.Show();
            this.Hide();
        }

        private void btnGestionClients_Click(object sender, EventArgs e)
        {
            var gestionClients = new GestionClientsForm();
            gestionClients.Owner = this;   // rend le dashboard propriétaire pour un retour facile
            gestionClients.Show();         // affiche la fenêtre de gestion clients
            this.Hide();                   // cache le dashboard (facultatif, mais habituel)
        }


        private void btnRafraichir_Click(object sender, EventArgs e)
        {
            ChargerDonnees();
            MessageBox.Show("Données rafraîchies", "Succès",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridViewStockBas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int idMedicament = Convert.ToInt32(dataGridViewStockBas.Rows[e.RowIndex].Cells["Id"].Value);
                MessageBox.Show($"Ouvrir fiche médicament ID: {idMedicament}", "Détails",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridViewCommandes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int idCommande = Convert.ToInt32(dataGridViewCommandes.Rows[e.RowIndex].Cells["id_commande"].Value);
                MessageBox.Show($"Ouvrir détails commande ID: {idCommande}", "Détails",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DashboardPharmacien_Load(object sender, EventArgs e)
        {

        }

        private void lblTitre_Click(object sender, EventArgs e)
        {

        }
        private void btnDeconnexion_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Close(); // ferme le dashboard
        }

    }
}