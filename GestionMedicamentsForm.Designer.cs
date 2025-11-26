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
        private TextBox txtRecherche;
        private Label lblRecherche;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnRetour = new Button();
            this.lblTitre = new Label();
            this.groupBoxFormulaire = new GroupBox();
            this.lblNom = new Label();
            this.txtNom = new TextBox();
            this.lblDescription = new Label();
            this.txtDescription = new TextBox();
            this.lblPrixAchat = new Label();
            this.txtPrixAchat = new TextBox();
            this.lblPrixVente = new Label();
            this.txtPrixVente = new TextBox();
            this.lblStock = new Label();
            this.txtStock = new TextBox();
            this.lblSeuilAlerte = new Label();
            this.txtSeuilAlerte = new TextBox();
            this.lblFournisseur = new Label();
            this.comboBoxFournisseur = new ComboBox();
            this.btnAjouter = new Button();
            this.btnModifier = new Button();
            this.btnSupprimer = new Button();
            this.btnNouveau = new Button();
            this.dataGridViewMedicaments = new DataGridView();
            this.txtRecherche = new TextBox();
            this.lblRecherche = new Label();

            this.groupBoxFormulaire.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMedicaments)).BeginInit();
            this.SuspendLayout();

            // lblTitre
            this.lblTitre.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblTitre.ForeColor = Color.Orange;
            this.lblTitre.Location = new Point(300, 20);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new Size(350, 40);
            this.lblTitre.Text = "💊 Gestion des Médicaments";

            // groupBoxFormulaire
            this.groupBoxFormulaire.Controls.Add(this.lblNom);
            this.groupBoxFormulaire.Controls.Add(this.txtNom);
            this.groupBoxFormulaire.Controls.Add(this.lblDescription);
            this.groupBoxFormulaire.Controls.Add(this.txtDescription);
            this.groupBoxFormulaire.Controls.Add(this.lblPrixAchat);
            this.groupBoxFormulaire.Controls.Add(this.txtPrixAchat);
            this.groupBoxFormulaire.Controls.Add(this.lblPrixVente);
            this.groupBoxFormulaire.Controls.Add(this.txtPrixVente);
            this.groupBoxFormulaire.Controls.Add(this.lblStock);
            this.groupBoxFormulaire.Controls.Add(this.txtStock);
            this.groupBoxFormulaire.Controls.Add(this.lblSeuilAlerte);
            this.groupBoxFormulaire.Controls.Add(this.txtSeuilAlerte);
            this.groupBoxFormulaire.Controls.Add(this.lblFournisseur);
            this.groupBoxFormulaire.Controls.Add(this.comboBoxFournisseur);
            this.groupBoxFormulaire.Controls.Add(this.btnAjouter);
            this.groupBoxFormulaire.Controls.Add(this.btnModifier);
            this.groupBoxFormulaire.Controls.Add(this.btnSupprimer);
            this.groupBoxFormulaire.Controls.Add(this.btnNouveau);
            this.groupBoxFormulaire.Controls.Add(this.btnRetour);
            this.groupBoxFormulaire.Location = new Point(30, 70);
            this.groupBoxFormulaire.Name = "groupBoxFormulaire";
            this.groupBoxFormulaire.Size = new Size(350, 500);
            this.groupBoxFormulaire.Text = "Ajouter un Nouveau Médicament";

            // Labels et TextBoxes
            int startY = 30;
            int stepY = 30;

            this.lblNom.Location = new Point(20, startY);
            this.lblNom.Size = new Size(100, 20);
            this.lblNom.Text = "Nom *";

            this.txtNom.Location = new Point(130, startY);
            this.txtNom.Size = new Size(180, 20);

            this.lblDescription.Location = new Point(20, startY + stepY);
            this.lblDescription.Size = new Size(100, 20);
            this.lblDescription.Text = "Description";

            this.txtDescription.Location = new Point(130, startY + stepY);
            this.txtDescription.Size = new Size(180, 20);

            this.lblPrixAchat.Location = new Point(20, startY + stepY * 2);
            this.lblPrixAchat.Size = new Size(100, 20);
            this.lblPrixAchat.Text = "Prix Achat";

            this.txtPrixAchat.Location = new Point(130, startY + stepY * 2);
            this.txtPrixAchat.Size = new Size(180, 20);

            this.lblPrixVente.Location = new Point(20, startY + stepY * 3);
            this.lblPrixVente.Size = new Size(100, 20);
            this.lblPrixVente.Text = "Prix Vente";

            this.txtPrixVente.Location = new Point(130, startY + stepY * 3);
            this.txtPrixVente.Size = new Size(180, 20);

            this.lblStock.Location = new Point(20, startY + stepY * 4);
            this.lblStock.Size = new Size(100, 20);
            this.lblStock.Text = "Stock";

            this.txtStock.Location = new Point(130, startY + stepY * 4);
            this.txtStock.Size = new Size(180, 20);

            this.lblSeuilAlerte.Location = new Point(20, startY + stepY * 5);
            this.lblSeuilAlerte.Size = new Size(100, 20);
            this.lblSeuilAlerte.Text = "Seuil Alerte";

            this.txtSeuilAlerte.Location = new Point(130, startY + stepY * 5);
            this.txtSeuilAlerte.Size = new Size(180, 20);

            this.lblFournisseur.Location = new Point(20, startY + stepY * 6);
            this.lblFournisseur.Size = new Size(100, 20);
            this.lblFournisseur.Text = "Fournisseur *";

            this.comboBoxFournisseur.Location = new Point(130, startY + stepY * 6);
            this.comboBoxFournisseur.Size = new Size(180, 21);

            // Buttons
            this.btnAjouter.Location = new Point(30, startY + stepY * 7);
            this.btnAjouter.Size = new Size(90, 35);
            this.btnAjouter.Text = "➕ Ajouter";
            this.btnAjouter.Click += new EventHandler(this.btnAjouter_Click);

            this.btnModifier.Location = new Point(130, startY + stepY * 7);
            this.btnModifier.Size = new Size(90, 35);
            this.btnModifier.Text = "✏️ Modifier";
            this.btnModifier.Click += new EventHandler(this.btnModifier_Click);

            this.btnSupprimer.Location = new Point(230, startY + stepY * 7);
            this.btnSupprimer.Size = new Size(90, 35);
            this.btnSupprimer.Text = "❌ Supprimer";
            this.btnSupprimer.Click += new EventHandler(this.btnSupprimer_Click);

            this.btnNouveau.Location = new Point(130, startY + stepY * 8);
            this.btnNouveau.Size = new Size(90, 35);
            this.btnNouveau.Text = "🆕 Nouveau";
            this.btnNouveau.Click += new EventHandler(this.btnNouveau_Click);

            this.btnRetour.Location = new Point(130, startY + stepY * 9);
            this.btnRetour.Size = new Size(90, 35);
            this.btnRetour.Text = "⬅️ Retour";
            this.btnRetour.Click += new EventHandler(this.btnRetour_Click);

            // DataGridView
            this.dataGridViewMedicaments.Location = new Point(400, 110);
            this.dataGridViewMedicaments.Name = "dataGridViewMedicaments";
            this.dataGridViewMedicaments.Size = new Size(750, 450);
            this.dataGridViewMedicaments.RowHeadersVisible = false;
            this.dataGridViewMedicaments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewMedicaments.CellDoubleClick += new DataGridViewCellEventHandler(this.dataGridViewMedicaments_CellDoubleClick);

            // Recherche
            this.txtRecherche.Location = new Point(500, 70);
            this.txtRecherche.Size = new Size(250, 20);
            this.txtRecherche.TextChanged += new EventHandler(this.txtRecherche_TextChanged);

            this.lblRecherche.Location = new Point(400, 70);
            this.lblRecherche.Size = new Size(100, 20);
            this.lblRecherche.Text = "🔍 Rechercher:";

            // Form
            this.ClientSize = new Size(1200, 631);
            this.Controls.Add(this.lblTitre);
            this.Controls.Add(this.groupBoxFormulaire);
            this.Controls.Add(this.dataGridViewMedicaments);
            this.Controls.Add(this.txtRecherche);
            this.Controls.Add(this.lblRecherche);
            this.Name = "GestionMedicamentsForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Gestion des Médicaments";

            this.groupBoxFormulaire.ResumeLayout(false);
            this.groupBoxFormulaire.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMedicaments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
