using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CanvasApiCallsTest
{
    public class clsRegExpressions
    {
        public static bool confirmAlphaNumeric(string text)
        {
            bool returnValue = false;
            string pattern = "^[a-zA-z0-9]+$";
            try
            {
                returnValue = Regex.IsMatch(text,pattern);
            }
            catch(Exception error)
            {
                Console.Write("[confirmAlphaNumeric]");
            }

            return returnValue;
        }
    }
}
