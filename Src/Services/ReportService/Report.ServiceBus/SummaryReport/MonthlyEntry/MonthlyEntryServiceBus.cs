using Report.Dto.Global;
using Report.Dto.SummaryReports.MonthlyEntry;
using Report.Repository.UnitOfwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Common;

namespace Report.ServiceBus.SummaryReport.MonthlyEntry
{
    public class MonthlyEntryServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : IMonthlyEntryServiceBus
    {
        private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
        private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

        public ResponseModel GetMonthlyEntryStatusReport(Nullable<int> departmentId, Nullable<int> unitId, Nullable<int> officeId, Nullable<int> month, Nullable<int> year, Nullable<int> districtId, Nullable<int> roleid, int pageSize, int currentPage)
        {
            try
            {
                var data = _IUnitOfWorkRepository.MonthlyEntry.GetMonthlyEntryStatusReport(departmentId, unitId, officeId, month, year, districtId, roleid,pageSize,currentPage);
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
