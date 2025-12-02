using System;
using System.Drawing;
using System.Windows.Forms;

namespace GestionDesMedicaments
{
    partial class LandingPage
    {
        private System.ComponentModel.IContainer components = null;
        private Button btnSeConnecter;
        private Label lblTitre;
        private Label lblSousTitre;
        private PictureBox pictureBoxLogo;
        private Panel panelHeader;

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
            this.components = new System.ComponentModel.Container();
            this.btnSeConnecter = new Button();
            this.lblTitre = new Label();
            this.lblSousTitre = new Label();
            this.pictureBoxLogo = new PictureBox();
            this.panelHeader = new Panel();

            this.SuspendLayout();

            // Panel Header avec fond orange
            this.panelHeader.BackColor = Color.FromArgb(255, 140, 0);
            this.panelHeader.Dock = DockStyle.Top;
            this.panelHeader.Height = 150;
            this.panelHeader.Controls.Add(this.lblTitre);
            this.panelHeader.Controls.Add(this.pictureBoxLogo);

            // Logo (icône pharmacie)
            this.pictureBoxLogo.Image = SystemIcons.Shield.ToBitmap();
            this.pictureBoxLogo.SizeMode = PictureBoxSizeMode.Zoom;
            this.pictureBoxLogo.Location = new Point(50, 30);
            this.pictureBoxLogo.Size = new Size(80, 80);
            this.pictureBoxLogo.BackColor = Color.Transparent;

            // Titre
            this.lblTitre.Font = new Font("Segoe UI", 32F, FontStyle.Bold);
            this.lblTitre.ForeColor = Color.White;
            this.lblTitre.Text = "💊 TaPharmacieDeRêve";
            this.lblTitre.Location = new Point(150, 40);
            this.lblTitre.AutoSize = true;

            // Sous-titre
            this.lblSousTitre.Font = new Font("Segoe UI", 14F, FontStyle.Regular);
            this.lblSousTitre.ForeColor = Color.FromArgb(100, 100, 100);
            this.lblSousTitre.Text = "Votre partenaire santé de confiance";
            this.lblSousTitre.Location = new Point(300, 200);
            this.lblSousTitre.AutoSize = true;

            // Bouton Se Connecter
            this.btnSeConnecter.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.btnSeConnecter.ForeColor = Color.White;
            this.btnSeConnecter.BackColor = Color.FromArgb(255, 140, 0);
            this.btnSeConnecter.FlatStyle = FlatStyle.Flat;
            this.btnSeConnecter.FlatAppearance.BorderSize = 0;
            this.btnSeConnecter.Size = new Size(250, 60);
            this.btnSeConnecter.Location = new Point(275, 280);
            this.btnSeConnecter.Text = "🔐 Se Connecter";
            this.btnSeConnecter.Cursor = Cursors.Hand;
            this.btnSeConnecter.Click += new EventHandler(this.btnSeConnecter_Click);

            // Form
            this.ClientSize = new Size(800, 500);
            this.BackColor = Color.FromArgb(250, 250, 250);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.lblSousTitre);
            this.Controls.Add(this.btnSeConnecter);
            this.Name = "LandingPage";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "TaPharmacieDeRêve - Accueil";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            this.ResumeLayout(false);
        }
    }
}

