using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Base_Presentation.LoginDetails
{
    public class ReadUserDetails
    {

        private string PathName { get; set; }
        private static string FILE_PATH = @"C:\BuddyUsers";
        public ReadUserDetails (string PathName)
        {
            this.PathName = PathName;
        }

        public string ReadFile()
        {
            string personalInfo = "";
            try
            {
                 personalInfo = File.ReadAllText(FILE_PATH +"\\" +PathName + "\\personalInfo.txt");
            }
            catch (Exception ex)
            {
                personalInfo = "FAIL";
            }
            return personalInfo;
        }
    }
}
