using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfArasoi.Database;
using WpfArasoi.Model;

namespace WpfArasoi.ViewModel
{
    internal class MainWindowViewModel
    {
        public ComboBoxItem[] CreatePrivilegesComboBox()
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

        public void ResponseToCreateManager(string email, string password, string confirmPassword, string privilegesType)
        {
            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirmPassword) ||
                string.IsNullOrWhiteSpace(privilegesType)) throw new LackOfData();

            if (!Email.IsValidEmail(email)) throw new InvalidEmail();

            if (password != confirmPassword) throw new MismatchingPassword();

            Manager.CreateManager(email, password, privilegesType);
        }
    }
}
