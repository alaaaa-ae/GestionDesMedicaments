using Guna.UI2.WinForms;
using System.Windows.Forms;

namespace GestionDesMedicaments
{
    partial class RegisterForm
    {
        private Guna2TextBox txtEmail;
        private Guna2TextBox txtPassword;
        private Guna2TextBox txtConfirm;
        private Guna2Button btnRegister;
        private Guna2HtmlLabel lblTitle;
        private Guna2HtmlLabel lblLogin;
        private Guna2ShadowForm guna2ShadowForm1;

        private void InitializeComponent()
        {
            this.txtEmail = new Guna2TextBox();
            this.txtPassword = new Guna2TextBox();
            this.txtConfirm = new Guna2TextBox();
            this.btnRegister = new Guna2Button();
            this.lblTitle = new Guna2HtmlLabel();
            this.lblLogin = new Guna2HtmlLabel();
            this.guna2ShadowForm1 = new Guna2ShadowForm();

            this.SuspendLayout();

            // Form
            this.Text = "Inscription";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(380, 460);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Title
            this.lblTitle.Text = "Créer un compte";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(255, 128, 0);
            this.lblTitle.Location = new System.Drawing.Point(80, 40);

            // Email
            this.txtEmail.PlaceholderText = "Email";
            this.txtEmail.BorderRadius = 8;
            this.txtEmail.Location = new System.Drawing.Point(60, 120);
            this.txtEmail.Size = new System.Drawing.Size(260, 40);

            // Password
            this.txtPassword.PlaceholderText = "Mot de passe";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.BorderRadius = 8;
            this.txtPassword.Location = new System.Drawing.Point(60, 180);
            this.txtPassword.Size = new System.Drawing.Size(260, 40);

            // Confirm Password
            this.txtConfirm.PlaceholderText = "Confirmer le mot de passe";
            this.txtConfirm.PasswordChar = '●';
            this.txtConfirm.BorderRadius = 8;
            this.txtConfirm.Location = new System.Drawing.Point(60, 240);
            this.txtConfirm.Size = new System.Drawing.Size(260, 40);

            // Register Button
            this.btnRegister.Text = "S'inscrire";
            this.btnRegister.FillColor = System.Drawing.Color.FromArgb(255, 128, 0);
            this.btnRegister.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRegister.ForeColor = System.Drawing.Color.White;
            this.btnRegister.BorderRadius = 8;
            this.btnRegister.Location = new System.Drawing.Point(60, 310);
            this.btnRegister.Size = new System.Drawing.Size(260, 45);
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);

            // Login link
            this.lblLogin.Text = "Déjà un compte ? Se connecter";
            this.lblLogin.ForeColor = System.Drawing.Color.Gray;
            this.lblLogin.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLogin.Location = new System.Drawing.Point(110, 380);
            this.lblLogin.Click += new System.EventHandler(this.lblLogin_Click);

            // Add controls
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtConfirm);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.lblLogin);

            this.guna2ShadowForm1.SetShadowForm(this);

            this.ResumeLayout(false);
        }
    }
}
