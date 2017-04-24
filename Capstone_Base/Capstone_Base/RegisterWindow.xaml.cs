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
using Capstone_Base.RegisterDetails;
namespace Capstone_Base
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private string registerName;
        private string registerPassword;
        private  string accessToken;

        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void btn_GoBack_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loggingIn = new LoginWindow();
            this.Close();
            loggingIn.Show();
        }

        private void btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            registerName = Register_name.Text;
            registerPassword = Register_pass.Text;
            accessToken = Register_Access.Text;
            SetUpFolders(registerName,registerPassword,accessToken);
        }

        private void SetUpFolders(string name, string password, string accessToken)
        {
            HiddenFolderCreator hidden = new HiddenFolderCreator(registerName);
            string response = hidden.CreateFolder();
            if (response.Contains("complete"))
            {
                //write password and accessToken  to file 
                WriteUserDetails write = new WriteUserDetails(registerPassword, accessToken, hidden.GetFullPath());
                write.WriteFile();
                MessageBox.Show(response);
                Success();
            }
            else
            {
                MessageBox.Show(response);
            }
        }

        private void Success()
        {
            LoginWindow loggingIn = new LoginWindow();
            this.Close();
            loggingIn.Show();
        }
    }
}
