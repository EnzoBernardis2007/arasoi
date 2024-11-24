using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfArasoi.Database;

namespace WpfArasoi.Model
{
    internal static class Privileges
    {
        public static string[] GetPrivilegesId()
        {
            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                string query = "SELECT name FROM privileges";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                List<string> names = new List<string>();

                while (reader.Read()) 
                {
                    names.Add(reader["name"].ToString());
                }
                
                return names.ToArray();
            }
        } 
    }
}
