using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CanvasApiLib.Objects;
using CanvasApiLib.API;
using System.IO;

namespace Capstone_Base.MainDetails
{
    /// <summary>
    /// handles all the information displayed for the main window
    /// </summary>
    public class StudentInfoHandler
    {
        private static string Access_token { get; set; }
        private static string Lms_url { get; set; }

        public StudentInfoHandler(string token)
        {
            Access_token = token;
            Lms_url = "https://lms.neumont.edu";
        }

        //method that gets the user's list of current classes
        //use linq to find classes whose last day did not pass current date
        public async Task<List<SchoolCourse>> Getcurrentclasses()
        {
            List<SchoolCourse> currentcourses = new List<SchoolCourse>();
            try
            {
                List<SchoolCourse> classes = await ClsCourseApi.GetCourses(Access_token, Lms_url, 0).ConfigureAwait(false);

                foreach (SchoolCourse active in classes)
                {
                    //get each end date of course 
                    DateTime dt = Convert.ToDateTime(active.End_at);
                    if (DateTime.Now < dt)
                    {
                        currentcourses.Add(active);
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.Write("error thrown ==> " + ex.Message);
            }


            return currentcourses;
        }



        //method that gets the user's todo list
        //the list will need some logic 
        public async Task<List<Assignment>> GetTodolist()
        {
            List<Assignment> not_submitted = new List<Assignment>();
            try
            {
                List<Assignment> tasks = await ClsCourseApi.List_Todo(Access_token, Lms_url, 0).ConfigureAwait(false);

                foreach (Assignment task in tasks)
                {
                    if (!task.Has_submitted)
                    {
                        not_submitted.Add(task);
                    }
                }

            }
            catch (FileNotFoundException ex)
            {
                Console.Write("error thrown ==> " + ex.Message);
            }

            return not_submitted;
        }


    }
}
