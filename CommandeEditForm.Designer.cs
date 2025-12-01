using System.Windows.Forms;

namespace GestionDesMedicaments.Clients
{
    partial class CommandeEditForm
    {
        private ComboBox cbClients;
        private DateTimePicker dtpDate;
        private ComboBox cbStatut;
        private Button btnOk;
        private Button btnCancel;
        private Label lblClient;
        private Label lblDate;
        private Label lblStatut;

        /// <summary>
        /// Méthode nécessaire pour le support du Designer.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbClients = new ComboBox();
            this.dtpDate = new DateTimePicker();
            this.cbStatut = new ComboBox();
            this.btnOk = new Button();
            this.btnCancel = new Button();
            this.lblClient = new Label();
            this.lblDate = new Label();
            this.lblStatut = new Label();

            this.SuspendLayout();

            // lblClient
            this.lblClient.AutoSize = true;
            this.lblClient.Location = new System.Drawing.Point(12, 15);
            this.lblClient.Text = "Client";

            // cbClients
            this.cbClients.Location = new System.Drawing.Point(80, 12);
            this.cbClients.Width = 260;
            this.cbClients.DropDownStyle = ComboBoxStyle.DropDownList;

            // lblDate
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(12, 50);
            this.lblDate.Text = "Date";

            // dtpDate
            this.dtpDate.Location = new System.Drawing.Point(80, 46);
            this.dtpDate.Format = DateTimePickerFormat.Custom;
            this.dtpDate.CustomFormat = "yyyy-MM-dd HH:mm";

            // lblStatut
            this.lblStatut.AutoSize = true;
            this.lblStatut.Location = new System.Drawing.Point(12, 85);
            this.lblStatut.Text = "Statut";

            // cbStatut
            this.cbStatut.Location = new System.Drawing.Point(80, 82);
            this.cbStatut.Width = 160;
            this.cbStatut.DropDownStyle = ComboBoxStyle.DropDownList;

            // btnOk
            this.btnOk.Location = new System.Drawing.Point(160, 120);
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);

            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(240, 120);
            this.btnCancel.Text = "Annuler";
            this.btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            // CommandeEditForm
            this.ClientSize = new System.Drawing.Size(360, 160);
            this.Controls.Add(this.lblClient);
            this.Controls.Add(this.cbClients);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.lblStatut);
            this.Controls.Add(this.cbStatut);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Text = "Créer / Modifier commande";
            this.StartPosition = FormStartPosition.CenterParent;

            this.Load += new System.EventHandler(this.CommandeEditForm_Load);

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
