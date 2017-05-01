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
using System.Windows.Shapes;
using CanvasApiLib.Objects;
namespace Capstone_Base_2.View
{
    /// <summary>
    /// Interaction logic for AssignmentWindow.xaml
    /// </summary>
    public partial class AssignmentWindow : Window
    {
        private Assignment currentTask;
        private MainWindow main;

        public AssignmentWindow(Assignment currentTask, MainWindow main)
        {
            InitializeComponent();
            this.currentTask = currentTask;
            this.main = main;
            UpdateData();
        }

        private void UpdateData()
        {

            lbl_Name.Content = "Assignment Name: " + currentTask.Name;
            lbl_Points.Content = "Points: " + currentTask.Points_Possible;

            text_Description.Text = FixDescription(currentTask.Description);

            lbl_Due_Date.Content = "Date: "+FixDateDisplay(currentTask.Due_at.ToString()) ;
        }

        private string FixDateDisplay(string data)
        {
            DateTime dateTime = Convert.ToDateTime(data);

            string conversion = dateTime.ToString("mm/dd/yy  hh:mm");

            return conversion;
        }

        private string FixDescription (string data)
        {
            string openBrackets = data.Replace("<p>", String.Empty);
            string closeBrackets = openBrackets.Replace("</p>", String.Empty);
            return closeBrackets;
        }

        private void Btn_GoBack_Click(object sender, RoutedEventArgs e)
        {
            main.Show();
            Close();
        }
    }
}
