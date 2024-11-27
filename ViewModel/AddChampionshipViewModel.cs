using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using WpfArasoi.View;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.ComponentModel;
using System.Collections.ObjectModel;
using WpfArasoi.Model;

namespace WpfArasoi.ViewModel
{
    internal class AddChampionshipViewModel
    {
        static MainWindowViewModel MainWindowViewModel { get; set; }

        public TextBox CreateDescriptionTextBox()
        {
            return new TextBox
            {
                Name = "DescriptionTextBox",
                Text = "Insira a descrição aqui...",
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
        }

        public Button CreateConfirmButton()
        {
            return new Button
            {
                Content = "Confirmar",
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
        }

        public static void SetMainWindowViewModel(MainWindowViewModel mainWindowViewModel)
        {
            MainWindowViewModel = mainWindowViewModel;
        }

        public void ResponseToCreateChampionship(ChampionshipModel championship)
        {
            Championship.CreateChampionship(championship);
            MainWindowViewModel.LoadChampionshipsList();
        }
    }
}
