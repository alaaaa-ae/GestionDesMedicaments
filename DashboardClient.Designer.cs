using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GestionDesMedicaments
{
    partial class DashboardClient
    {
        private Guna.UI2.WinForms.Guna2Button btnDeconnexion;

        internal System.Windows.Forms.DataGridView DataGridViewCommandes;
        internal System.Windows.Forms.DataGridView DataGridViewCommandeDetails;
        internal Guna.UI2.WinForms.Guna2Button btnVoirCommandes;
        internal Guna.UI2.WinForms.Guna2Button btnModifierCommande;
        internal Guna.UI2.WinForms.Guna2Button btnAnnulerCommande;

        private System.ComponentModel.IContainer components = null;
        internal System.Windows.Forms.DataGridView DataGridViewMedicaments;
        internal System.Windows.Forms.DataGridView DataGridViewPanier;
        internal Guna2Button btnAjouterPanier;
        internal Guna2Button btnValiderCommande;
        internal Guna2Button btnSupprimerPanier;
        internal Guna2Button btnViderPanier;
        internal Label lblMedicaments;
        internal Label lblPanier;
        internal Label lblTitre;
        internal Label lblTotal;
        internal Guna2TextBox txtRecherche;
        internal Guna2Button btnRechercher;
        internal Button btnRafraichir;
        internal System.Windows.Forms.Label lblWelcome;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.DataGridViewCommandes = new System.Windows.Forms.DataGridView();
            this.DataGridViewCommandeDetails = new System.Windows.Forms.DataGridView();
            this.btnVoirCommandes = new Guna.UI2.WinForms.Guna2Button();
            this.btnModifierCommande = new Guna.UI2.WinForms.Guna2Button();
            this.btnAnnulerCommande = new Guna.UI2.WinForms.Guna2Button();
            this.DataGridViewMedicaments = new System.Windows.Forms.DataGridView();
            this.DataGridViewPanier = new System.Windows.Forms.DataGridView();
            this.btnAjouterPanier = new Guna.UI2.WinForms.Guna2Button();
            this.btnValiderCommande = new Guna.UI2.WinForms.Guna2Button();
            this.btnSupprimerPanier = new Guna.UI2.WinForms.Guna2Button();
            this.btnViderPanier = new Guna.UI2.WinForms.Guna2Button();
            this.lblMedicaments = new System.Windows.Forms.Label();
            this.lblPanier = new System.Windows.Forms.Label();
            this.lblTitre = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtRecherche = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnRechercher = new Guna.UI2.WinForms.Guna2Button();
            this.btnRafraichir = new System.Windows.Forms.Button();
            this.lblWelcome = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewCommandes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewCommandeDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewMedicaments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewPanier)).BeginInit();
            this.SuspendLayout();


            // 
            // DataGridViewCommandes
            // 
            this.DataGridViewCommandes.AllowUserToAddRows = false;
            this.DataGridViewCommandes.ColumnHeadersHeight = 34;
            this.DataGridViewCommandes.Location = new System.Drawing.Point(770, 70);
            this.DataGridViewCommandes.Name = "DataGridViewCommandes";
            this.DataGridViewCommandes.ReadOnly = true;
            this.DataGridViewCommandes.RowHeadersVisible = false;
            this.DataGridViewCommandes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridViewCommandes.Size = new System.Drawing.Size(500, 240);
            this.DataGridViewCommandes.TabIndex = 0;
            this.DataGridViewCommandes.SelectionChanged += new System.EventHandler(this.DataGridViewCommandes_SelectionChanged);
            // 
            // DataGridViewCommandeDetails
            // 
            this.DataGridViewCommandeDetails.AllowUserToAddRows = false;
            this.DataGridViewCommandeDetails.ColumnHeadersHeight = 34;
            this.DataGridViewCommandeDetails.Location = new System.Drawing.Point(770, 340);
            this.DataGridViewCommandeDetails.Name = "DataGridViewCommandeDetails";
            this.DataGridViewCommandeDetails.RowHeadersVisible = false;
            this.DataGridViewCommandeDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridViewCommandeDetails.Size = new System.Drawing.Size(500, 260);
            this.DataGridViewCommandeDetails.TabIndex = 1;
            // 
            // btnVoirCommandes
            // 
            this.btnVoirCommandes.BorderRadius = 8;
            this.btnVoirCommandes.FillColor = System.Drawing.Color.SteelBlue;
            this.btnVoirCommandes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnVoirCommandes.ForeColor = System.Drawing.Color.White;
            this.btnVoirCommandes.Location = new System.Drawing.Point(770, 18);
            this.btnVoirCommandes.Name = "btnVoirCommandes";
            this.btnVoirCommandes.Size = new System.Drawing.Size(160, 36);
            this.btnVoirCommandes.TabIndex = 2;
            this.btnVoirCommandes.Text = "📦 Mes commandes";
            this.btnVoirCommandes.Click += new System.EventHandler(this.btnVoirCommandes_Click);
            // 
            // btnModifierCommande
            // 
            this.btnModifierCommande.BorderRadius = 8;
            this.btnModifierCommande.FillColor = System.Drawing.Color.Orange;
            this.btnModifierCommande.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnModifierCommande.ForeColor = System.Drawing.Color.White;
            this.btnModifierCommande.Location = new System.Drawing.Point(950, 18);
            this.btnModifierCommande.Name = "btnModifierCommande";
            this.btnModifierCommande.Size = new System.Drawing.Size(160, 36);
            this.btnModifierCommande.TabIndex = 3;
            this.btnModifierCommande.Text = "✏️ Modifier la commande";
            this.btnModifierCommande.Click += new System.EventHandler(this.btnModifierCommande_Click);
            // 
            // btnAnnulerCommande
            // 
            this.btnAnnulerCommande.BorderRadius = 8;
            this.btnAnnulerCommande.FillColor = System.Drawing.Color.Red;
            this.btnAnnulerCommande.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAnnulerCommande.ForeColor = System.Drawing.Color.White;
            this.btnAnnulerCommande.Location = new System.Drawing.Point(1130, 18);
            this.btnAnnulerCommande.Name = "btnAnnulerCommande";
            this.btnAnnulerCommande.Size = new System.Drawing.Size(140, 36);
            this.btnAnnulerCommande.TabIndex = 4;
            this.btnAnnulerCommande.Text = "❌ Annuler commande";
            this.btnAnnulerCommande.Click += new System.EventHandler(this.btnAnnulerCommande_Click);
            // 
            // DataGridViewMedicaments
            // 
            this.DataGridViewMedicaments.AllowUserToAddRows = false;
            this.DataGridViewMedicaments.ColumnHeadersHeight = 34;
            this.DataGridViewMedicaments.Location = new System.Drawing.Point(30, 150);
            this.DataGridViewMedicaments.Name = "DataGridViewMedicaments";
            this.DataGridViewMedicaments.RowHeadersVisible = false;
            this.DataGridViewMedicaments.RowHeadersWidth = 62;
            this.DataGridViewMedicaments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridViewMedicaments.Size = new System.Drawing.Size(720, 200);
            this.DataGridViewMedicaments.TabIndex = 6;
            this.DataGridViewMedicaments.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewMedicaments_CellDoubleClick);
            // 
            // DataGridViewPanier
            // 
            this.DataGridViewPanier.AllowUserToAddRows = false;
            this.DataGridViewPanier.ColumnHeadersHeight = 34;
            this.DataGridViewPanier.Location = new System.Drawing.Point(30, 450);
            this.DataGridViewPanier.Name = "DataGridViewPanier";
            this.DataGridViewPanier.RowHeadersVisible = false;
            this.DataGridViewPanier.RowHeadersWidth = 62;
            this.DataGridViewPanier.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridViewPanier.Size = new System.Drawing.Size(720, 150);
            this.DataGridViewPanier.TabIndex = 7;
            this.DataGridViewPanier.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewPanier_CellContentClick);
            // 
            // btnAjouterPanier
            // 
            this.btnAjouterPanier.BorderRadius = 8;
            this.btnAjouterPanier.FillColor = System.Drawing.Color.Orange;
            this.btnAjouterPanier.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAjouterPanier.ForeColor = System.Drawing.Color.White;
            this.btnAjouterPanier.Location = new System.Drawing.Point(300, 360);
            this.btnAjouterPanier.Name = "btnAjouterPanier";
            this.btnAjouterPanier.Size = new System.Drawing.Size(180, 40);
            this.btnAjouterPanier.TabIndex = 8;
            this.btnAjouterPanier.Text = "➕ Ajouter au panier";
            this.btnAjouterPanier.Click += new System.EventHandler(this.btnAjouterPanier_Click);
            // 
            // btnValiderCommande
            // 
            this.btnValiderCommande.BorderRadius = 8;
            this.btnValiderCommande.FillColor = System.Drawing.Color.DarkOrange;
            this.btnValiderCommande.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnValiderCommande.ForeColor = System.Drawing.Color.White;
            this.btnValiderCommande.Location = new System.Drawing.Point(300, 610);
            this.btnValiderCommande.Name = "btnValiderCommande";
            this.btnValiderCommande.Size = new System.Drawing.Size(180, 45);
            this.btnValiderCommande.TabIndex = 11;
            this.btnValiderCommande.Text = "✅ Valider ma commande";
            this.btnValiderCommande.Click += new System.EventHandler(this.btnValiderCommande_Click);
            // 
            // btnSupprimerPanier
            // 
            this.btnSupprimerPanier.BorderRadius = 8;
            this.btnSupprimerPanier.FillColor = System.Drawing.Color.Red;
            this.btnSupprimerPanier.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSupprimerPanier.ForeColor = System.Drawing.Color.White;
            this.btnSupprimerPanier.Location = new System.Drawing.Point(30, 610);
            this.btnSupprimerPanier.Name = "btnSupprimerPanier";
            this.btnSupprimerPanier.Size = new System.Drawing.Size(120, 35);
            this.btnSupprimerPanier.TabIndex = 9;
            this.btnSupprimerPanier.Text = "❌ Supprimer";
            this.btnSupprimerPanier.Click += new System.EventHandler(this.btnSupprimerPanier_Click);
            // 
            // btnViderPanier
            // 
            this.btnViderPanier.BorderRadius = 8;
            this.btnViderPanier.FillColor = System.Drawing.Color.DarkRed;
            this.btnViderPanier.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnViderPanier.ForeColor = System.Drawing.Color.White;
            this.btnViderPanier.Location = new System.Drawing.Point(160, 610);
            this.btnViderPanier.Name = "btnViderPanier";
            this.btnViderPanier.Size = new System.Drawing.Size(120, 35);
            this.btnViderPanier.TabIndex = 10;
            this.btnViderPanier.Text = "🗑️ Vider panier";
            this.btnViderPanier.Click += new System.EventHandler(this.btnViderPanier_Click);
            // 
            // lblMedicaments
            // 
            this.lblMedicaments.AutoSize = true;
            this.lblMedicaments.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblMedicaments.ForeColor = System.Drawing.Color.Orange;
            this.lblMedicaments.Location = new System.Drawing.Point(30, 120);
            this.lblMedicaments.Name = "lblMedicaments";
            this.lblMedicaments.Size = new System.Drawing.Size(182, 21);
            this.lblMedicaments.TabIndex = 3;
            this.lblMedicaments.Text = "Liste des Médicaments";
            // 
            // lblPanier
            // 
            this.lblPanier.AutoSize = true;
            this.lblPanier.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblPanier.ForeColor = System.Drawing.Color.Orange;
            this.lblPanier.Location = new System.Drawing.Point(30, 420);
            this.lblPanier.Name = "lblPanier";
            this.lblPanier.Size = new System.Drawing.Size(104, 21);
            this.lblPanier.TabIndex = 4;
            this.lblPanier.Text = "Votre Panier";
            // 
            // lblTitre
            // 
            this.lblTitre.AutoSize = true;
            this.lblTitre.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitre.ForeColor = System.Drawing.Color.Orange;
            this.lblTitre.Location = new System.Drawing.Point(280, 20);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(365, 30);
            this.lblTitre.TabIndex = 0;
            this.lblTitre.Text = "🛒 Commander des Médicaments";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.Orange;
            this.lblTotal.Location = new System.Drawing.Point(500, 610);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(100, 21);
            this.lblTotal.TabIndex = 5;
            this.lblTotal.Text = "Total: 0,00 €";
            // 
            // txtRecherche
            // 
            this.txtRecherche.BorderRadius = 8;
            this.txtRecherche.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtRecherche.DefaultText = "";
            this.txtRecherche.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtRecherche.Location = new System.Drawing.Point(30, 70);
            this.txtRecherche.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtRecherche.Name = "txtRecherche";
            this.txtRecherche.PlaceholderText = "Rechercher un médicament...";
            this.txtRecherche.SelectedText = "";
            this.txtRecherche.Size = new System.Drawing.Size(250, 36);
            this.txtRecherche.TabIndex = 1;
            this.txtRecherche.TextChanged += new System.EventHandler(this.txtRecherche_TextChanged);
            // 
            // btnRechercher
            // 
            this.btnRechercher.BorderRadius = 8;
            this.btnRechercher.FillColor = System.Drawing.Color.Orange;
            this.btnRechercher.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRechercher.ForeColor = System.Drawing.Color.White;
            this.btnRechercher.Location = new System.Drawing.Point(290, 70);
            this.btnRechercher.Name = "btnRechercher";
            this.btnRechercher.Size = new System.Drawing.Size(120, 36);
            this.btnRechercher.TabIndex = 2;
            this.btnRechercher.Text = "🔎 Rechercher";
            this.btnRechercher.Click += new System.EventHandler(this.btnRechercher_Click);
            // 
            // btnRafraichir
            // 
            this.btnRafraichir.BackColor = System.Drawing.Color.Orange;
            this.btnRafraichir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRafraichir.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRafraichir.ForeColor = System.Drawing.Color.White;
            this.btnRafraichir.Location = new System.Drawing.Point(600, 70);
            this.btnRafraichir.Name = "btnRafraichir";
            this.btnRafraichir.Size = new System.Drawing.Size(100, 36);
            this.btnRafraichir.TabIndex = 12;
            this.btnRafraichir.Text = "🔄 Rafraîchir";
            this.btnRafraichir.UseVisualStyleBackColor = false;
            this.btnRafraichir.Click += new System.EventHandler(this.btnRafraichir_Click);

            this.btnDeconnexion = new Guna.UI2.WinForms.Guna2Button();
            this.btnDeconnexion.BorderRadius = 8;
            this.btnDeconnexion.FillColor = System.Drawing.Color.Gray;
            this.btnDeconnexion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDeconnexion.ForeColor = System.Drawing.Color.White;
            this.btnDeconnexion.Location = new System.Drawing.Point(650, 20); // Ajuste selon ton layout
            this.btnDeconnexion.Name = "btnDeconnexion";
            this.btnDeconnexion.Size = new System.Drawing.Size(120, 36);
            this.btnDeconnexion.TabIndex = 14;
            this.btnDeconnexion.Text = "🚪 Déconnexion";
            this.btnDeconnexion.Click += new System.EventHandler(this.btnDeconnexion_Click);
            this.Controls.Add(this.btnDeconnexion);
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.Color.Orange;
            this.lblWelcome.Location = new System.Drawing.Point(30, 20);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(0, 21);
            this.lblWelcome.TabIndex = 13;
            // 
            // DashboardClient
            // 
            this.ClientSize = new System.Drawing.Size(927, 680);
            this.Controls.Add(this.DataGridViewCommandes);
            this.Controls.Add(this.DataGridViewCommandeDetails);
            this.Controls.Add(this.btnVoirCommandes);
            this.Controls.Add(this.btnModifierCommande);
            this.Controls.Add(this.btnAnnulerCommande);
            this.Controls.Add(this.btnRafraichir);
            this.Controls.Add(this.lblTitre);
            this.Controls.Add(this.txtRecherche);
            this.Controls.Add(this.btnRechercher);
            this.Controls.Add(this.lblMedicaments);
            this.Controls.Add(this.lblPanier);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.DataGridViewMedicaments);
            this.Controls.Add(this.DataGridViewPanier);
            this.Controls.Add(this.btnAjouterPanier);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.btnSupprimerPanier);
            this.Controls.Add(this.btnViderPanier);
            this.Controls.Add(this.btnValiderCommande);
            this.Name = "DashboardClient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Commander des Médicaments";
            this.Load += new System.EventHandler(this.CommanderForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewCommandes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewCommandeDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewMedicaments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewPanier)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
