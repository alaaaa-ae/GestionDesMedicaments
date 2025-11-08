using Guna.UI2.WinForms;
using System.Windows.Forms;

namespace GestionDesMedicaments
{
    partial class LoginForm
    {
        private Guna2TextBox txtEmail;
        private Guna2TextBox txtPassword;
        private Guna2Button btnLogin;
        private Guna2HtmlLabel lblTitle;
        private Guna2HtmlLabel lblCreateAccount;
        private Guna2ShadowForm guna2ShadowForm1;

        private void InitializeComponent()
        {
            this.txtEmail = new Guna2TextBox();
            this.txtPassword = new Guna2TextBox();
            this.btnLogin = new Guna2Button();
            this.lblTitle = new Guna2HtmlLabel();
            this.lblCreateAccount = new Guna2HtmlLabel();
            this.guna2ShadowForm1 = new Guna2ShadowForm();

            this.SuspendLayout();

            // Form
            this.Text = "Authentification";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(380, 420);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Title
            this.lblTitle.Text = "Connexion";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(255, 128, 0);
            this.lblTitle.Location = new System.Drawing.Point(120, 40);

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

            // Login button
            this.btnLogin.Text = "Se connecter";
            this.btnLogin.FillColor = System.Drawing.Color.FromArgb(255, 128, 0);
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.BorderRadius = 8;
            this.btnLogin.Location = new System.Drawing.Point(60, 250);
            this.btnLogin.Size = new System.Drawing.Size(260, 45);
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);

            // Create account link
            this.lblCreateAccount.Text = "Créer un compte";
            this.lblCreateAccount.ForeColor = System.Drawing.Color.Gray;
            this.lblCreateAccount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCreateAccount.Location = new System.Drawing.Point(140, 320);
            this.lblCreateAccount.Click += new System.EventHandler(this.lblCreateAccount_Click);

            // Add controls
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.lblCreateAccount);

            this.guna2ShadowForm1.SetShadowForm(this);

            this.ResumeLayout(false);
        }
    }
}
