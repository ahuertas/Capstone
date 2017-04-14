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
using Base_Presentation.MainDetails;
using canvasApiLib;
using canvasApiLib.Objects;

namespace Base_Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string Token { get; set; }
        private StudentInfoHandler studentInfo;
        private List<SchoolCourse> currentClasses = new List<SchoolCourse>();
        private List<Assignment> currentTasks = new List<Assignment>();

        public MainWindow()
        {

        }

        public MainWindow(string token)
        {
            this.Token = token;
            studentInfo = new StudentInfoHandler(token);

            LoadData().Wait();
            DisplayText();

            InitializeComponent();
        }

        public async Task LoadData ()
        {
            //load current classes
            currentClasses = await studentInfo.GetCurrentClasses();

            //load current tasks
            currentTasks = await studentInfo.GetTodoList();
        }
    
        private void DisplayText ()
        {
            string classes = string.Empty;
            string tasks = string.Empty;
            foreach (var Class in currentClasses)
            {
                classes += Class.Name + " \n";
            }

            foreach (var task in currentTasks)
            {
                tasks += task.Name + " due at " + task.Due_at + "\n";
            }

            classTextBox.Text = classes;
            taskTextBox.Text = tasks;
        }

    }
}
