using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace GestionDesMedicaments.Classes
{
    internal class Database
    {
        private static string connectionString =
            "Data Source=Alae\\GI2;Initial Catalog=PharmacieDB;Initial Catalog=PharmacieDB;Integrated Security=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}

