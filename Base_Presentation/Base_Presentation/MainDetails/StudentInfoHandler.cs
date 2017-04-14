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
        private static string LMS_URL { get; set; }

        public StudentInfoHandler (string token)
        {
            ACCESS_TOKEN = token;
            LMS_URL = "https://lms.neumont.edu";
        }

        //method that gets the user's list of current classes
            //use linq to find classes whose last day did not pass current date
        public async Task<List<SchoolCourse>> GetCurrentClasses()
        {
            List<SchoolCourse> currentCourses = new List<SchoolCourse>();
            try
            {
               List<SchoolCourse> classes = await ClsCourseApi.GetCourses(ACCESS_TOKEN, LMS_URL, 0);

                foreach (SchoolCourse active in classes)
                {
                    //get each end date of course 
                    DateTime dt = Convert.ToDateTime(active.End_at);
                    if(DateTime.Now < dt)
                    {
                        currentCourses.Add(active);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write("Error thrown " + ex.Message);
            }


            return currentCourses;
        }



        //method that gets the user's todo list
            //the list will need some logic 
       public async Task<List<Assignment>> GetTodoList()
       {
            List<Assignment> not_submitted = new List<Assignment>();
            try
            {
                List<Assignment> tasks = await ClsCourseApi.List_Todo(ACCESS_TOKEN,LMS_URL,0);

                foreach (Assignment task in tasks)
                {
                    if(!task.Has_submitted)
                    {
                        not_submitted.Add(task);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.Write("Error thrown");
            }

            return not_submitted;
       }
        

   }
}
