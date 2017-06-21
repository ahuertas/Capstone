using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CapstoneFinal.Navigation;
using CapstoneFinal.HelperClasses.ApiHelperClasses;
using System.Collections.ObjectModel;
using CapstoneFinal.ViewModels;
using CanvasApiLib.JsonObjects;
using System.Windows.Threading;
using System.Threading;
using System.Linq;
using CapstoneFinal.HelperClasses.NotificationHelpers;

namespace CapstoneFinal.UserControls
{
    /// <summary>
    /// Interaction logic for MainWindowControl.xaml
    /// </summary>
    public partial class MainWindowControl : UserControl
    {
        #region Variables
        private DistractionEliminator DE;
        public MainWindow MainView { get; private set; }
        private AssignmentWindow assignment;
        private NotificationTimer notifyTimer;
        private NotificationDevelopment notifyDev;
        private ObservableCollection<AssignmentViewModel> currentAssignments = new ObservableCollection<AssignmentViewModel>();
        private ObservableCollection<GradeViewModel> currentGrades = new ObservableCollection<GradeViewModel>();
        private ObservableCollection<SchoolCourseViewModel> currentCourses = new ObservableCollection<SchoolCourseViewModel>();
        private List<SchoolCourse> currentCoursesJson = new List<SchoolCourse>();
        private List<Assignment> currentAssignmentsJson = new List<Assignment>();
        private List<Enrollment> currentEnrollmentsJson = new List<Enrollment>();

        private string AccessToken;
        private string User;
        private bool OnStartUp = true;
        private bool alreadyCalled = false;
        #endregion

        public MainWindowControl(MainWindow main, string AccessToken, string username)
        {
            GlobalPopulate(main, AccessToken, username);
        }

        private void GlobalPopulate(MainWindow main, string AccessToken, string username)
        {
            #region setting variables
            this.MainView = main;
            this.AccessToken = AccessToken;
            this.User = username;
            StudentInfoHandler.ACCESS_TOKEN = this.AccessToken;
            NotificationLogic.ACCESS_TOKEN = this.AccessToken;
            #endregion

            InitializeComponent();

            #region notification Setup
            ResizeMainWindowToOriginal();
            notifyDev = new NotificationDevelopment();
            notifyDev.MainView = this.MainView;
            notifyDev.NotificationSetup();
            #endregion

            #region Important windows
            DE = new DistractionEliminator(this);
            assignment = new AssignmentWindow();
            #endregion

            #region Timers
            notifyTimer = new NotificationTimer(this);
            notifyTimer.StartTimer();
            NotificationLogic.currentList = currentAssignmentsJson;

            MasterCalendar.IsTodayHighlighted = true;
            #endregion
            //Methods of importance
            #region API Data setup
            Dispatcher.BeginInvoke(DispatcherPriority.Input, new ThreadStart(async () =>
            {
                await RetrieveData();
                ConvertJsonToObservable();
                CalendarAssignmentDate();
                PopulateClassComboBox();
            }));

            //ProcessRing.IsActive = false;
            OnStartUp = false;
            #endregion
        }

        public void ResizeMainWindowToOriginal()
        {
            MainView.Hide();
            MainView.Height = 750;
            MainView.Width = 1200;
            MainView.Show();
            //ProcessRing.IsActive = true;
        }

        public void NeccessaryUpdate()
        {
            if (NotificationLogic.IsUpdateable)
            {
                currentAssignmentsJson = NotificationLogic.RefreshedList;
            }
        }

        ////////LOGIC /////////////////////
        #region API Logic
        protected async Task RetrieveData()
        {
            UserName_lbl.Content = "Welcome, " + User;
            Date_lbl.Content = "Today's Date : " + DateTime.Now.ToString("MMM dd, yyyy");

            currentCoursesJson = await StudentInfoHandler.GetCurrentClasses().ConfigureAwait(false);
            currentAssignmentsJson = await StudentInfoHandler.GetTodolist().ConfigureAwait(false);
            currentEnrollmentsJson = await StudentInfoHandler.GetStudentEnrollment().ConfigureAwait(false);


        }

        public void ConvertJsonToObservable()
        {
            //start with courses
            foreach (SchoolCourse course in currentCoursesJson)
            {
                SchoolCourseViewModel courseViewModel = new SchoolCourseViewModel();
                courseViewModel.Id = course.Id;
                courseViewModel.Name = course.Name;
                courseViewModel.Start_at = course.Start_at;
                courseViewModel.End_at = course.End_at;

                currentCourses.Add(courseViewModel);
            }

            foreach (Assignment task in currentAssignmentsJson)
            {
                AssignmentViewModel taskViewModel = new AssignmentViewModel();
                taskViewModel.Points_Possible = task.Points_Possible;
                taskViewModel.Name = task.Name;
                taskViewModel.Description = task.Description;
                taskViewModel.Due_at = task.Due_at;
                taskViewModel.Html_Url = task.Html_Url;
                taskViewModel.Course_Id = task.Course_id;
                currentAssignments.Add(taskViewModel);
            }

            //to get the grades I need to loop through current course AND enrollment if the ids match create a grade object
            foreach (SchoolCourse course in currentCoursesJson)
            {
                foreach (Enrollment enroll in currentEnrollmentsJson)
                {
                    if (enroll.Course_Id == course.Id)
                    {
                        GradeViewModel currentGradeModel = new GradeViewModel();

                        currentGradeModel.Current_grade = enroll.Grade.Current_grade;
                        currentGradeModel.Current_Score = enroll.Grade.Current_Score;
                        currentGradeModel.ID = enroll.Course_Id;

                        currentGrades.Add(currentGradeModel);
                    }

                }
            }

        }
        #endregion

