using Master.Dto.Masters;
using Master.Dto.Shared;
using Master.ServiceBus.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Master.Service.Middleware;
using Core;
using Common.Repository;

namespace Master.Service.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class NodalOfficerController : ControllerBase
    {
        private readonly IUnitOfWorkService _IUnitOfWorkService;
        private readonly LogsService _logsService;
        public NodalOfficerController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            _IUnitOfWorkService = unitOfWorkService;
            _logsService = logsService;
        }

        [HttpPost]
        public async Task<ResponseModel> GetNodalOfficer(NodalOfficerFilterModel objModel)
        {
            try
            {
                var loginUserData = UserSession.Current;
                if (loginUserData.RoleId == (int)AccessRoles.Department)
                {
                    objModel.AdmDeptId = loginUserData.DepartmentId;
                }
                if (loginUserData.RoleId == (int)AccessRoles.Unit)
                {
                    objModel.AdmDeptId = loginUserData.DepartmentId;
                    objModel.UnitId = loginUserData.UnitId;
                }
                if (loginUserData.RoleId == (int)AccessRoles.SAD)
                {
                    objModel.DistrictId = loginUserData.DistrictId;
                }
                if (loginUserData.RoleId == (int)AccessRoles.NodalHod)
                {
                    objModel.AdmDeptId = loginUserData.DepartmentId;
                }
                if (loginUserData.RoleId == (int)AccessRoles.DepartmentD)
                {
                    objModel.AdmDeptId = loginUserData.DepartmentId;
                    objModel.DistrictId = loginUserData.DistrictId;
                }
                return await _IUnitOfWorkService.NodalOfficerServiceBus.GetNodalOfficer(objModel, loginUserData.RoleId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetNodalOfficer", ex.Message, ex.StackTrace, ex.Source, "MasterService/NodalOfficerController/GetNodalOfficer");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetNodalOfficerDropdownList()
        {
            try
            {
                return await _IUnitOfWorkService.NodalOfficerServiceBus.GetNodalOfficerDropdownList();
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetNodalOfficerDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/NodalOfficerController/GetNodalOfficerDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> AddEditNodalOfficer(NodalOfficerModel objModel)
        {
            try
            {
                var LoginUserData = UserSession.Current;
                if (LoginUserData.DepartmentId > 0)
                    objModel.AdmDeptId = LoginUserData.DepartmentId;
                if (LoginUserData.UnitId > 0)
                    objModel.UnitId = LoginUserData.UnitId;
                objModel.CreatedBy = LoginUserData.UserId;
                objModel.UpdatedBy = LoginUserData.UserId;
                return await _IUnitOfWorkService.NodalOfficerServiceBus.AddEditNodalOfficer(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditNodalOfficer", ex.Message, ex.StackTrace, ex.Source, "MasterService/NodalOfficerController/AddEditNodalOfficer");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> ActiveDeactiveNodalOfficer(NodalOfficerActiveDeactiveModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.UpdatedBy = UserId;
                objModel.DeleteBy = UserId;
                return await _IUnitOfWorkService.NodalOfficerServiceBus.ActiveDeactiveNodalOfficer(objModel, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveNodalOfficer", ex.Message, ex.StackTrace, ex.Source, "MasterService/NodalOfficerController/ActiveDeactiveNodalOfficer");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

    }
}
