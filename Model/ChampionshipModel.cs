using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfArasoi.Database;
using WpfArasoi.Prefab;
using WpfArasoi.ViewModel;

namespace WpfArasoi.Model
{
    internal class ChampionshipModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }
        public string Author { get; set; }
        public int InscriptionCount { get; set; }
        public ICommand Delete { get; set; }
        public MainWindowViewModel ViewModel { get; set; }

        public ChampionshipModel() 
        {
            Delete = new RelayCommand(DeleteMyself);
        }

        public ChampionshipModel(string name) 
        {
            Name = name;
        }

        public ChampionshipModel(string name, DateTime dateBegin, DateTime dateEnd)  
        {
            Name = name;
            DateBegin = dateBegin;
            DateEnd = dateEnd;
        }

        public void DeleteMyself()
        {
            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                string query = "DELETE FROM championship WHERE id = @id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                command.ExecuteNonQuery();
                ViewModel.LoadChampionshipsList();
            }
        }
    }
}
