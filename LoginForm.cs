using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
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

            if (username == "" || password == "")
            {
                MessageBox.Show("Veuillez remplir tous les champs !");
                return;
            }

            string hashed = Security.HashPassword(password);

            try
            {
                using (SqlConnection con = Database.GetConnection())
                {
                    con.Open();
                    string query = "SELECT COUNT(*) FROM Utilisateur WHERE nom_utilisateur=@user AND mot_de_passe=@pass";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@pass", hashed);

                    int count = (int)cmd.ExecuteScalar();

                    if (count == 1)
                    {
                        MessageBox.Show("Connexion réussie !");
                        // Ouvre le tableau de bord ici
                        // Exemple : new Dashboard().Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect !");
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
    }
}
