using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfArasoi.Model
{
    // Use when the user dont put all data to create something
    internal class LackOfData : Exception
    {
        // Default constructor
        public LackOfData() : base("Data is missing to be inserted into the database") { }

        // Special constructor
        public LackOfData(string mensagem) : base(mensagem) { }
    }
}
