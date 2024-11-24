using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfArasoi.Model
{
    // User dont put all data to create something
    internal class LackOfData : Exception
    {
        // Default constructor
        public LackOfData() : base("Data is missing to be inserted into the database") { }

        // Custom constructor
        public LackOfData(string message) : base(message) { }
    }

    // User put a wrong format email
    internal class InvalidEmail : Exception
    {
        // Default constructor
        public InvalidEmail() : base("Invalid email") { }

        // Custom constructor
        public InvalidEmail(string message) : base (message) { }
    }

    // User put mismatching passwords in fields
    internal class MismatchingPassword : Exception
    {
        // Default constructor
        public MismatchingPassword() : base("Mismatching password") { }

        // Custom constructor
        public MismatchingPassword(string message) : base(message) { }
    }
}
