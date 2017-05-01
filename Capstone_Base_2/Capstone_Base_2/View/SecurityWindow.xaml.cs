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
using Capstone_Base_2.Controller.SecurityDetails;
namespace Capstone_Base_2.View
{
    /// <summary>
    /// Interaction logic for SecurityWindow.xaml
    /// </summary>
    public partial class SecurityWindow : Window
    {
        private string _Name, _Secret;
        
        public SecurityWindow()
        {
            InitializeComponent();
        }

        private void Btn_Remeber_Click(object sender, RoutedEventArgs e)
        {
            _Name = Security_Name.Text;
            _Secret = Security_Answer.Text;
            FindPassword();
        }

        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            new LoginWindow().Show();
            Close();
        }

        protected void FindPassword()
        {
            AuthSecurity security = new AuthSecurity(_Name,_Secret);
            string response = security.RetrieveUserInfo();
            if (response.Contains("The username "))
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
            }

        }
    }
}
