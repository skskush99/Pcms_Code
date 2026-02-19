using Report.Dto.Global;
using Report.Dto.MISReport;
using Report.Dto.MISReport.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.ServiceBus.MISReportService.Login
{
    public interface ILoginReportServiceBus
    {
        ResponseModel GetLoginDetailReport(Nullable<System.DateTime> fromDate, Nullable<System.DateTime> toDate, Nullable<int> departmentId, Nullable<int> unitId, Nullable<int> officeId, Nullable<int> oicId, int pageSize, int currentPage);
        ResponseModel GetLogReport(LogReportFilterModel objModel);

    }
}
