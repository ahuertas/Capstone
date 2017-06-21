using Capstone_Base_2.Controller.LoginDetials;
using Capstone_Base_2.Controller.RegisterDetails;
using Capstone_Base_2.View.RegisterViews;
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

namespace Capstone_Base_2.View
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private string name, password;

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btn_Register_Click(object sender, RoutedEventArgs e)
        {
            new RegisterSlideIntro().Show();
            Close();
          
        }

        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            name = txt_Name.Text;
            password = passBox.Password.ToString();
            AuthenticateCredentials(name, password);
        }

        private void btn_Forgotten_Click(object sender, RoutedEventArgs e)
        {
            new SecurityWindow().Show();
            Close();
        }

        private void Success(string token)
        {
          new MainWindow(token, name).Show() ;
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
