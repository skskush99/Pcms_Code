using NextHearing.Dto.NextHearingModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextHearing.ServiceBus.NextHearingService
{
    public interface INextHearingService
    {
        Task<List<NextHearingData>> GetNextHearingListforUpdate();
        Task<NextHearingResponseData?> UpdateDecideDateUsingCNR(string cnr, string DecidedDate, Int64 caseid, int hearing_SNo = 1);
        Task<NextHearingResponseData1?> UpdateDecideDateUsingCNR1(string cnr, string DecidedDate, Int64 caseid, int hearing_SNo = 1);
    }
}
