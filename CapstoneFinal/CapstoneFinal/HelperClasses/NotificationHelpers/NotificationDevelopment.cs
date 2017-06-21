using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace CapstoneFinal.HelperClasses.NotificationHelpers
{
    public class NotificationDevelopment
    {
        public  MainWindow MainView { get; set; }
        private NotifyIcon notify;
        private bool _isExit;

        public void NotificationSetup()
        {
            MainView.Closing += MainView_Closing;
            IconSetup();
            CreateContextMenu();
        }
        #region important methods
        private void IconSetup()
        {
            notify = new NotifyIcon();
            NotificationBalloonTips.Notify = notify;
            notify.Icon = new System.Drawing.Icon("Buddybleu.ico");
            notify.DoubleClick += (d, args) => DisplayMainWindow();
            notify.Visible = true;
        }


        private void CreateContextMenu()
        {
            notify.ContextMenuStrip = new ContextMenuStrip();

            notify.ContextMenuStrip.Items.Add("MainWindow").Click += (s, e) => DisplayMainWindow();
            notify.ContextMenuStrip.Items.Add("Exit").Click += (s, e) => ExitApp();
        
        }
        #endregion

        #region Events
        private void MainView_Closing(object sender, CancelEventArgs e)
        {
            if (!_isExit)
            {
                e.Cancel = true;
                MainView.Hide();

                NotificationBalloonTips.StartPopUp();
            }
        }

        private void DisplayMainWindow()
        {
            if (MainView.IsVisible)
            {
                if (MainView.WindowState == WindowState.Minimized)
                {
                    MainView.WindowState = WindowState.Normal;
                }
                MainView.Activate();
            }
            else
            {
                MainView.Show();
            }
        }

        private void ExitApp()
        {
            _isExit = true;
            MainView.Close();
            notify.Dispose();

            notify = null;
        }
        #endregion
    }
}
