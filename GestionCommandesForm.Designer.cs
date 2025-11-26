using System.Windows.Forms;

namespace GestionDesMedicaments
{
    partial class GestionCommandesForm
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView DataGridViewCommandes;
        private DataGridView DataGridViewDetails;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.DataGridViewCommandes = new DataGridView();
            this.DataGridViewDetails = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewCommandes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewDetails)).BeginInit();
            this.SuspendLayout();

            // 
            // DataGridViewCommandes
            // 
            this.DataGridViewCommandes.AllowUserToAddRows = false;
            this.DataGridViewCommandes.AllowUserToDeleteRows = false;
            this.DataGridViewCommandes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.DataGridViewCommandes.Location = new System.Drawing.Point(20, 20);
            this.DataGridViewCommandes.Size = new System.Drawing.Size(600, 250);
            this.DataGridViewCommandes.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewCommandes_CellValueChanged);
            this.DataGridViewCommandes.SelectionChanged += new System.EventHandler(this.DataGridViewCommandes_SelectionChanged);

            // 
            // DataGridViewDetails
            // 
            this.DataGridViewDetails.AllowUserToAddRows = false;
            this.DataGridViewDetails.AllowUserToDeleteRows = false;
            this.DataGridViewDetails.Location = new System.Drawing.Point(20, 280);
            this.DataGridViewDetails.Size = new System.Drawing.Size(600, 150);

            // 
            // GestionCommandesForm
            // 
            this.ClientSize = new System.Drawing.Size(650, 450);
            this.Controls.Add(this.DataGridViewCommandes);
            this.Controls.Add(this.DataGridViewDetails);
            this.Text = "Gestion des commandes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewCommandes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewDetails)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
