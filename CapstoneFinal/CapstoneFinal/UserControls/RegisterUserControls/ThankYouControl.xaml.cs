using System;
using System.Windows;
using System.Windows.Controls;
using CapstoneFinal.Navigation;
using CapstoneFinal.HelperClasses;
using MahApps.Metro.Controls.Dialogs;

namespace CapstoneFinal.UserControls.RegisterUserControls
{
    /// <summary>
    /// Interaction logic for ThankYouControl.xaml
    /// </summary>
    public partial class ThankYouControl : UserControl, ISwitchable
    {
        public SecurityController SecControl { get; private set; }
        private UserNameControl UN;
        private AccessTokenControl ATC;

        public ThankYouControl(SecurityController SecControl)
        {
            InitializeComponent();
            this.SecControl = SecControl;
            UN = SecControl.UNControl;
            ATC = SecControl.UNControl.ATC;
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(SecControl);
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            SetupRegistration();
        }


        private async void SetupRegistration ()
        {
            FolderCreationHandler FCH = new FolderCreationHandler();
            string response = FCH.StartCreationProcess(UN.UserName);

            if(response.Contains("complete"))
            {
                UserDetailsHandler.AccessToken = ATC.AccessToken;
                UserDetailsHandler.Password = UN.NewPassword;
                UserDetailsHandler.SecretQuest = SecControl.Question;
                UserDetailsHandler.SecretAnswer = SecControl.Answer;
                UserDetailsHandler.FilePath = UN.UserName;

                UserDetailsHandler.WriteUserDetails();
                await this.ATC.MainMetro.ShowMessageAsync("Success", response, MessageDialogStyle.Affirmative);
                Success();
            }
            else
            {
                await this.ATC.MainMetro.ShowMessageAsync("Sorry", response);
            }
        }

        private void Success()
        {
            MainWindow main = SecControl.UNControl.ATC.guidePart3.guidePart2.guidePart1.Main;
            Switcher.Switch(new LoginControl(main));
        }
    }
}
