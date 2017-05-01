using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CanvasApiLib.Objects;
using System.Drawing;

namespace Capstone_Base_2.Controller.NofitcationDetails
{
    public class NotificationBalloonTips
    {

        public static NotifyIcon notify { get; set; }

        //when an assignment has been added

        //when an assignment the will be due in one hour

        public static void NewAssignmentPopUp(List<Assignment> tasks)
        {
            string balloonText = "";
            if(tasks.Count == 1)
            {
                balloonText = tasks[0].Name + " was recently added to your todo list";
            }
            else
            {
                foreach (Assignment item in tasks)
                {
                    balloonText += item.Name + ", ";
                }
                balloonText += ("was recently added to your todo list");
            }
            
            notify.BalloonTipTitle = "Alert";
            notify.BalloonTipText = balloonText;
            notify.BalloonTipIcon = ToolTipIcon.Info;

            notify.ShowBalloonTip(1500);
        }

        public static void WarningPopUp(List<Assignment> tasks)
        {
            string balloonText = "";
            if (tasks.Count == 1)
            {
                balloonText = tasks[0].Name + " is due in less than an hour";
            }
            else
            {
                foreach (Assignment item in tasks)
                {
                    balloonText += item.Name + ", ";
                }
                balloonText += (" are due in less than a hour!");
            }

            notify.BalloonTipTitle = "Alert";
            notify.BalloonTipText = balloonText;
            notify.BalloonTipIcon = ToolTipIcon.Warning;

            notify.ShowBalloonTip(1500);
        }

        public static void TimerPopUp()
        {
            
            notify.BalloonTipTitle = "Alert";
            notify.BalloonTipText = " Your study period is over! Good job";
            notify.BalloonTipIcon = ToolTipIcon.Info;

            notify.ShowBalloonTip(1500);
        }

        public static void StartPopUp()
        {
            //notify.Icon = SystemIcons.Exclamation;
            notify.BalloonTipTitle = "Psst";
            notify.BalloonTipText = "Your buddy will let you know if something happens!";
            notify.BalloonTipIcon = ToolTipIcon.Info;

            notify.ShowBalloonTip(500);
        }


    }
}
