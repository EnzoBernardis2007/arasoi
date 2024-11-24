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
        MainWindowViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();

            viewModel = new MainWindowViewModel();

            foreach (ComboBoxItem comboBoxItem in viewModel.CreatePrivilegesComboBox())
            {
                PrivilegesComboBox.Items.Add(comboBoxItem);
            }
        }

        private void SignupClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string email = EmailTextBox.Text;
                string password = PasswordPBox.Password;
                string confirmPassword = ConfirmPasswordBox.Password;

                var comboBoxSelected = PrivilegesComboBox.SelectedItem as ComboBoxItem;

                if (comboBoxSelected == null) throw new LackOfData();

                string privilegesType = comboBoxSelected.Tag.ToString();

                viewModel.ResponseToCreateManager(email, password, confirmPassword, privilegesType);
                MessageBox.Show("Registrado!");
            }
            catch (LackOfData ex)
            {
                MessageBox.Show("Insira todos os dados");
            }
            catch (InvalidEmail ex) 
            {
                MessageBox.Show("Email inválido");
            }
            catch (MismatchingPassword ex)
            {
                MessageBox.Show("As senhas não coincidem");
            }
            catch
            {
                MessageBox.Show("Erro ao criar usuário");
            }
        }
    }
}
