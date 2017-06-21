using CanvasApiLib.API;
using CanvasApiLib.JsonObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneFinal.HelperClasses.ApiHelperClasses
{
    public class StudentInfoHandler
    {
        private static string LMS_URL = "https://lms.neumont.edu";

        public static string ACCESS_TOKEN { get; set; }

        public static async Task<List<SchoolCourse>> GetCurrentClasses()
        {
            List<SchoolCourse> currentcourses = new List<SchoolCourse>();
            try
            {
                List<SchoolCourse> classes = await ClsCourseApi.GetCourses(ACCESS_TOKEN, LMS_URL, 0).ConfigureAwait(false);

                foreach (SchoolCourse active in classes)
                {
                    //get each end date of course 
                    if (DateTime.Now < Convert.ToDateTime(active.End_at))
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

        public static async Task<List<Assignment>> GetTodolist()
        {
            List<Assignment> not_submitted = new List<Assignment>();
            try
            {
                List<Assignment> tasks = await ClsCourseApi.List_Todo(ACCESS_TOKEN, LMS_URL, 0).ConfigureAwait(false);

                foreach (Assignment task in tasks)
                {
                    //if (!task.Has_submitted)
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

        public static async Task<List<Enrollment>> GetStudentEnrollment()
        {
            List<Enrollment> currentEnrollment = new List<Enrollment>();
            try
            {
                currentEnrollment = await ClsCourseApi.GetUserEnrollments(ACCESS_TOKEN,LMS_URL,0).ConfigureAwait(false);
            }
            catch (FileNotFoundException ex)
            {
                Console.Write("error thrown ==> " + ex.Message);
            }

            return currentEnrollment;
        }


    }
}
