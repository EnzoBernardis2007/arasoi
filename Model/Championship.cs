using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfArasoi.Database;
using WpfArasoi.Prefab;
using WpfArasoi.ViewModel;

namespace WpfArasoi.Model
{
    internal static class Championship
    {
        public static MainWindowViewModel mainWindowViewModel;
        public static void SetMainWindowViewModel(MainWindowViewModel main) { mainWindowViewModel =  main; }

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

        public static ObservableCollection<ChampionshipModel> GetChampionshipInfoList(MainWindowViewModel viewModel)
        {
            ObservableCollection<ChampionshipModel> championshipModels = new ObservableCollection<ChampionshipModel>();

            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {

                string query = @"
                    SELECT 
                        c.id AS championship_id,
                        c.name AS championship_name,
                        c.begin AS championship_begin,
                        c.end AS championship_end,
                        c.author AS championship_author,
                        COUNT(i.id) AS inscription_count
                    FROM 
                        championship c
                    LEFT JOIN 
                        inscription i
                    ON 
                        c.id = i.championship_id
                    GROUP BY 
                        c.id, c.name, c.begin, c.end, c.author;
                    ";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Cria um objeto ChampionshipModel com os dados retornados
                        championshipModels.Add(new ChampionshipModel
                        {
                            Id = reader["championship_id"].ToString(),
                            Name = reader["championship_name"].ToString(),
                            DateBegin = Convert.ToDateTime(reader["championship_begin"]),
                            DateEnd = Convert.ToDateTime(reader["championship_end"]),
                            Author = reader["championship_author"].ToString(),
                            InscriptionCount = Convert.ToInt32(reader["inscription_count"]),
                            ViewModel = viewModel
                        });
                    }
                }
            }

            return championshipModels;
        }

        public static ChampionshipModel GetChampionship(string id)
        {
            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                string query = "SELECT * FROM championship WHERE id = @id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new ChampionshipModel
                    {
                        Id = reader["id"].ToString(),
                        Name = reader["name"].ToString(),
                        Description = reader["description"]?.ToString(),
                        DateBegin = DateTime.Parse(reader["begin"].ToString()),
                        DateEnd = DateTime.Parse(reader["end"].ToString()),
                        Author = reader["author"].ToString()
                    };
                }

                return null;
            }
        }

        public static void UpdateChampionship(ChampionshipModel championship)
        {
            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                string query = @"
                    UPDATE championship
                    SET 
                        name = @name, 
                        begin = @begin, 
                        end = @end, 
                        description = @description
                    WHERE 
                        id = @id;
                ";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", championship.Id);
                command.Parameters.AddWithValue("@name", championship.Name);
                command.Parameters.AddWithValue("@description", championship.Description);
                command.Parameters.AddWithValue("@begin", championship.DateBegin);
                command.Parameters.AddWithValue("@end", championship.DateEnd);
                command.ExecuteNonQuery();
                mainWindowViewModel.LoadChampionshipsList();
            }
        }
    }
}
