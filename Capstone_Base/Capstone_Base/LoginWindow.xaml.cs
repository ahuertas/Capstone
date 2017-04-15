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
using Capstone_Base.LoginDetails;

namespace Capstone_Base
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private string password;
        private string name;

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Btn_Login_Click(object sender, RoutedEventArgs e)
        {
            name = txtBox_name.Text;
            password = txtBox_pass.Text;
            AuthenticateCredentials(name,password);
        }

        private void Btn_Register_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registry = new RegisterWindow();
            this.Close();
            registry.Show();
        }

        private void Success(string token)
        {
            MainWindow main = new MainWindow(token);
            main.Show();
            Close();
        }

        private void AuthenticateCredentials(string name, string pass)
        {
            Authenticate auth = new Authenticate(password, name);
            string response = auth.Validate();
            if (response.Contains("The user"))
            {
                MessageBox.Show(response);
            }
            else if (response.Contains("Incorrect"))
            {
                MessageBox.Show(response);
            }
            else
            {
                MessageBox.Show(response);
                string token = auth.RetrieveAccessToken();
                Success(token);

            }
        }
    }
}
