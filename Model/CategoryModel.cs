using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfArasoi.Model
{
    internal class CategoryModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string AgeGroup { get; set; }
        public string Sex { get; set; }
        public decimal MinWeight { get; set; }
        public decimal MaxWeight { get; set; } 
        public DateTime MinBirthYear { get; set; }
        public DateTime MaxBirthYear { get; set; }

        public override string ToString()
        {
            return $"{CategoryName} ({AgeGroup}) - {Sex}: {MinWeight}kg to {MaxWeight}kg, Born between {MinBirthYear:yyyy} and {MaxBirthYear:yyyy}";
        }
    }
}
