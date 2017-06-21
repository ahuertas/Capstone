
using System.Windows.Controls;
using CapstoneFinal.Navigation;
using MahApps.Metro.Controls;
using CapstoneFinal.UserControls;
using System;

namespace CapstoneFinal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            Switcher.mainWindow = this;
            Switcher.Switch(new LoginControl(this));

           // SetUpLoginControl();
        }

        private void SetUpLoginControl()
        {
            LoginControl login = new LoginControl(this);
            MainFocus.Children.Add(login);
        }

        public void Navigate(UserControl nextPage)
        {
            this.Content = nextPage;
        }

        public void Navigate(UserControl nextPage, object state)
        {
            this.Content = nextPage;
            ISwitchable s = nextPage as ISwitchable;
            if(s != null)
            {
                s.UtilizeState(state);
            } else
            {
                throw new ArgumentException("NextPage is not ISwitchable! " + nextPage.Name.ToString());
            }
        }

       
    }
}
