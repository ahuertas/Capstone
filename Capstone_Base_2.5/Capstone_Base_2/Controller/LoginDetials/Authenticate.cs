using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone_Base_2.Controller.LoginDetials
{
    public class Authenticate
    {
        protected string Password { get; set; }
        protected string Username { get; set; }
        protected string Token { get; set; }
       


        public Authenticate(string Password, string Username)
        {
            this.Password = Password;
            this.Username = Username;
        }

        //get the userinfo
        public string Validate()
        {
            string message = "";
            string personalInfo = UserDetailsReader.ReadFile(Username);
            //personal if is matched as Password:<password>(space)AccessToken:<accessToken>(space)Secret:<secret>
            if (personalInfo.Equals("FAIL"))
            {
                message = "The username you entered, doesn't exist";
            }
            else
            {
                string[] data = personalInfo.Split(' ');

                string password = PasswordClean(data[0]);
                SetAccessToken(data[1]);
                if (Password.Equals(password))
                {
                    message = "Welcome, " + Username;
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
            Token = update;
        }

        public string RetrieveAccessToken()
        {
            return Token;
        }



    }
}
