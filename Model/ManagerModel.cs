using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfArasoi.Database;
using WpfArasoi.Prefab;
using WpfArasoi.View;
using WpfArasoi.ViewModel;

namespace WpfArasoi.Model
{
    // This class is used in the list of users in tab of users
    internal class ManagerModel
    {
        public string Email { get; set; }
        public ICommand Delete { get; set; }
        private MainWindowViewModel viewModel { get; set; }

        public ManagerModel(string email, MainWindowViewModel viewModel)
        {
            Email = email;
            Delete = new RelayCommand(DeleteMyself);
            this.viewModel = viewModel;
        }

        public void DeleteMyself()
        {
            using (MySqlConnection connection = ConnectionFactory.GetConnection()) 
            {
                string query = "DELETE FROM manager WHERE email = @email";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@email", Email);
                command.ExecuteNonQuery();
                viewModel.LoadManagersList();
            }
        }
    }
}
