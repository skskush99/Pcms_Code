using Master.Dto.Shared;
using Master.Dto.Users;
using Master.ServiceBus.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Core.SsoEncryption;
using Master.Service.Middleware;
using Core;
using Common.Repository;

namespace PcmsUserManagementMicroServices.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly LogsService _logsService;
        private readonly IUnitOfWorkService unitOfWork;
        private IConfiguration Configuration;
        public UsersController(IUnitOfWorkService unitOfWorkService, IConfiguration configuration, LogsService logsService)
        {
            unitOfWork = unitOfWorkService;
            this.Configuration = configuration;
            _logsService = logsService;
        }

        [HttpPost]
        public async Task<ResponseModel> GetUserMapReqList(UsersMapReqFilterModel objModel)
        {
            try
            {
                var loginUserData = UserSession.Current;
                //if (loginUserData.RoleId == (int)AccessRoles.SAD || loginUserData.RoleId == (int)AccessRoles.DepartmentD)
                //{
                //    objModel.Data.RoleId = 0;
                //    objModel.Data.DistrictId = loginUserData.DistrictId;
                //}
                //if (loginUserData.RoleId == (int)AccessRoles.Unit)
                //{
                //    objModel.Data.RoleId = 0;
                //    objModel.Data.UnitId = loginUserData.UnitId;
                //}
                //if (loginUserData.RoleId == (int)AccessRoles.Office)
                //{
                //    objModel.Data.RoleId = loginUserData.RoleId;
                //    objModel.Data.OfficeId = loginUserData.OfficeId;
                //}
                //if (loginUserData.RoleId == (int)AccessRoles.DepartmentD)
                //{
                //    objModel.Data.DepartmentId = loginUserData.DepartmentId;
                //}
                //if (loginUserData.RoleId == (int)AccessRoles.Department)
                //{
                //    objModel.Data.DepartmentId = loginUserData.DepartmentId;
                //}
                return await unitOfWork.UserLogins.GetUserMapReqList(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetUsersList", ex.Message, ex.StackTrace, ex.Source, "MasterService/UsersController/GetUsersList");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> AddEditUserMapReq(UserMapReqAddEditModel objModel)
        {
            try
            {
                return await unitOfWork.UserLogins.AddEditUserMapReq(objModel.Data, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditUserMapReq", ex.Message, ex.StackTrace, ex.Source, "MasterService/UsersController/AddEditUser");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> MappedUserBySA(ApprovelUserModel objModel)
        {
            try
            {
                return await unitOfWork.UserLogins.MappedUserBySA(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "MappedUserBySA", ex.Message, ex.StackTrace, ex.Source, "MasterService/UsersController/MappedUserBySA");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> GetUsersList(UsersListReqestModel objModel)
        {
            try
            {
                var loginUserData = UserSession.Current;
                if (loginUserData.RoleId == (int)AccessRoles.SAD || loginUserData.RoleId == (int)AccessRoles.DepartmentD)
                {
                    objModel.Data.RoleId = 0;
                    objModel.Data.DistrictId = loginUserData.DistrictId;
                }
                if (loginUserData.RoleId == (int)AccessRoles.Unit)
                {
                    objModel.Data.RoleId = 0;
                    objModel.Data.UnitId = loginUserData.UnitId;
                }
                if (loginUserData.RoleId == (int)AccessRoles.Office)
                {
                    objModel.Data.RoleId = loginUserData.RoleId;
                    objModel.Data.OfficeId = loginUserData.OfficeId;
                }
                if (loginUserData.RoleId == (int)AccessRoles.DepartmentD)
                {
                    objModel.Data.DepartmentId = loginUserData.DepartmentId;
                }
                if (loginUserData.RoleId == (int)AccessRoles.Department)
                {
                    objModel.Data.DepartmentId = loginUserData.DepartmentId;
                }
                return await unitOfWork.UserLogins.GetUserList(objModel.Data);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetUsersList", ex.Message, ex.StackTrace, ex.Source, "MasterService/UsersController/GetUsersList");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> AddEditUser(UserAddEditModel objModel)
        {
            try
            {
                return await unitOfWork.UserLogins.AddEditUser(objModel.Data, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditUser", ex.Message, ex.StackTrace, ex.Source, "MasterService/UsersController/AddEditUser");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> ActiveDeactiveUser(ActiveDeactiveModel objModel)
        {
            try
            {
                return await unitOfWork.UserLogins.ActiveDeactiveUser(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveUser", ex.Message, ex.StackTrace, ex.Source, "MasterService/UsersController/ActiveDeactiveUser");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> MappedUser(MappedUserModel objModel)
        {
            try
            {
                return await unitOfWork.UserLogins.MappedUser(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "MappedUser", ex.Message, ex.StackTrace, ex.Source, "MasterService/UsersController/MappedUser");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> DemapUser(DemapUserModel objModel)
        {
            try
            {
                return await unitOfWork.UserLogins.DemapUser(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DemapUser", ex.Message, ex.StackTrace, ex.Source, "MasterService/UsersController/DemapUser");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> SSOLogin(LoginModel objModel)
        {
            try
            {
                objModel = new LoginModel
                {
                    SSOToken = objModel.SSOToken,
                    UserName = "PCMS.TEST",
                    Password = "R@jS$opcm21#",
                    IsSSOLogin = objModel.IsSSOLogin,
                    IPAddress = objModel.IPAddress
                };
                return await unitOfWork.UserLogins.SSOLogin(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "SSOLogin", ex.Message, ex.StackTrace, ex.Source, "MasterService/UsersController/SSOLogin");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> SsoProfile(SsoProfileModel objModel)
        {
            try
            {
                var encryptedPassword = AES.Encrypt(this.Configuration["SSOURL:WSPASSWORD"], this.Configuration["SSOURL:Encryption"]);
                var obj = new SsoProfileRequestModel
                {
                    SSOID = objModel.SSOID,
                    SsoBaseUrl = this.Configuration["SSOURL:GetUserDetailNew"],
                    UserName = this.Configuration["SSOURL:WSUSERNAME"],
                    Password = this.Configuration["SSOURL:WSPASSWORD"],
                    EncryptedPassword = encryptedPassword
                };
                return await unitOfWork.UserLogins.SsoProfile(obj);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "SsoProfile", ex.Message, ex.StackTrace, ex.Source, "MasterService/UsersController/SsoProfile");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

    }
}
