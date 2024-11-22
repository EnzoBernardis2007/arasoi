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

                string[] names = new string[reader.FieldCount];

                for(int i = 0; reader.Read(); i++)
                {
                    names[i] = reader["name"].ToString();
                }
                
                return names;
            }
        } 
    }
}
