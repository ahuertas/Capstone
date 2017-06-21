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
    /// Interaction logic for LoginControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl, ISwitchable
    {
        public MainWindow main { get; private set; } 

        public LoginControl(MainWindow main)
        {
            InitializeComponent();
            this.main = main;
            
            ResizeMainWindow();
        }


        private void ResizeMainWindow()
        {
            main.Height = 450;
            main.Width = 600;
        }

        private  void  Login_Btn_Click(object sender, RoutedEventArgs e)
        {
            //just switch main window view to main login
            string name = Name.Text;
            string password = Password.Password.ToString();

            AuthenticatCredentials(name,password);
        }

        private void ForgotPassword_Btn_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new SecurityReturnControl(this));
        }

        private void Register_Btn_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new RegisterSlideControlIntro(main));
        }


        private async void AuthenticatCredentials(string name, string passphrase)
        {
            AuthenticateHandler auth = new AuthenticateHandler();
            auth.Name = name;
            auth.Password = passphrase;
           
            string response = auth.Validate();
            if (response.Contains("The user"))
            {
               await main.ShowMessageAsync("Non-existant User",response);
            }
            else if (response.Contains("Incorrect"))
            {
               await main.ShowMessageAsync("Incorrect Password",response);
            }
            else
            {
               await main.ShowMessageAsync("Successful login",response);
                string token = auth.RetrieveAccessToken();
                SuccessfulLoginAsync(token);

            }
        }

        public async void SuccessfulLoginAsync(string token)
        {
            ProcessRing.IsActive = true;
            await Task.Delay(3000);
            Switcher.Switch(new MainWindowControl(main, token, Name.Text));
        }


        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
    }
}
