using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfArasoi.Model
{
    internal static class StringFormatter
    {
        public static string Capitalize(string text)
        {
            return char.ToUpper(text[0]) + text.Substring(1);
        }
    }
}
