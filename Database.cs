using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace GestionDesMedicaments.Classes
{
    public class Database
    {
        private static string connectionString =
            "Data Source=localhost;Initial Catalog=PharmacieDB;Integrated Security=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        /// <summary>
        /// Retourne la première colonne existante parmi une liste de colonnes candidates pour la table spécifiée.
        /// Permet d'être compatible avec des schémas différents (ex: id_client vs id_utilisateur).
        /// </summary>
        public static string GetExistingColumn(SqlConnection connection, string tableName, params string[] candidateColumns)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            if (string.IsNullOrWhiteSpace(tableName)) throw new ArgumentException("TableName requis", nameof(tableName));
            if (candidateColumns == null || candidateColumns.Length == 0)
                throw new ArgumentException("Fournir au moins une colonne candidate.", nameof(candidateColumns));

            bool closeConnection = false;
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
                closeConnection = true;
            }

            try
            {
                HashSet<string> existingColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                using (var cmd = new SqlCommand("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @table", connection))
                {
                    cmd.Parameters.AddWithValue("@table", tableName);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            existingColumns.Add(reader.GetString(0));
                        }
                    }
                }

                foreach (var candidate in candidateColumns)
                {
                    if (existingColumns.Contains(candidate))
                        return candidate;
                }

                throw new InvalidOperationException($"Impossible de trouver une des colonnes [{string.Join(", ", candidateColumns)}] dans la table {tableName}.");
            }
            finally
            {
                if (closeConnection)
                    connection.Close();
            }
        }
    }

}