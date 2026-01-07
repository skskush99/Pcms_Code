using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils
{
    public class ValidationUtility
    {
        public static bool IsValidMobileNumber(string mobileNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(mobileNumber))
                    return false;
                else if (IsNumeric(mobileNumber, false) == false)
                    return false;
                mobileNumber = mobileNumber.Trim();
                if (mobileNumber.Length == 10 && mobileNumber.StartsWith("0") == false)
                    return true;
                else
                    return false;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public static bool IsValidEmailAddress(string emailAddress)
        {
            try
            {
                if (String.IsNullOrEmpty(emailAddress))
                    return false;
                MailAddress m = new MailAddress(emailAddress, emailAddress);
                string MatchEmailPattern =
                            @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                     + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				            [0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                     + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				            [0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                     + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";
                if (emailAddress != null)
                    return Regex.IsMatch(emailAddress, MatchEmailPattern);
                else
                    return false;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public static bool IsNumeric(string numString, bool allowDecimal)
        {
            if (allowDecimal == false)
            {
                long number1 = 0;
                return long.TryParse(numString, out number1);
            }
            else
            {
                decimal number2 = 0;
                return decimal.TryParse(numString, out number2);
            }
        }
    }
}
