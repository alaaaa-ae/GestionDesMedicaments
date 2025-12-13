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
    public class DashboardPharmacien : Form
    {
        private string commandeUserColumn = "id_utilisateur";

        public DashboardPharmacien()
        {
            InitializeComponent();
            InitialiserColonnes();
            this.WindowState = FormWindowState.Maximized;

            // Événement Load
            this.Load += DashboardPharmacien_Load;
            this.Resize += DashboardPharmacien_Resize;
        }

        private void DashboardPharmacien_Load(object sender, EventArgs e)
        {
            ChargerDonnees();
            AjusterControles();
        }

        private void DashboardPharmacien_Resize(object sender, EventArgs e)
        {
            AjusterControles();
        }

        private void AjusterControles()
        {
            try
            {
                // Ajuster les DataGridViews en fonction de la taille
                int panelWidth = panelContent.Width - 30;
                int panelHeightSection1 = panelSection1.Height;
                int panelHeightSection2 = panelSection2.Height;

                // Section 1: Stock Bas et Commandes
                if (dataGridViewStockBas != null && dataGridViewCommandes != null)
                {
                    int gridWidth = panelWidth / 2 - 10;
                    int gridHeight = panelHeightSection1 - 40;

                    // Positionner les labels
                    lblStockBas.Location = new Point(0, 0);
                    lblCommandesRecentes.Location = new Point(panelWidth / 2 + 10, 0);

                    // Positionner et redimensionner les grids
                    dataGridViewStockBas.Location = new Point(0, 35);
                    dataGridViewStockBas.Size = new Size(gridWidth, gridHeight);

                    dataGridViewCommandes.Location = new Point(panelWidth / 2 + 10, 35);
                    dataGridViewCommandes.Size = new Size(gridWidth, gridHeight);
                }

                // Section 2: Populaires et Péremption
                if (dataGridViewPopulaires != null && dataGridViewAlertePeremption != null)
                {
                    int gridWidth = panelWidth / 2 - 10;
                    int gridHeight = panelHeightSection2 - 40;

                    // Positionner les labels
                    lblMedicamentsPopulaires.Location = new Point(0, 0);
                    lblAlertePeremption.Location = new Point(panelWidth / 2 + 10, 0);

                    // Positionner et redimensionner les grids
                    dataGridViewPopulaires.Location = new Point(0, 35);
                    dataGridViewPopulaires.Size = new Size(gridWidth, gridHeight);

                    dataGridViewAlertePeremption.Location = new Point(panelWidth / 2 + 10, 35);
                    dataGridViewAlertePeremption.Size = new Size(gridWidth, gridHeight);
                }

                // Ajuster automatiquement les colonnes
                AjusterColonnesAutomatiquement();
            }
            catch (Exception ex)
            {
                // Ignorer les erreurs de redimensionnement
                Console.WriteLine("Erreur ajustement: " + ex.Message);
            }
        }

        private void AjusterColonnesAutomatiquement()
        {
            AjusterColonnesDataGridView(dataGridViewStockBas);
            AjusterColonnesDataGridView(dataGridViewCommandes);
            AjusterColonnesDataGridView(dataGridViewPopulaires);
            AjusterColonnesDataGridView(dataGridViewAlertePeremption);
        }

        private void AjusterColonnesDataGridView(DataGridView dgv)
        {
            if (dgv == null || dgv.Columns.Count == 0) return;

            int scrollBarWidth = SystemInformation.VerticalScrollBarWidth;
            int availableWidth = dgv.ClientSize.Width - scrollBarWidth;

            if (availableWidth > 0)
            {
                // Répartir équitablement la largeur entre les colonnes
                int columnWidth = availableWidth / dgv.Columns.Count;
                foreach (DataGridViewColumn column in dgv.Columns)
                {
                    column.Width = columnWidth;
                }
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

        private void ChargerDonnees()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // Charger toutes les données
                ChargerStatistiques();
                ChargerMedicamentsStockBas();
                ChargerCommandesRecentes();
                ChargerMedicamentsPopulaires();
                ChargerMedicamentsAlertePeremption();

                // Vérifier les alertes
                VerifierAlertesStock();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des données:\n{ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void ChargerStatistiques()
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();

                    // CA Journalier
                    string queryCA = @"SELECT ISNULL(SUM(f.total), 0) 
                                     FROM Commande c
                                     INNER JOIN Facture f ON c.id_commande = f.id_commande
                                     WHERE CAST(c.date_commande AS DATE) = CAST(GETDATE() AS DATE) 
                                     AND c.statut != 'Annulée'
                                     AND f.statut != 'Annulée'";
                    using (SqlCommand cmdCA = new SqlCommand(queryCA, conn))
                    {
                        decimal caJournalier = Convert.ToDecimal(cmdCA.ExecuteScalar());
                        lblCAJournalier.Text = $"{caJournalier:C2}";
                    }

                    // Commandes Jour
                    string queryCommandes = @"SELECT COUNT(*) 
                                            FROM Commande 
                                            WHERE CAST(date_commande AS DATE) = CAST(GETDATE() AS DATE)
                                            AND statut != 'Annulée'";
                    using (SqlCommand cmdCommandes = new SqlCommand(queryCommandes, conn))
                    {
                        int commandesJour = Convert.ToInt32(cmdCommandes.ExecuteScalar());
                        lblCommandesJour.Text = commandesJour.ToString();
                    }

                    // Alertes Stock
                    string queryStockBas = @"SELECT COUNT(*) 
                                           FROM Medicament 
                                           WHERE stock <= seuil_alerte";
                    using (SqlCommand cmdStockBas = new SqlCommand(queryStockBas, conn))
                    {
                        int stockBas = Convert.ToInt32(cmdStockBas.ExecuteScalar());
                        lblAlertesStock.Text = stockBas.ToString();
                    }

                    // Clients Mois
                    string queryClients = $@"SELECT COUNT(DISTINCT {commandeUserColumn}) 
                                          FROM Commande 
                                          WHERE MONTH(date_commande) = MONTH(GETDATE()) 
                                          AND YEAR(date_commande) = YEAR(GETDATE())";
                    using (SqlCommand cmdClients = new SqlCommand(queryClients, conn))
                    {
                        int clientsMois = Convert.ToInt32(cmdClients.ExecuteScalar());
                        lblClientsMois.Text = clientsMois.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des statistiques:\n{ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                                   WHERE stock <= seuil_alerte";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        if (count > 0)
                        {
                            System.Media.SystemSounds.Exclamation.Play();
                            if (MessageBox.Show($"⚠️ ALERTE : {count} médicament(s) ont atteint le seuil d'alerte !\n\nSouhaitez-vous consulter la liste des stocks bas ?",
                                "Alerte Stock", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {
                                if (dataGridViewStockBas.Rows.Count > 0)
                                {
                                    dataGridViewStockBas.Focus();
                                    dataGridViewStockBas.Rows[0].Selected = true;
                                }
                            }
                        }
                    }
                }
            }
            catch { /* Ignorer les erreurs de notification */ }
        }

        private void ChargerMedicamentsAlertePeremption()
        {
            try
            {
                dataGridViewAlertePeremption.Rows.Clear();
                dataGridViewAlertePeremption.Columns.Clear();

                // Ajouter les colonnes
                dataGridViewAlertePeremption.Columns.Add("colMedicament", "MÉDICAMENT");
                dataGridViewAlertePeremption.Columns.Add("colStock", "STOCK");
                dataGridViewAlertePeremption.Columns.Add("colJours", "JOURS RESTANTS");

                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();

                    string query = @"SELECT TOP 10 
                                   nom, stock, 
                                   DATEDIFF(DAY, GETDATE(), date_peremption) as JoursRestants
                                   FROM Medicament 
                                   WHERE date_peremption IS NOT NULL
                                   AND date_peremption <= DATEADD(DAY, 30, GETDATE())
                                   AND stock > 0
                                   ORDER BY date_peremption ASC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int joursRestants = Convert.ToInt32(reader["JoursRestants"]);
                            int rowIndex = dataGridViewAlertePeremption.Rows.Add(
                                reader["nom"].ToString(),
                                reader["stock"].ToString(),
                                joursRestants.ToString()
                            );

                            // Colorer selon l'urgence