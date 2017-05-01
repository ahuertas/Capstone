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
using System.IO;
using System.Diagnostics;

namespace Capstone_Base_2.View
{
    /// <summary>
    /// Interaction logic for PDFWindow.xaml
    /// </summary>
    public partial class PDFWindow : Window
    {
        public PDFWindow()
        {
            InitializeComponent();

            //Pass in the relevant PDF filename to the user control, ensure you use the correct path to the file
            //var uc = new PDFController(@"PDF\NU Buddy Guide to retrieving an access token.pdf");

            //Set the user control to be hosted in the Windows Forms Host


            string path =  @"C:\Users\ahuer\Desktop\Capstone\Capstone\Capstone_Base_2\Capstone_Base_2\PDF\NU Buddy Guide to retrieving an access token.pdf";
            var uc = new PDFController(path);
            this.FormHost.Child = uc;

        }
    }
}
