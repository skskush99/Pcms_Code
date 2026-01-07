using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Core.Utils
{
    public static class CommonUtility
    {
        public static String GetRandomNumber(Int32 numberLenght)
        {
            Random random = new();
            String randomNumber = "";
            for (Int32 index = 0; index < numberLenght; index++)
            {
                randomNumber = string.Concat(randomNumber, random.Next(9).ToString());
            }
            return randomNumber;
        }
        public static void GetNameParts(string fullName, out string firstName, out string middleName, out string lastName)
        {
            try
            {
                firstName = lastName = middleName = "";
                string[] arr = fullName.Split(' ');
                if (arr[0] != null)
                    firstName = arr[0];
                if (arr.Length >= 2 && arr[1] != null)
                    lastName = arr[1];
                if (arr.Length >= 3 && arr[2] != null)
                {
                    lastName = arr[2];
                    middleName = arr[1];
                }
            }
            catch (Exception)
            { throw; }

        }
        public static string GetFinacialYear(DateTime? dt = null)
        {
            if (dt == null)
                dt = DateTime.Now;
            int currentYear = dt.Value.Year;//2014
            if (dt.Value.Month < 4)
                currentYear--;
            string returnValue = currentYear.ToString() + "-" + (currentYear + 1).ToString();
            return returnValue;
        }

        public static long GetFinacialYearInNumber(DateTime? dt = null)
        {
            if (dt == null)
                dt = DateTime.Now;
            int currentYear = dt.Value.Year;//2014
            if (dt.Value.Month < 4)
                currentYear--;
            string returnValue = currentYear.ToString() + (currentYear + 1).ToString();
            return Convert.ToInt64(returnValue);
        }
        public static DateTime GetStartDateOfFinancialYear(DateTime? dt = null)
        {
            if (dt == null)
                dt = DateTime.Now;
            int currentYear = dt.Value.Year;
            int currentMonth = dt.Value.Month;
            DateTime startDate;
            if (currentMonth <= 3)
            {
                startDate = new DateTime(currentYear - 1, 4, 1);
            }
            else
            {
                startDate = new DateTime(currentYear, 4, 1);
            }
            return startDate;
        }
        public static DateTime GetEndDateOfFinancialYear(DateTime? dt = null)
        {
            if (dt == null)
                dt = DateTime.Now;
            int currentYear = dt.Value.Year;
            int currentMonth = dt.Value.Month;
            DateTime endDate;
            if (currentMonth <= 3)
            {
                endDate = new DateTime(currentYear, 3, 31);
            }
            else
            {
                endDate = new DateTime(currentYear + 1, 3, 31);
            }
            return endDate;
        }
        public static List<dynamic> ConvertJsonToDynamicList(string jsonString)
        {
            // Parse the JSON string to a dynamic object
            dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(jsonString);

            // Convert the dynamic object to a list of dynamic objects
            List<dynamic> dynamicList = new List<dynamic>();

            // Iterate through the properties of the dynamic object and add them to the list
            foreach (var property in ((JObject)jsonObject).Properties())
            {
                dynamicList.Add(new { Key = property.Name, Value = property.Value.ToString() });
            }

            return dynamicList;
        }

        public static string DownloadFile(string filePath)
        {
            try
            {
                byte[] fileContent = File.ReadAllBytes(filePath);
                string base64Data = Convert.ToBase64String(fileContent);
                string dataUrl = $"data:application/octet-stream;base64,{base64Data}";
                return dataUrl;
            }
            catch
            {
                return null;
            }
        }
       
        #region Useing Sewadwaar
        public static string DecryptSewadwaar(string inputText)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(inputText);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount - 1 + 1];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }

        #endregion

    }
}
