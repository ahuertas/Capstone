using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone_Base_2.Controller.LoginDetials;
namespace Capstone_Base_2.Controller.SecurityDetails
{
    public class AuthSecurity
    {
        private string name, secret;
        //read the file input 
        public AuthSecurity (string name, string secret)
        {
            this.name = name;
            this.secret = secret;
        }

        
        public string RetrieveUserInfo()
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
                string phrase = SecretClean(data[2]);
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

        protected string PasswordClean(string password)
        {
            return password.Replace("Password:", string.Empty);
        }
        protected string SecretClean(string secret)
        {
            return secret.Replace("Secret:", string.Empty);
        }

        public bool IsCorrect(string phrase)
        {
            return secret.Equals(phrase);
        }

        

        //parse for the string secret

        //compare the values
    }
}
