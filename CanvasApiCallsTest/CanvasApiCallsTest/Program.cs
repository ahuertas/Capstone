using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CanvasApiCallsTest
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().Wait();
            Console.ReadLine();
        }

        public static async Task MainAsync()
        {
            string accessToken = "1~rEXWfK1cosjcKj30splSfou3kqdpupxFR8Corz9ZSg0lO6Pz0lJrRd6BgLwbyEAJ";
            string baseURl = "https://lms.neumont.edu";
            long courseId = 2106215; //2106225;

            try
            {
                ////SchoolCourse data = await ApiCalls.getCourseDetails(accessToken, baseURl, courseId);

                ////Console.WriteLine(data.Name + ", ends at " + data.End_at);

                List<SchoolCourse> allCourses = await ApiCalls.getCourses(accessToken, baseURl, courseId);
                ////string convertedData = JsonConvert.SerializeObject(data);
                //List<SchoolCourse> courses = JsonToObjects.convertJsonStringToCourseObject();
                List<Enrollment> data = await ApiCalls.GetCourseGrades(accessToken,baseURl,courseId,courseId);
                foreach (SchoolCourse classes in allCourses)
                {
                    if(Convert.ToDateTime(classes.End_at) > DateTime.Now)
                    {
                         Console.WriteLine(classes.Name);
                        foreach (var item in data)
                        {
                            if (item.Course_Id == classes.Id)
                            {
                                Console.WriteLine(classes.Name + " final grade is " + item.Grade.Current_grade + " or a " + item.Grade.Current_Score);
                            }
                        }
                    }
                }


                List<Assignment> data2 = await ApiCalls.list_course_todo(accessToken, baseURl, courseId);
                foreach (var task in data2)
                {
                    
                    foreach (var item in data)
                    {
                        if (item.Course_Id == task.Course_id)
                        {
                            Console.WriteLine(item.Grade.Final_grade);
                        }
                    }
                    if (!task.Due_date_required && task.Description.Length< 10)
                    {
                        Console.WriteLine("Particpation assignment: " + task.Name);
                    }
                    else
                    {
                        Console.WriteLine(task.Name + ": due " + task.Due_at);

                    }

                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        #region UserInput
        ///run program asking for username and password from canvas
        private static string username = "";
        private static string password = "";

        public void getCredentials()
        {
            Console.WriteLine("Enter canvas username: ");
            username = Console.ReadLine();
            Console.WriteLine("Enter canvas password: ");
            password = Console.ReadLine();

            Console.WriteLine("Username: " + username + " password: " + password );
        }
        #endregion

    }
}
