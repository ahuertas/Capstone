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
    /// Interaction logic for RAGControl3.xaml
    /// </summary>
    public partial class RAGControl3 : UserControl, ISwitchable
    {
        public RAGControl2 guidePart2 { get; private set; }
        public RAGControl3(RAGControl2 guidePart2)
        {
            InitializeComponent();
            this.guidePart2 = guidePart2;
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void Back_btn_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(guidePart2);
        }

        private void Next_btn_Click(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new AccessTokenControl(this));
        }
    }
}
