using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfArasoi.Model
{
    internal class BracketModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; } 
        public string CpfAKA { get; set; } 
        public string CpfAO { get; set; } 
        public int ScoreAka { get; set; }
        public int ScoreAo { get; set; }
        public int FoulAka { get; set; }
        public int FoulAo { get; set; }
    }
}
