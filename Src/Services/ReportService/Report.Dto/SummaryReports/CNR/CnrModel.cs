using Report.Dto.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Dto.SummaryReports.CNR
{
    public class CnrModel
    {
        public int RowNum { get; set; }
        public string LavelWise { get; set; }
        public int LavelId { get; set; }
        public int JprCnrNotFilled { get; set; }
        public int JprCnrFilled { get; set; }
        public int JuCnrNotFilled { get; set; }
        public int JuCnrFilled { get; set; }
        public int OtrCnrNotFilled { get; set; }
        public int OtrCnrFilled { get; set; }
    }

    public class CnrFianlModel
    {
        public IEnumerable<CnrModel> CNRList { get; set; }
        public DataPagingModel TablePaging { get; set; }
    }
}
