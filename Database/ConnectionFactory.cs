using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfArasoi.Database
{
    internal static class ConnectionFactory
    {
        static readonly string infos = "Server=localhost;Database=arasoi_database;User Id=root;Password=;";
        public static MySqlConnection GetConnection()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(infos);
                connection.Open();
                return connection;
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Erro ao criar conexão com o banco de dados: {ex.Message}");
                return null;
            }
        }
    }
}
