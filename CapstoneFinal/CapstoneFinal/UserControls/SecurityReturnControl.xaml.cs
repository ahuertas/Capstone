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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CapstoneFinal.Navigation;
using CapstoneFinal.UserControls;
using CapstoneFinal.UserControls.RegisterUserControls;
using CapstoneFinal.HelperClasses;
using MahApps.Metro.Controls.Dialogs;

namespace CapstoneFinal.UserControls
{
    /// <summary>
    /// Interaction logic for SecurityReturnControl.xaml
    /// </summary>
    public partial class SecurityReturnControl : UserControl, ISwitchable
    {
        private string _Name, _Secret, _Question;
        private LoginControl main;


        public SecurityReturnControl(LoginControl main)
        {
            InitializeComponent();
            this.main = main;
            Security_Answer.Visibility = Visibility.Hidden;
            Security_Question.Visibility = Visibility.Hidden;
            btn_Remeber.Visibility = Visibility.Hidden;
        }

        private void Btn_Remeber_Click(object sender, RoutedEventArgs e)
        {
            _Secret = Security_Answer.Text;
            AuthSecurityHandler.Secret = _Secret;
            FindPassword();
        }

        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(main);
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void start_Btn_Click(object sender, RoutedEventArgs e)
        {
            _Name = Security_Name.Text;
            AuthSecurityHandler.Name = _Name;
            Security_Question.Content = AuthSecurityHandler.RetrieveUserQuestion();
            if (!Security_Question.Content.Equals(""))
            {
                Security_Answer.Visibility = Visibility.Visible;
                Security_Question.Visibility = Visibility.Visible;
                btn_Remeber.Visibility = Visibility.Visible;
            }
        }

      
        private  async void FindPassword()
        {
            string response = AuthSecurityHandler.RetrieveUserInfo(_Name, _Secret);
            if (response.Contains("The username "))
            {
               await main.main.ShowMessageAsync("Username error",response);
            }
            else if (response.Contains("Incorrect"))
            {
                await main.main.ShowMessageAsync("error", response);
            }
            else
            {
                await main.main.ShowMessageAsync("success", response);
            }
        }
    }
}
