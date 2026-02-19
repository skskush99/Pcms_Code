using Report.ServiceBus.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Report.Dto.Dashboard;
using Microsoft.AspNetCore.Authorization;
using ReportService.Middleware;
using Common.Repository;

namespace ReportService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IUnitOfWorkService unitOfWork;
        private readonly LogsService _logsService;
        public DashboardController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            unitOfWork = unitOfWorkService;
            _logsService = logsService;
        }

        [HttpPost]
        public async Task<DashboardDataResponseModel> GetDashboardData(DashboardFilterModel objModel)
        {
            try
            {
                var loginUserData = UserSession.Current;
                if (loginUserData.DepartmentId > 0)
                    objModel.AdmDepttId = loginUserData.DepartmentId;
                if (loginUserData.UnitId > 0)
                    objModel.UnitId = loginUserData.UnitId;
                if (loginUserData.OfficeId > 0)
                    objModel.OfficeId = loginUserData.OfficeId;
                if (loginUserData.OICId > 0)
                    objModel.OICId = loginUserData.OICId;
                if (loginUserData.LawyerId > 0)
                    objModel.LawyerId = loginUserData.LawyerId;
                if (loginUserData.DistrictId > 0 && (loginUserData.RoleId == 6 || loginUserData.RoleId == 7))
                    objModel.DistrictId = loginUserData.DistrictId;
                objModel.RoleId = loginUserData.RoleId;
                return await unitOfWork.Dashboard.GetDashboardData(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDashboardData", ex.Message, ex.StackTrace, ex.Source, "ReportService/DashboardController/GetDashboardData");
                return new DashboardDataResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<DashboardResponseModel> GetPendingReportDistrictWise(DashboardFilterModel objModel)
        {
            try
            {
                var loginUserData = UserSession.Current;
                if (loginUserData.DepartmentId > 0)
                    objModel.AdmDepttId = loginUserData.DepartmentId;
                if (loginUserData.UnitId > 0)
                    objModel.UnitId = loginUserData.UnitId;
                if (loginUserData.OfficeId > 0)
                    objModel.OfficeId = loginUserData.OfficeId;
                if (loginUserData.OICId > 0)
                    objModel.OICId = loginUserData.OICId;
                if (loginUserData.LawyerId > 0)
                    objModel.LawyerId = loginUserData.LawyerId;
                if (loginUserData.DistrictId > 0 && (loginUserData.RoleId == 6 || loginUserData.RoleId == 7))
                    objModel.DistrictId = loginUserData.DistrictId;
                objModel.RoleId = loginUserData.RoleId;
                return await unitOfWork.Dashboard.GetPendingReportDistrictWise(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetPendingReportDistrictWise", ex.Message, ex.StackTrace, ex.Source, "ReportService/DashboardController/GetPendingReportDistrictWise");
                return new DashboardResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<DashboardResponseModel> GetPendingReportDepartmentWise(DashboardFilterModel objModel)
        {
            try
            {
                var loginUserData = UserSession.Current;
                if (loginUserData.DepartmentId > 0)
                    objModel.AdmDepttId = loginUserData.DepartmentId;
                if (loginUserData.UnitId > 0)
                    objModel.UnitId = loginUserData.UnitId;
                if (loginUserData.OfficeId > 0)
                    objModel.OfficeId = loginUserData.OfficeId;
                if (loginUserData.OICId > 0)
                    objModel.OICId = loginUserData.OICId;
                if (loginUserData.LawyerId > 0)
                    objModel.LawyerId = loginUserData.LawyerId;
                if (loginUserData.DistrictId > 0 && (loginUserData.RoleId == 6 || loginUserData.RoleId == 7))
                    objModel.DistrictId = loginUserData.DistrictId;
                objModel.RoleId = loginUserData.RoleId;
                return await unitOfWork.Dashboard.GetPendingReportDepartmentWise(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetPendingReportDepartmentWise", ex.Message, ex.StackTrace, ex.Source, "ReportService/DashboardController/GetPendingReportDepartmentWise");
                return new DashboardResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<DashboardResponseModel> GetPendingReportOfficeWise(DashboardFilterModel objModel)
        {
            try
            {
                var loginUserData = UserSession.Current;
                if (loginUserData.DepartmentId > 0)
                    objModel.AdmDepttId = loginUserData.DepartmentId;
                if (loginUserData.UnitId > 0)
                    objModel.UnitId = loginUserData.UnitId;
                if (loginUserData.OfficeId > 0)
                    objModel.OfficeId = loginUserData.OfficeId;
                if (loginUserData.OICId > 0)
                    objModel.OICId = loginUserData.OICId;
                if (loginUserData.LawyerId > 0)
                    objModel.LawyerId = loginUserData.LawyerId;
                if (loginUserData.DistrictId > 0 && (loginUserData.RoleId == 6 || loginUserData.RoleId == 7))
                    objModel.DistrictId = loginUserData.DistrictId;
                objModel.RoleId = loginUserData.RoleId;
                return await unitOfWork.Dashboard.GetPendingReportOfficeWise(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetPendingReportOfficeWise", ex.Message, ex.StackTrace, ex.Source, "ReportService/DashboardController/GetPendingReportOfficeWise");
                return new DashboardResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<DashboardResponseModel> GetPendingDetailReport(PendingDetailReportFilterModel objModel)
        {
            try
            {
                var loginUserData = UserSession.Current;
                if (loginUserData.DepartmentId > 0)
                    objModel.AdmDepttId = loginUserData.DepartmentId;
                if (loginUserData.UnitId > 0)
                    objModel.UnitId = loginUserData.UnitId;
                if (loginUserData.OfficeId > 0)
                    objModel.OfficeId = loginUserData.OfficeId;
                if (loginUserData.OICId > 0)
                    objModel.OICId = loginUserData.OICId;
                if (loginUserData.LawyerId > 0)
                    objModel.LawyerId = loginUserData.LawyerId;
                if (loginUserData.DistrictId > 0 && (loginUserData.RoleId == 6 || loginUserData.RoleId == 7))
                    objModel.DistrictId = loginUserData.DistrictId;
                objModel.RoleId = loginUserData.RoleId;
                return await unitOfWork.Dashboard.GetPendingDetailReport(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetPendingDetailReport", ex.Message, ex.StackTrace, ex.Source, "ReportService/DashboardController/GetPendingDetailReport");
                return new DashboardResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<DashboardResponseWithPaginationModel> GetDashboardPendencyReport(DashboardPendencyReportFilterModel objModel)
        {
            try
            {
                var loginUserData = UserSession.Current;
                if (loginUserData.DepartmentId > 0)
                    objModel.AdmDepttId = loginUserData.DepartmentId;
                if (loginUserData.UnitId > 0)
                    objModel.UnitId = loginUserData.UnitId;
                if (loginUserData.OfficeId > 0)
                    objModel.OfficeId = loginUserData.OfficeId;
                if (loginUserData.OICId > 0)
                    objModel.OICId = loginUserData.OICId;
                if (loginUserData.LawyerId > 0)
                    objModel.LawyerId = loginUserData.LawyerId;
                if (loginUserData.DistrictId > 0 && (loginUserData.RoleId == 6 || loginUserData.RoleId == 7))
                    objModel.DistrictId = loginUserData.DistrictId;
                objModel.RoleId = loginUserData.RoleId;
                return await unitOfWork.Dashboard.GetDashboardPendencyReport(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDashboardPendencyReport", ex.Message, ex.StackTrace, ex.Source, "ReportService/DashboardController/GetDashboardPendencyReport");
                return new DashboardResponseWithPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        

    }
}
