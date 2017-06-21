using CapstoneFinal.UserControls;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Controls;

namespace CapstoneFinal.HelperClasses.NotificationHelpers
{
    public class NotificationTimer
    {
        public static Timer notifyTimer = new Timer(60 * 60 * 1000);
        private MainWindowControl main;

        public NotificationTimer(MainWindowControl main)
        {
            this.main = main;
            notifyTimer.Elapsed += new ElapsedEventHandler(Timer_notify);
        }

        private void Timer_notify(object sender, ElapsedEventArgs e)
        {
            //when timer hits 0, check the methods
            //call the notifcation methods
            NotificationLogic.IsAssignmentGonnaBeLate();
            NotificationLogic.IsAssignmentLate();
            NotificationLogic.NewAssignmentAdded().Wait();
            //call the main window method
            main.NeccessaryUpdate();
            main.ConvertJsonToObservable();

            //reset timer again
            StartTimer();
        }

        public void StartTimer()
        {
            notifyTimer.Start();
        }

    }
}
