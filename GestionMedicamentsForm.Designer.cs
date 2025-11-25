using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GestionDesMedicaments
{
    partial class GestionMedicamentsForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitre;
        private GroupBox groupBoxFormulaire;
        private TextBox txtNom;
        private TextBox txtDescription;
        private TextBox txtPrixAchat;
        private TextBox txtPrixVente;
        private TextBox txtStock;
        private TextBox txtSeuilAlerte;
        private ComboBox comboBoxFournisseur;
        private Button btnAjouter;
        private Button btnModifier;
        private Button btnSupprimer;
        private Button btnNouveau;
        private DataGridView dataGridViewMedicaments;
        private TextBox txtRecherche;
        private Label lblRecherche;
        private DataGridView dataGridViewPanier;
        private Label lblPanier;
        private Label lblTotal;
        private Button btnSupprimerPanier;
        private Button btnViderPanier;


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
            this.lblTitre = new Label();
            this.groupBoxFormulaire = new GroupBox();
            this.txtNom = new TextBox();
            this.txtDescription = new TextBox();
            this.txtPrixAchat = new TextBox();
            this.txtPrixVente = new TextBox();
            this.txtStock = new TextBox();
            this.txtSeuilAlerte = new TextBox();
            this.comboBoxFournisseur = new ComboBox();
            this.btnAjouter = new Button();
            this.btnModifier = new Button();
            this.btnSupprimer = new Button();
            this.btnNouveau = new Button();
            this.dataGridViewMedicaments = new DataGridView();
            this.txtRecherche = new TextBox();
            this.lblRecherche = new Label();

            // Titre
            this.lblTitre.Text = "💊 Gestion des Médicaments";
            this.lblTitre.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblTitre.ForeColor = Color.Orange;
            this.lblTitre.Location = new System.Drawing.Point(300, 20);
            this.lblTitre.Size = new System.Drawing.Size(350, 40);

            // GroupBox Formulaire
            this.groupBoxFormulaire.Text = "Ajouter un Nouveau Médicament";
            this.groupBoxFormulaire.Location = new System.Drawing.Point(30, 70);
            this.groupBoxFormulaire.Size = new System.Drawing.Size(350, 400);

            // Champs du formulaire
            AddLabelAndControl(groupBoxFormulaire, "Nom *", 30, 30, txtNom, 120, 25);
            AddLabelAndControl(groupBoxFormulaire, "Description", 30, 70, txtDescription, 120, 60);
            AddLabelAndControl(groupBoxFormulaire, "Prix Achat *", 30, 150, txtPrixAchat, 120, 25);
            AddLabelAndControl(groupBoxFormulaire, "Prix Vente *", 30, 190, txtPrixVente, 120, 25);
            AddLabelAndControl(groupBoxFormulaire, "Stock *", 30, 230, txtStock, 120, 25);
            AddLabelAndControl(groupBoxFormulaire, "Seuil Alerte *", 30, 270, txtSeuilAlerte, 120, 25);

            // Fournisseur
            Label lblFournisseur = new Label();
            lblFournisseur.Text = "Fournisseur *";
            lblFournisseur.Location = new System.Drawing.Point(30, 310);
            lblFournisseur.Size = new System.Drawing.Size(100, 20);
            comboBoxFournisseur.Location = new System.Drawing.Point(140, 310);
            comboBoxFournisseur.Size = new System.Drawing.Size(180, 25);
            groupBoxFormulaire.Controls.Add(lblFournisseur);
            groupBoxFormulaire.Controls.Add(comboBoxFournisseur);

            // Boutons
            this.btnAjouter.Text = "➕ Ajouter";
            this.btnAjouter.Location = new System.Drawing.Point(30, 350);
            this.btnAjouter.Size = new System.Drawing.Size(90, 35);
            this.btnAjouter.Click += new EventHandler(this.btnAjouter_Click);

            this.btnModifier.Text = "✏️ Modifier";
            this.btnModifier.Location = new System.Drawing.Point(130, 350);
            this.btnModifier.Size = new System.Drawing.Size(90, 35);
            this.btnModifier.Click += new EventHandler(this.btnModifier_Click);

            this.btnSupprimer.Text = "❌ Supprimer";
            this.btnSupprimer.Location = new System.Drawing.Point(230, 350);
            this.btnSupprimer.Size = new System.Drawing.Size(90, 35);
            this.btnSupprimer.Click += new EventHandler(this.btnSupprimer_Click);

            this.btnNouveau.Text = "🆕 Nouveau";
            this.btnNouveau.Location = new System.Drawing.Point(130, 390);
            this.btnNouveau.Size = new System.Drawing.Size(90, 35);
            this.btnNouveau.Click += new EventHandler(this.btnNouveau_Click);

            groupBoxFormulaire.Controls.AddRange(new Control[] { btnAjouter, btnModifier, btnSupprimer, btnNouveau });

            // Recherche
            this.lblRecherche.Text = "🔍 Rechercher:";
            this.lblRecherche.Location = new System.Drawing.Point(400, 70);
            this.lblRecherche.Size = new System.Drawing.Size(100, 20);

            this.txtRecherche.Location = new System.Drawing.Point(500, 70);
            this.txtRecherche.Size = new System.Drawing.Size(250, 25);
            this.txtRecherche.TextChanged += new EventHandler(this.txtRecherche_TextChanged);

            // DataGridView
            this.dataGridViewMedicaments.Location = new System.Drawing.Point(400, 110);
            this.dataGridViewMedicaments.Size = new System.Drawing.Size(750, 450);
            this.dataGridViewMedicaments.RowHeadersVisible = false;
            this.dataGridViewMedicaments.AllowUserToAddRows = false;
            this.dataGridViewMedicaments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewMedicaments.CellDoubleClick += new DataGridViewCellEventHandler(this.dataGridViewMedicaments_CellDoubleClick);


            this.lblPanier = new Label();
            this.lblPanier.Text = "Votre Panier";
            this.lblPanier.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblPanier.ForeColor = Color.Orange;
            this.lblPanier.Location = new System.Drawing.Point(30, 400);

            // 🧺 dataGridViewPanier
            this.dataGridViewPanier = new DataGridView();
            this.dataGridViewPanier.Location = new System.Drawing.Point(30, 430);
            this.dataGridViewPanier.Size = new System.Drawing.Size(720, 150);
            this.dataGridViewPanier.RowHeadersVisible = false;
            this.dataGridViewPanier.AllowUserToAddRows = false;

            // 💰 lblTotal
            this.lblTotal = new Label();
            this.lblTotal.Text = "Total: 0,00 €";
            this.lblTotal.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblTotal.ForeColor = Color.Orange;
            this.lblTotal.Location = new System.Drawing.Point(500, 590);

            // ❌ btnSupprimerPanier
            this.btnSupprimerPanier = new Button();
            this.btnSupprimerPanier.Text = "❌ Supprimer";
            this.btnSupprimerPanier.BackColor = Color.Red;
            // ... configuration style

            // 🗑️ btnViderPanier
            this.btnViderPanier = new Button();
            this.btnViderPanier.Text = "🗑️ Vider panier";
            this.btnViderPanier.BackColor = Color.DarkRed;

            this.Controls.AddRange(new Control[] {
             lblPanier, dataGridViewPanier, lblTotal, btnSupprimerPanier, btnViderPanier
            });

            // Appliquer le thème orange aux boutons
            AppliquerThemeOrange();

            // Ajout des contrôles au formulaire
            this.Controls.AddRange(new Control[] {
                lblTitre, groupBoxFormulaire, lblRecherche, txtRecherche, dataGridViewMedicaments
            });

            this.ClientSize = new System.Drawing.Size(1200, 600);
            this.Text = "Gestion des Médicaments";
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void AddLabelAndControl(GroupBox groupBox, string labelText, int x, int y, Control control, int width, int height)
        {
            Label label = new Label();
            label.Text = labelText;
            label.Location = new System.Drawing.Point(x, y);
            label.Size = new System.Drawing.Size(100, 20);

            control.Location = new System.Drawing.Point(x + 110, y);
            control.Size = new System.Drawing.Size(width, height);

            if (control is TextBox textBox)
            {
                textBox.Multiline = (height > 25);
                if (textBox.Multiline)
                    textBox.ScrollBars = ScrollBars.Vertical;
            }

            groupBox.Controls.Add(label);
            groupBox.Controls.Add(control);
        }

        private void AppliquerThemeOrange()
        {
            Color orange = Color.FromArgb(255, 128, 0);

            var buttons = this.Controls.OfType<Button>();
            foreach (var btn in buttons)
            {
                btn.BackColor = orange;
                btn.ForeColor = Color.White;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
            }
        }
    }
}