using CapstoneFinal.HelperClasses.NotificationHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Models;

namespace CapstoneFinal.HelperClasses.ProxyServerHelper
{
    public class TitaniumProxy
    {

        private ProxyServer titanServer = new ProxyServer();
        private Dictionary<Guid, string> requestBodyHistory = new Dictionary<Guid, string>();
        private ExplicitProxyEndPoint explicitEndPoint = new ExplicitProxyEndPoint(IPAddress.Any, 8000, true)
        {
            //Exclude HTTPS addresses you don't want to proxy
            //Useful for clients that use certificate pinning
            //for example dropbox.com
            ExcludedHttpsHostNameRegex = new List<string>() { "google.com", "lms.neumont.edu" }

            //Use self-issued generic certificate on all HTTPS requests
            //Optimizes performance by not creating a certificate for each HTTPS-enabled domain
            //Useful when certificate trust is not required by proxy clients
            // GenericCertificate = new X509Certificate2(Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "genericcert.pfx"), "password")
        };

        public List<string> BlackListSites { get; set; }


       public void Start()
        {
            //locally trust root certificate used by this proxy
            titanServer.TrustRootCertificate = true;

            titanServer.BeforeRequest += OnRequest;
            titanServer.BeforeResponse += OnResponse;
            titanServer.ServerCertificateValidationCallback += OnCertificateValidation;
            titanServer.ClientCertificateSelectionCallback += OnCertificateSelection;

            var explicitEndPoint = new ExplicitProxyEndPoint(IPAddress.Any, 8000, true)
            {
                //Exclude HTTPS addresses you don't want to proxy
                //Useful for clients that use certificate pinning
                //for example dropbox.com
                ExcludedHttpsHostNameRegex = new List<string>() { "google.com", "lms.neumont.edu" }

                //Use self-issued generic certificate on all HTTPS requests
                //Optimizes performance by not creating a certificate for each HTTPS-enabled domain
                //Useful when certificate trust is not required by proxy clients
                // GenericCertificate = new X509Certificate2(Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "genericcert.pfx"), "password")
            };

            //An explicit endpoint is where the client knows about the existance of a proxy
            //So the client sends request in a proxy friendly manner
            titanServer.AddEndPoint(explicitEndPoint);
            titanServer.Start();


            //proxyServer.UpStreamHttpProxy = new ExternalProxy() { HostName = "localhost", Port = 8888 };
            //proxyServer.UpStreamHttpsProxy = new ExternalProxy() { HostName = "localhost", Port = 8888 };

            foreach (var endPoint in titanServer.ProxyEndPoints)
                Console.WriteLine("Listening on '{0}' endpoint at Ip {1} and port: {2} ",
                    endPoint.GetType().Name, endPoint.IpAddress, endPoint.Port);

            //Only explicit proxies can be set as system proxy!
            titanServer.SetAsSystemHttpProxy(explicitEndPoint);
            titanServer.SetAsSystemHttpsProxy(explicitEndPoint);
        }

        public void Stop()
        {
            titanServer.BeforeRequest -= OnRequest;
            titanServer.BeforeResponse -= OnResponse;
            titanServer.ServerCertificateValidationCallback -= OnCertificateValidation;
            titanServer.ClientCertificateSelectionCallback -= OnCertificateSelection;

            //titanServer.RemoveEndPoint(explicitEndPoint);
            titanServer.Stop();
        }


        /// Allows overriding default certificate validation logic
        public Task OnCertificateValidation(object sender, CertificateValidationEventArgs e)
        {
            //set IsValid to true/false based on Certificate Errors
            if (e.SslPolicyErrors == System.Net.Security.SslPolicyErrors.None)
                e.IsValid = true;

            return Task.FromResult(0);
        }

        /// Allows overriding default client certificate selection logic during mutual authentication
        public Task OnCertificateSelection(object sender, CertificateSelectionEventArgs e)
        {
            //set e.clientCertificate to override
            return Task.FromResult(0);
        }

        public async Task OnResponse(object sender, SessionEventArgs e)
        {
            //read response headers
            var responseHeaders = e.WebSession.Response.ResponseHeaders;

            //if (!e.ProxySession.Request.Host.Equals("medeczane.sgk.gov.tr")) return;
            if (e.WebSession.Request.Method == "GET" || e.WebSession.Request.Method == "POST")
            {
                if (e.WebSession.Response.ResponseStatusCode == "200")
                {
                    if (e.WebSession.Response.ContentType != null && e.WebSession.Response.ContentType.Trim().ToLower().Contains("text/html"))
                    {
                        byte[] bodyBytes = await e.GetResponseBody();
                        await e.SetResponseBody(bodyBytes);

                        string body = await e.GetResponseBodyAsString();
                        await e.SetResponseBodyString(body);
                    }
                }
            }

            //access request body/request headers etc by looking up using requestId
            if (requestBodyHistory.ContainsKey(e.Id))
            {
                var requestBody = requestBodyHistory[e.Id];
            }
        }

        public async Task OnRequest(object sender, SessionEventArgs e)
        {
            Console.WriteLine(e.WebSession.Request.Url);
            ////read request headers
            var requestHeaders = e.WebSession.Request.RequestHeaders;

            var method = e.WebSession.Request.Method.ToUpper();
            if ((method == "POST" || method == "PUT" || method == "PATCH"))
            {
                //Get/Set request body bytes
                byte[] bodyBytes = await e.GetRequestBody();
                await e.SetRequestBody(bodyBytes);

                //Get/Set request body as string
                string bodyString = await e.GetRequestBodyAsString();
                await e.SetRequestBodyString(bodyString);

                //store request Body/request headers etc with request Id as key
                //so that you can find it from response handler using request Id
                requestBodyHistory[e.Id] = bodyString;
            }

           foreach(string website in BlackListSites)
           {
                //Redirect example
                if (e.WebSession.Request.RequestUri.AbsoluteUri.Contains(website))
                {
                    await e.Redirect("https://lms.neumont.edu/");
                    NotificationBalloonTips.StudyBrokenAlert(website);
                }
           }
        }

    }
}
