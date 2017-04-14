using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CanvasApiCallsTest
{
    public class ApiCalls : HttpHelperMethods
    {
        
        /// <summary>
		/// Returns the details of a specific course
		/// https://canvas.instructure.com/doc/api/courses.html#method.courses.show
		/// </summary>
		/// <param name="accessToken"></param>
		/// <param name="baseUrl"></param>
		/// <param name="canvasCourseId"></param>
		/// <returns>Returns a json object representing a course object: https://canvas.instructure.com/doc/api/courses.html#Course, throws exception if successful status code is not received </returns>
        public static async Task<dynamic> getCourseDetails(string accessToken,string baseUrl,long canvasCourseId,long sisCourseId = 0)
        {
            string rval = string.Empty;
            string urlCommand = "/api/v1/courses/:id";

             //if we have a valid sis course id, ask canvas to find the course using that identifier
             //  this will be done when we look for an existing course during the cloning request
             urlCommand = (sisCourseId > 0) ? urlCommand.Replace(":id", "sis_course_id:" + sisCourseId.ToString()) : urlCommand.Replace(":id", canvasCourseId.ToString());

            using (HttpResponseMessage response = await httpGET(baseUrl, urlCommand, accessToken))
            {
                rval = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode || (rval.Contains("errors") && rval.Contains("message")))
                {
                    string msg = "[getCourseDetails]:[" + urlCommand + "] returned status[" + response.StatusCode + "]: " + response.ReasonPhrase;
                    Console.WriteLine(msg);
                    throw new HttpRequestException(rval);
                }
            }

            SchoolCourse data = JsonConvert.DeserializeObject<SchoolCourse>(rval);
           // SchoolCourse sc = new SchoolCourse();

            //JsonConvert.PopulateObject(rval,sc);

           // Console.WriteLine(data.Name + " , ends " + data.End_at);

            return data;
        }

        public static async Task<dynamic> getCourses(string accessToken, string baseUrl, long sisCourseId = 0)
        {
            string rval = string.Empty;
            string urlCommand = "/api/v1/courses/?per_page=100";

            using (HttpResponseMessage response = await httpGET(baseUrl,urlCommand,accessToken))
            {
                rval = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode || (rval.Contains("errors") && rval.Contains("message")))
                {
                    string msg = "[getCourseDetails]:[" + urlCommand + "] returned status[" + response.StatusCode + "]: " + response.ReasonPhrase;
                    Console.WriteLine(msg);
                    throw new HttpRequestException(rval);
                }

            }

            
            
            return JsonConvert.DeserializeObject<List<SchoolCourse>>(rval);
        }

        public static async Task<dynamic> list_course_todo(string accessToken,string baseUrl, long sisCourseId = 0)
        {
            string rval = string.Empty;
            string urlCommand = "/api/v1/users/self/todo";

            //urlCommand = (sisCourseId > 0) ? urlCommand.Replace(":id", "sis_course_id:" + sisCourseId.ToString()) : urlCommand.Replace(":id", canvasCourseId.ToString());

            using (HttpResponseMessage response = await httpGET(baseUrl, urlCommand, accessToken))
            {
                rval = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode || (rval.Contains("errors") && rval.Contains("message")))
                {
                    string msg = "[getCourseDetails]:[" + urlCommand + "] returned status[" + response.StatusCode + "]: " + response.ReasonPhrase;
                    Console.WriteLine(msg);
                    throw new HttpRequestException(rval);
                }
            }

           
            List<TaskOverview> tasks = JsonConvert.DeserializeObject<List<TaskOverview>>(rval);
            List<Assignment> assignments = new List<Assignment>();
            foreach (TaskOverview assign in tasks)
            {
                assignments.Add(assign.Assignments);
            }

            // JsonConvert.DeserializeObject(rval);
            //get all the assignment objects from your todo list


            return assignments;
        }

        public static async Task<dynamic> list_upcoming_events(string accessToken, string baseUrl, long sisCourseId = 0)
        {
            string rval = string.Empty;
            string urlCommand = "/api/v1/users/self/upcoming_events";

            //urlCommand = (sisCourseId > 0) ? urlCommand.Replace(":id", "sis_course_id:" + sisCourseId.ToString()) : urlCommand.Replace(":id", canvasCourseId.ToString());

            using (HttpResponseMessage response = await httpGET(baseUrl, urlCommand, accessToken))
            {
                rval = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode || (rval.Contains("errors") && rval.Contains("message")))
                {
                    string msg = "[getCourseDetails]:[" + urlCommand + "] returned status[" + response.StatusCode + "]: " + response.ReasonPhrase;
                    Console.WriteLine(msg);
                    throw new HttpRequestException(rval);
                }
            }
            return JsonConvert.DeserializeObject(rval);
        }

        public static async Task<dynamic> list_missing_submissions(string accessToken, string baseUrl, long sisCourseId = 0)
        {
            string rval = string.Empty;
            string urlCommand = "/api/v1/users/self/missing_submissions";

            //urlCommand = (sisCourseId > 0) ? urlCommand.Replace(":id", "sis_course_id:" + sisCourseId.ToString()) : urlCommand.Replace(":id", canvasCourseId.ToString());

            using (HttpResponseMessage response = await httpGET(baseUrl, urlCommand, accessToken))
            {
                rval = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode || (rval.Contains("errors") && rval.Contains("message")))
                {
                    string msg = "[getCourseDetails]:[" + urlCommand + "] returned status[" + response.StatusCode + "]: " + response.ReasonPhrase;
                    Console.WriteLine(msg);
                    throw new HttpRequestException(rval);
                }
            }
            return JsonConvert.DeserializeObject(rval);
        }
    }
}
