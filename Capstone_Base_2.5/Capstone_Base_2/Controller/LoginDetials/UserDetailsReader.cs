using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_Base_2.Controller.LoginDetials
{
    public static class  UserDetailsReader
    {
        private static string FILE_PATH = @"C:\BuddyUsers";
        public static string ReadFile(string PathName)
        {
            string personalInfo = "";
            try
            {
                personalInfo = File.ReadAllText(FILE_PATH + "\\" + PathName + "\\personalInfo.txt");
            }
            catch (Exception ex)
            {
                personalInfo = "FAIL";
                Console.Write(ex.Message);
            }
            return personalInfo;
        }

    }
}
