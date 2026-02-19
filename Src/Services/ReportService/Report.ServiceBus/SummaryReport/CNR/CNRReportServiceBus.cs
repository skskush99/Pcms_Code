using Report.Dto.Global;
using Report.Dto.SummaryReports.CNR;
using Report.Repository.UnitOfwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Common;

namespace Report.ServiceBus.SummaryReport.CNR
{
    public class CNRReportServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : ICNRReportServiceBus
    {
        private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
        private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

        public ResponseModel GetCNRReport(Nullable<int> admDepttId, Nullable<int> districtId, Nullable<int> unitId, Nullable<int> officeId, Nullable<int> lavelId, int pageSize, int currentPage)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CNRReport.GetCNRReport(admDepttId,districtId,unitId,officeId,lavelId,pageSize,currentPage);
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
       public ResponseModel GetCNRListSadReport(ref DataPagingModel TablePaging)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CNRReport.GetCNRListSadReport(ref TablePaging);
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

       public ResponseModel GetCNRListReport(ref DataPagingModel TablePaging, int DepartmentId, int UnitId, int OfficeId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CNRReport.GetCNRListReport(ref TablePaging,DepartmentId,UnitId,OfficeId);
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
