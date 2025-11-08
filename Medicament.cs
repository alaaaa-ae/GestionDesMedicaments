using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace GestionDesMedicaments.Classes
{
    internal class Medicament
    {
        // Propriétés correspondant aux colonnes de la table Medicament
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public decimal PrixAchat { get; set; }
        public decimal PrixVente { get; set; }
        public int Stock { get; set; }
        public int SeuilAlerte { get; set; }
        public int IdFournisseur { get; set; }

        // Constructeur par défaut
        public Medicament() { }

        // Constructeur avec paramètres
        public Medicament(string nom, string description, decimal prixAchat, decimal prixVente, int stock, int seuilAlerte, int idFournisseur)
        {
            Nom = nom;
            Description = description;
            PrixAchat = prixAchat;
            PrixVente = prixVente;
            Stock = stock;
            SeuilAlerte = seuilAlerte;
            IdFournisseur = idFournisseur;
        }

        // ============================
        // MÉTHODES DE GESTION CRUD
        // ============================

        // 🔹 Ajouter un médicament
        public bool Ajouter()
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = @"INSERT INTO Medicament (nom, description, prix_achat, prix_vente, stock, seuil_alerte, id_fournisseur)
                                 VALUES (@nom, @description, @prix_achat, @prix_vente, @stock, @seuil_alerte, @id_fournisseur)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nom", Nom);
                cmd.Parameters.AddWithValue("@description", Description ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@prix_achat", PrixAchat);
                cmd.Parameters.AddWithValue("@prix_vente", PrixVente);
                cmd.Parameters.AddWithValue("@stock", Stock);
                cmd.Parameters.AddWithValue("@seuil_alerte", SeuilAlerte);
                cmd.Parameters.AddWithValue("@id_fournisseur", IdFournisseur);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }

        // 🔹 Modifier un médicament
        public bool Modifier()
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = @"UPDATE Medicament SET
                                 nom = @nom,
                                 description = @description,
                                 prix_achat = @prix_achat,
                                 prix_vente = @prix_vente,
                                 stock = @stock,
                                 seuil_alerte = @seuil_alerte,
                                 id_fournisseur = @id_fournisseur
                                 WHERE id_medicament = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nom", Nom);
                cmd.Parameters.AddWithValue("@description", Description ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@prix_achat", PrixAchat);
                cmd.Parameters.AddWithValue("@prix_vente", PrixVente);
                cmd.Parameters.AddWithValue("@stock", Stock);
                cmd.Parameters.AddWithValue("@seuil_alerte", SeuilAlerte);
                cmd.Parameters.AddWithValue("@id_fournisseur", IdFournisseur);
                cmd.Parameters.AddWithValue("@id", Id);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }

        // 🔹 Supprimer un médicament
        public static bool Supprimer(int id)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "DELETE FROM Medicament WHERE id_medicament = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }

        // 🔹 Récupérer un médicament par ID
        public static Medicament GetById(int id)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "SELECT * FROM Medicament WHERE id_medicament = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Medicament
                    {
                        Id = (int)reader["id_medicament"],
                        Nom = reader["nom"].ToString(),
                        Description = reader["description"].ToString(),
                        PrixAchat = (decimal)reader["prix_achat"],
                        PrixVente = (decimal)reader["prix_vente"],
                        Stock = (int)reader["stock"],
                        SeuilAlerte = (int)reader["seuil_alerte"],
                        IdFournisseur = (int)reader["id_fournisseur"]
                    };
                }
            }
            return null;
        }

        // 🔹 Rechercher un médicament par nom (partiel)
        public static List<Medicament> RechercherParNom(string nom)
        {
            List<Medicament> medicaments = new List<Medicament>();

            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "SELECT * FROM Medicament WHERE nom LIKE @nom";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nom", "%" + nom + "%");

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    medicaments.Add(new Medicament
                    {
                        Id = (int)reader["id_medicament"],
                        Nom = reader["nom"].ToString(),
                        Description = reader["description"].ToString(),
                        PrixAchat = (decimal)reader["prix_achat"],
                        PrixVente = (decimal)reader["prix_vente"],
                        Stock = (int)reader["stock"],
                        SeuilAlerte = (int)reader["seuil_alerte"],
                        IdFournisseur = (int)reader["id_fournisseur"]
                    });
                }
            }
            return medicaments;
        }

        // 🔹 Récupérer tous les médicaments
        public static List<Medicament> GetAll()
        {
            List<Medicament> medicaments = new List<Medicament>();

            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "SELECT * FROM Medicament";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    medicaments.Add(new Medicament
                    {
                        Id = (int)reader["id_medicament"],
                        Nom = reader["nom"].ToString(),
                        Description = reader["description"].ToString(),
                        PrixAchat = (decimal)reader["prix_achat"],
                        PrixVente = (decimal)reader["prix_vente"],
                        Stock = (int)reader["stock"],
                        SeuilAlerte = (int)reader["seuil_alerte"],
                        IdFournisseur = (int)reader["id_fournisseur"]
                    });
                }
            }
            return medicaments;
        }

        // 🔹 Vérifier si un médicament est en alerte de stock
        public bool EstEnAlerteStock()
        {
            return Stock <= SeuilAlerte;
        }
    }
}

