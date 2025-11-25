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
        private GroupBox GroupBoxFiltres;
        private DateTimePicker DatePickerDebut;
        private DateTimePicker DatePickerFin;
        private ComboBox ComboBoxStatut;
        private Button BtnFiltrer;
        private GroupBox GroupBoxDetails;
        private Label LabelClient;
        private Label LabelTelephone;
        private Label LabelEmail;
        private Label LabelAdresse;
        private Label LabelDate;
        private Label LabelStatut;
        private Label LabelTotal;
        private Label LabelStatutFacture;
        private Button BtnImprimerFacture;
        private Button BtnRafraichir;
        private Button BtnExportExcel;
        private Panel PanelStats;
        private Label LabelTotalCommandes;
        private Label LabelChiffreAffaires;
        private Label LabelCommandesAttente;
        private Label label1;
        private Label label2;
        private Label label3;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.DataGridViewCommandes = new System.Windows.Forms.DataGridView();
            this.DataGridViewDetails = new System.Windows.Forms.DataGridView();
            this.GroupBoxFiltres = new System.Windows.Forms.GroupBox();
            this.DatePickerDebut = new System.Windows.Forms.DateTimePicker();
            this.DatePickerFin = new System.Windows.Forms.DateTimePicker();
            this.ComboBoxStatut = new System.Windows.Forms.ComboBox();
            this.BtnFiltrer = new System.Windows.Forms.Button();
            this.GroupBoxDetails = new System.Windows.Forms.GroupBox();
            this.LabelStatutFacture = new System.Windows.Forms.Label();
            this.LabelTotal = new System.Windows.Forms.Label();
            this.LabelStatut = new System.Windows.Forms.Label();
            this.LabelDate = new System.Windows.Forms.Label();
            this.LabelAdresse = new System.Windows.Forms.Label();
            this.LabelEmail = new System.Windows.Forms.Label();
            this.LabelTelephone = new System.Windows.Forms.Label();
            this.LabelClient = new System.Windows.Forms.Label();
            this.BtnImprimerFacture = new System.Windows.Forms.Button();
            this.BtnRafraichir = new System.Windows.Forms.Button();
            this.BtnExportExcel = new System.Windows.Forms.Button();
            this.PanelStats = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.LabelCommandesAttente = new System.Windows.Forms.Label();
            this.LabelChiffreAffaires = new System.Windows.Forms.Label();
            this.LabelTotalCommandes = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewCommandes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewDetails)).BeginInit();
            this.GroupBoxFiltres.SuspendLayout();
            this.GroupBoxDetails.SuspendLayout();
            this.PanelStats.SuspendLayout();
            this.SuspendLayout();

            // 
            // DataGridViewCommandes
            // 
            this.DataGridViewCommandes.AllowUserToAddRows = false;
            this.DataGridViewCommandes.ColumnHeadersHeight = 34;
            this.DataGridViewCommandes.Location = new System.Drawing.Point(20, 150);
            this.DataGridViewCommandes.Name = "DataGridViewCommandes";
            this.DataGridViewCommandes.RowHeadersVisible = false;
            this.DataGridViewCommandes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.DataGridViewCommandes.Size = new System.Drawing.Size(600, 200);
            this.DataGridViewCommandes.TabIndex = 0;
            this.DataGridViewCommandes.SelectionChanged += new System.EventHandler(this.DataGridViewCommandes_SelectionChanged);
            this.DataGridViewCommandes.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewCommandes_CellValueChanged);

            // 
            // DataGridViewDetails
            // 
            this.DataGridViewDetails.AllowUserToAddRows = false;
            this.DataGridViewDetails.ColumnHeadersHeight = 34;
            this.DataGridViewDetails.Location = new System.Drawing.Point(20, 400);
            this.DataGridViewDetails.Name = "DataGridViewDetails";
            this.DataGridViewDetails.RowHeadersVisible = false;
            this.DataGridViewDetails.Size = new System.Drawing.Size(600, 150);
            this.DataGridViewDetails.TabIndex = 1;

            // 
            // GroupBoxFiltres
            // 
            this.GroupBoxFiltres.Controls.Add(this.BtnFiltrer);
            this.GroupBoxFiltres.Controls.Add(this.ComboBoxStatut);
            this.GroupBoxFiltres.Controls.Add(this.DatePickerFin);
            this.GroupBoxFiltres.Controls.Add(this.DatePickerDebut);
            this.GroupBoxFiltres.Location = new System.Drawing.Point(20, 60);
            this.GroupBoxFiltres.Size = new System.Drawing.Size(600, 80);
            this.GroupBoxFiltres.Text = "Filtres";

            // Configuration des contrôles de filtres...
            this.DatePickerDebut.Location = new System.Drawing.Point(20, 30);
            this.DatePickerDebut.Size = new System.Drawing.Size(120, 20);

            this.DatePickerFin.Location = new System.Drawing.Point(150, 30);
            this.DatePickerFin.Size = new System.Drawing.Size(120, 20);

            this.ComboBoxStatut.Location = new System.Drawing.Point(280, 30);
            this.ComboBoxStatut.Size = new System.Drawing.Size(120, 21);

            this.BtnFiltrer.Location = new System.Drawing.Point(410, 30);
            this.BtnFiltrer.Size = new System.Drawing.Size(80, 25);
            this.BtnFiltrer.Text = "Filtrer";
            this.BtnFiltrer.BackColor = Color.Orange;
            this.BtnFiltrer.ForeColor = Color.White;
            this.BtnFiltrer.Click += new System.EventHandler(this.BtnFiltrer_Click);

            // 
            // GroupBoxDetails
            // 
            this.GroupBoxDetails.Controls.Add(this.LabelStatutFacture);
            this.GroupBoxDetails.Controls.Add(this.LabelTotal);
            this.GroupBoxDetails.Controls.Add(this.LabelStatut);
            this.GroupBoxDetails.Controls.Add(this.LabelDate);
            this.GroupBoxDetails.Controls.Add(this.LabelAdresse);
            this.GroupBoxDetails.Controls.Add(this.LabelEmail);
            this.GroupBoxDetails.Controls.Add(this.LabelTelephone);
            this.GroupBoxDetails.Controls.Add(this.LabelClient);
            this.GroupBoxDetails.Location = new System.Drawing.Point(630, 150);
            this.GroupBoxDetails.Size = new System.Drawing.Size(300, 400);
            this.GroupBoxDetails.Text = "Détails de la commande";

            // Configuration des labels de détails...
            this.LabelClient.Location = new System.Drawing.Point(20, 30);
            this.LabelClient.Size = new System.Drawing.Size(250, 20);
            this.LabelClient.Text = "Client: ";

            this.LabelTelephone.Location = new System.Drawing.Point(20, 60);
            this.LabelTelephone.Size = new System.Drawing.Size(250, 20);
            this.LabelTelephone.Text = "Téléphone: ";

            // ... configuration similaire pour les autres labels

            // 
            // BtnImprimerFacture
            // 
            this.BtnImprimerFacture.Location = new System.Drawing.Point(630, 560);
            this.BtnImprimerFacture.Size = new System.Drawing.Size(140, 35);
            this.BtnImprimerFacture.Text = "🧾 Imprimer Facture";
            this.BtnImprimerFacture.BackColor = Color.Orange;
            this.BtnImprimerFacture.ForeColor = Color.White;
            this.BtnImprimerFacture.Click += new System.EventHandler(this.BtnImprimerFacture_Click);

            // 
            // BtnRafraichir
            // 
            this.BtnRafraichir.Location = new System.Drawing.Point(480, 560);
            this.BtnRafraichir.Size = new System.Drawing.Size(140, 35);
            this.BtnRafraichir.Text = "🔄 Rafraîchir";
            this.BtnRafraichir.BackColor = Color.Orange;
            this.BtnRafraichir.ForeColor = Color.White;
            this.BtnRafraichir.Click += new System.EventHandler(this.BtnRafraichir_Click);

            // 
            // BtnExportExcel
            // 
            this.BtnExportExcel.Location = new System.Drawing.Point(330, 560);
            this.BtnExportExcel.Size = new System.Drawing.Size(140, 35);
            this.BtnExportExcel.Text = "📊 Export Excel";
            this.BtnExportExcel.BackColor = Color.Green;
            this.BtnExportExcel.ForeColor = Color.White;
            this.BtnExportExcel.Click += new System.EventHandler(this.BtnExportExcel_Click);

            // 
            // PanelStats
            // 
            this.PanelStats.Controls.Add(this.LabelTotalCommandes);
            this.PanelStats.Controls.Add(this.LabelChiffreAffaires);
            this.PanelStats.Controls.Add(this.LabelCommandesAttente);
            this.PanelStats.Controls.Add(this.label1);
            this.PanelStats.Controls.Add(this.label2);
            this.PanelStats.Controls.Add(this.label3);
            this.PanelStats.Location = new System.Drawing.Point(630, 60);
            this.PanelStats.Size = new System.Drawing.Size(300, 80);

            // Configuration des statistiques...
            this.LabelTotalCommandes.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.LabelTotalCommandes.ForeColor = Color.Orange;
            this.LabelTotalCommandes.Text = "0";

            // 
            // GestionCommandesForm
            // 
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Text = "Gestion des Commandes";
            this.StartPosition = FormStartPosition.CenterScreen;

            this.Controls.Add(this.DataGridViewCommandes);
            this.Controls.Add(this.DataGridViewDetails);
            this.Controls.Add(this.GroupBoxFiltres);
            this.Controls.Add(this.GroupBoxDetails);
            this.Controls.Add(this.PanelStats);
            this.Controls.Add(this.BtnImprimerFacture);
            this.Controls.Add(this.BtnRafraichir);
            this.Controls.Add(this.BtnExportExcel);

            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewCommandes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewDetails)).EndInit();
            this.GroupBoxFiltres.ResumeLayout(false);
            this.GroupBoxDetails.ResumeLayout(false);
            this.PanelStats.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}