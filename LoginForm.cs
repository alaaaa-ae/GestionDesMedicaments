using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using GestionDesMedicaments.Classes;
using Guna.UI2.WinForms;

namespace GestionDesMedicaments
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Veuillez remplir tous les champs !");
                return;
            }

            try
            {
                using (SqlConnection con = Database.GetConnection())
                {
                    con.Open();
                    string query = "SELECT mot_de_passe, role FROM Utilisateur WHERE nom_utilisateur=@user";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@user", username);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string storedHash = reader["mot_de_passe"].ToString();
                        string role = reader["role"].ToString();

                        if (Security.VerifyPassword(password, storedHash))
                        {
                            MessageBox.Show("Connexion réussie !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();

                            // ✅ Passer le nom d'utilisateur connecté au Dashboard correspondant
                            switch (role.ToLower())
                            {
                                case "admin":
                                    // new DashboardAdmin(username).Show();
                                    break;

                                case "pharmacien":
                                    new DashboardPharmacien().Show();
                                    break;

                                case "client":
                                    new DashboardClient(username).Show();
                                    break;

                                default:
                                    MessageBox.Show("Rôle inconnu !");
                                    break;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Mot de passe incorrect !");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nom d'utilisateur introuvable !");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }

        private void lblCreateAccount_Click(object sender, EventArgs e)
        {
            new RegisterForm().Show();
            this.Hide();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
