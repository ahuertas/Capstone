using CanvasApiLib.JsonObjects;
using CapstoneFinal.HelperClasses.ApiHelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneFinal.HelperClasses.NotificationHelpers
{
    public class NotificationLogic
    {
        public static List<Assignment> currentList;
        public static List<Assignment> RefreshedList { get; private set; }
        public static bool IsUpdateable { get; private set; }

        public static string ACCESS_TOKEN;

        public NotificationLogic() { }


        //determines when a assignment is late
        public static void IsAssignmentGonnaBeLate()
        {
            List<Assignment> needSubmission = new List<Assignment>();

            DateTime dateTime;

            foreach (Assignment task in currentList)
            {
                dateTime = Convert.ToDateTime(task.Due_at);

                if (DateTime.Now.AddHours(1) == dateTime)
                {
                    needSubmission.Add(task);
                }
            }

            if (needSubmission.Count != 0)
            {
                NotificationBalloonTips.WarningPopUp(needSubmission);
            }

            //if the assignment is gonna be late

        }

        public static void IsAssignmentLate()
        {
            List<Assignment> lateSubmission = new List<Assignment>();

            DateTime dateTime;

            foreach (Assignment task in currentList)
            {
                dateTime = Convert.ToDateTime(task.Due_at);

                if (DateTime.Now > dateTime && !task.Has_submitted)
                {
                    lateSubmission.Add(task);
                }
            }

            if (lateSubmission.Count != 0)
            {
                NotificationBalloonTips.LatePopUp(lateSubmission);
            }

        }

        //determines when an assigment is added to the list
        public static async Task NewAssignmentAdded()
        {

            //I have the old list, get a new list and compare sizes
            List<Assignment> refreshedTasks = await StudentInfoHandler.GetTodolist().ConfigureAwait(false);
            List<Assignment> newTasks = new List<Assignment>();
            //if there is a change grab the assignment that was last added to the list

            if (currentList.Count != refreshedTasks.Count)
            {
                foreach (Assignment item in refreshedTasks)
                {
                    if (!currentList.Contains(item))
                    {
                        newTasks.Add(item);
                    }
                }
                IsUpdateable = true;
            }

            RefreshedList = refreshedTasks;

            if (newTasks.Count != 0)
            {
                NotificationBalloonTips.NewAssignmentPopUp(newTasks);
            }
        }

    }
}
