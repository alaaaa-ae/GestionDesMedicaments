using System;
using System.Drawing;
using System.Windows.Forms;

namespace GestionDesMedicaments.Clients
{
    partial class CommandeEditForm
    {
        private ComboBox cbClients;
        private ComboBox cbMedicaments;
        private DateTimePicker dtpDate;
        private ComboBox cbStatut;
        private Button btnOk;
        private Button btnCancel;
        private Button btnAjouterMedicament;
        private Button btnSupprimerMedicament;
        private Label lblClient;
        private Label lblDate;
        private Label lblStatut;
        private Label lblMedicaments;
        private Label lblTotal;
        private Label lblTotalValue;
        private DataGridView dgvMedicamentsCommande;
        private NumericUpDown nudQuantite;
        private Label lblQuantite;

        /// <summary>
        /// Méthode nécessaire pour le support du Designer.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbClients = new ComboBox();
            this.cbMedicaments = new ComboBox();
            this.dtpDate = new DateTimePicker();
            this.cbStatut = new ComboBox();
            this.btnOk = new Button();
            this.btnCancel = new Button();
            this.btnAjouterMedicament = new Button();
            this.btnSupprimerMedicament = new Button();
            this.lblClient = new Label();
            this.lblDate = new Label();
            this.lblStatut = new Label();
            this.lblMedicaments = new Label();
            this.lblTotal = new Label();
            this.lblTotalValue = new Label();
            this.dgvMedicamentsCommande = new DataGridView();
            this.nudQuantite = new NumericUpDown();
            this.lblQuantite = new Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvMedicamentsCommande)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantite)).BeginInit();
            this.SuspendLayout();

            // lblClient
            this.lblClient.AutoSize = true;
            this.lblClient.Location = new Point(12, 15);
            this.lblClient.Text = "Client :";
            this.lblClient.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            // cbClients
            this.cbClients.Location = new Point(80, 12);
            this.cbClients.Width = 280;
            this.cbClients.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbClients.Font = new Font("Segoe UI", 9F);

            // lblDate
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new Point(12, 45);
            this.lblDate.Text = "Date :";
            this.lblDate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            // dtpDate
            this.dtpDate.Location = new Point(80, 42);
            this.dtpDate.Format = DateTimePickerFormat.Custom;
            this.dtpDate.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtpDate.Width = 200;
            this.dtpDate.Font = new Font("Segoe UI", 9F);

            // lblStatut
            this.lblStatut.AutoSize = true;
            this.lblStatut.Location = new Point(290, 45);
            this.lblStatut.Text = "Statut :";
            this.lblStatut.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            // cbStatut
            this.cbStatut.Location = new Point(340, 42);
            this.cbStatut.Width = 140;
            this.cbStatut.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbStatut.Font = new Font("Segoe UI", 9F);

            // lblMedicaments
            this.lblMedicaments.AutoSize = true;
            this.lblMedicaments.Location = new Point(12, 75);
            this.lblMedicaments.Text = "Médicaments :";
            this.lblMedicaments.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            // cbMedicaments
            this.cbMedicaments.Location = new Point(12, 95);
            this.cbMedicaments.Width = 300;
            this.cbMedicaments.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbMedicaments.Font = new Font("Segoe UI", 9F);

            // lblQuantite
            this.lblQuantite.AutoSize = true;
            this.lblQuantite.Location = new Point(320, 98);
            this.lblQuantite.Text = "Quantité :";
            this.lblQuantite.Font = new Font("Segoe UI", 9F);

            // nudQuantite
            this.nudQuantite.Location = new Point(380, 95);
            this.nudQuantite.Width = 80;
            this.nudQuantite.Minimum = 1;
            this.nudQuantite.Maximum = 1000;
            this.nudQuantite.Value = 1;
            this.nudQuantite.Font = new Font("Segoe UI", 9F);

            // btnAjouterMedicament
            this.btnAjouterMedicament.BackColor = Color.FromArgb(46, 204, 113);
            this.btnAjouterMedicament.FlatStyle = FlatStyle.Flat;
            this.btnAjouterMedicament.FlatAppearance.BorderSize = 0;
            this.btnAjouterMedicament.ForeColor = Color.White;
            this.btnAjouterMedicament.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnAjouterMedicament.Location = new Point(470, 93);
            this.btnAjouterMedicament.Size = new Size(100, 28);
            this.btnAjouterMedicament.Text = "➕ Ajouter";
            this.btnAjouterMedicament.Cursor = Cursors.Hand;
            this.btnAjouterMedicament.Click += new EventHandler(this.BtnAjouterMedicament_Click);

            // dgvMedicamentsCommande
            this.dgvMedicamentsCommande.AllowUserToAddRows = false;
            this.dgvMedicamentsCommande.AllowUserToDeleteRows = false;
            this.dgvMedicamentsCommande.Location = new Point(12, 130);
            this.dgvMedicamentsCommande.Size = new Size(560, 200);
            this.dgvMedicamentsCommande.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvMedicamentsCommande.BackgroundColor = Color.White;
            this.dgvMedicamentsCommande.BorderStyle = BorderStyle.None;
            this.dgvMedicamentsCommande.EnableHeadersVisualStyles = false;
            this.dgvMedicamentsCommande.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            this.dgvMedicamentsCommande.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dgvMedicamentsCommande.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.dgvMedicamentsCommande.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 246, 250);
            this.dgvMedicamentsCommande.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            this.dgvMedicamentsCommande.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // btnSupprimerMedicament
            this.btnSupprimerMedicament.BackColor = Color.FromArgb(231, 76, 60);
            this.btnSupprimerMedicament.FlatStyle = FlatStyle.Flat;
            this.btnSupprimerMedicament.FlatAppearance.BorderSize = 0;
            this.btnSupprimerMedicament.ForeColor = Color.White;
            this.btnSupprimerMedicament.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnSupprimerMedicament.Location = new Point(470, 340);
            this.btnSupprimerMedicament.Size = new Size(100, 28);
            this.btnSupprimerMedicament.Text = "🗑️ Supprimer";
            this.btnSupprimerMedicament.Cursor = Cursors.Hand;
            this.btnSupprimerMedicament.Click += new EventHandler(this.BtnSupprimerMedicament_Click);

            // lblTotal
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new Point(350, 345);
            this.lblTotal.Text = "Total :";
            this.lblTotal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            // lblTotalValue
            this.lblTotalValue.AutoSize = true;
            this.lblTotalValue.Location = new Point(400, 345);
            this.lblTotalValue.Text = "0,00 €";
            this.lblTotalValue.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblTotalValue.ForeColor = Color.FromArgb(46, 204, 113);

            // btnOk
            this.btnOk.BackColor = Color.FromArgb(46, 204, 113);
            this.btnOk.FlatStyle = FlatStyle.Flat;
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.ForeColor = Color.White;
            this.btnOk.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnOk.Location = new Point(400, 380);
            this.btnOk.Size = new Size(80, 35);
            this.btnOk.Text = "✓ Enregistrer";
            this.btnOk.Cursor = Cursors.Hand;
            this.btnOk.Click += new EventHandler(this.BtnOk_Click);

            // btnCancel
            this.btnCancel.BackColor = Color.FromArgb(149, 165, 166);
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.Font = new Font("Segoe UI", 9F);
            this.btnCancel.Location = new Point(490, 380);
            this.btnCancel.Size = new Size(80, 35);
            this.btnCancel.Text = "Annuler";
            this.btnCancel.Cursor = Cursors.Hand;
            this.btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            // CommandeEditForm
            this.BackColor = Color.White;
            this.ClientSize = new Size(580, 425);
            this.Controls.Add(this.lblClient);
            this.Controls.Add(this.cbClients);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.lblStatut);
            this.Controls.Add(this.cbStatut);
            this.Controls.Add(this.lblMedicaments);
            this.Controls.Add(this.cbMedicaments);
            this.Controls.Add(this.lblQuantite);
            this.Controls.Add(this.nudQuantite);
            this.Controls.Add(this.btnAjouterMedicament);
            this.Controls.Add(this.dgvMedicamentsCommande);
            this.Controls.Add(this.btnSupprimerMedicament);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblTotalValue);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Text = "Créer / Modifier commande";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.Load += new EventHandler(this.CommandeEditForm_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvMedicamentsCommande)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantite)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}