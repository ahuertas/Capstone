using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Security.Cryptography;

namespace CapstoneFinal.HelperClasses
{
   public static class UserDetailsHandler
    {
        private static string FILE_PATH = @"C:\BuddyUsers";

        public static string FilePath;
        public static string Password;
        public static string AccessToken;
        public static string SecretQuest;
        public static string SecretAnswer;



        public static string ReadUserDetails(string PathName)
        {
            string personalDetail = string.Empty;

            try
            {
                personalDetail = File.ReadAllText(FILE_PATH + "\\" + PathName + "\\personalInfo.txt");
            } 
            catch(Exception ex)
            {
                personalDetail = "FAIL";
               
            }

            return personalDetail;
        }

        public static void WriteUserDetails()
        {
            string trueFilePath =   FILE_PATH +"\\"+FilePath + "\\personalInfo.txt";
            StreamWriter file = new StreamWriter(trueFilePath);
            file.Write("Password:" + Password + ";AccessToken:" + AccessToken + ";SecretQuestion:" + SecretQuest + ";SecretAnswer:" + SecretAnswer);
            file.Flush();
            file.Close();
           

        }


    }
}
