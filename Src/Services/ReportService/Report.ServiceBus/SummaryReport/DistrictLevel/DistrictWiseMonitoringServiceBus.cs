using Report.Dto.SummaryReports.DistrictLevel;
using Report.Repository.UnitOfwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Common;

namespace Report.ServiceBus.SummaryReport.DistrictLevel
{
    public class DistrictWiseMonitoringServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : IDistrictWiseMonitoringServiceBus
    {
        private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
        private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

        public IEnumerable<DistrictWiseMonitoring> GetDistrictWiseMonitoringReport(Nullable<int> deptId, Nullable<int> unitId, Nullable<int> officeId, Nullable<int> districtId, Nullable<int> status, Nullable<int> level)
        {
            try
            {
                var data = _IUnitOfWorkRepository.DistrictWiseMonitoring.GetDistrictWiseMonitoringReport(deptId, unitId,  officeId,  districtId,  status, level);
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
