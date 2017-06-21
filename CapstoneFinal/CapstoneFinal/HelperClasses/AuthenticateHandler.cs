using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneFinal.HelperClasses
{
    public class AuthenticateHandler
    {
        public string Password { protected get; set; }

        public string Name { protected get; set; }

        public string AccessToken {protected get; set; }


        public string Validate()
        {
            string message = "";
            string personalInfo = UserDetailsHandler.ReadUserDetails(Name);
            //personal if is matched as Password:<password>(space)AccessToken:<accessToken>(space)Secret:<secret>
            if (personalInfo.Equals("FAIL"))
            {
                message = "The username you entered, doesn't exist";
            }
            else
            {
                string[] data = personalInfo.Split(';');

                string password = PasswordClean(data[0]);
                SetAccessToken(data[1]);
                if (Password.Equals(password))
                {
                    message = "Welcome, " + Name;
                }
                else
                {
                    message = "Incorrect password";
                }
            }
            return message;
        }

        protected string PasswordClean(string password)
        {
            string updatePassword = password.Replace("Password:", string.Empty);
            return updatePassword;
        }

        protected void SetAccessToken(string data)
        {
            string update = data.Replace("AccessToken:", string.Empty);
            AccessToken = update;
        }

        public string RetrieveAccessToken()
        {
            return AccessToken;
        }
    }
}
