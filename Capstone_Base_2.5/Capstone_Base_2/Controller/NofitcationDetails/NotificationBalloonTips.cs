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

        public static NotifyIcon Notify { get; set; }

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
            
            Notify.BalloonTipTitle = "Alert";
            Notify.BalloonTipText = balloonText;
            Notify.BalloonTipIcon = ToolTipIcon.Info;

            Notify.ShowBalloonTip(1500);
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

            Notify.BalloonTipTitle = "Alert";
            Notify.BalloonTipText = balloonText;
            Notify.BalloonTipIcon = ToolTipIcon.Warning;

            Notify.ShowBalloonTip(2500);
        }

        public static void TimerPopUp()
        {
            
            Notify.Icon = SystemIcons.Exclamation;
            Notify.BalloonTipTitle = "Time's Up!";
            Notify.BalloonTipText = " Your study period is over! Good job";
            Notify.BalloonTipIcon = ToolTipIcon.Info;

            Notify.ShowBalloonTip(1500);
        }

        public static void StartPopUp()
        {
            //notify.Icon = SystemIcons.Exclamation;
            Notify.BalloonTipTitle = "Psst";
            Notify.BalloonTipText = "Your buddy will let you know if something happens!";
            Notify.BalloonTipIcon = ToolTipIcon.Info;

            Notify.ShowBalloonTip(500);
        }

        public static void LatePopUp(List<Assignment> tasks)
        {
            string balloonText = "";
            if (tasks.Count == 1)
            {
                balloonText = tasks[0].Name + " is late!";
            }
            else
            {
                foreach (Assignment item in tasks)
                {
                    balloonText += item.Name + ", ";
                }
                balloonText += (" are late!");
            }

            Notify.Icon = SystemIcons.Exclamation;
            Notify.BalloonTipTitle = "Important";
            Notify.BalloonTipText = balloonText;
            Notify.BalloonTipIcon = ToolTipIcon.Warning;

            Notify.ShowBalloonTip(5000);
        }

        public  static void StudyBrokenAlert(string distraction)
        {
            Notify.Icon = SystemIcons.Hand;
            Notify.BalloonTipTitle = "Hey!";
            Notify.BalloonTipText = "You should be working, not on : " + distraction;
            Notify.BalloonTipIcon = ToolTipIcon.Info;

            Notify.ShowBalloonTip(2500);
        }

    }
}
