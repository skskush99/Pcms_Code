using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Dto.SummaryReports.Action
{
    public class DashBoardActionTobeTakenNew
    {
        public string CourtName { get; set; }
        public int CourtId { get; set; }
        public Nullable<int> DistrictId { get; set; }
        public string DistrictName { get; set; }
        public string AdmDepttShortName { get; set; }
        public Nullable<int> ReplyNotFiledUpto3 { get; set; }
        public Nullable<int> ReplyNotFiledMoreThan3M { get; set; }
        public Nullable<int> ReplyNotFiledMoreThan1Year { get; set; }
        public Nullable<int> FactualReportMoreThen1Year { get; set; }
        public Nullable<int> MoreThan1YrTo10Yr { get; set; }
        public Nullable<int> MoreThan10Yr { get; set; }
        public Nullable<int> MoreThan20Yr { get; set; }
        public Nullable<int> OrderPendingComplianceUpto3M { get; set; }
        public Nullable<int> OrderPendingComplianceMoreThan3M { get; set; }
        public Nullable<int> OrderPendingComplianceMoreThan1Yr { get; set; }
        public Nullable<int> OrderPendingAppealUpto3M { get; set; }
        public Nullable<int> OrderPendingAppealMoreThan3M { get; set; }
        public Nullable<int> OrderPendingAppealMoreThan1Yr { get; set; }
        public Nullable<int> RedCategory { get; set; }
        public Nullable<int> ContemptCases { get; set; }
        public Nullable<int> casewithoutcaseno { get; set; }
    }
}

