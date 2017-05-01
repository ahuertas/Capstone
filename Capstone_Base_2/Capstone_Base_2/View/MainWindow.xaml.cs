using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Capstone_Base_2.Controller.MainDetails;
using CanvasApiLib.Objects;
using System.Windows.Threading;
using System.Threading;
using Capstone_Base_2.View;
using Capstone_Base_2.Controller.NofitcationDetails;
namespace Capstone_Base_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string name;
        private List<SchoolCourse> classes;
        private List<Assignment> totalAssignments;
        private AssignmentWindow AssignWin;
        private AlertTimer altTimer;
       
        public MainWindow(string token, string name) 
        {
            this.name = name;
            StudentInfoHandler.ACCESS_TOKEN = token;
            AlertLogic.ACCESS_TOKEN = token;
            InitializeComponent();

            TotalSetUp().Wait();

            UpdateComboBox();

            new NotificationIcon(this);

            altTimer = new AlertTimer(this);
            altTimer.StartTimer();
            AlertLogic.currentList = totalAssignments;
           
        }
        protected async Task TotalSetUp ()
        {
            lbl_Name.Content = name;
            dateBox.Text = DateTime.Now.ToString("MMMM dd, yyyy");
            classes = await StudentInfoHandler.GetCurrentClasses().ConfigureAwait(false);
            totalAssignments = await StudentInfoHandler.GetTodolist().ConfigureAwait(false);
        }

        #region UIChange Triggers 
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //when the course is selected Update the tasks
            string course = ClassCBox.SelectedItem.ToString();
            FilterForListBox(course);

        }

        private void Assignments_List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string assignment = Assignments_List.SelectedItem.ToString();
            Assignment currentAssignment = totalAssignments.First(item => item.Name.Equals(assignment));
            AssignWin = new AssignmentWindow(currentAssignment, this);
            Hide();
            AssignWin.Show();
        }

        private void Participation_List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string assignment = Participation_List.SelectedItem.ToString();
            Assignment currentAssignment = totalAssignments.First(item => item.Name.Equals(assignment));
            AssignWin = new AssignmentWindow(currentAssignment, this);
            Hide();
            AssignWin.Show();
        }
        #endregion
        #region Update to UI 
        private void UpdateComboBox()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Input, new ThreadStart(() =>
            {
                foreach (SchoolCourse course in classes)
                {
                    ClassCBox.Items.Add(course.Name);
                }

            }));
        }

        private void FilterForListBox(string course)
        {
            SchoolCourse currentcourse = classes.First(item => item.Name.Equals(course));          
            List<Assignment> classRelated = FilterClasses.FilteAssignments(currentcourse, totalAssignments);
            List<Assignment> particpation = FilterClasses.FilterParticaption(classRelated);
            UpdateListBoxes(classRelated,particpation);
        }

        private void UpdateListBoxes(List<Assignment> assignments, List<Assignment> participations)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Input, new ThreadStart(() =>
            {
                foreach (Assignment assignment in assignments)
                {
                    Assignments_List.Items.Add(assignment.Name);
                }

                foreach (Assignment participation in participations)
                {
                    Participation_List.Items.Add(participation.Name);
                }

            }));


        }
        #endregion

        #region Timer methods

        public void NeccessaryUpdate()
        {
            if(AlertLogic.isUpdateable)
            {
                totalAssignments = AlertLogic.refreshedList;
            }
        }
        #endregion

    }
}
