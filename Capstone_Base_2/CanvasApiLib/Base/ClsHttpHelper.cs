using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CanvasApiLib.Base
{
    public class ClsHttpHelper
    {
        /// <summary>
        /// Generic logic to execute an HTTP GET call
        /// </summary>
        /// <param name="baseUrl">url of the API to call, i.e. beta or test</param>
        /// <param name="urlCommand">api command, including parameters</param>
        /// <param name="accessToken">your access token to authenticate to your Canvas instance</param>
        /// <returns>http response from api endpoint</returns>
        protected static async Task<HttpResponseMessage> HttpGET(string baseUrl, string urlCommand, string accessToken)
        {
            HttpResponseMessage response = null;
            Console.WriteLine("Called API[" + urlCommand + "]");

            try
            {
                HttpClientHandler handler = new HttpClientHandler();
                handler.AllowAutoRedirect = false;
                using (HttpClient client = new HttpClient(handler, true))
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    response = await client.GetAsync(urlCommand).ConfigureAwait(false);
                }
            } 
            catch (Exception error)
            {
                Console.WriteLine("[EXCEPTION] Failed to execute HTTP GET command: [" + baseUrl + urlCommand + "]");
                throw error;
            }

            return response;
        }


    }
}
