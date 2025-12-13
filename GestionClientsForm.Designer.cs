using System;
using System.Drawing;
using System.Windows.Forms;

namespace GestionDesMedicaments
{
    partial class GestionClientsForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitre;
        private Label lblRecherche;
        private TextBox txtRecherche;
        private DataGridView dataGridViewClients;
        private DataGridView dataGridViewFactures;

        private GroupBox groupBoxFormulaire;
        private GroupBox groupBoxPaiement;
        private TextBox txtHiddenId, txtNomUtilisateur, txtMotDePasse, txtNom, txtPrenom, txtAdresse, txtTelephone, txtEmail;
        private TextBox txtMontantPaiement;
        private ComboBox comboBoxModePaiement;
        private Button btnAjouter, btnModifier, btnSupprimer, btnNouveau, btnRetour, btnEnregistrerPaiement;

        private readonly Color AccentOrange = Color.FromArgb(255, 140, 0);
        private readonly Color Bg = Color.FromArgb(248, 248, 248);

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GestionClientsForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "GestionClientsForm";
            this.Load += new System.EventHandler(this.GestionClientsForm_Load);
            this.ResumeLayout(false);

        }
    }
}

