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

        private GroupBox groupBoxFormulaire;
        private TextBox txtHiddenId, txtNomUtilisateur, txtMotDePasse, txtNom, txtPrenom, txtAdresse, txtTelephone, txtEmail;
        private Button btnAjouter, btnModifier, btnSupprimer, btnNouveau, btnRetour;

        private readonly Color AccentOrange = Color.FromArgb(255, 140, 0);
        private readonly Color Bg = Color.FromArgb(248, 248, 248);

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // Form properties
            this.Text = "Gestion des Clients";
            this.ClientSize = new Size(900, 650);
            this.BackColor = Bg;
            this.Font = new Font("Segoe UI", 9F);

            // Labels
            lblTitre = new Label()
            {
                Text = "Gestion des Clients",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.Black,
                Location = new Point(20, 12),
                AutoSize = true
            };

            lblRecherche = new Label()
            {
                Text = "🔍 Recherche",
                Location = new Point(20, 58),
                AutoSize = true
            };

            // TextBox Recherche
            txtRecherche = new TextBox()
            {
                Location = new Point(120, 54),
                Width = 300
            };

            // DataGridView
            dataGridViewClients = new DataGridView()
            {
                Location = new Point(20, 90),
                Width = 840,
                Height = 250,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dataGridViewClients.EnableHeadersVisualStyles = false;
            dataGridViewClients.ColumnHeadersDefaultCellStyle.BackColor = AccentOrange;
            dataGridViewClients.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewClients.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewClients.ColumnHeadersHeight = 36;

            // GroupBox Formulaire
            groupBoxFormulaire = new GroupBox()
            {
                Text = "Détails du client",
                Location = new Point(20, 360),
                Width = 840,
                Height = 240,
                Font = new Font("Segoe UI", 10F)
            };

            int labelX = 14, inputX = 160, y = 28, gap = 34;

            // Hidden ID
            txtHiddenId = new TextBox() { Visible = false };

            // NomUtilisateur
            var lblUser = new Label() { Text = "Nom d'utilisateur * :", Location = new Point(labelX, y), AutoSize = true };
            txtNomUtilisateur = new TextBox() { Location = new Point(inputX, y), Width = 220 };
            y += gap;

            // Mot de passe
            var lblPwd = new Label() { Text = "Mot de passe * :", Location = new Point(labelX, y), AutoSize = true };
            txtMotDePasse = new TextBox() { Location = new Point(inputX, y), Width = 220, UseSystemPasswordChar = true };
            y += gap;

            // Nom
            var lblN = new Label() { Text = "Nom :", Location = new Point(labelX, y), AutoSize = true };
            txtNom = new TextBox() { Location = new Point(inputX, y), Width = 220 };
            y += gap;

            // Prénom
            var lblP = new Label() { Text = "Prénom :", Location = new Point(labelX, y), AutoSize = true };
            txtPrenom = new TextBox() { Location = new Point(inputX, y), Width = 220 };
            y += gap;

            // Adresse
            var lblAdr = new Label() { Text = "Adresse :", Location = new Point(420, 28), AutoSize = true };
            txtAdresse = new TextBox() { Location = new Point(520, 28), Width = 260 };

            // Téléphone
            var lblTel = new Label() { Text = "Téléphone :", Location = new Point(420, 62), AutoSize = true };
            txtTelephone = new TextBox() { Location = new Point(520, 62), Width = 260 };

            // Email
            var lblMail = new Label() { Text = "Email :", Location = new Point(420, 96), AutoSize = true };
            txtEmail = new TextBox() { Location = new Point(520, 96), Width = 260 };

            // Buttons
            btnAjouter = new Button() { Text = "➕ Ajouter", Location = new Point(420, 140), BackColor = AccentOrange, ForeColor = Color.Black, FlatStyle = FlatStyle.Flat };
            btnModifier = new Button() { Text = "✏️ Modifier", Location = new Point(520, 140), BackColor = Color.FromArgb(60, 160, 60), ForeColor = Color.Black, FlatStyle = FlatStyle.Flat };
            btnSupprimer = new Button() { Text = "❌ Supprimer", Location = new Point(620, 140), BackColor = Color.FromArgb(200, 50, 50), ForeColor = Color.Black, FlatStyle = FlatStyle.Flat };
            btnNouveau = new Button() { Text = "🆕 Nouveau", Location = new Point(720, 140), BackColor = Color.Gray, ForeColor = Color.Black, FlatStyle = FlatStyle.Flat };

            groupBoxFormulaire.Controls.AddRange(new Control[] {
                txtHiddenId,
                lblUser, txtNomUtilisateur,
                lblPwd, txtMotDePasse,
                lblN, txtNom,
                lblP, txtPrenom,
                lblAdr, txtAdresse,
                lblTel, txtTelephone,
                lblMail, txtEmail,
                btnAjouter, btnModifier, btnSupprimer, btnNouveau
            });

            // Bouton Retour
            btnRetour = new Button()
            {
                Text = "🔙 Retour",
                Location = new Point(20, 610),
                Size = new Size(100, 32),
                BackColor = Color.DarkGray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };

            // Ajouter tous les contrôles au Form
            this.Controls.AddRange(new Control[]
            {
                lblTitre, lblRecherche, txtRecherche, dataGridViewClients,
                groupBoxFormulaire, btnRetour
            });
        }
    }
}
