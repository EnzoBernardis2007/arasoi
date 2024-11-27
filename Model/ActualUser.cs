using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfArasoi.Database;

namespace WpfArasoi.Model
{
    internal static class ActualUser
    {
        public static string Id { get; set; }
        public static string Email { get; set; }

        public static void SetIdWithEmail(string email)
        {
            using (MySqlConnection connection = ConnectionFactory.GetConnection()) 
            {
                string query = "SELECT id FROM manager WHERE email = @email";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@email", email);
                MySqlDataReader reader = command.ExecuteReader();

                reader.Read();

                Id = reader["id"].ToString();
            }
        }

        public static void SetEmail(string email)
        {
            Email = email;
        }
    }
}
