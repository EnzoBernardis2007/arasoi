using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfArasoi.Model;
using WpfArasoi.ViewModel;

namespace WpfArasoi.View
{
    public partial class ChampionshipView : Window
    {
        ChampionshipModel championship;
        ChampionshipViewModel championshipViewModel;
        public ChampionshipView(string championshipId)
        {
            InitializeComponent();

            // Primeiro obtenha o campeonato
            championship = Championship.GetChampionship(championshipId);

            // Depois inicialize o ViewModel com o Id correto
            championshipViewModel = new ChampionshipViewModel(championship.Id);
            this.DataContext = championshipViewModel;

            // Preencha os campos da UI
            NameTextBox.Text = championship.Name;
            DateBeginDatePicker.SelectedDate = championship.DateBegin;
            DateEndDatePicker.SelectedDate = championship.DateEnd;
            DescriptionTextBox.Text = championship.Description;
        }

        private void UpdateChampionshipClick(object sender, RoutedEventArgs e)
        {
            championship.Name = NameTextBox.Text;
            championship.DateBegin = DateBeginDatePicker.SelectedDate.Value;
            championship.DateEnd = DateEndDatePicker.SelectedDate.Value;
            championship.Description = DescriptionTextBox.Text;

            Championship.UpdateChampionship(championship);
        }

        private void CreateBracketsClick(object sender, RoutedEventArgs e)
        {
            Championship.CreateBrackets(championship.Id);
        }
    }
}
