using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CanvasApiCallsTest
{
    [JsonObject]
    public class Enrollment
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("user_id")]
        public long User_ID { get; set; }

        [JsonProperty("course_id")]
        public long Course_Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("created_at")]
        public string Create_at { get; set; }

        [JsonProperty("updated_at")]
        public string Updated_at { get; set; }

        [JsonProperty("course_section_id")]
        public string Course_Section_id { get; set; }

        [JsonProperty("grades")]
        public Grades Grade { get; set; }

        [JsonProperty("html_url")]
        public string Html_url { get; set; }




    }

    [JsonObject]
    public class Grades
    {
        [JsonProperty("html_url")]
        public string Html_url { get; set; }

        [JsonProperty("current_score")]
        public string Current_Score { get; set; }

        [JsonProperty("current_grade")]
        public string Current_grade { get; set; }

        [JsonProperty("Final_score")]
        public string Final_score { get; set; }

        [JsonProperty("Final_grade")]
        public string Final_grade { get; set; }

    }
}
