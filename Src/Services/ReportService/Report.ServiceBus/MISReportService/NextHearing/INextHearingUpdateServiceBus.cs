using Report.Dto.Global;
using Report.Dto.MISReport.NextHearing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.ServiceBus.MISReportService.NextHearing
{
    public interface INextHearingUpdateServiceBus
    {
        IEnumerable<UpdateNextHearingHistory> GetNextHearingUpdateReport(DataPagingModel TablePaging);
    }
}
