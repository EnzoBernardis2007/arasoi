using Google.Protobuf.Reflection;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfArasoi.Database;
using WpfArasoi.Model;
using WpfArasoi.ViewModel;

namespace WpfArasoi.View
{
    /// <summary>
    /// Lógica interna para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            foreach (ComboBoxItem comboBoxItem in MainWindowViewModel.CreatePrivilegesComboBox())
            {
                PrivilegesComboBox.Items.Add(comboBoxItem);
            }
        }

        private void SignupClick(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordPBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;
            var comboBoxSelected = PrivilegesComboBox.SelectedItem as ComboBoxItem;
            string privilegesType = comboBoxSelected.Tag.ToString();

            if(!Email.IsValidEmail(email))
            {
                MessageBox.Show("Insira um email válido");
                return;
            }

            if(password != password.Replace(" ", ""))
            {
                MessageBox.Show("A senha não pode conter espaços");
                return;
            }

            if(password != confirmPassword)
            {
                MessageBox.Show("As senhas estão diferentes");
                return;
            }

            using (MySqlConnection connection = ConnectionFactory.GetConnection())
            {
                string salt = Password.GenerateSalt();
                string password_hash = Password.GenerateHash(password, salt);

                string[] parameters = { "@email", "@password_hash", "@salt", "@privileges_name" };
                string[] values = { email, password_hash, salt, privilegesType };

                string query = "INSERT INTO manager VALUES (UUID(), @email, @password_hash, @salt, @privileges_name)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        command.Parameters.AddWithValue(parameters[i], values[i]);
                    }

                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Entrou!");
            }
        }
    }
}
