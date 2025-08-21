using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using WpfArasoi.Model;
using System.Linq;

namespace WpfArasoi.ViewModel
{
    internal class ChampionshipViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<BracketModel> _brackets;
        public ObservableCollection<BracketModel> Brackets
        {
            get => _brackets;
            set
            {
                if (_brackets != value)
                {
                    _brackets = value;
                    OnPropertyChanged(nameof(Brackets));
                }
            }
        }

        private string _championshipName;
        public string ChampionshipName
        {
            get => _championshipName;
            set
            {
                if (_championshipName != value)
                {
                    _championshipName = value;
                    OnPropertyChanged(nameof(ChampionshipName));
                }
            }
        }

        private DateTime _dateBegin;
        public DateTime DateBegin
        {
            get => _dateBegin;
            set
            {
                if (_dateBegin != value)
                {
                    _dateBegin = value;
                    OnPropertyChanged(nameof(DateBegin));
                }
            }
        }

        private DateTime _dateEnd;
        public DateTime DateEnd
        {
            get => _dateEnd;
            set
            {
                if (_dateEnd != value)
                {
                    _dateEnd = value;
                    OnPropertyChanged(nameof(DateEnd));
                }
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        public ChampionshipViewModel(string championshipId)
        {
            LoadChampionship(championshipId);
            LoadBrackets(championshipId);
        }

        private void LoadChampionship(string championshipId)
        {
            // Simulação de consulta
            var championship = Championship.GetChampionship(championshipId);
            if (championship != null)
            {
                ChampionshipName = championship.Name;
                DateBegin = championship.DateBegin;
                DateEnd = championship.DateEnd;
                Description = championship.Description;
            }
        }

        private void LoadBrackets(string championshipId)
        {
            // Consulta os brackets do campeonato
            var bracketsList = Bracket.GetBracketModels(championshipId);

            Brackets = new ObservableCollection<BracketModel>(
                bracketsList.OrderBy(b => b.CategoryId)
            );
        }

        // Event de notificação
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Métodos para os botões
        public void UpdateChampionship()
        {
            var championship = new ChampionshipModel
            {
                Name = ChampionshipName,
                DateBegin = DateBegin,
                DateEnd = DateEnd,
                Description = Description
            };
            Championship.UpdateChampionship(championship);
        }
    }
}
