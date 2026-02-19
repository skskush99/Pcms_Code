using Core.Models;
using Report.Dto.Dashboard;
namespace Report.Repository.Dashboard
{
    public interface IDashboardRepository
    {
        Task<DashboardDataResponseModel> GetDashboardData(DashboardFilterModel objModel);
        Task<DashboardResponseModel> GetPendingReportCourtWise(DashboardFilterModel objModel);
        Task<DashboardResponseModel> GetPendingReportDistrictWise(DashboardFilterModel objModel);
        Task<DashboardResponseModel> GetPendingReportDepartmentWise(DashboardFilterModel objModel);
        Task<DashboardResponseModel> GetPendingReportOfficeWise(DashboardFilterModel objModel);
        Trn_CaseRegistrations GetCaseDetails(Nullable<int> CaseId);
        Task<DashboardResponseModel> GetPendingDetailReport(PendingDetailReportFilterModel objModel);
        Task<DashboardResponseWithPaginationModel> GetDashboardPendencyReport(DashboardPendencyReportFilterModel objModel);
    }
}
