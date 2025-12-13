using System;
using System.Drawing;
using System.Windows.Forms;

namespace GestionDesMedicaments
{
    partial class DashboardPharmacien
    {
        private Button btnDeconnexion;
        private System.ComponentModel.IContainer components = null;
        private Label lblTitre;
        private Panel panelStats;
        private Label lblCAJournalier;
        private Label lblCommandesJour;
        private Label lblAlertesStock;
        private Label lblClientsMois;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private DataGridView dataGridViewStockBas;
        private DataGridView dataGridViewCommandes;
        private DataGridView dataGridViewPopulaires;
        private DataGridView dataGridViewAlertePeremption;
        private Label lblStockBas;
        private Label lblCommandesRecentes;
        private Label lblMedicamentsPopulaires;
        private Label lblAlertePeremption;
        private Button btnRafraichir;
        private Panel panelSidebar;
        private Button btnSidebarMedicaments;
        private Button btnSidebarCommandes;
        private Button btnSidebarClients;
        private Panel panelContent;
        private TableLayoutPanel tableLayoutPrincipal;
        private Panel panelSection1;
        private Panel panelSection2;
        private Panel panelSection3;
        private Panel panelSection4;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitre = new Label();
            this.panelStats = new Panel();
            this.label1 = new Label();
            this.lblCAJournalier = new Label();
            this.label2 = new Label();
            this.lblCommandesJour = new Label();
            this.label3 = new Label();
            this.lblAlertesStock = new Label();
            this.label4 = new Label();
            this.lblClientsMois = new Label();
            this.dataGridViewStockBas = new DataGridView();
            this.dataGridViewCommandes = new DataGridView();
            this.dataGridViewPopulaires = new DataGridView();
            this.dataGridViewAlertePeremption = new DataGridView();
            this.lblStockBas = new Label();
            this.lblCommandesRecentes = new Label();
            this.lblMedicamentsPopulaires = new Label();
            this.lblAlertePeremption = new Label();
            this.btnRafraichir = new Button();
            this.btnDeconnexion = new Button();
            this.panelSidebar = new Panel();
            this.btnSidebarMedicaments = new Button();
            this.btnSidebarCommandes = new Button();
            this.btnSidebarClients = new Button();
            this.panelContent = new Panel();
            this.tableLayoutPrincipal = new TableLayoutPanel();
            this.panelSection1 = new Panel();
            this.panelSection2 = new Panel();
            this.panelSection3 = new Panel();
            this.panelSection4 = new Panel();

            // Début de l'initialisation
            this.SuspendLayout();

            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = Color.FromArgb(50, 50, 60);
            this.panelSidebar.Dock = DockStyle.Left;
            this.panelSidebar.Width = 220;
            this.panelSidebar.Controls.Add(this.btnSidebarMedicaments);
            this.panelSidebar.Controls.Add(this.btnSidebarCommandes);
            this.panelSidebar.Controls.Add(this.btnSidebarClients);
            this.panelSidebar.Controls.Add(this.btnRafraichir);
            this.panelSidebar.Controls.Add(this.btnDeconnexion);

            // 
            // btnSidebarMedicaments
            // 
            this.btnSidebarMedicaments.Dock = DockStyle.Top;
            this.btnSidebarMedicaments.Height = 60;
            this.btnSidebarMedicaments.Text = "💊 Médicaments";
            this.btnSidebarMedicaments.BackColor = Color.FromArgb(70, 70, 80);
            this.btnSidebarMedicaments.ForeColor = Color.White;
            this.btnSidebarMedicaments.FlatStyle = FlatStyle.Flat;
            this.btnSidebarMedicaments.FlatAppearance.BorderSize = 0;
            this.btnSidebarMedicaments.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnSidebarMedicaments.TextAlign = ContentAlignment.MiddleLeft;
            this.btnSidebarMedicaments.Padding = new Padding(20, 0, 0, 0);
            this.btnSidebarMedicaments.Cursor = Cursors.Hand;
            this.btnSidebarMedicaments.Click += new EventHandler(this.btnSidebarMedicaments_Click);
            this.btnSidebarMedicaments.MouseEnter += (s, e) => { this.btnSidebarMedicaments.BackColor = Color.FromArgb(255, 140, 0); };
            this.btnSidebarMedicaments.MouseLeave += (s, e) => { this.btnSidebarMedicaments.BackColor = Color.FromArgb(70, 70, 80); };

