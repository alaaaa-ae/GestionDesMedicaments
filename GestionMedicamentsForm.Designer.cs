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
            this.dataGridViewAlertePeremption = new DataGridView();
            this.txtRecherche = new TextBox();
            this.lblRecherche = new Label();
            this.lblAlertePeremption = new Label();

            this.groupBoxFormulaire.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMedicaments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAlertePeremption)).BeginInit();
            this.SuspendLayout();

            // lblTitre
            this.lblTitre.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            this.lblTitre.ForeColor = Color.FromArgb(255, 140, 0);
            this.lblTitre.Location = new Point(400, 20);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new Size(400, 40);
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

            // DataGridView Médicaments
            this.dataGridViewMedicaments.Location = new Point(400, 110);
            this.dataGridViewMedicaments.Name = "dataGridViewMedicaments";
            this.dataGridViewMedicaments.Size = new Size(750, 300);
            this.dataGridViewMedicaments.RowHeadersVisible = false;
            this.dataGridViewMedicaments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewMedicaments.CellDoubleClick += new DataGridViewCellEventHandler(this.dataGridViewMedicaments_CellDoubleClick);
            this.dataGridViewMedicaments.EnableHeadersVisualStyles = false;
            this.dataGridViewMedicaments.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 140, 0);
            this.dataGridViewMedicaments.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dataGridViewMedicaments.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.dataGridViewMedicaments.ColumnHeadersHeight = 36;

            // DataGridView Alerte Péremption
            this.dataGridViewAlertePeremption.Location = new Point(400, 440);
            this.dataGridViewAlertePeremption.Name = "dataGridViewAlertePeremption";
            this.dataGridViewAlertePeremption.Size = new Size(750, 150);
            this.dataGridViewAlertePeremption.RowHeadersVisible = false;
            this.dataGridViewAlertePeremption.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewAlertePeremption.EnableHeadersVisualStyles = false;
            this.dataGridViewAlertePeremption.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 100, 0);
            this.dataGridViewAlertePeremption.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dataGridViewAlertePeremption.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.dataGridViewAlertePeremption.ColumnHeadersHeight = 36;

            // Label Alerte Péremption
            this.lblAlertePeremption.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblAlertePeremption.ForeColor = Color.FromArgb(255, 140, 0);
            this.lblAlertePeremption.Location = new Point(400, 410);
            this.lblAlertePeremption.Name = "lblAlertePeremption";
            this.lblAlertePeremption.Size = new Size(400, 25);
            this.lblAlertePeremption.Text = "⏰ Médicaments en Alerte Péremption (30 jours)";

            // Recherche
            this.txtRecherche.Location = new Point(500, 70);
            this.txtRecherche.Size = new Size(250, 25);
            this.txtRecherche.BorderStyle = BorderStyle.FixedSingle;
            this.txtRecherche.TextChanged += new EventHandler(this.txtRecherche_TextChanged);

            this.lblRecherche.Location = new Point(400, 72);
            this.lblRecherche.Size = new Size(100, 20);
            this.lblRecherche.Text = "🔍 Rechercher:";
            this.lblRecherche.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblRecherche.ForeColor = Color.FromArgb(255, 140, 0);

            // Appliquer thème orange aux boutons
            Color orange = Color.FromArgb(255, 140, 0);
            this.btnAjouter.BackColor = orange;
            this.btnAjouter.ForeColor = Color.White;
            this.btnAjouter.FlatStyle = FlatStyle.Flat;
            this.btnAjouter.FlatAppearance.BorderSize = 0;
            this.btnAjouter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            this.btnModifier.BackColor = Color.FromArgb(60, 160, 60);
            this.btnModifier.ForeColor = Color.White;
            this.btnModifier.FlatStyle = FlatStyle.Flat;
            this.btnModifier.FlatAppearance.BorderSize = 0;
            this.btnModifier.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            this.btnSupprimer.BackColor = Color.FromArgb(200, 50, 50);
            this.btnSupprimer.ForeColor = Color.White;
            this.btnSupprimer.FlatStyle = FlatStyle.Flat;
            this.btnSupprimer.FlatAppearance.BorderSize = 0;
            this.btnSupprimer.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            this.btnNouveau.BackColor = Color.Gray;
            this.btnNouveau.ForeColor = Color.White;
            this.btnNouveau.FlatStyle = FlatStyle.Flat;
            this.btnNouveau.FlatAppearance.BorderSize = 0;
            this.btnNouveau.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            this.btnRetour.BackColor = Color.FromArgb(100, 100, 100);
            this.btnRetour.ForeColor = Color.White;
            this.btnRetour.FlatStyle = FlatStyle.Flat;
            this.btnRetour.FlatAppearance.BorderSize = 0;
            this.btnRetour.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            // GroupBox style
            this.groupBoxFormulaire.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.groupBoxFormulaire.ForeColor = orange;

            // Form
            this.ClientSize = new Size(1200, 620);
            this.BackColor = Color.FromArgb(255, 250, 240);
            this.Controls.Add(this.lblTitre);
            this.Controls.Add(this.groupBoxFormulaire);
            this.Controls.Add(this.dataGridViewMedicaments);
            this.Controls.Add(this.lblAlertePeremption);
            this.Controls.Add(this.dataGridViewAlertePeremption);
            this.Controls.Add(this.txtRecherche);
            this.Controls.Add(this.lblRecherche);
            this.Name = "GestionMedicamentsForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "💊 Gestion des Médicaments - TaPharmacieDeRêve";

            this.groupBoxFormulaire.ResumeLayout(false);
            this.groupBoxFormulaire.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMedicaments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAlertePeremption)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
