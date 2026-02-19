using Report.Dto.Global;
using Report.Dto.MISReport.NextHearing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Repository.MISReport.NextHearing
{
    public interface INextHearingUpdateRepository
    {
        IEnumerable<UpdateNextHearingHistory> GetNextHearingUpdateReport(DataPagingModel TablePaging);
    }
}
