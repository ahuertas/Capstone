using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CanvasApiLib.Objects
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
