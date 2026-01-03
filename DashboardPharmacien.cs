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
        private readonly Color AccentColor = Color.FromArgb(0, 120, 215); // Modern Blue
        private readonly Color BackgroundColor = Color.FromArgb(243, 244, 246); // Light Gray
        private readonly Color CardColor = Color.White;

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
                int panelWidth = panelContent.Width - 40; // More padding
                int halfWidth = (panelWidth - 20) / 2;
                int topSectionHeight = panelContent.Height / 2 - 60;
                
                // Section 1: Stock Bas & Commandes (Top)
                if (dataGridViewStockBas != null && dataGridViewCommandes != null)
                {
                    // Section 1 Labels
                    lblStockBas.Location = new Point(20, 20);
                    lblCommandesRecentes.Location = new Point(20 + halfWidth + 20, 20);

                    // Section 1 Grids
                    dataGridViewStockBas.Location = new Point(20, 50);
                    dataGridViewStockBas.Size = new Size(halfWidth, topSectionHeight);

                    dataGridViewCommandes.Location = new Point(20 + halfWidth + 20, 50);
                    dataGridViewCommandes.Size = new Size(halfWidth, topSectionHeight);
                }

                // Section 2: Péremption (Bottom)
                if (dataGridViewAlertePeremption != null)
                {
                    int startY = 50 + topSectionHeight + 40;

                    // Section 2 Labels
                    lblAlertePeremption.Location = new Point(20, startY);

                    // Section 2 Grids
                    dataGridViewAlertePeremption.Location = new Point(20, startY + 30);
                    dataGridViewAlertePeremption.Size = new Size(panelWidth, topSectionHeight);
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
                ChargerMedicamentsStockBas();
                ChargerCommandesRecentes();
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

        // ChargerStatistiques REMOVED

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