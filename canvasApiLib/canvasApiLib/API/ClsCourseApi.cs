using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using canvasApiLib.Base;
using canvasApiLib.Objects;

namespace canvasApiLib.API
{
    public class ClsCourseApi : clsHttpHelper
    {
       
        public static async Task<dynamic> GetCourseDetails(string accessToken, string baseUrl, long canvasCourseId, long sisCourseId = 0)
        {
            string rval = string.Empty;
            string urlCommand = "/api/v1/courses/:id";

            //if we have a valid sis course id, ask canvas to find the course using that identifier
            //  this will be done when we look for an existing course during the cloning request
            urlCommand = (sisCourseId > 0) ? urlCommand.Replace(":id", "sis_course_id:" + sisCourseId.ToString()) : urlCommand.Replace(":id", canvasCourseId.ToString());

            using (HttpResponseMessage response = await HttpGET(baseUrl, urlCommand, accessToken))
            {
                rval = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode || (rval.Contains("errors") && rval.Contains("message")))
                {
                    string msg = "[getCourseDetails]:[" + urlCommand + "] returned status[" + response.StatusCode + "]: " + response.ReasonPhrase;
                    Console.WriteLine(msg);
                    throw new HttpRequestException(rval);
                }
            }

            return JsonConvert.DeserializeObject<SchoolCourse>(rval); ;
        }


        public static async Task<dynamic> getCourses(string accessToken, string baseUrl, long sisCourseId = 0)
        {
            string rval = string.Empty;
            string urlCommand = "/api/v1/courses/?per_page=100";

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
            return JsonConvert.DeserializeObject<List<SchoolCourse>>(rval);
        }
    }
}
