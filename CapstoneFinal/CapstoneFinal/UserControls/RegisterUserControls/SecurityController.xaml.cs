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
using CapstoneFinal.UserControls;
using CapstoneFinal.UserControls.RegisterUserControls;
using MahApps.Metro.Controls.Dialogs;
using CapstoneFinal.Navigation;


namespace CapstoneFinal.UserControls.RegisterUserControls
{
    /// <summary>
    /// Interaction logic for SecurityController.xaml
    /// </summary>
    public partial class SecurityController : UserControl , ISwitchable
    {
        public UserNameControl UNControl { get; private set; }
        public  string Question { get; private set; }

        public  string Answer { get; private set; }

        public SecurityController(UserNameControl UNControl)
        {
            InitializeComponent();
            this.UNControl = UNControl;

            string name = UNControl.UserName;
            string pass = UNControl.NewPassword.ToString();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(UNControl);
        }

        private async void Next_Click(object sender, RoutedEventArgs e)
        {
            Question = SecurityQuest.Text;
            Answer = SecurityAnswer.Text;
            if (Question.Length == 0)
            {
                await UNControl.ATC.MainMetro.ShowMessageAsync("Question Problem","Please enter a question that we can use");
            }
            else if (Answer.Length == 0)
            {
                await UNControl.ATC.MainMetro.ShowMessageAsync("Answer Problem", "Please enter a answer for your question");
            }
            else
            {
                Switcher.Switch(new ThankYouControl(this));
            }

        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
    }
}
