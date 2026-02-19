using System.Globalization;

namespace ReportService.Common
{
    public class Application
    {
        public Application()
        {
            //
            // TODO: Add constructor logic here

            //
        }
        public static DateTime MinDate
        {
            get
            {
                return DateTime.ParseExact("01/01/1947", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
        }
    }
}
