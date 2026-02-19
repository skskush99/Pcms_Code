using Report.Dto.SummaryReports.DistrictLevel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Repository.SummaryReports.DistrictLevel
{
    public interface IDistrictWiseMonitoringRepository
    {
        IEnumerable<DistrictWiseMonitoring> GetDistrictWiseMonitoringReport(Nullable<int> deptId, Nullable<int> unitId, Nullable<int> officeId, Nullable<int> districtId, Nullable<int> status, Nullable<int> level);
    }
}
