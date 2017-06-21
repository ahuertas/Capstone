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

namespace Capstone_Base_2.View.RegisterViews
{
    /// <summary>
    /// Interaction logic for SecurityPhrase.xaml
    /// </summary>
    public partial class SecurityPhrase : Window
    {
        private string accesstoken = "";
        private string question, answer = "";
        private string[] data = new string[5];

        public SecurityPhrase()
        {
            InitializeComponent();
        }

        public SecurityPhrase(string accesstoken, string name, string newPassword)
        {
            this.accesstoken = accesstoken;
            data[0] = accesstoken;
            data[1] = name;
            data[2] = newPassword;
            InitializeComponent();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            question = SecurityQuest.Text;
            answer = SecurityAnswer.Text;
            if(question.Equals(""))
            {
                MessageBox.Show("Please enter a question that we can use");
            }
            else if(answer.Equals(""))
            {
                MessageBox.Show("Please enter a answer for your question");
            }
            else
            {
             data[3] = question;
             data[4] = answer;
             new ThankYouPage(data).Show();
             Close();
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            new UserName(accesstoken).Show();
            Close();
        }
    }
}
