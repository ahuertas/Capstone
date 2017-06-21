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
using MahApps.Metro.Controls;
using CapstoneFinal.ViewModels;

namespace CapstoneFinal
{
    /// <summary>
    /// Interaction logic for AssignmentWindow.xaml
    /// </summary>
    public partial class AssignmentWindow 
    {
        List<AssignmentViewModel> tasks = new List<AssignmentViewModel>();
        List<SchoolCourseViewModel> course = new List<SchoolCourseViewModel>();
        //MAx height 501
        //Max width 774

        //pass in the data
        public AssignmentWindow()
        {
            InitializeComponent();
            this.Closing += AssignmentWindow_Closing;
         
        }

        private void AssignmentWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        public void OpenTab(AssignmentViewModel task, SchoolCourseViewModel course)
        {
           
            TabItem tab = CreateTab(task,course);

            if(!AssigmentTabControl.Items.Contains(tab))
            {
                AssigmentTabControl.Items.Add(tab);
            }
           
        }
      
        private TabItem CreateTab(AssignmentViewModel task, SchoolCourseViewModel course)
        {
            TabItem tab1 = new TabItem();
            tab1.Header = task.Name;
            
            //tab1.Margin = new Thickness(0,0,-387,-250);
            //Margin = "0,0,-387,-250.2"
            tab1.VerticalAlignment = VerticalAlignment.Stretch;
            tab1.HorizontalAlignment = HorizontalAlignment.Stretch;
            tab1.Content = CreateFormWithin(task, course);
            
            return tab1;
        }

        private Grid CreateFormWithin(AssignmentViewModel task, SchoolCourseViewModel course)
        {
            Grid grid1 = new Grid();
            grid1.HorizontalAlignment= HorizontalAlignment.Stretch;
            grid1.VerticalAlignment = VerticalAlignment.Stretch;
           // grid1.Height = '*';
            //grid1.Width = '*';

            Label Task_Name = new Label();
            //<Label x:Name="lbl_Name" Content="Label" HorizontalAlignment="Left" Margin="10,24,0,0" VerticalAlignment="Top" Width="236"/>
            Task_Name.HorizontalAlignment = HorizontalAlignment.Stretch;
            Task_Name.VerticalAlignment = VerticalAlignment.Stretch;
            //Task_Name.Width = 436;
            Task_Name.Content = "Assignment Name : " +  task.Name;
            Task_Name.Margin = new Thickness(10,24,0,0);
            Task_Name.FontSize = 20;
            grid1.Children.Add(Task_Name);

            Label Points_Poss = new Label();
            //<Label x:Name="lbl_Points" Content="Label" HorizontalAlignment="Left" Margin="10,63,0,0" VerticalAlignment="Top" Width="224"/>
            Points_Poss.HorizontalAlignment = HorizontalAlignment.Stretch;
            Points_Poss.VerticalAlignment = VerticalAlignment.Stretch;
           // Points_Poss.Width = 444;
            Points_Poss.Content = "Assignment value :" + task.Points_Possible + " pts";
            Points_Poss.Margin = new Thickness(10,64,0,0);
            Points_Poss.FontSize = 20;
            grid1.Children.Add(Points_Poss);

            Label class_Name = new Label();
            class_Name.HorizontalAlignment = HorizontalAlignment.Stretch;
            class_Name.VerticalAlignment = VerticalAlignment.Stretch;
            //class_Name.Width = 444;
            class_Name.Content = "Class Name : " + course.Name;
            class_Name.Margin = new Thickness(10,44,0,0);
            class_Name.FontSize = 20;
            grid1.Children.Add(class_Name);


            Label Due_Date = new Label();
            //<Label x:Name="lbl_Due_Date" Content="Label" HorizontalAlignment="Left" Margin="10,94,0,0" VerticalAlignment="Top" Width="224" Height="28"/>
            Due_Date.HorizontalAlignment = HorizontalAlignment.Stretch;
            Due_Date.VerticalAlignment = VerticalAlignment.Stretch;
          //  Due_Date.Width = 444;
           // Due_Date.Height = 128;
            Due_Date.Margin = new Thickness(10,84,100,0);
            Due_Date.Content = "Due at : " +  Convert.ToDateTime(task.Due_at).ToString("MM, dd, yyyy, hh:mm:ss");
            Due_Date.FontSize = 20;
            grid1.Children.Add(Due_Date);


            WebBrowser web1 = new WebBrowser();
            //<WebBrowser x:Name="text_Description" HorizontalAlignment="Left" Height="188" Margin="10,148,0,0"  VerticalAlignment="Top" Width="374" />
            web1.HorizontalAlignment = HorizontalAlignment.Stretch;
            web1.VerticalAlignment = VerticalAlignment.Stretch;
           // web1.Height = 401;
           // web1.Width = 774;
           if(task.Description != null)
            {
                web1.NavigateToString(task.Description);
            } else
            {
                web1.NavigateToString(" ");
            }
            web1.Margin = new Thickness(10,148,0,0);
            grid1.Children.Add(web1);

            return grid1;
        }

       

        /*
         * <TabItem Header="TabItem">
                <Grid Background="White"/>
            </TabItem>
            <TabItem Header="TabItem">
                <Grid Background="White"/>
            </TabItem>
         */
    }

    /*
     * <Grid>
        <Label x:Name="lbl_Name" Content="Label" HorizontalAlignment="Left" Margin="10,24,0,0" VerticalAlignment="Top" Width="236"/>
        <Label x:Name="lbl_Points" Content="Label" HorizontalAlignment="Left" Margin="10,63,0,0" VerticalAlignment="Top" Width="224"/>

        <Button x:Name="Btn_GoBack" Content="close" HorizontalAlignment="Left" Margin="309,341,0,0" VerticalAlignment="Top" Width="75" Click="Btn_GoBack_Click"/>
        <Label x:Name="lbl_Due_Date" Content="Label" HorizontalAlignment="Left" Margin="10,94,0,0" VerticalAlignment="Top" Width="224" Height="28"/>
        <WebBrowser x:Name="text_Description" HorizontalAlignment="Left" Height="188" Margin="10,148,0,0"  VerticalAlignment="Top" Width="374" />

    </Grid>
     */
}
