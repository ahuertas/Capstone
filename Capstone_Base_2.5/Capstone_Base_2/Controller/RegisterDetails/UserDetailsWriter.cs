using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_Base_2.Controller.RegisterDetails
{
    /// <summary>
    /// This class will write the user information to a personalinfo.txt
    /// </summary>
    public static class UserDetailsWriter
    {
        //private string FilePath, Password, AccessToken, SecretKey;

        public static void WriteFileDetails(string FilePath,string Password, string AccessToken, string SecretQuest, string SecretAnswer)
        {
            string trueFilePath = FilePath + "\\personalInfo.txt";
            StreamWriter file = new StreamWriter(FilePath + "\\personalInfo.txt");
            file.Write("Password:" + Password + " AccessToken:" + AccessToken + " SecretQuestion:"+SecretQuest + " SecretAnswer:" + SecretAnswer);
            file.Flush();
            file.Close();
            File.Encrypt(FilePath + "\\personalInfo.txt");

        }
       
    }
}
