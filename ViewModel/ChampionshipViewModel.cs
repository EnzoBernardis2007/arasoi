using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
