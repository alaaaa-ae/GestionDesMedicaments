using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GestionDesMedicaments.Classes
{
    public class Fournisseur
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Contact { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }

        public static List<Fournisseur> GetAll()
        {
            List<Fournisseur> fournisseurs = new List<Fournisseur>();

            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "SELECT * FROM Fournisseur ORDER BY nom";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    fournisseurs.Add(new Fournisseur
                    {
                        Id = (int)reader["id_fournisseur"],
                        Nom = reader["nom"].ToString(),
                        Contact = reader["contact"]?.ToString(),
                        Telephone = reader["telephone"]?.ToString(),
                        Email = reader["email"]?.ToString()
                    });
                }
            }
            return fournisseurs;
        }
    }
}