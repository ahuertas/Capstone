using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone_Base_2.Controller.NofitcationDetails;
using System.Windows.Forms;
using System.Drawing;
using System.Windows;
using System.ComponentModel;

namespace Capstone_Base_2.Controller.NofitcationDetails
{
    public class NotificationIcon
    {
        private MainWindow main;
        private NotifyIcon MyNotifyIcon;
        private bool _isExit;

        public NotificationIcon(MainWindow main)
        {
            this.main = main;

            main.Closing += MainWindow_Closing;

            NotifationSetUp();
            
            CreateContextMenu();
        }

        private void NotifationSetUp()
        {
            MyNotifyIcon = new System.Windows.Forms.NotifyIcon();
            NotificationBalloonTips.Notify = MyNotifyIcon;
            MyNotifyIcon.DoubleClick += (s, args) => DisplayMainWindow();
            MyNotifyIcon.Icon = new Icon("Buddybleu.ico");
            MyNotifyIcon.Visible = true;
        }

        private void CreateContextMenu()
        {
            MyNotifyIcon.ContextMenuStrip =
                new System.Windows.Forms.ContextMenuStrip();
            MyNotifyIcon.ContextMenuStrip.Items.Add("MainWindow").Click += (s, e) => DisplayMainWindow();

            MyNotifyIcon.ContextMenuStrip.Items.Add("Exit").Click += (s, e) => ExitApp();
        }

        private void DisplayMainWindow()
        {
            if (main.IsVisible)
            {
                if (main.WindowState == WindowState.Minimized)
                {
                    main.WindowState = WindowState.Normal;
                }
                main.Activate();
            }
            else
            {
                main.Show();
            }
        }

        private void ExitApp()
        {
            _isExit = true;
            main.Close();
            MyNotifyIcon.Dispose();
            MyNotifyIcon = null;
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (!_isExit)
            {
                e.Cancel = true;
                main.Hide();
               
                NotificationBalloonTips.StartPopUp();
            }
        }
    }
}
