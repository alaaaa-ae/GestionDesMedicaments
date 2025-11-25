using System;
using System.Data.SqlClient;
using System.Windows.Forms;
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

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            cmbRole.Items.Add("client");
            cmbRole.Items.Add("pharmacien");
            cmbRole.SelectedIndex = 0; // rôle par défaut
            ToggleClientFields();
        }

        private void cmbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToggleClientFields();
        }

        private void ToggleClientFields()
        {
            bool isClient = cmbRole.SelectedItem.ToString() == "client";

            
            txtEmailClient.Enabled = isClient;
           
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string confirm = txtConfirm.Text;
            string role = cmbRole.SelectedItem.ToString();

       
            string email = txtEmailClient.Text.Trim();
      

            // Vérification champs obligatoires
            if (username == "" || password == "" || confirm == "")
            {
                MessageBox.Show("Veuillez remplir tous les champs obligatoires !");
                return;
            }

            if (password != confirm)
            {
                MessageBox.Show("Les mots de passe ne correspondent pas !");
                return;
            }

            if (role == "client" && (username == ""  || email == ""))
            {
                MessageBox.Show("Veuillez remplir les informations personnelles du client !");
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

                    // Insertion selon rôle
                    string insertQuery = role == "client"
                        ? @"INSERT INTO Utilisateur 
                            (nom_utilisateur, mot_de_passe, role, email) 
                            VALUES (@user, @pass, 'client', @email)"
                        : @"INSERT INTO Utilisateur 
                            (nom_utilisateur, mot_de_passe, role) 
                            VALUES (@user, @pass, 'pharmacien')";

                    SqlCommand cmd = new SqlCommand(insertQuery, con);
                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@pass", hashed);

                    if (role == "client")
                    {
                       cmd.Parameters.AddWithValue("@nom_utilisateur", username);
                        cmd.Parameters.AddWithValue("@email", email);
                       
                    }

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

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
