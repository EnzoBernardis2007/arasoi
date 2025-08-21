using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using WpfArasoi.Database;

namespace WpfArasoi.Model
{
    class Bracket
    {
        public static BracketModel[] GetBracketModels(string championshipId)
        {
            List<BracketModel> brackets = new List<BracketModel>();

            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand("GetBracketsDetailsByChampionship", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_championship_id", championshipId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BracketModel bracket = new BracketModel
                            {
                                Id = reader.GetInt32("bracket_id"),
                                CategoryId = reader.GetInt32("category_id"),
                                AthleteNameAKA = reader.GetString("athlete_aka_name"),
                                AthleteNameAO = reader.GetString("athlete_ao_name"),
                                AthleteIdAKA = reader.GetString("athlete_aka_cpf"),
                                AthleteIdAO = reader.GetString("athlete_ao_cpf"),
                                ScoreAka = 0,
                                ScoreAo = 0,
                                FoulAka = 0,
                                FoulAo = 0
                            };

                            brackets.Add(bracket);
                        }
                    }
                }
            }

            return brackets.ToArray();
        }
    }
}
