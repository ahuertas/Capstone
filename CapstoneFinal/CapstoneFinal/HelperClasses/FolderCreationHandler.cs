using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneFinal.HelperClasses
{
    public  class FolderCreationHandler
    {
        public static string Name { private get; set; }

        private static string DIRECTORY_NAME = @"C:\BuddyUsers";

        private  enum FolderResponse { SUCCESS, FAILURE};

        public static string FullFilePath { get; private set; }

        public string StartCreationProcess(String name)
        {
            Name = name;
            FullFilePath = DIRECTORY_NAME + "\\" + Name;
            CreateBuddyDirectory();
            return CreateFolder();
        }

        public void CreateBuddyDirectory()
        {
            if (!Directory.Exists(DIRECTORY_NAME))
            {
                Directory.CreateDirectory(DIRECTORY_NAME);
                DirectoryInfo di = new DirectoryInfo(DIRECTORY_NAME);
                //if hidden...
                if ((di.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                {
                    di.Attributes |= FileAttributes.Hidden;
                }
            }
        }

        public string CreateFolder()
        {
            string message = "";
            if (!DoesFolderExist(Name))
            {
                //create a string path to specify the subfolder under the top level folder
                Directory.CreateDirectory(Path.Combine(DIRECTORY_NAME, Name));
                message = FolderResponse.SUCCESS + " registration complete";
            }
            else
            {
                message = FolderResponse.FAILURE + ": user already exists";
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
