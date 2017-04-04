using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace NU_Base_Progam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer dtClockTime = new DispatcherTimer();

            dtClockTime.Interval = new TimeSpan(0, 0, 1); //in Hour, Minutes, Second.
            dtClockTime.Tick += dtClockTime_Tick;

            dtClockTime.Start();
        }

        private void dtClockTime_Tick(object sender, EventArgs e)
        {
            lblClockTime.Content = DateTime.Now.ToLongTimeString();
        }
    }
}
