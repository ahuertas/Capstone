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
    /// Interaction logic for RAGControl1.xaml
    /// </summary>
    public partial class RAGControl1 : UserControl , ISwitchable
    {
        public MainWindow Main { get; private set; }
        public RegisterSlideControlIntro RSC { get; private set; }
        public RAGControl1(RegisterSlideControlIntro RSC)
        {
            InitializeComponent();
            this.RSC = RSC;
            this.Main = RSC.Main;
        }



        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void Back_btn_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(RSC);
        }

        private void Next_btn_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new RAGControl2(this));
        }
    }
}
