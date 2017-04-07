using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasApiCallsTest
{
    public class Course
    {
        // the unique identifier for the course
        public long id { get; protected set; }

        // the SIS identifier for the course, if defined. This field is only included if
        // the user has permission to view SIS information.
        public string sis_course_id { get; protected set; }

        //the integration identifier for the course, if defined. 
        //This field is only included if the user has permission to view SIS information
        public string integration_id { get; protected set; }

        //full name of the course
        public string name { get; protected set; }

        //the course code
        public string course_code { get; protected set; }

        //the current state of the course one of 'unpublished', 'available', 'completed', 
        //or deleted
        public string workflow_state { get; protected set; }

        //the account associated with the course
        public string account_id { get; protected set; }

        //the root account associated with the course
        public string root_account_id { get; protected set; }

        //the enrollment term associated with the account
        public string enrollment_term_id { get; protected set; }

        //the grading standard associated with the course
        public string grading_standard_id  {get; protected set;}
   
        //the start date for the course if applicable
        public string start_at {get; protected set;}

        //the end date for the course =,if applicable
        public string end_at{get; protected set;}

        //the course-set locale, if applicable
        public string locale {get; protected set;}

        //A list of enrollments linking the current user to the course. for student
        //enrollments, grading information may be included if include[]=total_scores
        public string enrollments {get; protected set;}

        //optional: the total number of active  and invited students in the course
        public int total_students {get; protected set;}

        //course calender
        public bool calender {get; protected set;}

        // the type of page that users will see when they first visit the course - 'feed':
        // Recent Activity Dashboard - 'wiki': Wiki Front Page - 'modules': Course
        // Modules/Sections Page - 'assignments': Course Assignments List - 'syllabus':
        // Course Syllabus Page other types may be added in the future
        public string default_view { get; protected set; }

        //optional: user generated HTML for the course syllabus
        public string syllabus_body { get; protected set; }

        // optional: the number of submissions needing grading returned only if the current
        // user has grading rights and include[]=needs_grading_count
        public string needs_grading_count { get; protected set; }

        // optional: the enrollment term object for the course returned only if
        // include[]=term
        public string term { get; protected set; }

        // optional (beta): information on progress through the course returned only if
        // include[]=course_progress
        public string course_progress { get; protected set; }
       
        // weight final grade based on assignment group percentages
        public bool apply_assignment_group_weights { get; protected set; }

        // optional: the permissions the user has for the course. returned only for a
        // single course and include[]=permissions
        public Permissions permissions { get; protected set; }

        // optional: the public description of the course
        public string public_description { get; protected set; }

        public long storage_quota_mb { get; protected set; }

        public long storage_quota_used_mb { get; protected set; }

        public bool hide_final_grades { get; protected set; }

        public string license { get; protected set; }

        public bool allow_student_assignment_edits { get; protected set; }

        public bool allow_wiki_comments { get; protected set; }

        public bool allow_student_forum_attachments { get; protected set; }

        public bool open_enrollment { get; protected set; }

        public bool self_enrollment { get; protected set; }

        public bool restrict_enrollments_to_course_dates { get; protected set; }

        public string course_format { get; protected set; }
        // optional: this will be true if this user is currently prevented from viewing the
        // course because of date restriction settings
        public bool  access_restricted_by_date { get; protected set; }

        // The course's IANA time zone name.
        public string  time_zone { get; protected set; }

    }

    public class Permissions
    {
        public bool is_public { get; protected set; }

        public bool is_public_to_auth_users { get; protected set; }

        public bool public_syllabus { get; protected set; }

        public bool public_syllabus_to_auth { get; protected set; }

    }

    
}
