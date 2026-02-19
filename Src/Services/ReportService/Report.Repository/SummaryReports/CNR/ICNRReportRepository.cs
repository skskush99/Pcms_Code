using Report.Dto.Global;
using Report.Dto.SummaryReports.CNR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Repository.SummaryReports.CNR
{
    public interface ICNRReportRepository
    {
        ResponseModel GetCNRReport(Nullable<int> admDepttId, Nullable<int> districtId, Nullable<int> unitId, Nullable<int> officeId, Nullable<int> lavelId, int pageSize, int currentPage);
        ResponseModel GetCNRListSadReport(ref DataPagingModel TablePaging);
        ResponseModel GetCNRListReport(ref DataPagingModel TablePaging, int DepartmentId, int UnitId, int OfficeId);
    }
}
