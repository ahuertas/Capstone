using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CanvasApiLib.JsonObjects
{
    [JsonObject]
    public class Assignment
    {
        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("due_at")]
        public string Due_at { get; set; }

        [JsonProperty("points_possible")]
        public string Points_Possible { get; set; }

        [JsonProperty("created_at")]
        public string Created_at { get; set; }

        [JsonProperty("course_id")]
        public long Course_id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("has_submitted_submissions")]
        public bool Has_submitted { get; set; }

        [JsonProperty("html_url")]
        public string Html_Url { get; set; }

        [JsonProperty("locked_for_user")]
        public bool Locked_for_user { get; set; }

        [JsonProperty("submissions_download_url")]
        public string Submissions_download_url { get; set; }

        [JsonProperty("due_date_required")]
        public bool Due_date_required { get; set; }
    }
}
