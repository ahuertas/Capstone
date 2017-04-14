using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using canvasApiLib.Objects;
using canvasApiLib.API;

namespace Base_Presentation.MainDetails
{
    /// <summary>
    /// Handles all the information displayed for the main window
    /// </summary>
   public class StudentInfoHandler
   {
        private static string ACCESS_TOKEN { get; set; }

        public StudentInfoHandler (string token)
        {
            ACCESS_TOKEN = token;
        }

        //method that gets the user's list of current classes
            //use linq to find classes whose last day did not pass current date
        public List<SchoolCourse> getCurrentClasses()
        {
            //List<SchoolCourse> classes

            return null;
        }



        //method that gets the user's todo list
            //the list will need some logic 
        

   }
}
