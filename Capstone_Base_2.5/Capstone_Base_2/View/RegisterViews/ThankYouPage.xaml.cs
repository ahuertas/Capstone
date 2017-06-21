using Capstone_Base_2.Controller.RegisterDetails;
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
    /// Interaction logic for ThankYouPage.xaml
    /// </summary>
    public partial class ThankYouPage : Window
    {
        private string[] data;
        private FolderCreationKit folderCreationKit;
        private string RegName, RegPass, RegAccessToken, RegSecretQuest, RegSecretAnswer;

        private void Next_Click(object sender, RoutedEventArgs e)
        {

        }

        public ThankYouPage()
        {
            InitializeComponent();
        }

        public ThankYouPage(string[] data)
        {
            this.data = data;
            RegName = data[1] ;
            RegPass =data[2] ;
            RegAccessToken = data[0];
            RegSecretQuest = data[3];
            RegSecretAnswer =data[4];
            SetupFolders();
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            new SecurityPhrase(data[0],data[1],data[3]).Show();
            Close();
        }
        protected void SetupFolders()
        {
            folderCreationKit = new FolderCreationKit(RegName);
            string response = folderCreationKit.CreateFolder();
            if (response.Contains("complete"))
            {
                //write password and accessToken  to file 
                UserDetailsWriter.WriteFileDetails(folderCreationKit.FileFullPath, RegPass, RegAccessToken, RegSecretQuest, RegSecretAnswer);
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
