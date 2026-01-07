

namespace Core
{
    public static class Extensions
    {
        public static string GetDisplayName<TEnum>(this TEnum enumValue)
            where TEnum : struct
        {
            try
            {
                return enumValue.GetType()
                    .GetMember(enumValue.ToString())
                    .First()
                    .GetCustomAttribute<DisplayAttribute>()
                    .GetName();
            }
            catch (Exception e)
            {
                return enumValue.ToString();
            }
        }

        public static DateTime ParseCustomIndianDate(this string date)
        {
            try
            {
                string[] formats = { "dd/MM/yyyy", "dd-MM-yyyy" };
                var dt = DateTime.ParseExact(date, formats, new CultureInfo("en-US"), DateTimeStyles.None);
                return dt;
            }
            catch
            {
                var dt = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                return dt;
            }
        }

        public static string ConvertToStringDate(this DateTime value)
        {
            return value.ToString("dd/MM/yyyy").Replace("-", "/") ?? "";
        }

        public static DateTime UtcToIst(this DateTime date)
        {
            var istDate = TimeZoneInfo.ConvertTimeFromUtc(date, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            return istDate;
        }

        public static DateTime ParseTour(string date)
        {
            try
            {
                var dt = DateTime.ParseExact(date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                return dt;
            }
            catch
            {
                return DateTime.Now;
            }
        }

        public static string CreateHashPassword(string password)
        {
            if (password == null)
                throw new ArgumentNullException(nameof(password));
            byte[] salt;
            byte[] bytes;
            using (Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, 16, 1000))
            {
                salt = rfc2898DeriveBytes.Salt;
                bytes = rfc2898DeriveBytes.GetBytes(32);
            }
            byte[] inArray = new byte[49];
            Buffer.BlockCopy((Array)salt, 0, (Array)inArray, 1, 16);
            Buffer.BlockCopy((Array)bytes, 0, (Array)inArray, 17, 32);
            return Convert.ToBase64String(inArray);
        }

        /// <summary>
        /// Encryption for password
        /// </summary>
        /// <param name="Plaintext"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static string EncryptTripleDES(string Plaintext, string Key)
        {
            byte[] Buffer = new byte[0];
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();

            MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();

            DES.Key = hashMD5.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(Key));

            DES.Mode = CipherMode.ECB;

            ICryptoTransform DESEncrypt = DES.CreateEncryptor();

            Buffer = System.Text.ASCIIEncoding.ASCII.GetBytes(Plaintext);

            string TripleDES = Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));

            return TripleDES;
        }

        /// <summary>
        /// Decryption of password
        /// </summary>
        /// <param name="base64Text"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static string DecryptTripleDES(string base64Text, string Key)
        {
            byte[] Buffer = new byte[0];

            TripleDESCryptoServiceProvider DES = new

            TripleDESCryptoServiceProvider();

            MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();

            DES.Key = hashMD5.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(Key));

            DES.Mode = CipherMode.ECB;

            ICryptoTransform DESDecrypt = DES.CreateDecryptor();

            Buffer = Convert.FromBase64String(base64Text);

            string DecTripleDES = System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));

            return DecTripleDES;

        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string RandomAlphaNumericstring(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string ConvertNumbertoWords(long number)
        {
            if (number == 0) return "ZERO";
            if (number < 0) return "minus " + ConvertNumbertoWords(Math.Abs(number));
            string words = "";
            if ((number / 100000) > 0)
            {
                words += ConvertNumbertoWords(number / 100000) + " LAKES ";
                number %= 100000;
            }
            if ((number / 1000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000) + " THOUSAND ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ConvertNumbertoWords(number / 100) + " HUNDRED ";
                number %= 100;
            }
            //if ((number / 10) > 0)
            //{
            //    words += ConvertNumbertoWords(number / 10) + " RUPEES ";
            //    number %= 10;
            //}  
            if (number > 0)
            {
                if (words != "") words += "AND ";
                var unitsMap = new[]
                {
            "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN"
        };
                var tensMap = new[]
                {
            "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY"
        };
                if (number < 20) words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0) words += " " + unitsMap[number % 10];
                }
            }


            return words;

        }
    }

    public static class EnumHelper<T>
    {
        public static T GetValueFromName(string name)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();

            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DisplayAttribute)) as DisplayAttribute;
                if (attribute != null)
                {
                    if (attribute.Name == name)
                    {
                        return (T)field.GetValue(null);
                    }
                }
                else
                {
                    if (field.Name == name)
                        return (T)field.GetValue(null);
                }
            }

            throw new ArgumentOutOfRangeException("name");
        }
    }

    public static class NumberToWordsExtension
    {
        private static string[] ones = { "", " One", " Two", " Three", " Four", " Five", " Six", " Seven", " Eight", " Nine", " Ten", " Eleven", " Twelve", " Thirteen", " Fourteen", " Fifteen", " Sixteen", " Seventeen", " Eighteen", " Nineteen" };
        private static string[] tens = { "", "", " Twenty", " Thirty", " Forty", " Fifty", " Sixty", " Seventy", " Eighty", " Ninety" };

        public static string ToWords(this decimal number)
        {
            if (number == 0)
                return "Zero";

            if (number < 0)
                return "Minus " + (-number).ToWords();

            int intPart = (int)number;
            decimal decPart = number - intPart;

            string words = "";

            if (intPart > 0)
            {
                words += ConvertToWords(intPart);
            }

            if (decPart > 0)
            {
                int decimalPart = (int)(decPart * 100);
                words += " and " + ConvertToWords(decimalPart) + " Paise";
            }

            return words + " Rupees";
        }

        private static string ConvertToWords(int number)
        {
            if (number == 0)
                return "";

            if (number < 20)
                return ones[number];

            if (number < 100)
                return tens[number / 10] + ones[number % 10];

            if (number < 1000)
                return ones[number / 100] + " Hundred " + ConvertToWords(number % 100);

            if (number < 100000)
                return ones[number / 1000] + " Thousand " + ConvertToWords(number % 1000);

            if (number < 10000000)
                return ones[number / 100000] + " Lakh " + ConvertToWords(number % 100000);

            return ConvertToWords(number / 10000000) + " Crore " + ConvertToWords(number % 10000000);
        }
    }
}
