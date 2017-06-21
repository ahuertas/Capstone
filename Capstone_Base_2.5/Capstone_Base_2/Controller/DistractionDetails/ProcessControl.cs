using Capstone_Base_2.Controller.NofitcationDetails;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Threading;

namespace Capstone_Base_2.Controller.DistractionDetails
{
   public class ProcessControl
    {
        public List<string> Distractions { get; set; }

        public DispatcherTimer DTimer { get; set; }

        public MainWindow main;

        public bool IsDone { get; set; }

        private int hours, minutes, seconds;

        public ProcessControl (MainWindow main)
        {
            this.main = main;
            IsDone = false;
        }

#region Logic

        private void FindChromeTabs()
        {
            Process[] procsChrome = Process.GetProcessesByName("chrome");
            if (procsChrome.Length <= 0)
            {
                Console.WriteLine("chrome is not running");
            }
            else
            {
                foreach (Process proc in procsChrome)
                {

                    //the chrome process must have a window
                    if (proc.MainWindowHandle == IntPtr.Zero)
                    {
                        continue;
                    }

                    //to find the tabs we firest need to locate something reliable = the New tab button
                    AutomationElement root = AutomationElement.FromHandle(proc.MainWindowHandle);
                    System.Windows.Automation.Condition conNewTab = new PropertyCondition(AutomationElement.NameProperty, "New Tab");
                    AutomationElement elmNewTab = root.FindFirst(TreeScope.Descendants, conNewTab);

                    //get the tabstrip by getting the parent of the new tab button
                    TreeWalker treewalker = TreeWalker.ContentViewWalker;
                    if (elmNewTab != null)
                    {
                        AutomationElement elmTabStrip = treewalker.GetParent(elmNewTab);

                        //loop through the tabs and et the names which is the page title
                        System.Windows.Automation.Condition condTabItem = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TabItem);
                        foreach (AutomationElement tabitem in elmTabStrip.FindAll(TreeScope.Children, condTabItem))
                        {
                            Console.WriteLine(tabitem.Current.Name);
                            foreach (String distraction in Distractions)
                            {
                                if (tabitem.Current.Name.ToLower().Contains(distraction))
                                {
                                    //send an alert to the user
                                    NotificationBalloonTips.StudyBrokenAlert(distraction);
                                }
                            }
                        }
                    }

                }
            }
        }

        private void FindProcess(String distraction)
        {

            Process[] runningProcesses = Process.GetProcesses();

            foreach (Process proc in runningProcesses)
            {

                if (!String.IsNullOrEmpty(proc.MainWindowTitle))
                {
                    if (proc.MainWindowTitle.Contains(distraction))
                    {
                        if (!proc.HasExited)
                        {
                            NotificationBalloonTips.StudyBrokenAlert(distraction);
                            proc.Kill();
                        }
                    }
                    else if (proc.ProcessName.Contains(distraction))
                    {
                        if (!proc.HasExited)
                        {
                            NotificationBalloonTips.StudyBrokenAlert(distraction);
                            proc.Kill();
                        }
                    }
                }
            }
        }

        #endregion

        private void DispatchTimerLogic()
        {
            DTimer = new DispatcherTimer();
            TimerSetup();

            DTimer.Interval = new TimeSpan(0, 0, 1);
            DTimer.Tick += new EventHandler(Timer_tick);
            DTimer.Start();
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
                
                NotificationBalloonTips.TimerPopUp();
                IsDone = true;
            }

            //Dispatcher.Invoke(() =>
            //{
            string hh = Convert.ToString(hours);
            string mm = Convert.ToString(minutes);
            string ss = Convert.ToString(seconds);

            main.TimerLabel.Content = hh;
            main.TimerLabelMM.Content = mm;
            main.TimerLabelSS.Content = ss;
            // });
            if (!IsDone)
            {
                ActivateCounterMeasures();
            }
        }

        private void TimerSetup()
        {
            if (main.TimerHours.Text == "")
            {
                main.TimerHours.Text = "0";
            }

            if (main.TimerMinutes.Text == "")
            {
                main.TimerMinutes.Text = "0";
            }

            if (main.TimerSeconds.Text == "")
            {
                main.TimerSeconds.Text = "0";
            }

            hours = Convert.ToInt32(main.TimerHours.Text);
            minutes = Convert.ToInt32(main.TimerMinutes.Text);
            seconds = Convert.ToInt32(main.TimerSeconds.Text);
        }


        private void ActivateCounterMeasures()
        {

            FindChromeTabs();

            foreach (String dist in Distractions)
            {
                FindProcess(dist);
            }

        }

        public void Add(String data)
        {
            Distractions.Add(data);
            main.DistractionListUpdate();
        }

        public void Remove()
        {
            if (Distractions.Count > 0)
            {
                Distractions.RemoveAt(Distractions.Count - 1);
               main.DistractionListUpdate();
            }
        }
    }



}
