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

                //List<SchoolCourse> allCourses = await ApiCalls.getCourses(accessToken, baseURl, courseId);
                ////string convertedData = JsonConvert.SerializeObject(data);
                ////List<SchoolCourse> courses = JsonToObjects.convertJsonStringToCourseObject();
                //foreach (SchoolCourse classes in allCourses)
                //{
                //    Console.WriteLine(classes.Name);
                //}

                dynamic data = await ApiCalls.list_upcoming_events(accessToken, baseURl, courseId);
                Console.WriteLine(data);
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
