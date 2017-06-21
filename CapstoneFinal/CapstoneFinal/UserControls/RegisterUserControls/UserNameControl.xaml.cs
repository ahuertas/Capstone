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
using MahApps.Metro.Controls.Dialogs;

namespace CapstoneFinal.UserControls.RegisterUserControls
{
    /// <summary>
    /// Interaction logic for UserNameControl.xaml
    /// </summary>
    public partial class UserNameControl : UserControl, ISwitchable
    {

        public string UserName { get; private set; }
        public string NewPassword { get; private set; }

        public AccessTokenControl ATC { get; private set; }

        public UserNameControl(AccessTokenControl ATC)
        {
            InitializeComponent();
            this.ATC = ATC;
            string access = this.ATC.AccessToken;
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(ATC);
        }

        private async void Next_Click(object sender, RoutedEventArgs e)
        {
            UserName = UserName_TxtBox.Text;
            NewPassword = password.Password.ToString();

            if (UserName.Length == 0)
            {
                await this.ATC.MainMetro.ShowMessageAsync("Uh Oh", "Please enter a user name before continueing");
            }
            else if (NewPassword.Length == 0)
            {
                await this.ATC.MainMetro.ShowMessageAsync("Uh...", "Please enter a password before continueing");
            }
            else if (UserName.Length != 0 && NewPassword.Length != 0)
            {
                Switcher.Switch(new SecurityController(this));
            }
        }
    }
}
