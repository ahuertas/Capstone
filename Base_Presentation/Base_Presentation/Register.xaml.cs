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
using Base_Presentation.RegisterDetails;

namespace Base_Presentation
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        //variables
        string registerName;
        string registerPassword;
        string accessToken;

        public Register()
        {
            InitializeComponent();
        }

        private void btn_GoBack_Click(object sender, RoutedEventArgs e)
        {
            Login loggingIn = new Login();
            this.Close();
            loggingIn.Show();
        }

        private void btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            //create a hidden folder in the specified location, aka documents
            registerName = Register_name.Text;
            registerPassword = Register_pass.Text;
            accessToken = Register_Access.Text;
            //the folder name will be the username
            HiddenFolderCreator hidden = new HiddenFolderCreator(registerName);
            string response = hidden.CreateFolder();
            if (response.Contains("complete"))
            {
                 //write password and accessToken  to file 
                 WriteUserDetails write = new WriteUserDetails(registerPassword,accessToken,hidden.GetFullPath());
                 write.WriteFile();
                MessageBox.Show(response);
            }
            else
            {
                MessageBox.Show(response);
            }
            Login loggingIn = new Login();
            this.Close();
            loggingIn.Show();

        }


    }
}
