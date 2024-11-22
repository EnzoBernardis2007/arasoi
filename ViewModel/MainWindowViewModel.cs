using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfArasoi.Database;
using WpfArasoi.Model;

namespace WpfArasoi.ViewModel
{
    internal class MainWindowViewModel
    {
        public static ComboBoxItem[] CreatePrivilegesComboBox()
        {
            string[] privileges = Privileges.GetPrivilegesId();
            ComboBoxItem[] comboBoxItems = new ComboBoxItem[privileges.Length];

            for(int i = 0; i < privileges.Length; i++)
            {
                comboBoxItems[i] = new ComboBoxItem
                {
                    Content = StringFormatter.Capitalize(privileges[i]),
                    Tag = privileges[i],
                };
            }

            return comboBoxItems;
        }
    }
}
