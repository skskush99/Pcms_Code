using Report.Dto.Global;
using Report.Dto.MISReport;
using Report.Dto.MISReport.Login;
using Report.Repository.UnitOfwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Common;

namespace Report.ServiceBus.MISReportService.Login
{
    public class LoginReportServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : ILoginReportServiceBus
    {
        private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
        private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

        public ResponseModel GetLoginDetailReport(Nullable<System.DateTime> fromDate, Nullable<System.DateTime> toDate, Nullable<int> departmentId, Nullable<int> unitId, Nullable<int> officeId, Nullable<int> oicId, int pageSize, int currentPage)
        {
            try
            {
                var data = _IUnitOfWorkRepository.LoginReport.GetLoginDetailReport(fromDate,toDate,departmentId,unitId,officeId,oicId, pageSize, currentPage);
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ResponseModel GetLogReport(LogReportFilterModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.LoginReport.GetLogReport(objModel);
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
