using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Dto.SummaryReports.CNR
{
    public class CNR_Report
    {
        public Nullable<int> CnrNotFilled { get; set; }
        public Nullable<int> CnrFilled { get; set; }
        public Nullable<int> DistrictId { get; set; }
        public string DistrictName { get; set; }
        public Nullable<int> AdmDepttId { get; set; }
        public string AdmDepttShortName { get; set; }
        public Nullable<int> UnitId { get; set; }
        public string UnitName { get; set; }
        public Nullable<int> OfficeId { get; set; }
        public string OfficeName { get; set; }
        public Nullable<int> RowNum { get; set; }
    }
}
