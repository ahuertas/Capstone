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
using CapstoneFinal.Navigation;
using CapstoneFinal.UserControls;
using CapstoneFinal.UserControls.RegisterUserControls;

namespace CapstoneFinal.UserControls.RegisterUserControls
{
    /// <summary>
    /// Interaction logic for RegisterSlideControlIntro.xaml
    /// </summary>
    public partial class RegisterSlideControlIntro : UserControl , ISwitchable
    {
        public MainWindow Main { get; private set; }
        public RegisterSlideControlIntro(MainWindow main)
        {
            InitializeComponent();
            this.Main = main;      
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void Login_btn_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new LoginControl(Main));
        }

        private void Next_btn_Click(object sender, RoutedEventArgs e)
        {
            //slide 1
            Switcher.Switch(new RAGControl1(this));
        }
    }
}
