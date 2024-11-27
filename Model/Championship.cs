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
    internal static class Championship
    {
        public static void CreateChampionship(ChampionshipModel championship)
        {
            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                string query = "INSERT INTO championship VALUES (UUID(), @name, @description, @begin, @end, @author, false)";

                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@name", championship.Name);
                command.Parameters.AddWithValue("@description", championship.Description);
                command.Parameters.AddWithValue("@begin", championship.DateBegin);
                command.Parameters.AddWithValue("@end", championship.DateEnd);
                command.Parameters.AddWithValue("@author", ActualUser.Id);

                command.ExecuteNonQuery();
            }
        }

        public static ObservableCollection<ChampionshipModel> GetChampionshipList()
        {
            ObservableCollection<ChampionshipModel> championshipModels = new ObservableCollection<ChampionshipModel>();

            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                string query = "SELECT name FROM championship";

                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    championshipModels.Add(new ChampionshipModel(reader["name"].ToString()));
                }
            }

            return championshipModels;
        }
    }
}