            // 
            // btnSidebarCommandes
            // 
            this.btnSidebarCommandes.Dock = DockStyle.Top;
            this.btnSidebarCommandes.Height = 60;
            this.btnSidebarCommandes.Text = "📦 Commandes";
            this.btnSidebarCommandes.BackColor = Color.FromArgb(70, 70, 80);
            this.btnSidebarCommandes.ForeColor = Color.White;
            this.btnSidebarCommandes.FlatStyle = FlatStyle.Flat;
            this.btnSidebarCommandes.FlatAppearance.BorderSize = 0;
            this.btnSidebarCommandes.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnSidebarCommandes.TextAlign = ContentAlignment.MiddleLeft;
            this.btnSidebarCommandes.Padding = new Padding(20, 0, 0, 0);
            this.btnSidebarCommandes.Cursor = Cursors.Hand;
            this.btnSidebarCommandes.Click += new EventHandler(this.btnSidebarCommandes_Click);
            this.btnSidebarCommandes.MouseEnter += (s, e) => { this.btnSidebarCommandes.BackColor = Color.FromArgb(255, 140, 0); };
            this.btnSidebarCommandes.MouseLeave += (s, e) => { this.btnSidebarCommandes.BackColor = Color.FromArgb(70, 70, 80); };

            // 
            // btnSidebarClients
            // 
            this.btnSidebarClients.Dock = DockStyle.Top;
            this.btnSidebarClients.Height = 60;
            this.btnSidebarClients.Text = "👥 Clients";
            this.btnSidebarClients.BackColor = Color.FromArgb(70, 70, 80);
            this.btnSidebarClients.ForeColor = Color.White;
            this.btnSidebarClients.FlatStyle = FlatStyle.Flat;
            this.btnSidebarClients.FlatAppearance.BorderSize = 0;
            this.btnSidebarClients.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnSidebarClients.TextAlign = ContentAlignment.MiddleLeft;
            this.btnSidebarClients.Padding = new Padding(20, 0, 0, 0);
            this.btnSidebarClients.Cursor = Cursors.Hand;
            this.btnSidebarClients.Click += new EventHandler(this.btnSidebarClients_Click);
            this.btnSidebarClients.MouseEnter += (s, e) => { this.btnSidebarClients.BackColor = Color.FromArgb(255, 140, 0); };
            this.btnSidebarClients.MouseLeave += (s, e) => { this.btnSidebarClients.BackColor = Color.FromArgb(70, 70, 80); };

            // 
            // btnRafraichir
            // 
            this.btnRafraichir.Dock = DockStyle.Bottom;
            this.btnRafraichir.Height = 60;
            this.btnRafraichir.Text = "🔄 Rafraîchir";
            this.btnRafraichir.BackColor = Color.FromArgb(100, 150, 200);
            this.btnRafraichir.ForeColor = Color.White;
            this.btnRafraichir.FlatStyle = FlatStyle.Flat;
            this.btnRafraichir.FlatAppearance.BorderSize = 0;
            this.btnRafraichir.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnRafraichir.TextAlign = ContentAlignment.MiddleLeft;
            this.btnRafraichir.Padding = new Padding(20, 0, 0, 0);
            this.btnRafraichir.Cursor = Cursors.Hand;
            this.btnRafraichir.Click += new EventHandler(this.btnRafraichir_Click);

            // 
            // btnDeconnexion
            // 
            this.btnDeconnexion.Dock = DockStyle.Bottom;
            this.btnDeconnexion.Height = 60;
            this.btnDeconnexion.Text = "🔒 Déconnexion";
            this.btnDeconnexion.BackColor = Color.FromArgb(200, 50, 50);
            this.btnDeconnexion.ForeColor = Color.White;
            this.btnDeconnexion.FlatStyle = FlatStyle.Flat;
            this.btnDeconnexion.FlatAppearance.BorderSize = 0;
            this.btnDeconnexion.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnDeconnexion.TextAlign = ContentAlignment.MiddleLeft;
            this.btnDeconnexion.Padding = new Padding(20, 0, 0, 0);
            this.btnDeconnexion.Cursor = Cursors.Hand;
            this.btnDeconnexion.Click += new EventHandler(this.btnDeconnexion_Click);

