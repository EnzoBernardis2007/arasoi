using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfArasoi.Model
{
    internal class ChampionshipModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }

        public ChampionshipModel(string name, DateTime dateBegin, DateTime dateEnd)  
        {
            Name = name;
            DateBegin = dateBegin;
            DateEnd = dateEnd;
        }

        public string[] ListValues()
        {
            string[] values = new string[5];
            values[0] = Name;
            values[1] = Description;
            values[2] = DateBegin.ToString("yyyy-MM-dd");
            values[3] = DateEnd.ToString("yyyy-MM-dd");
            return values;
        }
    }
}
