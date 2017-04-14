using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace CanvasApiCallsTest
{
    [JsonObject]
    public class TaskOverview
    {

        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("assignment")]
        public Assignment Assignments { get; set; }

      
        [JsonProperty("course_id")]
        public long Course_id { get; set; }

      
        [JsonProperty("html_url")]
        public string Html_Url { get; set; }

    }

}
