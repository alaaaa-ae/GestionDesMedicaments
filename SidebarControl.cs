using System;
using System.Drawing;
using System.Windows.Forms;

namespace GestionDesMedicaments
{
    public class SidebarControl : UserControl
    {
        public event EventHandler MedicamentsClicked;
        public event EventHandler CommandesClicked;
        public event EventHandler ClientsClicked;
        public event EventHandler DashboardClicked;
        public event EventHandler RefreshClicked;
        public event EventHandler DeconnexionClicked;

        private Button btnDashboard;
        private Button btnMedicaments;
        private Button btnCommandes;
        private Button btnClients;
        private Button btnRefresh;
        private Button btnDeconnexion;
        private Panel panelSidebar;
        private Label lblLogo;

        public SidebarControl()
        {
            InitializeComponent();
        }

        public void SetActiveButton(string buttonName)
        {
            // Réinitialiser tous les boutons
            btnDashboard.BackColor = Color.FromArgb(70, 70, 80);
            btnMedicaments.BackColor = Color.FromArgb(70, 70, 80);
            btnCommandes.BackColor = Color.FromArgb(70, 70, 80);
            btnClients.BackColor = Color.FromArgb(70, 70, 80);

            // Activer le bouton sélectionné
            switch (buttonName.ToLower())
            {
                case "dashboard":
                    btnDashboard.BackColor = Color.FromArgb(255, 140, 0);
                    break;
                case "medicaments":
                    btnMedicaments.BackColor = Color.FromArgb(255, 140, 0);
                    break;
                case "commandes":
                    btnCommandes.BackColor = Color.FromArgb(255, 140, 0);
                    break;
                case "clients":
                    btnClients.BackColor = Color.FromArgb(255, 140, 0);
                    break;
            }
        }

