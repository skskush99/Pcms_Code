using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Dto.SummaryReports.DistrictLevel
{
    public class DistrictWiseMonitoring
    {
        public Nullable<int> DepttID { get; set; }
        public Nullable<int> UnitID { get; set; }
        public Nullable<int> OfficeID { get; set; }
        public Nullable<int> DistrictID { get; set; }
        public Nullable<int> LevelID { get; set; }
        public string AdmDepttName { get; set; }
        public string UnitName { get; set; }
        public string OfficeName { get; set; }
        public string DistrictName { get; set; }
        public Nullable<int> Total { get; set; }
        public Nullable<int> TotalEntryMonthly { get; set; }
        public Nullable<int> TotalRedCategory { get; set; }
        public Nullable<int> RplyNotFile { get; set; }
        public Nullable<int> TotalContempt { get; set; }
    }
}
