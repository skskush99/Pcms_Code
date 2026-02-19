using Core.Models;
using Report.Dto.Dashboard;
using Report.Repository.UnitOfwork;
using static Core.Common;
namespace Report.ServiceBus.Dashboard
{
    public class DashboardServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : IDashboardServiceBus
    {
        private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
        private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

        public async Task<DashboardDataResponseModel> GetDashboardData(DashboardFilterModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.Dashboard.GetDashboardData(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DashboardResponseModel> GetPendingReportCourtWise(DashboardFilterModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.Dashboard.GetPendingReportCourtWise(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DashboardResponseModel> GetPendingReportDistrictWise(DashboardFilterModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.Dashboard.GetPendingReportDistrictWise(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DashboardResponseModel> GetPendingReportDepartmentWise(DashboardFilterModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.Dashboard.GetPendingReportDepartmentWise(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DashboardResponseModel> GetPendingReportOfficeWise(DashboardFilterModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.Dashboard.GetPendingReportOfficeWise(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        

        public Trn_CaseRegistrations GetCaseDetails(Nullable<int> CaseId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.Dashboard.GetCaseDetails(CaseId);
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DashboardResponseModel> GetPendingDetailReport(PendingDetailReportFilterModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.Dashboard.GetPendingDetailReport(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DashboardResponseWithPaginationModel> GetDashboardPendencyReport(DashboardPendencyReportFilterModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.Dashboard.GetDashboardPendencyReport(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        
    }
}
