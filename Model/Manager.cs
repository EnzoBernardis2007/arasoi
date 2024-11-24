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
    internal static class Manager
    {
        // Add a new manager in the database
        public static void CreateManager(string email, string password, string privilegesType)
        {
            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                string salt = Password.GenerateSalt();
                string password_hash = Password.GenerateHash(password, salt);

                // This variables is used to avoid repetition in code
                string[] parameters = { "@email", "@password_hash", "@salt", "@privileges_name" };
                string[] values = { email, password_hash, salt, privilegesType };

                string query = "INSERT INTO manager VALUES (UUID(), @email, @password_hash, @salt, @privileges_name)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        command.Parameters.AddWithValue(parameters[i], values[i]);
                    }

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
