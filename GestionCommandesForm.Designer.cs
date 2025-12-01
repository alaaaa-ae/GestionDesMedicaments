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
        private DateTimePicker dtpFrom;
        private DateTimePicker dtpTo;
        private TextBox txtClient;
        private Label lblFrom;
        private Label lblTo;
        private Label lblClient;
        private Label lblFiltres;

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
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.txtClient = new System.Windows.Forms.TextBox();
            this.lblFrom = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblClient = new System.Windows.Forms.Label();
            this.lblFiltres = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewCommandes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewDetails)).BeginInit();
            this.SuspendLayout();

            // DataGridViewCommandes
            this.DataGridViewCommandes.AllowUserToAddRows = false;
            this.DataGridViewCommandes.AllowUserToDeleteRows = false;
            this.DataGridViewCommandes.Location = new System.Drawing.Point(20, 80);
            this.DataGridViewCommandes.Name = "DataGridViewCommandes";
            this.DataGridViewCommandes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridViewCommandes.Size = new System.Drawing.Size(760, 260);
            this.DataGridViewCommandes.TabIndex = 0;
            this.DataGridViewCommandes.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewCommandes_CellValueChanged);
            this.DataGridViewCommandes.SelectionChanged += new System.EventHandler(this.DataGridViewCommandes_SelectionChanged);

            // DataGridViewDetails
            this.DataGridViewDetails.AllowUserToAddRows = false;
            this.DataGridViewDetails.AllowUserToDeleteRows = false;
            this.DataGridViewDetails.Location = new System.Drawing.Point(20, 360);
            this.DataGridViewDetails.Name = "DataGridViewDetails";
            this.DataGridViewDetails.Size = new System.Drawing.Size(760, 180);
            this.DataGridViewDetails.TabIndex = 1;

            // Buttons and filters
            this.lblFiltres.AutoSize = true;
            this.lblFiltres.Location = new System.Drawing.Point(20, 18);
            this.lblFiltres.Name = "lblFiltres";
            this.lblFiltres.Text = "Filtres / Recherche :";

            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(20, 40);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Text = "De";

            this.dtpFrom.Location = new System.Drawing.Point(50, 36);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Format = DateTimePickerFormat.Short;
            this.dtpFrom.Width = 110;

            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(170, 40);
            this.lblTo.Name = "lblTo";
            this.lblTo.Text = "À";

            this.dtpTo.Location = new System.Drawing.Point(190, 36);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Format = DateTimePickerFormat.Short;
            this.dtpTo.Width = 110;

            this.lblClient.AutoSize = true;
            this.lblClient.Location = new System.Drawing.Point(310, 40);
            this.lblClient.Name = "lblClient";
            this.lblClient.Text = "Client";

            this.txtClient.Location = new System.Drawing.Point(350, 36);
            this.txtClient.Name = "txtClient";
            this.txtClient.Width = 200;

            this.btnRechercher.Location = new System.Drawing.Point(570, 33);
            this.btnRechercher.Name = "btnRechercher";
            this.btnRechercher.Size = new System.Drawing.Size(80, 26);
            this.btnRechercher.Text = "Rechercher";
            this.btnRechercher.Click += new System.EventHandler(this.btnRechercher_Click);

            this.btnNouveau.Location = new System.Drawing.Point(660, 33);
            this.btnNouveau.Name = "btnNouveau";
            this.btnNouveau.Size = new System.Drawing.Size(60, 26);
            this.btnNouveau.Text = "Nouveau";
            this.btnNouveau.Click += new System.EventHandler(this.btnNouveau_Click);

            this.btnModifier.Location = new System.Drawing.Point(730, 33);
            this.btnModifier.Name = "btnModifier";
            this.btnModifier.Size = new System.Drawing.Size(50, 26);
            this.btnModifier.Text = "Editer";
            this.btnModifier.Click += new System.EventHandler(this.btnModifier_Click);

            this.btnSupprimer.Location = new System.Drawing.Point(730, 350);
            this.btnSupprimer.Name = "btnSupprimer";
            this.btnSupprimer.Size = new System.Drawing.Size(50, 26);
            this.btnSupprimer.Text = "Suppr";
            this.btnSupprimer.Click += new System.EventHandler(this.btnSupprimer_Click);

            this.btnImprimer.Location = new System.Drawing.Point(660, 350);
            this.btnImprimer.Name = "btnImprimer";
            this.btnImprimer.Size = new System.Drawing.Size(60, 26);
            this.btnImprimer.Text = "Imprimer";
            this.btnImprimer.Click += new System.EventHandler(this.btnImprimer_Click);

            this.btnStats.Location = new System.Drawing.Point(580, 350);
            this.btnStats.Name = "btnStats";
            this.btnStats.Size = new System.Drawing.Size(70, 26);
            this.btnStats.Text = "Statistiques";
            this.btnStats.Click += new System.EventHandler(this.btnStats_Click);

            // GestionCommandesForm
            this.ClientSize = new System.Drawing.Size(800, 560);
            this.Controls.Add(this.lblFiltres);
            this.Controls.Add(this.lblFrom);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.lblClient);
            this.Controls.Add(this.txtClient);
            this.Controls.Add(this.btnRechercher);
            this.Controls.Add(this.btnNouveau);
            this.Controls.Add(this.btnModifier);
            this.Controls.Add(this.DataGridViewCommandes);
            this.Controls.Add(this.DataGridViewDetails);
            this.Controls.Add(this.btnSupprimer);
            this.Controls.Add(this.btnImprimer);
            this.Controls.Add(this.btnStats);
            this.Name = "GestionCommandesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestion des commandes";
            this.Load += new System.EventHandler(this.GestionCommandesForm_Load);

            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewCommandes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
