using System;
using System.Drawing;
using System.Windows.Forms;

namespace GestionDesMedicaments
{
    partial class GestionCommandesForm
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView DataGridViewCommandes;
        private DataGridView DataGridViewDetails;
        private Button btnNouveau;
        private Button btnModifier;
        private Button btnSupprimer;
        private Button btnImprimer;
        private Button btnStats;
        private Button btnRechercher;
        private Button btnRetour;
        private Button btnReinitialiser;
        private DateTimePicker dtpDate;
        private TextBox txtClient;
        private Label lblDate;
        private Label lblClient;
        private Label lblFiltres;
        private Panel panelHeader;
        private Panel panelRecherche;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.DataGridViewCommandes = new System.Windows.Forms.DataGridView();
            this.DataGridViewDetails = new System.Windows.Forms.DataGridView();
            this.btnNouveau = new System.Windows.Forms.Button();
            this.btnModifier = new System.Windows.Forms.Button();
            this.btnSupprimer = new System.Windows.Forms.Button();
            this.btnImprimer = new System.Windows.Forms.Button();
            this.btnStats = new System.Windows.Forms.Button();
            this.btnRechercher = new System.Windows.Forms.Button();
            this.btnRetour = new System.Windows.Forms.Button();
            this.btnReinitialiser = new System.Windows.Forms.Button();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.txtClient = new System.Windows.Forms.TextBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblClient = new System.Windows.Forms.Label();
            this.lblFiltres = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.panelRecherche = new System.Windows.Forms.Panel();

            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewCommandes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewDetails)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.panelRecherche.SuspendLayout();
            this.SuspendLayout();

            // Panel Header
            this.panelHeader.BackColor = Color.FromArgb(255, 140, 0);
            this.panelHeader.Dock = DockStyle.Top;
            this.panelHeader.Height = 60;
            this.panelHeader.Padding = new Padding(10);

            var lblTitre = new Label
            {
                Text = "📦 Gestion des Commandes",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(15, 15),
                AutoSize = true
            };
            this.panelHeader.Controls.Add(lblTitre);
            this.panelHeader.Controls.Add(this.btnRetour);

            // btnRetour
            this.btnRetour.BackColor = Color.FromArgb(100, 100, 100);
            this.btnRetour.FlatStyle = FlatStyle.Flat;
            this.btnRetour.FlatAppearance.BorderSize = 0;
            this.btnRetour.ForeColor = Color.White;
            this.btnRetour.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnRetour.Location = new Point(680, 15);
            this.btnRetour.Size = new Size(100, 30);
            this.btnRetour.Text = "🔙 Retour";
            this.btnRetour.Cursor = Cursors.Hand;
            this.btnRetour.Click += new EventHandler(this.btnRetour_Click);

            // Panel Recherche
            this.panelRecherche.BackColor = Color.FromArgb(255, 250, 240);
            this.panelRecherche.Dock = DockStyle.Top;
            this.panelRecherche.Height = 80;
            this.panelRecherche.Padding = new Padding(15, 10, 15, 10);

            // lblFiltres
            this.lblFiltres.AutoSize = true;
            this.lblFiltres.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblFiltres.ForeColor = Color.FromArgb(255, 140, 0);
            this.lblFiltres.Location = new Point(15, 10);
            this.lblFiltres.Text = "🔍 Recherche";

            // lblDate
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblDate.ForeColor = Color.FromArgb(100, 100, 100);
            this.lblDate.Location = new Point(15, 40);
            this.lblDate.Text = "📅 Date :";

            // dtpDate
            this.dtpDate.Location = new Point(80, 37);
            this.dtpDate.Size = new Size(150, 25);
            this.dtpDate.Format = DateTimePickerFormat.Short;
            this.dtpDate.Font = new Font("Segoe UI", 9F);
            this.dtpDate.CalendarForeColor = Color.FromArgb(255, 140, 0);

            // lblClient
            this.lblClient.AutoSize = true;
            this.lblClient.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblClient.ForeColor = Color.FromArgb(100, 100, 100);
            this.lblClient.Location = new Point(240, 40);
            this.lblClient.Text = "👤 Client :";

            // txtClient
            this.txtClient.Location = new Point(295, 37);
            this.txtClient.Size = new Size(200, 25);
            this.txtClient.Font = new Font("Segoe UI", 9F);
            this.txtClient.BorderStyle = BorderStyle.FixedSingle;

            // btnRechercher
            this.btnRechercher.BackColor = Color.FromArgb(255, 140, 0);
            this.btnRechercher.FlatStyle = FlatStyle.Flat;
            this.btnRechercher.FlatAppearance.BorderSize = 0;
            this.btnRechercher.ForeColor = Color.White;
            this.btnRechercher.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnRechercher.Location = new Point(510, 35);
            this.btnRechercher.Size = new Size(100, 30);
            this.btnRechercher.Text = "🔍 Rechercher";
            this.btnRechercher.Cursor = Cursors.Hand;
            this.btnRechercher.Click += new EventHandler(this.btnRechercher_Click);

            // btnReinitialiser
            this.btnReinitialiser.BackColor = Color.FromArgb(100, 100, 100);
            this.btnReinitialiser.FlatStyle = FlatStyle.Flat;
            this.btnReinitialiser.FlatAppearance.BorderSize = 0;
            this.btnReinitialiser.ForeColor = Color.White;
            this.btnReinitialiser.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnReinitialiser.Location = new Point(620, 35);
            this.btnReinitialiser.Size = new Size(90, 30);
            this.btnReinitialiser.Text = "🔄 Réinitialiser";
            this.btnReinitialiser.Cursor = Cursors.Hand;
            this.btnReinitialiser.Click += new EventHandler(this.btnReinitialiser_Click);

            // Panel Recherche - Add controls
            this.panelRecherche.Controls.Add(this.lblFiltres);
            this.panelRecherche.Controls.Add(this.lblDate);
            this.panelRecherche.Controls.Add(this.dtpDate);
            this.panelRecherche.Controls.Add(this.lblClient);
            this.panelRecherche.Controls.Add(this.txtClient);
            this.panelRecherche.Controls.Add(this.btnRechercher);
            this.panelRecherche.Controls.Add(this.btnReinitialiser);

            // DataGridViewCommandes
            this.DataGridViewCommandes.AllowUserToAddRows = false;
            this.DataGridViewCommandes.AllowUserToDeleteRows = false;
            this.DataGridViewCommandes.Location = new Point(20, 140);
            this.DataGridViewCommandes.Size = new Size(760, 260);
            this.DataGridViewCommandes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.DataGridViewCommandes.BackgroundColor = Color.White;
            this.DataGridViewCommandes.BorderStyle = BorderStyle.None;
            this.DataGridViewCommandes.EnableHeadersVisualStyles = false;
            this.DataGridViewCommandes.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 140, 0);
            this.DataGridViewCommandes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.DataGridViewCommandes.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.DataGridViewCommandes.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(236, 240, 241);
            this.DataGridViewCommandes.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 246, 250);
            this.DataGridViewCommandes.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            this.DataGridViewCommandes.CellValueChanged += new DataGridViewCellEventHandler(this.DataGridViewCommandes_CellValueChanged);
            this.DataGridViewCommandes.SelectionChanged += new EventHandler(this.DataGridViewCommandes_SelectionChanged);

            // DataGridViewDetails
            this.DataGridViewDetails.AllowUserToAddRows = false;
            this.DataGridViewDetails.AllowUserToDeleteRows = false;
            this.DataGridViewDetails.Location = new Point(20, 420);
            this.DataGridViewDetails.Size = new Size(760, 180);
            this.DataGridViewDetails.BackgroundColor = Color.White;
            this.DataGridViewDetails.BorderStyle = BorderStyle.None;
            this.DataGridViewDetails.EnableHeadersVisualStyles = false;
            this.DataGridViewDetails.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 140, 0);
            this.DataGridViewDetails.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.DataGridViewDetails.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.DataGridViewDetails.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(236, 240, 241);
            this.DataGridViewDetails.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 246, 250);
            this.DataGridViewDetails.DefaultCellStyle.Font = new Font("Segoe UI", 9F);

            // btnNouveau
            this.btnNouveau.BackColor = Color.FromArgb(255, 140, 0);
            this.btnNouveau.FlatStyle = FlatStyle.Flat;
            this.btnNouveau.FlatAppearance.BorderSize = 0;
            this.btnNouveau.ForeColor = Color.White;
            this.btnNouveau.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnNouveau.Location = new Point(500, 610);
            this.btnNouveau.Size = new Size(90, 35);
            this.btnNouveau.Text = "➕ Nouveau";
            this.btnNouveau.Cursor = Cursors.Hand;
            this.btnNouveau.Click += new EventHandler(this.btnNouveau_Click);

            // btnModifier
            this.btnModifier.BackColor = Color.FromArgb(60, 160, 60);
            this.btnModifier.FlatStyle = FlatStyle.Flat;
            this.btnModifier.FlatAppearance.BorderSize = 0;
            this.btnModifier.ForeColor = Color.White;
            this.btnModifier.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnModifier.Location = new Point(600, 610);
            this.btnModifier.Size = new Size(85, 35);
            this.btnModifier.Text = "✏️ Modifier";
            this.btnModifier.Cursor = Cursors.Hand;
            this.btnModifier.Click += new EventHandler(this.btnModifier_Click);

            // btnSupprimer
            this.btnSupprimer.BackColor = Color.FromArgb(200, 50, 50);
            this.btnSupprimer.FlatStyle = FlatStyle.Flat;
            this.btnSupprimer.FlatAppearance.BorderSize = 0;
            this.btnSupprimer.ForeColor = Color.White;
            this.btnSupprimer.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnSupprimer.Location = new Point(690, 610);
            this.btnSupprimer.Size = new Size(90, 35);
            this.btnSupprimer.Text = "🗑️ Supprimer";
            this.btnSupprimer.Cursor = Cursors.Hand;
            this.btnSupprimer.Click += new EventHandler(this.btnSupprimer_Click);

            // btnImprimer
            this.btnImprimer.BackColor = Color.FromArgb(100, 150, 200);
            this.btnImprimer.FlatStyle = FlatStyle.Flat;
            this.btnImprimer.FlatAppearance.BorderSize = 0;
            this.btnImprimer.ForeColor = Color.White;
            this.btnImprimer.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnImprimer.Location = new Point(20, 610);
            this.btnImprimer.Size = new Size(100, 35);
            this.btnImprimer.Text = "🖨️ Imprimer";
            this.btnImprimer.Cursor = Cursors.Hand;
            this.btnImprimer.Click += new EventHandler(this.btnImprimer_Click);

            // btnStats
            this.btnStats.BackColor = Color.FromArgb(255, 140, 0);
            this.btnStats.FlatStyle = FlatStyle.Flat;
            this.btnStats.FlatAppearance.BorderSize = 0;
            this.btnStats.ForeColor = Color.White;
            this.btnStats.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnStats.Location = new Point(130, 610);
            this.btnStats.Size = new Size(110, 35);
            this.btnStats.Text = "📊 Statistiques";
            this.btnStats.Cursor = Cursors.Hand;
            this.btnStats.Click += new EventHandler(this.btnStats_Click);

            // GestionCommandesForm
            this.BackColor = Color.FromArgb(255, 250, 240);
            this.ClientSize = new Size(800, 660);
            this.Text = "📦 Gestion des Commandes - TaPharmacieDeRêve";
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelRecherche);
            this.Controls.Add(this.DataGridViewCommandes);
            this.Controls.Add(this.DataGridViewDetails);
            this.Controls.Add(this.btnNouveau);
            this.Controls.Add(this.btnModifier);
            this.Controls.Add(this.btnSupprimer);
            this.Controls.Add(this.btnImprimer);
            this.Controls.Add(this.btnStats);
            this.Name = "GestionCommandesForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Gestion des Commandes";
            this.Load += new EventHandler(this.GestionCommandesForm_Load);

            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewCommandes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewDetails)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelRecherche.ResumeLayout(false);
            this.panelRecherche.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
