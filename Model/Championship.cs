using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using WpfArasoi.Database;
using WpfArasoi.ViewModel;

namespace WpfArasoi.Model
{
    internal static class Championship
    {
        public static MainWindowViewModel mainWindowViewModel;
        public static void SetMainWindowViewModel(MainWindowViewModel main) { mainWindowViewModel = main; }

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

        public static ChampionshipModel[] GetChampionships()
        {
            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                List<ChampionshipModel> championships = new List<ChampionshipModel>();

                string query = "SELECT id, name FROM championship";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    championships.Add(new ChampionshipModel
                    {
                        Id = reader["id"].ToString(),
                        Name = reader["name"].ToString()
                    });
                }

                return championships.ToArray();
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


        //  LOGIC NEEDS CHANGES
        public static void CreateBrackets(string championshipId)
        {
            List<AthleteModel> athletes = Athlete.GetInscribedAthletes(championshipId);
            List<CategoryModel> categories = new List<CategoryModel>();

            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                string query = "SELECT * FROM kumiteCategories";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    categories.Add(new CategoryModel()
                    {
                        Id = reader.GetInt32("id"),
                        CategoryName = reader.GetString("category_name"),
                        AgeGroup = reader.GetString("age_group"),
                        Sex = reader.GetString("sex"),
                        MinWeight = reader.IsDBNull(reader.GetOrdinal("min_weight")) ? 0 : reader.GetDecimal("min_weight"),
                        MaxWeight = reader.IsDBNull(reader.GetOrdinal("max_weight")) ? 1000 : reader.GetDecimal("max_weight"),
                        MinBirthYear = reader.GetDateTime("min_birth_year"),
                        MaxBirthYear = reader.GetDateTime("max_birth_year")
                    });
                }
            }

            List<AthleteModel> anotherList = new List<AthleteModel>();

            // Distribuindo atletas nas categorias
            for (int i = 0; i < categories.Count; i++)
            {
                for (int j = athletes.Count - 1; j >= 0; j--)
                {
                    int birthYear = athletes[j].Birthday.Year;
                    int minYear = categories[i].MinBirthYear.Year;
                    int maxYear = categories[i].MaxBirthYear.Year;
                    
                    bool sexMatch = athletes[j].Sex == categories[i].Sex;
                    bool yearMatch = birthYear >= minYear && birthYear <= maxYear;
                    bool weightMatch = athletes[j].Weight >= categories[i].MinWeight && athletes[j].Weight <= categories[i].MaxWeight;

                    if (sexMatch && yearMatch && weightMatch)
                    {
                        athletes[j].CategoryId = categories[i].Id;
                        anotherList.Add(athletes[j]);
                        athletes.RemoveAt(j);
                    }
                }
            }


            MessageBox.Show(anotherList.Count.ToString());
            MessageBox.Show(athletes.Count.ToString());

            // Embaralha os atletas
            Random random = new Random();
            anotherList = anotherList.OrderBy(a => random.Next()).ToList();

            // Função interna para criar os brackets
            List<BracketModel> OrderAthletesByWeight(List<AthleteModel> givenAthletes)
            {
                List<BracketModel> brackets = new List<BracketModel>();
                List<AthleteModel> tempAthletes = new List<AthleteModel>(givenAthletes);

                while (tempAthletes.Count > 1)
                {
                    double bestDifferenceFrom3 = double.MaxValue;
                    int bestI = -1;
                    int bestJ = -1;

                    for (int i = 0; i < tempAthletes.Count - 1; i++)
                    {
                        for (int j = i + 1; j < tempAthletes.Count; j++)
                        {
                            if (tempAthletes[i].Cpf == tempAthletes[j].Cpf)
                                continue;

                            double weightDiff = Math.Abs(tempAthletes[i].Weight - tempAthletes[j].Weight);
                            double diffFrom3 = Math.Abs(weightDiff - 3.0);

                            if (diffFrom3 < bestDifferenceFrom3)
                            {
                                bestDifferenceFrom3 = diffFrom3;
                                bestI = i;
                                bestJ = j;
                            }
                        }
                    }

                    if (bestI != -1 && bestJ != -1)
                    {
                        brackets.Add(new BracketModel
                        {
                            CategoryId = tempAthletes[bestI].CategoryId,
                            AthleteIdAKA = tempAthletes[bestI].Id,
                            AthleteIdAO = tempAthletes[bestJ].Id
                        });

                        if (bestI > bestJ)
                        {
                            tempAthletes.RemoveAt(bestI);
                            tempAthletes.RemoveAt(bestJ);
                        }
                        else
                        {
                            tempAthletes.RemoveAt(bestJ);
                            tempAthletes.RemoveAt(bestI);
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                return brackets;
            }

            // Insere no banco
            void ToDatabase(List<BracketModel> brackets)
            {
                foreach (var item in brackets)
                {
                    using (MySqlConnection connection = ConnectionFactory.GetConnection())
                    {
                        string query = @"
                    INSERT INTO brackets 
                        (category_id, athlete_id_AKA, athlete_id_AO, score_AKA, score_AO, foul_AKA, foul_AO, championship_id)
                    VALUES 
                        (@category_id, @athlete_id_AKA, @athlete_id_AO, @score_AKA, @score_AO, @foul_AKA, @foul_AO, @championship_id)";

                        var command = new MySqlCommand(query, connection);

                        command.Parameters.AddWithValue("@category_id", item.CategoryId);
                        command.Parameters.AddWithValue("@athlete_id_AKA", item.AthleteIdAKA);
                        command.Parameters.AddWithValue("@athlete_id_AO", item.AthleteIdAO);
                        command.Parameters.AddWithValue("@score_AKA", item.ScoreAka);
                        command.Parameters.AddWithValue("@score_AO", item.ScoreAo);
                        command.Parameters.AddWithValue("@foul_AKA", item.FoulAka);
                        command.Parameters.AddWithValue("@foul_AO", item.FoulAo);
                        command.Parameters.AddWithValue("@championship_id", championshipId);

                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Chaves inseridas no banco com sucesso!");
            }

            List<BracketModel> list = OrderAthletesByWeight(anotherList);
            ToDatabase(list);
        }

    }
}
