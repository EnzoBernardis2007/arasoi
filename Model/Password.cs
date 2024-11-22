using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WpfArasoi.Database;

namespace WpfArasoi.Model
{
    internal static class Password
    {
        public static string GetSaltFromEmail(string email)
        {
            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                string query = "SELECT salt FROM manager WHERE email = @email";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@email", email);

                MySqlDataReader readerSelectSalt = command.ExecuteReader();

                return readerSelectSalt["salt"].ToString();
            }
        }

        public static string GenerateHash(string password, string salt) 
        {
            var sha256 = SHA256.Create();
            var saltedPassword = Encoding.UTF8.GetBytes(password + salt);
            var hash = sha256.ComputeHash(saltedPassword);
            return Convert.ToBase64String(hash);
        }

        public static bool ValidPassword(string email, string password) 
        {
            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                string salt = GetSaltFromEmail(email);
                string password_hash = GenerateHash(password, salt);

                string query = "SELECT * FROM manager WHERE email = @email AND password_hash = @password_hash";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@password_hash", password_hash);

                MySqlDataReader reader = command.ExecuteReader();

                return reader.HasRows;
            }
        }
    }
}
