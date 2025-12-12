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
        private SidebarControl sidebar;
        private Panel panelContent;
        private Panel panelStats;
        private Label lblTitre;
        private Label lblCAJournalier, lblCommandesJour, lblAlertesStock, lblClientsMois;
        private ListView listViewStockBas, listViewCommandes, listViewPopulaires, listViewAlertePeremption;
        private Panel panelGraphique;

        public DashboardPharmacien()
        {
            InitializeComponent();
            InitialiserColonnes();
            ChargerDonnees();
            this.WindowState = FormWindowState.Maximized;
        }

        private void InitializeComponent()
        {
            this.sidebar = new SidebarControl();
            this.panelContent = new Panel();
            this.panelStats = new Panel();
            this.lblTitre = new Label();
            this.lblCAJournalier = new Label();
            this.lblCommandesJour = new Label();
            this.lblAlertesStock = new Label();
            this.lblClientsMois = new Label();
            this.listViewStockBas = new ListView();
            this.listViewCommandes = new ListView();
            this.listViewPopulaires = new ListView();
            this.listViewAlertePeremption = new ListView();
            this.panelGraphique = new Panel();

            this.SuspendLayout();

            // Sidebar
            this.sidebar.Dock = DockStyle.Left;
            this.sidebar.SetActiveButton("dashboard");
            this.sidebar.MedicamentsClicked += Sidebar_MedicamentsClicked;
            this.sidebar.CommandesClicked += Sidebar_CommandesClicked;
            this.sidebar.ClientsClicked += Sidebar_ClientsClicked;
            this.sidebar.RefreshClicked += Sidebar_RefreshClicked;
            this.sidebar.DeconnexionClicked += Sidebar_DeconnexionClicked;

            // Panel Content
            this.panelContent.Dock = DockStyle.Fill;
            this.panelContent.BackColor = Color.FromArgb(245, 247, 250);
            this.panelContent.Padding = new Padding(20);
            this.panelContent.AutoScroll = true;

            // Titre
            this.lblTitre.Text = "📊 Tableau de Bord";
            this.lblTitre.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            this.lblTitre.ForeColor = Color.FromArgb(50, 50, 60);
            this.lblTitre.Location = new Point(20, 20);
            this.lblTitre.AutoSize = true;

            // Panel Stats
            this.panelStats.Location = new Point(20, 80);
            this.panelStats.Size = new Size(this.panelContent.Width - 40, 120);
            this.panelStats.BackColor = Color.White;
            this.panelStats.BorderStyle = BorderStyle.None;
            this.panelStats.Padding = new Padding(20);

            int statWidth = (this.panelStats.Width - 60) / 4;
            int statX = 20;

            // CA Journalier
            var lblCA = new Label { Text = "💰 CA Journalier", Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Color.FromArgb(100, 100, 120), Location = new Point(statX, 15), AutoSize = true };
            this.lblCAJournalier.Text = "0,00 €";
            this.lblCAJournalier.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblCAJournalier.ForeColor = Color.FromArgb(46, 125, 50);
            this.lblCAJournalier.Location = new Point(statX, 40);
            this.lblCAJournalier.AutoSize = true;
            this.panelStats.Controls.Add(lblCA);
            this.panelStats.Controls.Add(this.lblCAJournalier);
            statX += statWidth;

            // Commandes Jour
            var lblCmd = new Label { Text = "📦 Commandes Aujourd'hui", Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Color.FromArgb(100, 100, 120), Location = new Point(statX, 15), AutoSize = true };
            this.lblCommandesJour.Text = "0";
            this.lblCommandesJour.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblCommandesJour.ForeColor = Color.FromArgb(25, 118, 210);
            this.lblCommandesJour.Location = new Point(statX, 40);
            this.lblCommandesJour.AutoSize = true;
            this.panelStats.Controls.Add(lblCmd);
            this.panelStats.Controls.Add(this.lblCommandesJour);
            statX += statWidth;

            // Alertes Stock
            var lblAlert = new Label { Text = "⚠️ Alertes Stock", Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Color.FromArgb(100, 100, 120), Location = new Point(statX, 15), AutoSize = true };
            this.lblAlertesStock.Text = "0";
            this.lblAlertesStock.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblAlertesStock.ForeColor = Color.FromArgb(211, 47, 47);
            this.lblAlertesStock.Location = new Point(statX, 40);
            this.lblAlertesStock.AutoSize = true;
            this.panelStats.Controls.Add(lblAlert);
            this.panelStats.Controls.Add(this.lblAlertesStock);
            statX += statWidth;

            // Clients Mois
            var lblClients = new Label { Text = "👥 Clients Ce Mois", Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Color.FromArgb(100, 100, 120), Location = new Point(statX, 15), AutoSize = true };
            this.lblClientsMois.Text = "0";
            this.lblClientsMois.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblClientsMois.ForeColor = Color.FromArgb(156, 39, 176);
            this.lblClientsMois.Location = new Point(statX, 40);
            this.lblClientsMois.AutoSize = true;
            this.panelStats.Controls.Add(lblClients);
            this.panelStats.Controls.Add(this.lblClientsMois);

            // Panel Graphique
            this.panelGraphique.Location = new Point(20, 220);
            this.panelGraphique.Size = new Size((this.panelContent.Width - 60) / 2, 300);
            this.panelGraphique.BackColor = Color.White;
            this.panelGraphique.BorderStyle = BorderStyle.None;
            this.panelGraphique.Paint += PanelGraphique_Paint;

            var lblGraphique = new Label
            {
                Text = "📈 Évolution des Ventes (7 derniers jours)",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 60),
                Location = new Point(15, 15),
                AutoSize = true
            };
            this.panelGraphique.Controls.Add(lblGraphique);

            // ListViews
            ConfigurerListView(this.listViewStockBas, "⚠️ Médicaments Stock Bas", new Point(20, 540), new Size((this.panelContent.Width - 60) / 2, 250));
            ConfigurerListView(this.listViewCommandes, "📦 Commandes Récentes", new Point((this.panelContent.Width - 20) / 2 + 10, 540), new Size((this.panelContent.Width - 60) / 2, 250));
            ConfigurerListView(this.listViewPopulaires, "🔥 Médicaments Populaires", new Point(20, 810), new Size((this.panelContent.Width - 60) / 2, 250));
            ConfigurerListView(this.listViewAlertePeremption, "⏰ Alertes Péremption", new Point((this.panelContent.Width - 20) / 2 + 10, 810), new Size((this.panelContent.Width - 60) / 2, 250));

            // Ajouter les contrôles
            this.panelContent.Controls.Add(this.lblTitre);
            this.panelContent.Controls.Add(this.panelStats);
            this.panelContent.Controls.Add(this.panelGraphique);
            this.panelContent.Controls.Add(this.listViewStockBas);
            this.panelContent.Controls.Add(this.listViewCommandes);
            this.panelContent.Controls.Add(this.listViewPopulaires);
            this.panelContent.Controls.Add(this.listViewAlertePeremption);

            this.Controls.Add(this.sidebar);
            this.Controls.Add(this.panelContent);

            this.BackColor = Color.FromArgb(245, 247, 250);
            this.Text = "💊 Dashboard Pharmacien";
            this.ResumeLayout(false);

            // Ajuster les tailles après le chargement
            this.Load += (s, e) => AjusterTailles();
        }

        private void AjusterTailles()
        {
            int contentWidth = this.panelContent.Width - 40;
            int statWidth = (contentWidth - 60) / 4;
            this.panelStats.Size = new Size(contentWidth, 120);
            this.panelGraphique.Size = new Size((contentWidth - 20) / 2, 300);
            
            int listWidth = (contentWidth - 20) / 2;
            this.listViewStockBas.Size = new Size(listWidth, 250);
            this.listViewCommandes.Location = new Point(listWidth + 30, 540);
            this.listViewCommandes.Size = new Size(listWidth, 250);
            this.listViewPopulaires.Size = new Size(listWidth, 250);
            this.listViewAlertePeremption.Location = new Point(listWidth + 30, 810);
            this.listViewAlertePeremption.Size = new Size(listWidth, 250);
        }

        private void ConfigurerListView(ListView lv, string titre, Point location, Size size)
        {
            lv.Location = location;
            lv.Size = size;
            lv.View = View.Details;
            lv.FullRowSelect = true;
            lv.GridLines = true;
            lv.BorderStyle = BorderStyle.None;
            lv.BackColor = Color.White;
            lv.Font = new Font("Segoe UI", 9F);
            lv.HeaderStyle = ColumnHeaderStyle.Nonclickable;

            var lblTitre = new Label
            {
                Text = titre,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 60),
                Location = new Point(location.X, location.Y - 25),
                AutoSize = true
            };
            this.panelContent.Controls.Add(lblTitre);
        }

        private void PanelGraphique_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT CAST(date_commande AS DATE) as Date, ISNULL(SUM(f.total), 0) as Total
                                   FROM Commande c
                                   LEFT JOIN Facture f ON c.id_commande = f.id_commande
                                   WHERE c.date_commande >= DATEADD(DAY, -7, GETDATE())
                                   AND c.statut != 'Annulée'
                                   GROUP BY CAST(date_commande AS DATE)
                                   ORDER BY Date";
                    
                    var dates = new List<DateTime>();
                    var valeurs = new List<decimal>();
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dates.Add(Convert.ToDateTime(reader["Date"]));
                                valeurs.Add(Convert.ToDecimal(reader["Total"]));
                            }
                        }
                    }

                    if (valeurs.Count == 0) return;

                    Graphics g = e.Graphics;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    
                    int padding = 40;
                    int graphWidth = this.panelGraphique.Width - padding * 2;
                    int graphHeight = this.panelGraphique.Height - padding * 2 - 30;
                    int startX = padding;
                    int startY = padding + 30;
                    
                    decimal maxValue = valeurs.Max();
                    if (maxValue == 0) maxValue = 1;

                    // Dessiner les axes
                    Pen axisPen = new Pen(Color.FromArgb(200, 200, 200), 1);
                    g.DrawLine(axisPen, startX, startY, startX + graphWidth, startY);
                    g.DrawLine(axisPen, startX, startY, startX, startY - graphHeight);

                    // Dessiner les barres
                    int barWidth = graphWidth / valeurs.Count - 5;
                    Brush barBrush = new SolidBrush(Color.FromArgb(255, 140, 0));
                    
                    for (int i = 0; i < valeurs.Count; i++)
                    {
                        int barHeight = (int)((double)valeurs[i] / (double)maxValue * graphHeight);
                        int x = startX + i * (barWidth + 5) + 2;
                        int y = startY - barHeight;
                        
                        g.FillRectangle(barBrush, x, y, barWidth, barHeight);
                        
                        // Valeur au-dessus
                        string valueText = valeurs[i].ToString("C0");
                        SizeF textSize = g.MeasureString(valueText, new Font("Segoe UI", 8F));
                        g.DrawString(valueText, new Font("Segoe UI", 8F), Brushes.Black, 
                            x + barWidth / 2 - textSize.Width / 2, y - textSize.Height - 2);
                    }
                }
            }
            catch { }
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
            this.panelGraphique.Invalidate();
        }

        private void ChargerStatistiques()
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();

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

                    string queryCommandes = @"SELECT COUNT(*) 
                                            FROM Commande 
                                            WHERE CAST(date_commande AS DATE) = CAST(GETDATE() AS DATE)
                                            AND statut != 'Annulée'";
                    SqlCommand cmdCommandes = new SqlCommand(queryCommandes, conn);
                    object resultCmd = cmdCommandes.ExecuteScalar();
                    int commandesJour = resultCmd != null && resultCmd != DBNull.Value ? Convert.ToInt32(resultCmd) : 0;
                    lblCommandesJour.Text = commandesJour.ToString();

                    string queryStockBas = @"SELECT COUNT(*) 
                                           FROM Medicament 
                                           WHERE stock <= seuil_alerte";
                    SqlCommand cmdStockBas = new SqlCommand(queryStockBas, conn);
                    object resultStock = cmdStockBas.ExecuteScalar();
                    int stockBas = resultStock != null && resultStock != DBNull.Value ? Convert.ToInt32(resultStock) : 0;
                    lblAlertesStock.Text = stockBas.ToString();

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
                                   WHERE stock <= seuil_alerte";
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
            catch { }
        }

        private void ChargerMedicamentsAlertePeremption()
        {
            try
            {
                using (SqlConnection conn = Database.GetConnection())
                {
                    conn.Open();
                    string checkColumn = @"SELECT COUNT(*) 
                                          FROM INFORMATION_SCHEMA.COLUMNS 
                                          WHERE TABLE_NAME = 'Medicament' 
                                          AND COLUMN_NAME = 'date_peremption'";
                    SqlCommand cmdCheck = new SqlCommand(checkColumn, conn);
                    int columnExists = Convert.ToInt32(cmdCheck.ExecuteScalar());

                    this.listViewAlertePeremption.Columns.Clear();
                    this.listViewAlertePeremption.Items.Clear();

                    if (columnExists > 0)
                    {
                        this.listViewAlertePeremption.Columns.Add("Médicament", 200);
                        this.listViewAlertePeremption.Columns.Add("Stock", 80);
                        this.listViewAlertePeremption.Columns.Add("Jours Restants", 120);

                        string query = @"SELECT TOP 10 
                                       nom, stock, DATEDIFF(DAY, GETDATE(), date_peremption) as JoursRestants
                                       FROM Medicament 
                                       WHERE date_peremption IS NOT NULL
                                       AND date_peremption <= DATEADD(DAY, 30, GETDATE())
                                       ORDER BY date_peremption ASC";
                        
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var item = new ListViewItem(reader["nom"].ToString());
                                    item.SubItems.Add(reader["stock"].ToString());
                                    item.SubItems.Add(reader["JoursRestants"].ToString());
                                    this.listViewAlertePeremption.Items.Add(item);
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void ChargerMedicamentsStockBas()
        {
            try
            {
                this.listViewStockBas.Columns.Clear();
                this.listViewStockBas.Items.Clear();
                this.listViewStockBas.Columns.Add("ID", 60);
                this.listViewStockBas.Columns.Add("Nom", 250);
                this.listViewStockBas.Columns.Add("Stock", 80);
                this.listViewStockBas.Columns.Add("Seuil", 80);
                this.listViewStockBas.Columns.Add("Statut", 120);

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

                foreach (var m in medicaments)
                {
                    var item = new ListViewItem(m.Id.ToString());
                    item.SubItems.Add(m.Nom);
                    item.SubItems.Add(m.Stock.ToString());
                    item.SubItems.Add(m.SeuilAlerte.ToString());
                    item.SubItems.Add(m.Statut);
                    if (m.Stock == 0) item.ForeColor = Color.Red;
                    this.listViewStockBas.Items.Add(item);
                }
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
                    this.listViewCommandes.Columns.Clear();
                    this.listViewCommandes.Items.Clear();
                    this.listViewCommandes.Columns.Add("ID", 60);
                    this.listViewCommandes.Columns.Add("Client", 200);
                    this.listViewCommandes.Columns.Add("Date", 150);
                    this.listViewCommandes.Columns.Add("Statut", 120);
                    this.listViewCommandes.Columns.Add("Total", 100);

                    string query = $@"SELECT TOP 10 c.id_commande, u.nom + ' ' + u.prenom as Client, 
                                   c.date_commande, c.statut, ISNULL(f.total, 0) as total
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
                                item.SubItems.Add(reader["Client"].ToString());
                                item.SubItems.Add(Convert.ToDateTime(reader["date_commande"]).ToString("dd/MM/yyyy HH:mm"));
                                item.SubItems.Add(reader["statut"].ToString());
                                item.SubItems.Add(Convert.ToDecimal(reader["total"]).ToString("C2"));
                                this.listViewCommandes.Items.Add(item);
                            }
                        }
                    }
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
                    this.listViewPopulaires.Columns.Clear();
                    this.listViewPopulaires.Items.Clear();
                    this.listViewPopulaires.Columns.Add("Médicament", 300);
                    this.listViewPopulaires.Columns.Add("Quantité Vendue", 150);

                    string query = @"SELECT TOP 5 m.nom, SUM(lc.quantite) as QuantiteVendue
                                   FROM LigneCommande lc
                                   INNER JOIN Medicament m ON lc.id_medicament = m.id_medicament
                                   INNER JOIN Commande c ON lc.id_commande = c.id_commande
                                   WHERE c.date_commande >= DATEADD(DAY, -30, GETDATE())
                                   GROUP BY m.nom
                                   ORDER BY QuantiteVendue DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new ListViewItem(reader["nom"].ToString());
                                item.SubItems.Add(reader["QuantiteVendue"].ToString());
                                this.listViewPopulaires.Items.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur chargement médicaments populaires: {ex.Message}", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Événements Sidebar
        private void Sidebar_MedicamentsClicked(object sender, EventArgs e)
        {
            OuvrirForm(new GestionMedicamentsForm());
        }

        private void Sidebar_CommandesClicked(object sender, EventArgs e)
        {
            OuvrirForm(new GestionCommandesForm());
        }

        private void Sidebar_ClientsClicked(object sender, EventArgs e)
        {
            OuvrirForm(new GestionClientsForm());
        }

        private void Sidebar_RefreshClicked(object sender, EventArgs e)
        {
            ChargerDonnees();
            MessageBox.Show("Données rafraîchies", "Succès",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

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
