using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Base_Presentation.RegisterDetails
{
    public  class WriteUserDetails
    {
        //writes the user password and accessToken to file.

        //double encrypt the file. << later
        private string Password { get; set; }
        private string AccessToken { get; set; }
        private string FilePath { get; set; }

        public WriteUserDetails(string password, string accessToken, string folder)
        {
            Password = password;
            AccessToken = accessToken;
            FilePath = folder;
        }

        public void WriteFile()
        {
            //write the folder to the specified path;
            StreamWriter file = new StreamWriter(FilePath + "\\personalInfo.txt");
            file.Write("Password:" + Password + " AccessToken:" + AccessToken);
            file.Close();
        }

        
    }
}
