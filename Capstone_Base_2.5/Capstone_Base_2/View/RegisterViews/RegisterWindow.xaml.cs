using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Capstone_Base_2.Controller.RegisterDetails;
using System.IO;

namespace Capstone_Base_2.View
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private FolderCreationKit folderCreationKit;
        private string RegName, RegPass, RegAccessToken, RegSecret;
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void Btn_Return_Login_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow goBack = new LoginWindow();
            Close();
            goBack.Show();
        }

        private void Btn_Submit_Reg_Click(object sender, RoutedEventArgs e)
        {
            RegName = txt_New_Name.Text;
            RegPass = txt_New_Pass.Text;
            RegAccessToken = txt_New_AT.Text;
            RegSecret = txt_New_Sec.Text;
            SetupFolders();
        }

        protected void SetupFolders()
        {
             folderCreationKit = new FolderCreationKit(RegName);
            string response = folderCreationKit.CreateFolder();
            if (response.Contains("complete"))
            {
                //write password and accessToken  to file 
                //UserDetailsWriter.WriteFileDetails(folderCreationKit.FileFullPath,RegPass,RegAccessToken,RegSecret);
                MessageBox.Show(response);
                Success();
            }
            else
            {
                MessageBox.Show(response);
            }
        }

        protected void Success()
        {
            LoginWindow successReturn = new LoginWindow();
            Close();
            successReturn.Show();
        }
        
    }
}
