using System;
using System.Drawing;
using System.Windows.Forms;

namespace GestionDesMedicaments
{
    partial class GestionMedicamentsForm
    {
        private System.ComponentModel.IContainer components = null;
        private Button btnRetour;
        private Label lblTitre;
        private GroupBox groupBoxFormulaire;
        private Label lblNom;
        private TextBox txtNom;
        private Label lblDescription;
        private TextBox txtDescription;
        private Label lblPrixAchat;
        private TextBox txtPrixAchat;
        private Label lblPrixVente;
        private TextBox txtPrixVente;
        private Label lblStock;
        private TextBox txtStock;
        private Label lblSeuilAlerte;
        private TextBox txtSeuilAlerte;
        private Label lblFournisseur;
        private ComboBox comboBoxFournisseur;
        private Button btnAjouter;
        private Button btnModifier;
        private Button btnSupprimer;
        private Button btnNouveau;
        private DataGridView dataGridViewMedicaments;
        private DataGridView dataGridViewAlertePeremption;
        private TextBox txtRecherche;
        private Label lblRecherche;
        private Label lblAlertePeremption;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GestionMedicamentsForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "GestionMedicamentsForm";
            this.Load += new System.EventHandler(this.GestionMedicamentsForm_Load);
            this.ResumeLayout(false);

        }
    }
}
