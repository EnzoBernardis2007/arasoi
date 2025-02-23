using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfArasoi.Database;

namespace WpfArasoi.Model
{
    internal class Athlete
    {
        public static List<AthleteModel> GetInscribedAthletes(string championshipId)
        {
            List<AthleteModel> athletes = new List<AthleteModel>();

            using (MySqlConnection connection = ConnectionFactory.GetConnection()) 
            {
                string query = "CALL GetAthletesByChampionship(@id)";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", championshipId);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    athletes.Add(new AthleteModel
                    {
                        Email = reader.GetString("email"),
                        PasswordHash = reader.GetString("password_hash"),
                        Salt = reader.GetString("salt"),
                        Cpf = reader.GetString("cpf"),
                        FullLegalName = reader.GetString("full_legal_name"),
                        PreferedName = reader.IsDBNull(reader.GetOrdinal("prefered_name")) ? null : reader.GetString("prefered_name"),
                        GenderName = reader.GetString("gender_name"),
                        Birthday = reader.GetDateTime("birthday"),
                        Height = reader.GetDouble("height"),
                        Weight = reader.GetInt32("weight"),
                        Sex = reader.GetString("sex"),
                        Kyu = reader.GetInt32("kyu"),
                        Dan = reader.GetInt32("dan"),
                        Dojo = reader.GetString("dojo"),
                        City = reader.GetString("city"),
                        Wins = reader.IsDBNull(reader.GetOrdinal("wins")) ? 0 : reader.GetInt32("wins"),
                        Defeats = reader.IsDBNull(reader.GetOrdinal("defeats")) ? 0 : reader.GetInt32("defeats")
                    });
                }
            }

            return athletes;
        }
    }
}
