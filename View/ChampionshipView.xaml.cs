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

namespace WpfArasoi.View
{
    public partial class ChampionshipView : Window
    {
        ChampionshipModel championship;
        public ChampionshipView(string championshipId)
        {
            InitializeComponent();

            championship = Championship.GetChampionship(championshipId);

            NameTextBox.Text = championship.Name;
            DateBeginDatePicker.SelectedDate = championship.DateBegin;
            DateEndDatePicker.SelectedDate= championship.DateEnd;
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
    }
}
