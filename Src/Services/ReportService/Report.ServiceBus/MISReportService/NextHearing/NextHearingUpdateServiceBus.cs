using Report.Dto.Global;
using Report.Dto.MISReport.NextHearing;
using Report.Repository.UnitOfwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Common;

namespace Report.ServiceBus.MISReportService.NextHearing
{
   public class NextHearingUpdateServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : INextHearingUpdateServiceBus
    {
        private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
        private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

        public IEnumerable<UpdateNextHearingHistory> GetNextHearingUpdateReport(DataPagingModel TablePaging)
        {
            try
            {
                var data = _IUnitOfWorkRepository.NextHearingUpdate.GetNextHearingUpdateReport(TablePaging);
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
