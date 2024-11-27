using MySql.Data.MySqlClient;
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
using WpfArasoi.Database;
using WpfArasoi.Model;
using WpfArasoi.ViewModel;

namespace WpfArasoi.View
{
    public partial class AddChampionshipView : Window
    {
        ChampionshipModel championship;
        AddChampionshipViewModel viewModel;

        public AddChampionshipView()
        {
            InitializeComponent();
            viewModel = new AddChampionshipViewModel();
        }

        private void AddChampionshipClickFirstStep(object sender, RoutedEventArgs e)
        {
            try
            {
                championship = new ChampionshipModel(NameTextBox.Text,
                DateBeginDatePicker.SelectedDate.Value,
                DateEndDatePicker.SelectedDate.Value);
            } catch
            {
                MessageBox.Show("Falta informações");
                return;
            }

            MainGrid.Children.Clear();

            // Create description TextBox
            TextBox descriptionTextBox = viewModel.CreateDescriptionTextBox();

            // Setup description TextBox
            Grid.SetRow(descriptionTextBox, 0);
            Grid.SetRowSpan(descriptionTextBox, 3);
            Grid.SetColumn(descriptionTextBox, 0);
            Grid.SetColumnSpan(descriptionTextBox, 2);

            // Create new button to second step
            Button button = viewModel.CreateConfirmButton();

            // Setup button
            button.Click += AddChampionshipClickSecondStep;

            Grid.SetRow(button, 4);
            Grid.SetColumn(button, 0);
            Grid.SetColumnSpan(button, 2);

            // Put new components
            MainGrid.Children.Add(descriptionTextBox);
            MainGrid.Children.Add(button);
        }

        private void AddChampionshipClickSecondStep(object sender, RoutedEventArgs e) 
        {
            TextBox descriptionTextBox = WindowManager.GetUIElementWithName<TextBox>(MainGrid, "DescriptionTextBox");
            championship.Description = descriptionTextBox.Text;

            Championship.CreateChampionship(championship);

            MessageBox.Show("Campeonato criado!");
        }
    }
}
