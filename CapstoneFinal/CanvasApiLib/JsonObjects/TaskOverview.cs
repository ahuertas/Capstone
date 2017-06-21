using System;
using Newtonsoft.Json;
using CanvasApiLib.JsonObjects;

namespace CanvasApiLib
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
