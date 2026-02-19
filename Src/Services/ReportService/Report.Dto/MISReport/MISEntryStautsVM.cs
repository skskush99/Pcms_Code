using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Dto.MISReport
{
    public class MISEntryStautsVM
    {

    }

    public class LogReportFilterModel
    {
        public string? LogType { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }
}
