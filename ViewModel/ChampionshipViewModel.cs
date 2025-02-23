using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfArasoi.Model;

namespace WpfArasoi.ViewModel
{
    internal class ChampionshipViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ManagerModel> _managers;
        public ObservableCollection<ManagerModel> Managers
        {
            get => _managers;
            set
            {
                if (_managers != value)
                {
                    _managers = value;
                    OnPropertyChanged(nameof(Managers));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ComboBoxItem[] CreateChampionshipsComboBox()
        {
            ChampionshipModel[] championships = Championship.GetChampionships();
            ComboBoxItem[] comboBoxItems = new ComboBoxItem[championships.Length];

            for (int i = 0; i < championships.Length; i++)
            {
                comboBoxItems[i] = new ComboBoxItem
                {
                    Content = StringFormatter.Capitalize(championships[i].Name),
                    Tag = championships[i].Id
                };
            }

            return comboBoxItems;
        }
    }
}
