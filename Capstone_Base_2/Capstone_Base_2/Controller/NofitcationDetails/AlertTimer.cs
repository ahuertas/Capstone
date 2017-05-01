using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Capstone_Base_2.Controller;
using Capstone_Base_2.View;
namespace Capstone_Base_2.Controller.NofitcationDetails
{
    public class AlertTimer
    {
        public static Timer timer = new Timer(1 *60 *1000);
        private MainWindow main;

        public AlertTimer(MainWindow main)
        {
            this.main = main;
            timer.Elapsed += new ElapsedEventHandler(timer_notify);
        }

        private void timer_notify(object sender, ElapsedEventArgs e)
        {
            //when timer hits 0, check the methods
            //call the notifcation methods
            AlertLogic.IsAssignmentGonnaBeLate();
            AlertLogic.NewAssignmentAdded().Wait();
            //call the main window method
            main.NeccessaryUpdate();
            //reset timer again
            StartTimer();
        }

        public  void StartTimer()
        {
            timer.Start();
        }

    }
}
