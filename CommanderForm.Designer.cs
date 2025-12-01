using Guna.UI2.WinForms;

namespace GestionDesMedicaments
{
    partial class CommanderForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridViewMedicaments;
        private System.Windows.Forms.DataGridView dataGridViewPanier;
        private System.Windows.Forms.Button btnAjouterPanier;
        private System.Windows.Forms.Button btnValiderCommande;
        private System.Windows.Forms.Button btnSupprimerPanier;
        private System.Windows.Forms.Button btnViderPanier;
        private System.Windows.Forms.Label lblMedicaments;
        private System.Windows.Forms.Label lblPanier;
        private System.Windows.Forms.Label lblTitre;
        private System.Windows.Forms.Label lblTotal;
        private Guna2TextBox txtRecherche;
        private System.Windows.Forms.Button btnRechercher;

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
            this.dataGridViewMedicaments = new System.Windows.Forms.DataGridView();
            this.dataGridViewPanier = new System.Windows.Forms.DataGridView();
            this.btnAjouterPanier = new System.Windows.Forms.Button();
            this.btnValiderCommande = new System.Windows.Forms.Button();
            this.btnSupprimerPanier = new System.Windows.Forms.Button();
            this.btnViderPanier = new System.Windows.Forms.Button();
            this.lblMedicaments = new System.Windows.Forms.Label();
            this.lblPanier = new System.Windows.Forms.Label();
            this.lblTitre = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtRecherche = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnRechercher = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMedicaments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPanier)).BeginInit();
            this.SuspendLayout();

            // 🧾 lblTitre
            this.lblTitre.AutoSize = true;
            this.lblTitre.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitre.ForeColor = System.Drawing.Color.Orange;
            this.lblTitre.Location = new System.Drawing.Point(280, 20);
            this.lblTitre.Size = new System.Drawing.Size(320, 30);
            this.lblTitre.Text = "🛒 Commander des Médicaments";

            // 🔍 txtRecherche
            this.txtRecherche.Location = new System.Drawing.Point(30, 70);
            this.txtRecherche.Size = new System.Drawing.Size(250, 23);
            this.txtRecherche.PlaceholderText = "Rechercher un médicament...";

            // 🔎 btnRechercher
            this.btnRechercher.Text = "🔎 Rechercher";
            this.btnRechercher.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRechercher.BackColor = System.Drawing.Color.Orange;
            this.btnRechercher.ForeColor = System.Drawing.Color.White;
            this.btnRechercher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRechercher.Location = new System.Drawing.Point(290, 70);
            this.btnRechercher.Size = new System.Drawing.Size(120, 25);
            this.btnRechercher.Click += new System.EventHandler(this.btnRechercher_Click);

            // 💊 lblMedicaments
            this.lblMedicaments.AutoSize = true;
            this.lblMedicaments.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblMedicaments.ForeColor = System.Drawing.Color.Orange;
            this.lblMedicaments.Location = new System.Drawing.Point(30, 110);
            this.lblMedicaments.Size = new System.Drawing.Size(180, 21);
            this.lblMedicaments.Text = "Liste des Médicaments";

            // 🧮 dataGridViewMedicaments
            this.dataGridViewMedicaments.Location = new System.Drawing.Point(30, 140);
            this.dataGridViewMedicaments.Size = new System.Drawing.Size(720, 200);
            this.dataGridViewMedicaments.RowHeadersVisible = false;
            this.dataGridViewMedicaments.AllowUserToAddRows = false;
            this.dataGridViewMedicaments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewMedicaments.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMedicaments_CellEndEdit);

            // ➕ btnAjouterPanier
            this.btnAjouterPanier.Text = "➕ Ajouter au panier";
            this.btnAjouterPanier.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAjouterPanier.BackColor = System.Drawing.Color.Orange;
            this.btnAjouterPanier.ForeColor = System.Drawing.Color.White;
            this.btnAjouterPanier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAjouterPanier.Location = new System.Drawing.Point(300, 350);
            this.btnAjouterPanier.Size = new System.Drawing.Size(180, 40);
            this.btnAjouterPanier.Click += new System.EventHandler(this.btnAjouterPanier_Click);

            // 🧺 lblPanier
            this.lblPanier.AutoSize = true;
            this.lblPanier.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblPanier.ForeColor = System.Drawing.Color.Orange;
            this.lblPanier.Location = new System.Drawing.Point(30, 410);
            this.lblPanier.Size = new System.Drawing.Size(110, 21);
            this.lblPanier.Text = "Votre Panier";

            // 🧺 dataGridViewPanier
            this.dataGridViewPanier.Location = new System.Drawing.Point(30, 440);
            this.dataGridViewPanier.Size = new System.Drawing.Size(720, 150);
            this.dataGridViewPanier.RowHeadersVisible = false;
            this.dataGridViewPanier.AllowUserToAddRows = false;
            this.dataGridViewPanier.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewPanier.ReadOnly = true;

            // ❌ btnSupprimerPanier
            this.btnSupprimerPanier.Text = "❌ Supprimer";
            this.btnSupprimerPanier.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSupprimerPanier.BackColor = System.Drawing.Color.Red;
            this.btnSupprimerPanier.ForeColor = System.Drawing.Color.White;
            this.btnSupprimerPanier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSupprimerPanier.Location = new System.Drawing.Point(30, 600);
            this.btnSupprimerPanier.Size = new System.Drawing.Size(120, 35);
            this.btnSupprimerPanier.Click += new System.EventHandler(this.btnSupprimerPanier_Click);

            // 🗑️ btnViderPanier
            this.btnViderPanier.Text = "🗑️ Vider panier";
            this.btnViderPanier.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnViderPanier.BackColor = System.Drawing.Color.DarkRed;
            this.btnViderPanier.ForeColor = System.Drawing.Color.White;
            this.btnViderPanier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViderPanier.Location = new System.Drawing.Point(160, 600);
            this.btnViderPanier.Size = new System.Drawing.Size(120, 35);
            this.btnViderPanier.Click += new System.EventHandler(this.btnViderPanier_Click);

            // 💰 lblTotal
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.Orange;
            this.lblTotal.Location = new System.Drawing.Point(500, 600);
            this.lblTotal.Size = new System.Drawing.Size(100, 21);
            this.lblTotal.Text = "Total: 0,00 €";

            // ✅ btnValiderCommande
            this.btnValiderCommande.Text = "✅ Valider ma commande";
            this.btnValiderCommande.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnValiderCommande.BackColor = System.Drawing.Color.DarkOrange;
            this.btnValiderCommande.ForeColor = System.Drawing.Color.White;
            this.btnValiderCommande.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnValiderCommande.Location = new System.Drawing.Point(300, 600);
            this.btnValiderCommande.Size = new System.Drawing.Size(180, 45);
            this.btnValiderCommande.Click += new System.EventHandler(this.btnValiderCommande_Click);

            // ⚙️ Form
            this.ClientSize = new System.Drawing.Size(800, 680);
            this.Controls.Add(this.lblTitre);
            this.Controls.Add(this.txtRecherche);
            this.Controls.Add(this.btnRechercher);
            this.Controls.Add(this.lblMedicaments);
            this.Controls.Add(this.lblPanier);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.dataGridViewMedicaments);
            this.Controls.Add(this.dataGridViewPanier);
            this.Controls.Add(this.btnAjouterPanier);
            this.Controls.Add(this.btnSupprimerPanier);
            this.Controls.Add(this.btnViderPanier);
            this.Controls.Add(this.btnValiderCommande);
            this.Name = "CommanderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Commander des Médicaments";
            this.Load += new System.EventHandler(this.CommanderForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMedicaments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPanier)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}