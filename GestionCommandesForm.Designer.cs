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
            this.SuspendLayout();
            // 
            // GestionCommandesForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "GestionCommandesForm";
            this.Load += new System.EventHandler(this.GestionCommandesForm_Load_1);
            this.ResumeLayout(false);

        }
    }
}
