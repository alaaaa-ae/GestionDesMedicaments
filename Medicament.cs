using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GestionDesMedicaments.Classes;

namespace GestionDesMedicaments
{
    internal class Medicament
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public decimal PrixAchat { get; set; }
        public decimal PrixVente { get; set; }
        public int Stock { get; set; }
        public int SeuilAlerte { get; set; }
        public int IdFournisseur { get; set; }

        public Medicament() { }

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

        public bool Ajouter()
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = @"INSERT INTO Medicament 
                                (nom, description, prix_achat, prix_vente, stock, seuil_alerte, id_fournisseur)
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
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Modifier()
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = @"UPDATE Medicament SET
                                 nom=@nom, description=@description, prix_achat=@prix_achat,
                                 prix_vente=@prix_vente, stock=@stock, seuil_alerte=@seuil_alerte,
                                 id_fournisseur=@id_fournisseur WHERE id_medicament=@id";
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
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public static bool Supprimer(int id)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "DELETE FROM Medicament WHERE id_medicament=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public static Medicament GetById(int id)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "SELECT * FROM Medicament WHERE id_medicament=@id";
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
                return null;
            }
        }

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

        public bool EstEnAlerteStock()
        {
            return Stock <= SeuilAlerte;
        }
    }
}
