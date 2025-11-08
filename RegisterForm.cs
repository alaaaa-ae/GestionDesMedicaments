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
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtEmail.Text.Trim();
            string password = txtPassword.Text;
            string confirm = txtConfirm.Text;

            if (username == "" || password == "")
            {
                MessageBox.Show("Veuillez remplir tous les champs !");
                return;
            }

            if (password != confirm)
            {
                MessageBox.Show("Les mots de passe ne correspondent pas !");
                return;
            }

            string hashed = Security.HashPassword(password);

            try
            {
                using (SqlConnection con = Database.GetConnection())
                {
                    con.Open();

                    // Vérifier si l'utilisateur existe déjà
                    string checkQuery = "SELECT COUNT(*) FROM Utilisateur WHERE nom_utilisateur=@user";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                    checkCmd.Parameters.AddWithValue("@user", username);
                    int exists = (int)checkCmd.ExecuteScalar();

                    if (exists > 0)
                    {
                        MessageBox.Show("Nom d'utilisateur déjà pris !");
                        return;
                    }

                    // Insérer l'utilisateur
                    string insertQuery = "INSERT INTO Utilisateur (nom_utilisateur, mot_de_passe, role) VALUES (@user, @pass, 'pharmacien')";
                    SqlCommand cmd = new SqlCommand(insertQuery, con);
                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@pass", hashed);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Compte créé avec succès !");
                    new LoginForm().Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }

        private void lblLogin_Click(object sender, EventArgs e)
        {
            new LoginForm().Show();
            this.Hide();
        }
    }
}
