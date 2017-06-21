using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CanvasApiLib.Objects
{
    [JsonObject]
    public class SchoolCourse
    {
        //the unique identifier to the course
        [JsonProperty("id")]
        public long Id { get; set; }

        //the name of the course
        [JsonProperty("name")]
        public string Name { get; set; }

        //the account associated with the course
        [JsonProperty("account_id")]
        public long Account_id { get; set; }

        //the start date of the course, if applicable
        [JsonProperty("start_at")]
        public string Start_at { get; set; }

        //the grading standard associated with the course
        [JsonProperty("grading_stardard_id")]
        public long? Grandard_standard_id { get; set; }

        //is the class public
        [JsonProperty("is_public")]
        public bool? Is_public { get; set; }

        //the code for the course
        [JsonProperty("course_code")]
        public string Course_code { get; set; }

        //
        [JsonProperty("default_view")]
        public string Default_view { get; set; }

        [JsonProperty("root_account_id")]
        public long? Root_account_id { get; set; }

        [JsonProperty("enrollment_term_id")]
        public long? Enrollment_term_id { get; set; }

        //the end date of the course if applicable 
        [JsonProperty("end_at")]
        public string End_at { get; set; }

        [JsonProperty("public_syllabus")]
        public bool? Public_syllabus { get; set; }

        [JsonProperty("public_syllabus_auth_users")]
        public bool? Public_syllabus_auth_users { get; set; }

        [JsonProperty("apply_assignment_group_weights")]
        public bool? Apply_assignment_group_weights { get; set; }

        [JsonProperty("time_zone")]
        public string Time_zone { get; set; }

        [JsonProperty("hide_final_grades")]
        public bool? Hide_final_grade { get; set; }

        [JsonProperty("workflow_state")]
        public string Workflow_state { get; set; }

        [JsonProperty("restrict_enrollments_to_course_dates")]
        public bool? Restrict_enrollment_to_course_dates { get; set; }

        public SchoolCourse() { }
    }

}