            // 
            // panelContent
            // 
            this.panelContent.Dock = DockStyle.Fill;
            this.panelContent.BackColor = Color.FromArgb(255, 250, 240);
            this.panelContent.Padding = new Padding(15);
            this.panelContent.Controls.Add(this.tableLayoutPrincipal);

            // 
            // tableLayoutPrincipal
            // 
            this.tableLayoutPrincipal.ColumnCount = 1;
            this.tableLayoutPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.tableLayoutPrincipal.Controls.Add(this.lblTitre, 0, 0);
            this.tableLayoutPrincipal.Controls.Add(this.panelStats, 0, 1);
            this.tableLayoutPrincipal.Controls.Add(this.panelSection1, 0, 2);
            this.tableLayoutPrincipal.Controls.Add(this.panelSection2, 0, 3);
            this.tableLayoutPrincipal.Dock = DockStyle.Fill;
            this.tableLayoutPrincipal.RowCount = 4;
            this.tableLayoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            this.tableLayoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 120F));
            this.tableLayoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            this.tableLayoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            this.tableLayoutPrincipal.Padding = new Padding(0);

            // 
            // lblTitre
            // 
            this.lblTitre.Dock = DockStyle.Fill;
            this.lblTitre.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            this.lblTitre.ForeColor = Color.FromArgb(255, 140, 0);
            this.lblTitre.Text = "📊 Tableau de Bord Pharmacien";
            this.lblTitre.TextAlign = ContentAlignment.MiddleLeft;
            this.lblTitre.AutoSize = false;

            // 
            // panelStats
            // 
            this.panelStats.Dock = DockStyle.Fill;
            this.panelStats.BackColor = Color.White;
            this.panelStats.BorderStyle = BorderStyle.FixedSingle;
            this.panelStats.Controls.Add(this.label1);
            this.panelStats.Controls.Add(this.lblCAJournalier);
            this.panelStats.Controls.Add(this.label2);
            this.panelStats.Controls.Add(this.lblCommandesJour);
            this.panelStats.Controls.Add(this.label3);
            this.panelStats.Controls.Add(this.lblAlertesStock);
            this.panelStats.Controls.Add(this.label4);
            this.panelStats.Controls.Add(this.lblClientsMois);
            this.panelStats.Margin = new Padding(0, 10, 0, 10);

            // 
            // label1
            // 
            this.label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.label1.ForeColor = Color.FromArgb(255, 140, 0);
            this.label1.Location = new Point(30, 15);
            this.label1.Name = "label1";
            this.label1.Size = new Size(150, 23);
            this.label1.Text = "💰 CA Journalier";
            this.label1.AutoSize = false;

            // 
            // lblCAJournalier
            // 
            this.lblCAJournalier.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblCAJournalier.ForeColor = Color.Green;
            this.lblCAJournalier.Location = new Point(30, 45);
            this.lblCAJournalier.Name = "lblCAJournalier";
            this.lblCAJournalier.Size = new Size(150, 30);
            this.lblCAJournalier.Text = "0,00 €";
            this.lblCAJournalier.AutoSize = false;

            // 
            // label2
            // 
            this.label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.label2.ForeColor = Color.FromArgb(255, 140, 0);
            this.label2.Location = new Point(200, 15);
            this.label2.Name = "label2";
            this.label2.Size = new Size(180, 23);
            this.label2.Text = "📦 Commandes Aujourd'hui";
            this.label2.AutoSize = false;

            // 
            // lblCommandesJour
            // 
            this.lblCommandesJour.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblCommandesJour.ForeColor = Color.FromArgb(0, 100, 200);
            this.lblCommandesJour.Location = new Point(200, 45);
            this.lblCommandesJour.Name = "lblCommandesJour";
            this.lblCommandesJour.Size = new Size(100, 30);
            this.lblCommandesJour.Text = "0";
            this.lblCommandesJour.AutoSize = false;

            // 
            // label3
            // 
            this.label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.label3.ForeColor = Color.FromArgb(255, 140, 0);
            this.label3.Location = new Point(400, 15);
            this.label3.Name = "label3";
            this.label3.Size = new Size(120, 23);
            this.label3.Text = "⚠️ Alertes Stock";
            this.label3.AutoSize = false;

            // 
            // lblAlertesStock
            // 
            this.lblAlertesStock.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblAlertesStock.ForeColor = Color.Red;
            this.lblAlertesStock.Location = new Point(400, 45);
            this.lblAlertesStock.Name = "lblAlertesStock";
            this.lblAlertesStock.Size = new Size(100, 30);
            this.lblAlertesStock.Text = "0";
            this.lblAlertesStock.AutoSize = false;

            // 
            // label4
            // 
            this.label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.label4.ForeColor = Color.FromArgb(255, 140, 0);
            this.label4.Location = new Point(580, 15);
            this.label4.Name = "label4";
            this.label4.Size = new Size(130, 23);
            this.label4.Text = "👥 Clients Ce Mois";
            this.label4.AutoSize = false;

            // 
            // lblClientsMois
            // 
            this.lblClientsMois.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblClientsMois.ForeColor = Color.FromArgb(150, 0, 200);
            this.lblClientsMois.Location = new Point(580, 45);
            this.lblClientsMois.Name = "lblClientsMois";
            this.lblClientsMois.Size = new Size(100, 30);
            this.lblClientsMois.Text = "0";
            this.lblClientsMois.AutoSize = false;

            // 
            // panelSection1
            // 
            this.panelSection1.Dock = DockStyle.Fill;
            this.panelSection1.BackColor = Color.Transparent;
            this.panelSection1.Margin = new Padding(0, 10, 0, 5);
            this.panelSection1.Controls.Add(this.lblStockBas);
            this.panelSection1.Controls.Add(this.dataGridViewStockBas);
            this.panelSection1.Controls.Add(this.lblCommandesRecentes);
            this.panelSection1.Controls.Add(this.dataGridViewCommandes);

            // 
            // lblStockBas
            // 
            this.lblStockBas.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblStockBas.ForeColor = Color.FromArgb(255, 140, 0);
            this.lblStockBas.Location = new Point(0, 0);
            this.lblStockBas.Name = "lblStockBas";
            this.lblStockBas.Size = new Size(250, 30);
            this.lblStockBas.Text = "⚠️ Médicaments Stock Bas";
            this.lblStockBas.AutoSize = false;

            // 
            // dataGridViewStockBas
            // 
            this.dataGridViewStockBas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dataGridViewStockBas.Location = new Point(0, 35);
            this.dataGridViewStockBas.Name = "dataGridViewStockBas";
            this.dataGridViewStockBas.RowHeadersWidth = 62;
            this.dataGridViewStockBas.TabIndex = 3;
            this.dataGridViewStockBas.EnableHeadersVisualStyles = false;
            this.dataGridViewStockBas.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 140, 0);
            this.dataGridViewStockBas.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dataGridViewStockBas.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.dataGridViewStockBas.Width = this.panelContent.Width / 2 - 10;
            this.dataGridViewStockBas.Height = this.panelSection1.Height - 40;

            // 
            // lblCommandesRecentes
            // 
            this.lblCommandesRecentes.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblCommandesRecentes.ForeColor = Color.FromArgb(255, 140, 0);
            this.lblCommandesRecentes.Location = new Point(this.panelContent.Width / 2 + 10, 0);
            this.lblCommandesRecentes.Name = "lblCommandesRecentes";
            this.lblCommandesRecentes.Size = new Size(250, 30);
            this.lblCommandesRecentes.Text = "📦 Commandes Récentes";
            this.lblCommandesRecentes.AutoSize = false;

            // 
            // dataGridViewCommandes
            // 
            this.dataGridViewCommandes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dataGridViewCommandes.Location = new Point(this.panelContent.Width / 2 + 10, 35);
            this.dataGridViewCommandes.Name = "dataGridViewCommandes";
            this.dataGridViewCommandes.RowHeadersWidth = 62;
            this.dataGridViewCommandes.TabIndex = 5;
            this.dataGridViewCommandes.EnableHeadersVisualStyles = false;
            this.dataGridViewCommandes.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 140, 0);
            this.dataGridViewCommandes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dataGridViewCommandes.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.dataGridViewCommandes.Width = this.panelContent.Width / 2 - 10;
            this.dataGridViewCommandes.Height = this.panelSection1.Height - 40;

            // 
            // panelSection2
            // 
            this.panelSection2.Dock = DockStyle.Fill;
            this.panelSection2.BackColor = Color.Transparent;
            this.panelSection2.Margin = new Padding(0, 5, 0, 0);
            this.panelSection2.Controls.Add(this.lblMedicamentsPopulaires);
            this.panelSection2.Controls.Add(this.dataGridViewPopulaires);
            this.panelSection2.Controls.Add(this.lblAlertePeremption);
            this.panelSection2.Controls.Add(this.dataGridViewAlertePeremption);

            // 
            // lblMedicamentsPopulaires
            // 
            this.lblMedicamentsPopulaires.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblMedicamentsPopulaires.ForeColor = Color.FromArgb(255, 140, 0);
            this.lblMedicamentsPopulaires.Location = new Point(0, 0);
            this.lblMedicamentsPopulaires.Name = "lblMedicamentsPopulaires";
            this.lblMedicamentsPopulaires.Size = new Size(300, 30);
            this.lblMedicamentsPopulaires.Text = "🔥 Médicaments Populaires (30j)";
            this.lblMedicamentsPopulaires.AutoSize = false;

            // 
            // dataGridViewPopulaires
            // 
            this.dataGridViewPopulaires.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dataGridViewPopulaires.Location = new Point(0, 35);
            this.dataGridViewPopulaires.Name = "dataGridViewPopulaires";
            this.dataGridViewPopulaires.RowHeadersWidth = 62;
            this.dataGridViewPopulaires.TabIndex = 7;
            this.dataGridViewPopulaires.EnableHeadersVisualStyles = false;
            this.dataGridViewPopulaires.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 140, 0);
            this.dataGridViewPopulaires.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dataGridViewPopulaires.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.dataGridViewPopulaires.Width = this.panelContent.Width / 2 - 10;
            this.dataGridViewPopulaires.Height = this.panelSection2.Height - 40;

            // 
            // lblAlertePeremption
            // 
            this.lblAlertePeremption.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblAlertePeremption.ForeColor = Color.FromArgb(255, 140, 0);
            this.lblAlertePeremption.Location = new Point(this.panelContent.Width / 2 + 10, 0);
            this.lblAlertePeremption.Name = "lblAlertePeremption";
            this.lblAlertePeremption.Size = new Size(300, 30);
            this.lblAlertePeremption.Text = "⏰ Médicaments Alerte Péremption (30j)";
            this.lblAlertePeremption.AutoSize = false;

            // 
            // dataGridViewAlertePeremption
            // 
            this.dataGridViewAlertePeremption.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dataGridViewAlertePeremption.Location = new Point(this.panelContent.Width / 2 + 10, 35);
            this.dataGridViewAlertePeremption.Name = "dataGridViewAlertePeremption";
            this.dataGridViewAlertePeremption.RowHeadersWidth = 62;
            this.dataGridViewAlertePeremption.TabIndex = 12;
            this.dataGridViewAlertePeremption.EnableHeadersVisualStyles = false;
            this.dataGridViewAlertePeremption.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 140, 0);
            this.dataGridViewAlertePeremption.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dataGridViewAlertePeremption.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.dataGridViewAlertePeremption.Width = this.panelContent.Width / 2 - 10;
            this.dataGridViewAlertePeremption.Height = this.panelSection2.Height - 40;

            // 
            // DashboardPharmacien
            // 
            this.ClientSize = new Size(1200, 800);
            this.BackColor = Color.FromArgb(255, 250, 240);
            this.Controls.Add(this.panelSidebar);
            this.Controls.Add(this.panelContent);
            this.Name = "DashboardPharmacien";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "💊 Dashboard Pharmacien - TaPharmacieDeRêve";
            this.Load += new EventHandler(this.DashboardPharmacien_Load);

            this.ResumeLayout(false);
        }
    }
}