using Report.Dto.Global;
using Report.Dto.SummaryReports.MonthlyEntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Repository.SummaryReports.MonthlyEntry
{
    public interface IMonthlyEntryRepository
    {
        ResponseModel GetMonthlyEntryStatusReport(Nullable<int> departmentId, Nullable<int> unitId, Nullable<int> officeId, Nullable<int> month, Nullable<int> year, Nullable<int> districtId, Nullable<int> roleid, int pageSize, int currentPage);
    }
}
