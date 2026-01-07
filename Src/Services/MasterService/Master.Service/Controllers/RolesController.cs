using Master.Dto.Roles;
using Master.Dto.Shared;
using Master.ServiceBus.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Master.Service.Middleware;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Common.Repository;
using System.Drawing.Printing;

namespace PcmsUserManagementMicroServices.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class RolesController : ControllerBase
    {
        private readonly IUnitOfWorkService unitOfWork;
        private readonly LogsService _logsService;
        public RolesController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            unitOfWork = unitOfWorkService;
            _logsService = logsService;
        }

        [HttpGet]
        public async Task<ResponseModel> GetRoles(int PageNo, int PageSize)
        {
            try
            {
                return await unitOfWork.Roles.GetRoles(PageNo, PageSize);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetRoles", ex.Message, ex.StackTrace, ex.Source, "MasterService/RolesController/GetRoles");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> RolesDropdownList()
        {
            try
            {
                return await unitOfWork.Roles.GetRolesDropdownList();
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "RolesDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/RolesController/RolesDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetRolesNodelOfficerDropdownList()
        {
            try
            {
                //string UniqueId = Guid.NewGuid().ToString();
                //_logsService.LogInformation($"{UniqueId} This is an Information log, general info about app flow.", UniqueId);
                ////_logger.LogTrace("LogTrace: Entering the LogAllLevels endpoint with Trace-level logging.");
                return await unitOfWork.Roles.GetRolesNodelOfficerDropdownList(UserSession.Current.RoleId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetRolesNodelOfficerDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/RolesController/GetRolesNodelOfficerDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> AddRole(RoleRequestModel objModel)
        {
            try
            {
                return await unitOfWork.Roles.AddRole(objModel.Data, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddRole", ex.Message, ex.StackTrace, ex.Source, "MasterService/RolesController/AddRole");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> ActiveDeactiveRole(RoleActiveDeactiveModel objModel)
        {
            try
            {
                return await unitOfWork.Roles.ActiveDeactiveRole(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveRole", ex.Message, ex.StackTrace, ex.Source, "MasterService/RolesController/ActiveDeactiveRole");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> DBAction(DBActionModel objModel)
        {
            try
            {
                var userLoginData = UserSession.Current;
                objModel.SSOID = userLoginData.SSOID;
                objModel.UserId = userLoginData.UserId;
                objModel.RoleId = userLoginData.RoleId;
                objModel.IPAddress = userLoginData.IPAddress;
                return await unitOfWork.Roles.DBAction(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DBAction", ex.Message, ex.StackTrace, ex.Source, "MasterService/RolesController/DBAction");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

    }
}
