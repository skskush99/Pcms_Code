using Report.ServiceBus.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Report.Dto.DetailReports;
using Microsoft.AspNetCore.Authorization;
using ReportService.Middleware;
using Core;
using Common.Repository;

namespace ReportService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class DetailReportsController : ControllerBase
    {
        private readonly IUnitOfWorkService unitOfWork;
        private readonly LogsService _logsService;
        public DetailReportsController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            unitOfWork = unitOfWorkService;
            _logsService = logsService;
        }
        

        [HttpPost]
        public async Task<DetailReportsResponseModel> GetDistrictWiseReport(DistrictWiseModel objModel)
        {
            try
            {
                #region User Wise Filter
                var loginUserData = UserSession.Current;
                if (loginUserData.RoleId == (int)AccessRoles.Department)
                {
                    objModel.DepartmentId = loginUserData.DepartmentId;
                }
                #endregion
                return await unitOfWork.DistrictWiseService.GetDistrictWiseReport(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDistrictWiseReport", ex.Message, ex.StackTrace, ex.Source, "ReportService/DetailReportsController/GetDistrictWiseReport");
                return new DetailReportsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<DetailReportsResponseModel> GetMahilaAtayacharIPCReport(MahilaAtayacharIPCModel objModel)
        {
            try
            {
                #region User Wise Filter
                var loginUserData = UserSession.Current;
                if (loginUserData.RoleId == (int)AccessRoles.Department)
                {
                    objModel.DepartmentId = loginUserData.DepartmentId;
                }
                #endregion
                return await unitOfWork.MahilaAtayacharService.GetMahilaAtayacharIPCReport(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetMahilaAtayacharIPCReport", ex.Message, ex.StackTrace, ex.Source, "ReportService/DetailReportsController/GetMahilaAtayacharIPCReport");
                return new DetailReportsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        [HttpPost]
        public async Task<DetailReportsResponseModel> GetMahilaAtayacharBNSReport(MahilaAtayacharBNSModel objModel)
        {
            try
            {
                #region User Wise Filter
                var loginUserData = UserSession.Current;
                if (loginUserData.RoleId == (int)AccessRoles.Department)
                {
                    objModel.DepartmentId = loginUserData.DepartmentId;
                }
                #endregion
                return await unitOfWork.MahilaAtayacharService.GetMahilaAtayacharBNSReport(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetMahilaAtayacharBNSReport", ex.Message, ex.StackTrace, ex.Source, "ReportService/DetailReportsController/GetMahilaAtayacharBNSReport");
                return new DetailReportsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


    }
}
