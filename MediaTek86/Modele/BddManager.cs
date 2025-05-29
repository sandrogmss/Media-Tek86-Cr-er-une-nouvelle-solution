using System;
using MySql.Data.MySqlClient;

namespace MediaTek86.Modele
{
    public class BddManager
    {
        private static MySqlConnection connexion;

        // Chaîne de connexion
        private static readonly string connectionString = "server=localhost;user=root;database=mediatek86;password=;";

        public static MySqlConnection GetConnection()
        {
            if (connexion == null)
            {
                connexion = new MySqlConnection(connectionString);
            }
            return connexion;
        }

        public static void OpenConnection()
        {
            if (connexion == null)
            {
                GetConnection();
            }

            if (connexion.State == System.Data.ConnectionState.Closed)
                connexion.Open();
        }

        public static void CloseConnection()
        {
            if (connexion != null && connexion.State == System.Data.ConnectionState.Open)
                connexion.Close();
        }
    }
}
