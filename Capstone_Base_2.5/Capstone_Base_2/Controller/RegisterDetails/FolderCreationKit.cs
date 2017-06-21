using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_Base_2.Controller.RegisterDetails
{
    public class FolderCreationKit
    {
        private string UserName;
        private static string DIRECTORY_NAME = @"C:\BuddyUsers";
        private static string PDF_DIRECTORY = @".\PDF\NU Buddy Guide to retrieving an access token.pdf";
        private enum Response { SUCCESS, FAILURE };
        public string FileFullPath { get; set; }

        public FolderCreationKit(string UserFolderPath)
        {
            UserName = UserFolderPath;
            FileFullPath = DIRECTORY_NAME +
                "\\"+UserName;
            CreateTopLevelFolder();
        }

        public void CreateTopLevelFolder()
        {
            if (!Directory.Exists(DIRECTORY_NAME))
            {
                Directory.CreateDirectory(DIRECTORY_NAME);
                DirectoryInfo di = new DirectoryInfo(DIRECTORY_NAME);
                //if hidden...
                if ((di.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                {
                    di.Attributes |= FileAttributes.Hidden;
                  //  File.Copy(PDF_DIRECTORY, DIRECTORY_NAME, true);

                }

            }
        }

        public string CreateFolder()
        {
            string message = "";
            if (!DoesFolderExist(UserName))
            {
                //create a string path to specify the subfolder under the top level folder
                Directory.CreateDirectory(Path.Combine(DIRECTORY_NAME, UserName));
                message = Response.SUCCESS + " registration complete";
            }
            else
            {
                message = Response.FAILURE + ": user already exists";
            }
            return message;
        }

        protected bool DoesFolderExist(string foldername)
        {
            string pathString = Path.Combine(DIRECTORY_NAME, foldername);
            bool isExist = false;
            if (Directory.Exists(pathString))
            {
                isExist = true;
            }
            return isExist;
        }

    }
}
