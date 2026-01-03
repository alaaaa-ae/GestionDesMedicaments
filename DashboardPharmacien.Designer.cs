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
            this.dataGridViewStockBas = new DataGridView();
            this.dataGridViewCommandes = new DataGridView();
            this.dataGridViewCommandes = new DataGridView();
            this.dataGridViewAlertePeremption = new DataGridView();
            this.lblStockBas = new Label();
            this.lblCommandesRecentes = new Label();
            this.lblCommandesRecentes = new Label();
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
            this.panelSidebar.BackColor = Color.FromArgb(31, 41, 55); // Modern Dark
            this.panelSidebar.Dock = DockStyle.Left;
            this.panelSidebar.Width = 250; // Slightly wider
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
            this.btnSidebarMedicaments.Text = "  💊  Médicaments";
            this.btnSidebarMedicaments.BackColor = Color.FromArgb(31, 41, 55);
            this.btnSidebarMedicaments.ForeColor = Color.FromArgb(229, 231, 235); // Light Gray
            this.btnSidebarMedicaments.FlatStyle = FlatStyle.Flat;
            this.btnSidebarMedicaments.FlatAppearance.BorderSize = 0;
            this.btnSidebarMedicaments.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
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
            this.btnSidebarCommandes.Text = "  📦  Commandes";
            this.btnSidebarCommandes.BackColor = Color.FromArgb(31, 41, 55);
            this.btnSidebarCommandes.ForeColor = Color.FromArgb(229, 231, 235);
            this.btnSidebarCommandes.FlatStyle = FlatStyle.Flat;
            this.btnSidebarCommandes.FlatAppearance.BorderSize = 0;
            this.btnSidebarCommandes.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
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
            this.btnSidebarClients.Text = "  👥  Clients";
            this.btnSidebarClients.BackColor = Color.FromArgb(31, 41, 55);
            this.btnSidebarClients.ForeColor = Color.FromArgb(229, 231, 235);
            this.btnSidebarClients.FlatStyle = FlatStyle.Flat;
            this.btnSidebarClients.FlatAppearance.BorderSize = 0;
            this.btnSidebarClients.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
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
            this.btnRafraichir.Text = "  🔄  Rafraîchir";
            this.btnRafraichir.BackColor = Color.FromArgb(55, 65, 81); // Slightly lighter
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
            this.btnDeconnexion.Text = "  🔒  Déconnexion";
            this.btnDeconnexion.BackColor = Color.FromArgb(220, 38, 38); // Modern Red
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
            this.panelContent.Dock = DockStyle.Fill;
            this.panelContent.BackColor = Color.FromArgb(243, 244, 246); // Light Gray Background
            this.panelContent.Padding = new Padding(20);
            this.panelContent.Controls.Add(this.tableLayoutPrincipal);

            // 
            // tableLayoutPrincipal
            // 
            this.tableLayoutPrincipal.ColumnCount = 1;
            this.tableLayoutPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.tableLayoutPrincipal.Controls.Add(this.lblTitre, 0, 0);
            this.tableLayoutPrincipal.Controls.Add(this.lblTitre, 0, 0);
            // panelStats removed
            this.tableLayoutPrincipal.Controls.Add(this.panelSection1, 0, 1);
            this.tableLayoutPrincipal.Controls.Add(this.panelSection2, 0, 2);
            this.tableLayoutPrincipal.Dock = DockStyle.Fill;
            this.tableLayoutPrincipal.RowCount = 3;
            this.tableLayoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            this.tableLayoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            this.tableLayoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            this.tableLayoutPrincipal.Padding = new Padding(0);

            // 
            // lblTitre
            // 
            this.lblTitre.Dock = DockStyle.Fill;
            this.lblTitre.Dock = DockStyle.Fill;
            this.lblTitre.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            this.lblTitre.ForeColor = Color.FromArgb(17, 24, 39); // Dark Text
            this.lblTitre.Text = "Tableau de Bord";
            this.lblTitre.TextAlign = ContentAlignment.MiddleLeft;
            this.lblTitre.AutoSize = false;



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
            this.panelSection2.Controls.Add(this.lblAlertePeremption);
            this.panelSection2.Controls.Add(this.dataGridViewAlertePeremption);



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
            this.ClientSize = new Size(1280, 800);
            this.BackColor = Color.FromArgb(243, 244, 246); // Light Gray Background
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