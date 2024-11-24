using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfArasoi.Database;
using WpfArasoi.ViewModel;

namespace WpfArasoi.Model
{
    internal class Manager
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

        // Get a list of ManagerModel objects 
        public static ObservableCollection<ManagerModel> GetManangersList(MainWindowViewModel viewModel)
        {
            ObservableCollection<ManagerModel> managerModels = new ObservableCollection<ManagerModel>();

            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                string query = "SELECT email FROM manager";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    managerModels.Add(new ManagerModel(reader["email"].ToString(), viewModel));
                }
            }

            return managerModels;
        }

        // Gets the privileges from a user
        public static string GetPrivilegeTypeFromEmail(string email) 
        {
            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                string query = "SELECT privileges_name FROM manager WHERE email = @email";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@email", email);
                MySqlDataReader reader = command.ExecuteReader();
                reader.Read();

                return reader["privileges_name"].ToString();
            }
        }
    }
}
