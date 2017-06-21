using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone_Base_2.Controller.LoginDetials;
using System.Windows;

namespace Capstone_Base_2.Controller.SecurityDetails
{
    public class AuthSecurity
    {
        private static string name, secret;
        //read the file input 
        public AuthSecurity ()
        {
            
        }

        public  static string RetrieveUserQuestion(string name)
        {
            string question = "";
            string personalInfo = UserDetailsReader.ReadFile(name);
            if (personalInfo.Equals("FAIL"))
            {
                MessageBox.Show( "The username you entered, doesn't exist");
            }
            else
            {
                string[] data = personalInfo.Split(' ');
                question = QuestionClean(data[3]);
            }
            return question;
        }

        public static string RetrieveUserInfo(string name, string secret)
        {
            string message = "";
            string personalInfo = UserDetailsReader.ReadFile(name);

            if (personalInfo.Equals("FAIL"))
            {
                message = "The username you entered, doesn't exist";
            }
            else
            {
                string[] data = personalInfo.Split(' ');

                string password = PasswordClean(data[0]);
                string phrase = SecretClean(data[4]);
                if (IsCorrect(phrase))
                {
                    message = "Thank you for your patience, " + name + " your password is : " + password;
                }
                else
                {
                    message = "Incorrect secret phrase...... hmmmm..... are you really " + name + " ?";
                }
            }
            return message;
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
            return question.Replace("SecretQuestion:",string.Empty);
        }


        public static bool IsCorrect(string phrase)
        {
            return secret.Equals(phrase);
        }

    }
}
