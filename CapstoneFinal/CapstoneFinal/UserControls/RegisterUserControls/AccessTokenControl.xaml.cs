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
using MahApps.Metro.Controls;

namespace CapstoneFinal.UserControls.RegisterUserControls
{
    /// <summary>
    /// Interaction logic for AccessTokenControl.xaml
    /// </summary>
    public partial class AccessTokenControl : UserControl, ISwitchable
    {
         public RAGControl3 guidePart3 { get; private set; }

        public string AccessToken { get; private set; }

        public MetroWindow MainMetro { get; private set; }

        public AccessTokenControl(RAGControl3 guidePart3)
        {
            InitializeComponent();
            this.guidePart3 = guidePart3;
            MainMetro =  this.guidePart3.guidePart2.guidePart1.Main;
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(guidePart3);
        }

        private async void Next_Click(object sender, RoutedEventArgs e)
        {
            this.AccessToken = AccessToken_TxtBox.Text;
            if (AccessToken.Length ==0 || AccessToken.Length < 10)
            {
                //await this.ShowMessageAync("Please enter a AccessToken, the application needs it to retrieve the neccessary data");
                await this.MainMetro.ShowMessageAsync("Uh Oh", "You left the access token either empty or incomplete");
            }
            else
            {
                Switcher.Switch(new UserNameControl(this));
            }
        }

       
    }
}
