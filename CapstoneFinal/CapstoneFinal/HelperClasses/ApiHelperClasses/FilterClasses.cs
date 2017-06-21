using CanvasApiLib.JsonObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneFinal.HelperClasses.ApiHelperClasses
{
    public class FilterClasses
    {
        public static List<Assignment> FilterParticaption(List<Assignment> totalAssignment)
        {
            List<Assignment> particpation = new List<Assignment>();
            foreach (Assignment task in totalAssignment)
            {
                double pointsWorth = Double.Parse(task.Points_Possible);
                if (!task.Due_date_required && (pointsWorth <= 10.0))
                {
                    particpation.Add(task);
                }
            }

            return particpation;
        }


        //method to filter assignments based on classes
        public static List<Assignment> FilteAssignments(SchoolCourse course, List<Assignment> totalAssignment)
        {
            List<Assignment> ClassAssignments = new List<Assignment>();

            foreach (Assignment task in totalAssignment)
            {
                if (task.Course_id.Equals(course.Id))
                {
                    ClassAssignments.Add(task);
                }


            }

            return ClassAssignments;
        }
    }
}
