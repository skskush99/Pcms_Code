using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Dto.SummaryReports.MonthlyEntry
{
    public class MonthlyReportSummary
    {
        public int No { get; set; }
        public string AdmDepttName { get; set; }
        public string UnitName { get; set; }
        public string OfficeName { get; set; }
        public Nullable<int> OfficeId { get; set; }
        public Nullable<int> TotalC_5 { get; set; }
        public Nullable<int> TotalC_9 { get; set; }
        public Nullable<int> TotalC_13 { get; set; }
        public Nullable<int> TotalC_17 { get; set; }
    }
}
