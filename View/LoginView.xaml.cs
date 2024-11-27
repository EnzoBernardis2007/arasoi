using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace WpfArasoi.View
{
    /// <summary>
    /// Lógica interna para LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void LoginButton(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;

            if (!Email.IsValidEmail(email) || string.IsNullOrWhiteSpace(password)) 
            {
                MessageBox.Show("Preencha todos os campos de forma válida");
                return;
            }
            
            if (Password.ValidPassword(email, password))
            {
                MessageBox.Show("Entrando..........");
                ActualUser.SetIdWithEmail(email);
                ActualUser.SetEmail(email);
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            } else
            {
                MessageBox.Show("Senha ou email errado");
            }
        }
    }
}
