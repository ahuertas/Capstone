using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace canvasApiLib.Base
{
    class ClsRegExpressionss
    {
        public static bool ConfirmAlphaNumeric(string text)
        {
            bool returnValue = false;
            string pattern = "^[a-zA-z0-9]+$";
            try
            {
                returnValue = Regex.IsMatch(text, pattern);
            }
            catch (Exception error)
            {
                Console.Write("[confirmAlphaNumeric]");
            }

            return returnValue;
        }

    }
}
