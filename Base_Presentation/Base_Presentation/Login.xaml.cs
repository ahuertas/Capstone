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
using Base_Presentation.LoginDetails;
namespace Base_Presentation
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private string password;
        private string name;

        public Login()
        {
            InitializeComponent();
        }

        private void btn_Register_Click(object sender, RoutedEventArgs e)
        {
            Register registry = new Register();
            this.Close();
            registry.Show();
            
        }

        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            password = txtBox_pass.Text;
            name = txtBox_name.Text;
            //find the users file
            Authenticate auth = new Authenticate(password,name);
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
                //if the user file doesn't exist  throw alert
                //if  user exist read text file and see if the password matches

        }

        private void Success(string token)
        {
            //pass in access token
            MainWindow main = new MainWindow(token);
            main.Show();
            this.Close();
        }
    }
}
