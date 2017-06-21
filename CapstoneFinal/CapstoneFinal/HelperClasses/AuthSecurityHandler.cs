using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;

namespace CapstoneFinal.HelperClasses
{
   public  class AuthSecurityHandler
    {
        private static MetroWindow Window { get; set; }

        public static string Secret { get; set; }

        public static string Name { get; set; }
        //read the file input 
       

        public static string RetrieveUserQuestion()
        {
            string personalInfo = RetrievePersonalInfo();
            string[] data = personalInfo.Split(';');
            string question = QuestionClean(data[2]);
            
            return question;
        }

        public static string RetrieveUserInfo(string name, string secret)
        {
            string message = "";
            string personalInfo = RetrievePersonalInfo();
                string[] data = personalInfo.Split(';');

                string password = PasswordClean(data[0]);
                string phrase = SecretClean(data[3]);
                if (IsCorrect(phrase))
                {
                    message = "Thank you for your patience, " + name + " your password is : " + password;
                }
                else
                {
                    message = "Incorrect secret phrase...... hmmmm..... are you really " + name + " ?";
                }
            
            return message;
        }

        public static  string RetrievePersonalInfo()
        {
           
            string personalInfo = UserDetailsHandler.ReadUserDetails(Name);
            if (personalInfo.Equals("FAIL"))
            {
                ShowDialog();
                personalInfo = "";
            }

            return personalInfo;

        }

        private static async void ShowDialog()
        {
            await Window.ShowMessageAsync("Sorry...", "The username you entered, doesn't exist");
        }



        protected static string PasswordClean(string password)
        {
            return password.Replace("Password:", string.Empty);
        }

        protected static string SecretClean(string secret)
        {
            return secret.Replace("SecretAnswer:", string.Empty);
        }

        protected static string QuestionClean(string question)
        {
            return question.Replace("SecretQuestion:", string.Empty);
        }


        public static bool IsCorrect(string phrase)
        {
            return Secret.Equals(phrase);
        }


    }
}
