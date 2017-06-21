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

namespace Capstone_Base_2.View.RegisterViews
{
    /// <summary>
    /// Interaction logic for UserName.xaml
    /// </summary>
    public partial class UserName : Window
    {
        private string accesstoken, name , newPassword;

        public UserName()
        {
            InitializeComponent();
        }

        public UserName(string accesstoken)
        {
            this.accesstoken = accesstoken;
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            new AccessToken().Show();
            Close();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            name = UserName_TxtBox.Text;
            newPassword = password.Password.ToString();

            if(name.Equals(""))
            {
                MessageBox.Show("Please enter a user name before continueing");
            }
            else if(newPassword.Equals(""))
            {
                MessageBox.Show("Please enter a password before continueing");
            }
            else if(!name.Equals("") && !newPassword.Equals("")) 
            {
                new SecurityPhrase(accesstoken, name, newPassword).Show();
                Close();
            }


        }
    }
}
