using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GestionDesMedicaments
{
    partial class DashboardClient : Form
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView DataGridViewMedicaments;
        private System.Windows.Forms.DataGridView DataGridViewPanier;
        private Guna2Button btnAjouterPanier;
        private Guna2Button btnValiderCommande;
        private Guna2Button btnSupprimerPanier;
        private Guna2Button btnViderPanier;
        private Label lblMedicaments;
        private Label lblPanier;
        private Label lblTitre;
        private Label lblTotal;
        private Guna2TextBox txtRecherche;
        private Guna2Button btnRechercher;
        private Button btnRafraichir;
        private System.Windows.Forms.Label lblWelcome;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
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

            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewMedicaments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewPanier)).BeginInit();
            this.SuspendLayout();

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
            this.lblMedicaments.Size = new System.Drawing.Size(272, 32);
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
            this.lblPanier.Size = new System.Drawing.Size(154, 32);
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
            this.lblTitre.Size = new System.Drawing.Size(527, 45);
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
            this.lblTotal.Size = new System.Drawing.Size(154, 32);
            this.lblTotal.TabIndex = 5;
            this.lblTotal.Text = "Total: 0,00 €";

            // lblWelcome
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.Color.Orange;
            this.lblWelcome.Location = new System.Drawing.Point(30, 20);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(0, 32);
            this.lblWelcome.TabIndex = 13;

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

            // 
            // DashboardClient
            // 
            this.ClientSize = new System.Drawing.Size(800, 680);
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
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewMedicaments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewPanier)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}