using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfArasoi.Model
{
    public class BracketModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string AthleteIdAKA { get; set; }
        public string AthleteIdAO { get; set; }
        public int ScoreAka { get; set; } = 0;
        public int ScoreAo { get; set; } = 0;
        public int FoulAka { get; set; } = 0;
        public int FoulAo { get; set; } = 0;
    }
}
