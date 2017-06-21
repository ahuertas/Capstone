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
    /// Interaction logic for RegisterAccessGuide3.xaml
    /// </summary>
    public partial class RegisterAccessGuide3 : Window
    {
        public RegisterAccessGuide3()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            new RegisterAccessGuide2().Show();
            Close();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            new AccessToken().Show();
            Close();
        }
    }
}
