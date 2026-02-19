using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Dto.MISReport.NextHearing
{
    public class UpdateNextHearingHistory
    {
        public string RowNum { get; set; }
        public int Id { get; set; }
        public int caseid { get; set; }
        public string CNRNumber { get; set; }

        public string AdmDepttName { get; set; }
        public string UnitName { get; set; }
        public string OfficeName { get; set; }
        public string CourtName { get; set; }
        public string CaseDetail { get; set; }
        //public string NextHearingDate { get; set; }
        public string NextHearing_Date { get; set; }
        public string CreatedDate { get; set; }
        //public string CourtType { get; set; }
        public int TotalRecords { get; set; }
    }
}
