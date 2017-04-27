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
using Capstone_Base.MainDetails;
using CanvasApiLib.Objects;
using System.Windows.Forms;
using System.Windows.Threading;
using System.Threading;
using System.Drawing;
using System.ComponentModel;

namespace Capstone_Base
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Object mylock = new Object();
        private string token;
        private NotifyIcon MyNotifyIcon;
        private StudentInfoHandler studentInfo;
        private List<SchoolCourse> currentClasses;
        private List<Assignment> currentTasks;
        private bool _isExit;

        public  MainWindow(string token)
        {
            this.token = token;
            InitializeComponent();
            this.Closing += MainWindow_Closing;
        
            currentClasses  = new List<SchoolCourse>();
            currentTasks = new List<Assignment>();

            Run().Wait();
            NotifationSetUp();
            CreateContextMenu();
        }

        public async Task Run()
        {
            studentInfo = new StudentInfoHandler(token);

            await LoadData().ConfigureAwait(false);

            DisplayText();
        }

        public async Task LoadData()
        {
            //load current classes
            currentClasses = await studentInfo.Getcurrentclasses().ConfigureAwait(false);

            //load current tasks
            currentTasks = await studentInfo.GetTodolist(). ConfigureAwait(false);
        }

        private void DisplayText()
        {

            Dispatcher.BeginInvoke(DispatcherPriority.Input, new ThreadStart(() =>
            {
                try
                {
                    foreach (var Class in currentClasses)
                    {
                        classBox.Items.Add(Class.Name);
                    }

                    foreach (var task in currentTasks)
                    {
                        taskBox.Items.Add(task.Name + "\n" +
                        "due at :" + task.Due_at + " \n" +
                        task.Points_Possible + "\n");
                    }
                }
                catch (Exception err)
                {
                    System.Windows.MessageBox.Show(err.Message);
                }

            }
            ));
        }
       
        private void NotifationSetUp()
        {
            MyNotifyIcon = new System.Windows.Forms.NotifyIcon();
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
            if(IsVisible)
            {
                if(this.WindowState == WindowState.Minimized)
                {
                    this.WindowState = WindowState.Normal;
                }
                this.Activate();
            }
            else
            {
                Show();
            }
        }

        private void ExitApp()
        {
            _isExit = true;
            this.Close();
            MyNotifyIcon.Dispose();
            MyNotifyIcon = null;
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if(!_isExit)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

    }
}
