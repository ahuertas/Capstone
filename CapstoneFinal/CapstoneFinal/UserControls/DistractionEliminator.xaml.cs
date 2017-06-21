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
using System.Windows.Threading;
using CapstoneFinal.HelperClasses.ProxyServerHelper;
using CapstoneFinal.HelperClasses.NotificationHelpers;

namespace CapstoneFinal.UserControls
{
    /// <summary>
    /// Interaction logic for DistractionEliminator.xaml
    /// </summary>
    public partial class DistractionEliminator : UserControl, ISwitchable
    {
        #region Variables
        private MainWindowControl main;
        private List<string> blockedLists = new List<string>();
        private DispatcherTimer DTimer;
        private int hours, minutes, seconds;
        private TitaniumProxy titan;

       
        #endregion

        public DistractionEliminator(MainWindowControl main)
        {
            InitializeComponent();
            this.main = main;
            titan = new TitaniumProxy();
            
        }

        public void Resize()
        {
            main.MainView.Height = 600;
            main.MainView.Width = 1000;
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        #region Buttons
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            main.ResizeMainWindowToOriginal();
            Switcher.Switch(main);
        }

        private void Block_Click(object sender, RoutedEventArgs e)
        {
            string newWebsite = site_txtBlk.Text;
            blockedLists.Add(newWebsite);
            UpdateBlockedList();
        }

        private void Unblock_Click(object sender, RoutedEventArgs e)
        {
            blockedLists.RemoveAt(blockedLists.Count-1);
            UpdateBlockedList();
        }

        private void UpdateBlockedList()
        {
            BlockedList.Items.Clear();
            foreach(string website in blockedLists)
            {
                BlockedList.Items.Add(website);
            }
        }
        #endregion

        #region Proxy
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            DispatchTimerLogic();
            Stop.IsEnabled = false;
            titan.BlackListSites = blockedLists;
            titan.Start();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            DTimer.Stop();
            titan.Stop();
        }
        #endregion

        #region Dispatch Timer 
        private void DispatchTimerLogic()
        {
            DTimer = new DispatcherTimer();
            TimerSetup();

            DTimer.Interval = new TimeSpan(0, 0, 1);
            DTimer.Tick += new EventHandler(Timer_tick);
            DTimer.Start();
        }

        private void FailSafe_Checked(object sender, RoutedEventArgs e)
        {
            Stop.IsEnabled = true;
            FailSafe.IsChecked = false;
        }

        private void Timer_tick(object sender, EventArgs e)
        {
            seconds = seconds - 1;

            if (seconds == -1)
            {
                minutes = minutes - 1;
                seconds = 59;
            }

            if (minutes == -1)
            {
                hours = hours - 1;
                minutes = 59;
            }

            if (hours == 0 && minutes == 0 && seconds == 0)
            {
                DTimer.Stop();
                //stop the proxy
                titan.Stop();
                //send a alert
                NotificationBalloonTips.TimerPopUp();
            }

           
            string hh = Convert.ToString(hours);
            string mm = Convert.ToString(minutes);
            string ss = Convert.ToString(seconds);

            VisibleHours.Content = hh;
            VisibleMinutes.Content = mm;
            VisibleSeconds.Content = ss;          
        }


        private void TimerSetup()
        {
            if (TimerHours.Text == "")
            {
                TimerHours.Text = "0";
            }

            if (TimerMinutes.Text == "")
            {
                TimerMinutes.Text = "0";
            }

            if (TimerSeconds.Text == "")
            {
                TimerSeconds.Text = "0";
            }

            hours = Convert.ToInt32(TimerHours.Text);
            minutes = Convert.ToInt32(TimerMinutes.Text);
            seconds = Convert.ToInt32(TimerSeconds.Text);
        }



        #endregion
    }
}
