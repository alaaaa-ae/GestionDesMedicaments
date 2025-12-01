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

                    // ✅ REQUÊTE CORRIGÉE : Récupérer plus d'informations
                    string query = @"
                        SELECT 
                            u.mot_de_passe, 
                            u.role, 
                            u.id_utilisateur,
                            u.nom,
                            u.prenom,
                            c.id_client
                        FROM Utilisateur u
                        LEFT JOIN Client c ON u.nom = c.nom AND u.prenom = c.prenom
                        WHERE u.nom_utilisateur = @user";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@user", username);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string storedHash = reader["mot_de_passe"].ToString();
                        string role = reader["role"].ToString();
                        int idUtilisateur = Convert.ToInt32(reader["id_utilisateur"]);
                        string nom = reader["nom"].ToString();
                        string prenom = reader["prenom"].ToString();

                        // ✅ Récupération de l'ID client (peut être null)
                        int idClient = 0;
                        if (reader["id_client"] != DBNull.Value)
                        {
                            idClient = Convert.ToInt32(reader["id_client"]);
                        }

                        if (Security.VerifyPassword(password, storedHash))
                        {
                            MessageBox.Show("Connexion réussie !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();

                            string nomComplet = $"{nom} {prenom}";

                            // ✅ Navigation selon le rôle avec les bons paramètres
                            switch (role.ToLower())
                            {
                                case "admin":
                                    // new DashboardAdmin(username).Show();
                                    new DashboardPharmacien().Show(); // Temporairement le même dashboard
                                    break;

                                case "pharmacien":
                                    new DashboardPharmacien().Show();
                                    break;

                                case "client":
                                    if (idClient == 0)
                                    {
                                        // ✅ Si pas d'ID client trouvé, essayer de le créer ou utiliser une valeur par défaut
                                        idClient = GetOrCreateClientId(idUtilisateur, nom, prenom);
                                    }

                                    new DashboardClient(nomComplet, idClient).Show();
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

        // ✅ MÉTHODE POUR RÉCUPÉRER OU CRÉER UN ID CLIENT
        private int GetOrCreateClientId(int idUtilisateur, string nom, string prenom)
        {
            try
            {
                using (SqlConnection con = Database.GetConnection())
                {
                    con.Open();

                    // 1. Vérifier si le client existe déjà
                    string checkQuery = @"
                        SELECT id_client 
                        FROM Client 
                        WHERE nom = @nom AND prenom = @prenom";

                    SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                    checkCmd.Parameters.AddWithValue("@nom", nom);
                    checkCmd.Parameters.AddWithValue("@prenom", prenom);

                    object result = checkCmd.ExecuteScalar();

                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }

                    // 2. Créer un nouveau client si non trouvé
                    string insertQuery = @"
                        INSERT INTO Client (nom, prenom, email, telephone)
                        OUTPUT INSERTED.id_client
                        VALUES (@nom, @prenom, @email, @telephone)";

                    SqlCommand insertCmd = new SqlCommand(insertQuery, con);
                    insertCmd.Parameters.AddWithValue("@nom", nom);
                    insertCmd.Parameters.AddWithValue("@prenom", prenom);
                    insertCmd.Parameters.AddWithValue("@email", "");
                    insertCmd.Parameters.AddWithValue("@telephone", "");

                    return (int)insertCmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la récupération/création du client : {ex.Message}");
                return 1; // Valeur par défaut temporaire
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