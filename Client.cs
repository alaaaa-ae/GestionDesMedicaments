using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GestionDesMedicaments.Classes
{
    public class Client
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Adresse { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public DateTime DateCreation { get; set; }

        public static Client GetById(int id)
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                string query = "SELECT * FROM Client WHERE id_client = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Client
                    {
                        Id = (int)reader["id_client"],
                        Nom = reader["nom"].ToString(),
                        Prenom = reader["prenom"].ToString(),
                        Adresse = reader["adresse"].ToString(),
                        Telephone = reader["telephone"].ToString(),
                        Email = reader["email"].ToString(),
                        DateCreation = (DateTime)reader["date_creation"]
                    };
                }
            }
            return null;
        }
    }
}