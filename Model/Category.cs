using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfArasoi.Database;

namespace WpfArasoi.Model
{
    internal class Category
    {
        public static void CreateCategories(string championshipId)
        {
            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                string query = "CALL CreateCategories(@id)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", championshipId);
                command.ExecuteNonQuery();
            }
        }
    }
}
