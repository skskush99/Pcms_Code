using Master.Dto.Shared;
using Master.Dto.Users;
using Master.ServiceBus.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Master.Service.Middleware;
using Common.Repository;
namespace PcmsUserManagementMicroServices.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserLoginController : ControllerBase
    {
        private readonly LogsService _logsService;
        private readonly IUnitOfWorkService unitOfWork;
        public UserLoginController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            unitOfWork = unitOfWorkService;
            _logsService = logsService; 
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> Login(LoginModel objModel)
        {
            try
            {
                return await unitOfWork.UserLogins.Login(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "Login", ex.Message, ex.StackTrace, ex.Source, "MasterService/UserLoginController/Login");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetUserMenu(long RoleId = 0)
        {
            try
            {
                var LoginUserData = UserSession.Current;
                return await unitOfWork.UserLogins.GetUserMenulist(LoginUserData.RoleId, LoginUserData.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetUserMenu", ex.Message, ex.StackTrace, ex.Source, "MasterService/UserLoginController/GetUserMenu");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> LoginLogs()
        {
            try
            {
                var LoginUserData = UserSession.Current;
                var authUser = new TokenAuthModel()
                {
                    //Token = Core.Common.Encrypt(LoginUserData.UserId + "|" + LoginUserData.RoleId + "|" + LoginUserData.DepartmentId + "|" + LoginUserData.UnitId + "|" + LoginUserData.OfficeId + "|" + LoginUserData.OICId + "|" + LoginUserData.DistrictId + "|" + LoginUserData.LawyerId + "|" + LoginUserData.SSOID + "|" + LoginUserData.LoginOn + "|" + LoginUserData.IPAddress),
                    Token = Core.Common.Encrypt(LoginUserData.UserId + "|" + LoginUserData.RoleId + "|" + LoginUserData.DepartmentId + "|" + LoginUserData.UnitId + "|" + LoginUserData.OfficeId + "|" + LoginUserData.DistrictId + "|" + LoginUserData.SSOID + "|" + LoginUserData.LoginOn + "|" + LoginUserData.IPAddress),
                    Status = true,
                    Message = "",
                    UserId = LoginUserData.UserId,
                    RoleId = LoginUserData.RoleId,
                    LoginOn = LoginUserData.LoginOn,
                    IPAddress = LoginUserData.IPAddress
                };
                return await unitOfWork.UserLogins.Loginlogs(authUser);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "LoginLogs", ex.Message, ex.StackTrace, ex.Source, "MasterService/UserLoginController/LoginLogs");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> Logout()
        {
            try
            {
                var LoginUserData = UserSession.Current;
                var authUser = new TokenAuthModel()
                {
                    Token = Core.Common.Encrypt(LoginUserData.UserId + "|" + LoginUserData.RoleId + "|" + LoginUserData.DepartmentId + "|" + LoginUserData.UnitId + "|" + LoginUserData.OfficeId + "|" + LoginUserData.OICId + "|" + LoginUserData.DistrictId + "|" + LoginUserData.LawyerId + "|" + LoginUserData.SSOID + "|" + LoginUserData.LoginOn + "|" + LoginUserData.IPAddress),
                    Status = true,
                    Message = "",
                    UserId = LoginUserData.UserId,
                    RoleId = LoginUserData.RoleId,
                    LoginOn = LoginUserData.LoginOn,
                    IPAddress = LoginUserData.IPAddress
                };
                return await unitOfWork.UserLogins.Logout(authUser);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "Logout", ex.Message, ex.StackTrace, ex.Source, "MasterService/UserLoginController/Logout");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

    }
}