        private void InitializeComponent()
        {
            this.panelSidebar = new Panel();
            this.lblLogo = new Label();
            this.btnDashboard = new Button();
            this.btnMedicaments = new Button();
            this.btnCommandes = new Button();
            this.btnClients = new Button();
            this.btnRefresh = new Button();
            this.btnDeconnexion = new Button();

            this.SuspendLayout();

            // panelSidebar
            this.panelSidebar.BackColor = Color.FromArgb(50, 50, 60);
            this.panelSidebar.Dock = DockStyle.Fill;
            this.panelSidebar.Controls.Add(this.lblLogo);
            this.panelSidebar.Controls.Add(this.btnDashboard);
            this.panelSidebar.Controls.Add(this.btnMedicaments);
            this.panelSidebar.Controls.Add(this.btnCommandes);
            this.panelSidebar.Controls.Add(this.btnClients);
            this.panelSidebar.Controls.Add(this.btnRefresh);
            this.panelSidebar.Controls.Add(this.btnDeconnexion);

            // lblLogo
            this.lblLogo.Dock = DockStyle.Top;
            this.lblLogo.Height = 80;
            this.lblLogo.Text = "💊\nPharmacie";
            this.lblLogo.ForeColor = Color.White;
            this.lblLogo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            this.lblLogo.BackColor = Color.FromArgb(40, 40, 50);

            // btnDashboard
            this.btnDashboard.Dock = DockStyle.Top;
            this.btnDashboard.Height = 60;
            this.btnDashboard.Text = "📊 Dashboard";
            this.btnDashboard.BackColor = Color.FromArgb(70, 70, 80);
            this.btnDashboard.ForeColor = Color.White;
            this.btnDashboard.FlatStyle = FlatStyle.Flat;
            this.btnDashboard.FlatAppearance.BorderSize = 0;
            this.btnDashboard.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnDashboard.TextAlign = ContentAlignment.MiddleLeft;
            this.btnDashboard.Padding = new Padding(20, 0, 0, 0);
            this.btnDashboard.Cursor = Cursors.Hand;
            this.btnDashboard.Click += (s, e) => { SetActiveButton("dashboard"); DashboardClicked?.Invoke(this, e); };
            this.btnDashboard.MouseEnter += (s, e) => { if (btnDashboard.BackColor != Color.FromArgb(255, 140, 0)) btnDashboard.BackColor = Color.FromArgb(90, 90, 100); };
            this.btnDashboard.MouseLeave += (s, e) => { if (btnDashboard.BackColor != Color.FromArgb(255, 140, 0)) btnDashboard.BackColor = Color.FromArgb(70, 70, 80); };

            // btnMedicaments
            this.btnMedicaments.Dock = DockStyle.Top;
            this.btnMedicaments.Height = 60;
            this.btnMedicaments.Text = "💊 Médicaments";
            this.btnMedicaments.BackColor = Color.FromArgb(70, 70, 80);
            this.btnMedicaments.ForeColor = Color.White;
            this.btnMedicaments.FlatStyle = FlatStyle.Flat;
            this.btnMedicaments.FlatAppearance.BorderSize = 0;
            this.btnMedicaments.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnMedicaments.TextAlign = ContentAlignment.MiddleLeft;
            this.btnMedicaments.Padding = new Padding(20, 0, 0, 0);
            this.btnMedicaments.Cursor = Cursors.Hand;
            this.btnMedicaments.Click += (s, e) => { SetActiveButton("medicaments"); MedicamentsClicked?.Invoke(this, e); };
            this.btnMedicaments.MouseEnter += (s, e) => { if (btnMedicaments.BackColor != Color.FromArgb(255, 140, 0)) btnMedicaments.BackColor = Color.FromArgb(90, 90, 100); };
            this.btnMedicaments.MouseLeave += (s, e) => { if (btnMedicaments.BackColor != Color.FromArgb(255, 140, 0)) btnMedicaments.BackColor = Color.FromArgb(70, 70, 80); };

            // btnCommandes
            this.btnCommandes.Dock = DockStyle.Top;
            this.btnCommandes.Height = 60;
            this.btnCommandes.Text = "📦 Commandes";
            this.btnCommandes.BackColor = Color.FromArgb(70, 70, 80);
            this.btnCommandes.ForeColor = Color.White;
            this.btnCommandes.FlatStyle = FlatStyle.Flat;
            this.btnCommandes.FlatAppearance.BorderSize = 0;
            this.btnCommandes.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnCommandes.TextAlign = ContentAlignment.MiddleLeft;
            this.btnCommandes.Padding = new Padding(20, 0, 0, 0);
            this.btnCommandes.Cursor = Cursors.Hand;
            this.btnCommandes.Click += (s, e) => { SetActiveButton("commandes"); CommandesClicked?.Invoke(this, e); };
            this.btnCommandes.MouseEnter += (s, e) => { if (btnCommandes.BackColor != Color.FromArgb(255, 140, 0)) btnCommandes.BackColor = Color.FromArgb(90, 90, 100); };
            this.btnCommandes.MouseLeave += (s, e) => { if (btnCommandes.BackColor != Color.FromArgb(255, 140, 0)) btnCommandes.BackColor = Color.FromArgb(70, 70, 80); };

            // btnClients
            this.btnClients.Dock = DockStyle.Top;
            this.btnClients.Height = 60;
            this.btnClients.Text = "👥 Clients";
            this.btnClients.BackColor = Color.FromArgb(70, 70, 80);
            this.btnClients.ForeColor = Color.White;
            this.btnClients.FlatStyle = FlatStyle.Flat;
            this.btnClients.FlatAppearance.BorderSize = 0;
            this.btnClients.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnClients.TextAlign = ContentAlignment.MiddleLeft;
            this.btnClients.Padding = new Padding(20, 0, 0, 0);
            this.btnClients.Cursor = Cursors.Hand;
            this.btnClients.Click += (s, e) => { SetActiveButton("clients"); ClientsClicked?.Invoke(this, e); };
            this.btnClients.MouseEnter += (s, e) => { if (btnClients.BackColor != Color.FromArgb(255, 140, 0)) btnClients.BackColor = Color.FromArgb(90, 90, 100); };
            this.btnClients.MouseLeave += (s, e) => { if (btnClients.BackColor != Color.FromArgb(255, 140, 0)) btnClients.BackColor = Color.FromArgb(70, 70, 80); };

            // btnRefresh
            this.btnRefresh.Dock = DockStyle.Bottom;
            this.btnRefresh.Height = 60;
            this.btnRefresh.Text = "🔄 Rafraîchir";
            this.btnRefresh.BackColor = Color.FromArgb(100, 150, 200);
            this.btnRefresh.ForeColor = Color.White;
            this.btnRefresh.FlatStyle = FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnRefresh.TextAlign = ContentAlignment.MiddleLeft;
            this.btnRefresh.Padding = new Padding(20, 0, 0, 0);
            this.btnRefresh.Cursor = Cursors.Hand;
            this.btnRefresh.Click += (s, e) => RefreshClicked?.Invoke(this, e);
            this.btnRefresh.MouseEnter += (s, e) => btnRefresh.BackColor = Color.FromArgb(120, 170, 220);
            this.btnRefresh.MouseLeave += (s, e) => btnRefresh.BackColor = Color.FromArgb(100, 150, 200);

            // btnDeconnexion
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
            this.btnDeconnexion.Click += (s, e) => DeconnexionClicked?.Invoke(this, e);
            this.btnDeconnexion.MouseEnter += (s, e) => btnDeconnexion.BackColor = Color.FromArgb(220, 70, 70);
            this.btnDeconnexion.MouseLeave += (s, e) => btnDeconnexion.BackColor = Color.FromArgb(200, 50, 50);

            // SidebarControl
            this.Width = 220;
            this.Dock = DockStyle.Left;
            this.Controls.Add(this.panelSidebar);

            this.ResumeLayout(false);
        }
    }
}

