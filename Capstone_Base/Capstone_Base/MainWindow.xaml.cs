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
using System.Windows;
using System.Windows.Threading;
using System.Threading;

namespace Capstone_Base
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Object mylock = new Object();
        private string token;
        private string classes = " ";
        private string tasks = " ";
        private StudentInfoHandler studentInfo;
        private List<SchoolCourse> currentClasses = new List<SchoolCourse>();
        private List<Assignment> currentTasks = new List<Assignment>();

        public MainWindow()
        {

        }

        public  MainWindow(string token)
        {
            this.token = token;
            InitializeComponent();
            Run().Wait();

            //classBox.Items.Add(classes);
            //Console.WriteLine(classes);

            //taskBox.Items.Add(tasks);
            //Console.WriteLine(tasks);
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
                    MessageBox.Show(err.Message);
                }

            }
            ));
        }

    }
}