        #region View Data

        private void PopulateClassComboBox()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Input, new ThreadStart(() =>
            {
                foreach (SchoolCourseViewModel course in currentCourses)
                {
                    ClassListBox.Items.Add(course.Name);
                }
            }));
        }

        private void CalendarAssignmentDate()
        {
            DateTime dueDate;
            List<DateTime> allDueDates = new List<DateTime>();
            foreach (AssignmentViewModel task in currentAssignments)
            {
                dueDate = Convert.ToDateTime(task.Due_at);
                allDueDates.Add(dueDate);
            }

            foreach (DateTime date in allDueDates)
            {
                MasterCalendar.SelectedDates.Add(date);
            }

        }

        private void MasterCalendar_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!OnStartUp)
            {
                //grab the selected date and update the assignments and particpations
                string date = MasterCalendar.SelectedDate.ToString();
                //pass in to the method
                date = Convert.ToDateTime(date).ToString("MM/dd/yyyy");

                UpdateListViewBasedOnDate(date);
                alreadyCalled = false;
            }

        }

        private void UpdateListViewBasedOnDate(string date)
        {
            if (!alreadyCalled)
            {
                ParticipationList.Items.Clear();
                AssignmentList.Items.Clear();
                foreach (AssignmentViewModel task in currentAssignments)
                {
                    double points = Convert.ToDouble(task.Points_Possible);
                    if (task.Name.Length > 5 && points > 5)
                    {
                        //update assignment
                        string Taskdate = Convert.ToDateTime(task.Due_at).ToString("MM/dd/yyyy");
                        if (Taskdate.Equals(date))
                        {
                            Dispatcher.BeginInvoke(DispatcherPriority.Input, new ThreadStart(() =>
                            {
                                AssignmentList.Items.Add(task.Name);
                            }));
                        }
                    }
                    else
                    {
                        //update participaton
                        string Taskdate = Convert.ToDateTime(task.Due_at).ToString("MM/dd/yyyy");
                        if (Taskdate.Equals(date))
                        {
                            Dispatcher.BeginInvoke(DispatcherPriority.Input, new ThreadStart(() =>
                            {
                                ParticipationList.Items.Add(task.Name);
                            }));
                        }

                    }
                }
                alreadyCalled = true;
            }
            CalendarAssignmentDate();

        }

        private void FilterAssignments(string course)
        {
            SchoolCourse currentcourse = currentCoursesJson.First(item => item.Name.Equals(course));
            List<Assignment> classRelated = FilterClasses.FilteAssignments(currentcourse, currentAssignmentsJson);
            List<Assignment> particpation = FilterClasses.FilterParticaption(classRelated);
            List<Assignment> classRelatedFilter = new List<Assignment>();
            GradeViewModel classGrade = new GradeViewModel();

            foreach (Assignment task in classRelated)
            {
                if (!particpation.Contains(task))
                {
                    classRelatedFilter.Add(task);
                }
            }

            foreach (GradeViewModel grade in currentGrades)
            {
                if (grade.ID == currentcourse.Id)
                {
                    classGrade = grade;
                }
            }


            UpdateListViews(classRelatedFilter, particpation, classGrade);
        }

        private void UpdateListViews(List<Assignment> assignments, List<Assignment> participations, GradeViewModel classGrade)
        {
            GradeList.Items.Clear();
            AssignmentList.Items.Clear();
            ParticipationList.Items.Clear();
            Dispatcher.BeginInvoke(DispatcherPriority.Input, new ThreadStart(() =>
            {
                foreach (Assignment assignment in assignments)
                {
                    AssignmentList.Items.Add(assignment.Name);
                }

                foreach (Assignment participation in participations)
                {
                    ParticipationList.Items.Add(participation.Name);
                }

                GradeList.Items.Add("Current Grade : " + classGrade.Current_grade);
                GradeList.Items.Add("Current Score :" + classGrade.Current_Score);
            }));


        }

        #endregion

        #region Button Press
        private void ClassListBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            string selected_course = ClassListBox.SelectedItem.ToString();
            FilterAssignments(selected_course);
        }

        private void Distract_Btn_Click(object sender, RoutedEventArgs e)
        {
            DE.Resize();
            Switcher.Switch(DE);
        }

        #endregion

        #region ListChanged
        private void AssignmentList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AssignmentList.SelectedItem != null)
            {
                string name = AssignmentList.SelectedItem.ToString();
                AssignmentViewModel AVM = new AssignmentViewModel();
                SchoolCourseViewModel SCM = new SchoolCourseViewModel();

                foreach (AssignmentViewModel task in currentAssignments)
                {
                    if (task.Name.Equals(name))
                    {
                        AVM = task;
                    }

                }

                foreach (SchoolCourseViewModel course in currentCourses)
                {
                    if (course.Id == AVM.Course_Id)
                    {
                        SCM = course;
                    }
                }

                assignment.OpenTab(AVM, SCM);
                assignment.Show();
            }


        }

        private void ParticipationList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string name = ParticipationList.SelectedItem.ToString();
            AssignmentViewModel AVM = new AssignmentViewModel();
            SchoolCourseViewModel SCM = new SchoolCourseViewModel();

            foreach (AssignmentViewModel task in currentAssignments)
            {
                if (task.Name.Equals(name))
                {
                    AVM = task;
                }

            }

            foreach (SchoolCourseViewModel course in currentCourses)
            {
                if (course.Id == AVM.Course_Id)
                {
                    SCM = course;
                }
            }

            assignment.OpenTab(AVM, SCM);
            assignment.Show();
        }
        #endregion
    }
}
