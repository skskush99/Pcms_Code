using Report.Dto.Global;
using Report.Dto.SummaryReports.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.ServiceBus.SummaryReport.User
{
    public interface IUserRegistrationServiceBus
    {
        ResponseModel GetUserRegistrationSummaryReport(Nullable<int> departmentId, Nullable<int> unitId, Nullable<int> office, Nullable<int> roleId, int pageSize, int currentPage);
    }
}
