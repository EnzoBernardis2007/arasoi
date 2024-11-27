using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfArasoi.ViewModel;

namespace WpfArasoi.Model
{
    internal class ChampionshipModel : Model
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }
        public string Author { get; set; }
        public int InscriptionCount { get; set; }

        public ChampionshipModel() { }

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
    }
}
