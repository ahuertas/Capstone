using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Capstone_Base.RegisterDetails
{
    /// <summary>
    /// creates a hidden folder with the user's name on it
    /// </summary>
    public class HiddenFolderCreator
    {
        private string UserName { get; set; }
        private static string DirectoryName  = @"C:\BuddyUsers"; 
        private enum Response {SUCCESS,FAILURE};
      
        public HiddenFolderCreator(string path)
        {
            UserName = path;
            CreateTopLevelFolder();
        }

        protected void CreateTopLevelFolder()
        {
           
            //if the main folder exists already create the sub folder, else create the directory first then subfolder.
            if (!Directory.Exists(DirectoryName))
            {
                Directory.CreateDirectory(DirectoryName);
                DirectoryInfo di = new DirectoryInfo(DirectoryName);
                //if hidden...
                if((di.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden )
                { 
                    di.Attributes |= FileAttributes.Hidden;
                }
              
            }
        }


         public string CreateFolder()
        {
            string message = "";
            if (!DoesFolderExist(UserName))
            {
                
                 //create a string path to specify the subfolder under the top level folder
                 string pathString = Path.Combine(DirectoryName, UserName);
                 Directory.CreateDirectory(pathString);
                 message = Response.SUCCESS + " registration complete";
            }
            else
            {
                message = Response.FAILURE + ": user already exists";
            }
            return message;
        }

        public string GetFullPath()
        {
            return DirectoryName + "\\" + UserName;
        }

        public bool DoesFolderExist(string foldername)
        {
            string pathString = Path.Combine(DirectoryName,foldername);
            bool isExist = false;
            if(Directory.Exists(pathString))
            {
                isExist = true;
            }
            return isExist;
        }


    }
}
