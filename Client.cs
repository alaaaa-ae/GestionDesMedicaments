using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GestionDesMedicaments.Classes
{
    public class Client
    {
        public int Id { get; set; }
        public string NomUtilisateur { get; set; }
        public string MotDePasse { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Adresse { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public DateTime DateCreation { get; set; }

        private const string TableName = "Utilisateur";

        public static List<Client> GetAll(string filter = "")
        {
            var list = new List<Client>();
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = $"SELECT * FROM {TableName} WHERE role='client'";
                if (!string.IsNullOrEmpty(filter))
                {
                    sql += " AND (nom LIKE @f OR prenom LIKE @f OR nom_utilisateur LIKE @f OR telephone LIKE @f)";
                }

                using (var cmd = new SqlCommand(sql, conn))
                {
                    if (!string.IsNullOrEmpty(filter))
                        cmd.Parameters.AddWithValue("@f", $"%{filter}%");

                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            list.Add(new Client
                            {
                                Id = Convert.ToInt32(rdr["id_utilisateur"]),
                                NomUtilisateur = rdr["nom_utilisateur"].ToString(),
                                MotDePasse = rdr["mot_de_passe"].ToString(),
                                Nom = rdr["nom"].ToString(),
                                Prenom = rdr["prenom"].ToString(),
                                Adresse = rdr["adresse"].ToString(),
                                Telephone = rdr["telephone"].ToString(),
                                Email = rdr["email"].ToString(),
                                DateCreation = Convert.ToDateTime(rdr["date_creation"])
                            });
                        }
                    }
                }
            }
            return list;
        }

        public bool Ajouter()
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = $@"
                INSERT INTO {TableName} (nom_utilisateur, mot_de_passe, nom, prenom, adresse, telephone, email, role, date_creation)
                VALUES (@nom_utilisateur, @pwd, @nom, @prenom, @adresse, @telephone, @email, 'client', GETDATE());
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nom_utilisateur", NomUtilisateur);
                    cmd.Parameters.AddWithValue("@pwd", MotDePasse);
                    cmd.Parameters.AddWithValue("@nom", Nom ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@prenom", Prenom ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@adresse", Adresse ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@telephone", Telephone ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@email", Email ?? (object)DBNull.Value);

                    var idObj = cmd.ExecuteScalar();
                    if (idObj != null) Id = Convert.ToInt32(idObj);
                    return Id > 0;
                }
            }
        }

        public bool Modifier()
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = $@"
                UPDATE {TableName} SET
                nom_utilisateur=@nom_utilisateur,
                mot_de_passe=@pwd,
                nom=@nom,
                prenom=@prenom,
                adresse=@adresse,
                telephone=@telephone,
                email=@email
                WHERE id_utilisateur=@id";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nom_utilisateur", NomUtilisateur);
                    cmd.Parameters.AddWithValue("@pwd", MotDePasse);
                    cmd.Parameters.AddWithValue("@nom", Nom ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@prenom", Prenom ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@adresse", Adresse ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@telephone", Telephone ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@email", Email ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@id", Id);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public static bool Supprimer(int id)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = $"DELETE FROM {TableName} WHERE id_utilisateur=@id";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
