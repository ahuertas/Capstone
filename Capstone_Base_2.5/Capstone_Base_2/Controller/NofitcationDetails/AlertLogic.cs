using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CanvasApiLib.Objects;
using Capstone_Base_2.Controller.MainDetails;
namespace Capstone_Base_2.Controller.NofitcationDetails
{
    public class AlertLogic
    {

        public static List<Assignment> currentList;
        public static List<Assignment> refreshedList { get; private set; }
        public static bool isUpdateable { get; private set; }
        public static string ACCESS_TOKEN;

        public AlertLogic()
        {

        }


        //determines when a assignment is late
        public static  void IsAssignmentGonnaBeLate()
        {
            List<Assignment> needSubmission = new List<Assignment>();

            DateTime dateTime;

            foreach (Assignment task in currentList)
            {
                dateTime = Convert.ToDateTime(task.Due_at);

                if(DateTime.Now.AddHours(1) > dateTime)
                {
                    needSubmission.Add(task);
                }
            }

            if(needSubmission.Count != 0)
            {
                NotificationBalloonTips.WarningPopUp(needSubmission);
            }

        }

        //determines when an assigment is added to the list
        public static async Task NewAssignmentAdded()
        {

            //I have the old list, get a new list and compare sizes
            List<Assignment> refreshedTasks = await StudentInfoHandler.GetTodolist().ConfigureAwait(false);
            List<Assignment> newTasks = new List<Assignment>();
            //if there is a change grab the assignment that was last added to the list

            if(currentList.Count != refreshedTasks.Count)
            {
                foreach (Assignment item in refreshedTasks)
                {
                    if(!currentList.Contains(item))
                    {
                        newTasks.Add(item);
                    }
                }
                isUpdateable = true;
            }
           
           refreshedList = refreshedTasks;

            if(newTasks.Count != 0)
            {
                    NotificationBalloonTips.NewAssignmentPopUp(newTasks);
            }
        }



        //determines when the study timer is done


    }
}
