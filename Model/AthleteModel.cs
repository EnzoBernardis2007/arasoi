using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WpfArasoi.Model
{
    internal class AthleteModel
    {
        public int CategoryId { get; set; }
        // App Info
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }

        // Personal Info
        public string Cpf { get; set; }
        public string FullLegalName { get; set; }
        public string PreferedName { get; set; }
        public string GenderName { get; set; }

        // Balancing Info
        public DateTime Birthday { get; set; }
        public double Height { get; set; } 
        public int Weight { get; set; } 
        public string Sex { get; set; } 
        public int Kyu { get; set; } 
        public int Dan { get; set; } 

        // Additional Info
        public string Dojo { get; set; }
        public string City { get; set; }
        public int Wins { get; set; } 
        public int Defeats { get; set; }

        public override string ToString()
        {
            string msg = $"{FullLegalName}: Peso '{Weight}', altura '{Height}'";
            return msg;
        }
        public override bool Equals(object obj)
        {
            if (obj is AthleteModel other)
            {
                return Cpf == other.Cpf && FullLegalName == other.FullLegalName;
            }
            return false;
        }
    }
}
