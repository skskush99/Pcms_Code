using Report.Dto.Global;
using Report.Dto.SummaryReports.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Repository.SummaryReports.User
{
    public interface IUserRegistrationRepository
    {
        ResponseModel GetUserRegistrationSummaryReport(Nullable<int> departmentId, Nullable<int> unitId, Nullable<int> office, Nullable<int> roleId, int pageSize, int currentPage);
    }
}
