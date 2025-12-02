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
            this.components = new System.ComponentModel.Container();

            // Form properties
            this.Text = "💊 Gestion des Clients - TaPharmacieDeRêve";
            this.ClientSize = new Size(1200, 750);
            this.BackColor = Color.FromArgb(255, 250, 240);
            this.Font = new Font("Segoe UI", 9F);

            // Labels
            lblTitre = new Label()
            {
                Text = "👥 Gestion des Clients",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = AccentOrange,
                Location = new Point(20, 12),
                AutoSize = true
            };

            lblRecherche = new Label()
            {
                Text = "🔍 Recherche",
                Location = new Point(20, 58),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = AccentOrange
            };

            // TextBox Recherche
            txtRecherche = new TextBox()
            {
                Location = new Point(120, 54),
                Width = 300,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Label Factures
            var lblFacturesTitre = new Label()
            {
                Text = "📄 Factures",
                Location = new Point(590, 58),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = AccentOrange
            };
            this.Controls.Add(lblFacturesTitre);

            // DataGridView Clients
            dataGridViewClients = new DataGridView()
            {
                Location = new Point(20, 90),
                Width = 550,
                Height = 200,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dataGridViewClients.EnableHeadersVisualStyles = false;
            dataGridViewClients.ColumnHeadersDefaultCellStyle.BackColor = AccentOrange;
            dataGridViewClients.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewClients.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewClients.ColumnHeadersHeight = 36;

            // DataGridView Factures
            dataGridViewFactures = new DataGridView()
            {
                Location = new Point(590, 90),
                Width = 570,
                Height = 200,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dataGridViewFactures.EnableHeadersVisualStyles = false;
            dataGridViewFactures.ColumnHeadersDefaultCellStyle.BackColor = AccentOrange;
            dataGridViewFactures.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewFactures.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewFactures.ColumnHeadersHeight = 36;

            // GroupBox Formulaire
            groupBoxFormulaire = new GroupBox()
            {
                Text = "💼 Détails du client",
                Location = new Point(20, 310),
                Width = 550,
                Height = 200,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = AccentOrange
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
            btnAjouter = new Button() { Text = "➕ Ajouter", Location = new Point(20, 150), BackColor = AccentOrange, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Size = new Size(100, 35), Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            btnModifier = new Button() { Text = "✏️ Modifier", Location = new Point(130, 150), BackColor = Color.FromArgb(60, 160, 60), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Size = new Size(100, 35), Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            btnSupprimer = new Button() { Text = "❌ Supprimer", Location = new Point(240, 150), BackColor = Color.FromArgb(200, 50, 50), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Size = new Size(100, 35), Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
            btnNouveau = new Button() { Text = "🆕 Nouveau", Location = new Point(350, 150), BackColor = Color.Gray, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Size = new Size(100, 35), Font = new Font("Segoe UI", 9F, FontStyle.Bold) };

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

            // GroupBox Paiement
            groupBoxPaiement = new GroupBox()
            {
                Text = "💳 Gestion des Factures et Paiements",
                Location = new Point(590, 310),
                Width = 570,
                Height = 200,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = AccentOrange
            };

            var lblFactures = new Label() { Text = "📋 Factures du client sélectionné:", Location = new Point(10, 25), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };

            var lblMontant = new Label() { Text = "Montant:", Location = new Point(10, 160), AutoSize = true };
            txtMontantPaiement = new TextBox() { Location = new Point(80, 157), Width = 150 };

            var lblMode = new Label() { Text = "Mode:", Location = new Point(250, 160), AutoSize = true };
            comboBoxModePaiement = new ComboBox() { Location = new Point(300, 157), Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            comboBoxModePaiement.Items.AddRange(new string[] { "Espèces", "Carte bancaire", "Chèque", "Virement" });
            comboBoxModePaiement.SelectedIndex = 0;

            btnEnregistrerPaiement = new Button()
            {
                Text = "✅ Enregistrer Paiement",
                Location = new Point(460, 155),
                Size = new Size(100, 30),
                BackColor = AccentOrange,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };

            groupBoxPaiement.Controls.AddRange(new Control[] { lblFactures, lblMontant, txtMontantPaiement, lblMode, comboBoxModePaiement, btnEnregistrerPaiement });

            // Bouton Retour
            btnRetour = new Button()
            {
                Text = "🔙 Retour",
                Location = new Point(20, 710),
                Size = new Size(120, 35),
                BackColor = Color.FromArgb(100, 100, 100),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };

            // Ajouter tous les contrôles au Form
            this.Controls.AddRange(new Control[]
            {
                lblTitre, lblRecherche, txtRecherche, dataGridViewClients, dataGridViewFactures,
                groupBoxFormulaire, groupBoxPaiement, btnRetour
            });
        }
    }
}

