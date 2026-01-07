using Master.Dto.Masters;
using Master.Dto.Shared;
using Master.Service.Middleware;
using Master.ServiceBus.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Common.Repository;

namespace Master.Service.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AdminDepartmentController : ControllerBase
    {
        private readonly LogsService _logsService;
        private readonly IUnitOfWorkService _IUnitOfWorkService;

        public AdminDepartmentController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            _IUnitOfWorkService = unitOfWorkService;
            _logsService = logsService;
        }

        [HttpPost]
        public async Task<ResponseModel> GetAdmDep(AdminRequestFilterModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                return await _IUnitOfWorkService.AdminDepartmentServiceBus.GetAdmDep(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetAdmDep", ex.Message, ex.StackTrace, ex.Source, "MasterService/AdminDepartmentController/GetAdmDep");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> AdmDepDropdownList()
        {
            try
            {
                return await _IUnitOfWorkService.AdminDepartmentServiceBus.GetAdmDepDropdownList(UserSession.Current.DepartmentId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AdmDepDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/AdminDepartmentController/AdmDepDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        [HttpPost]
        public async Task<ResponseModel> AddEditAdmDep(AdminRequestModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.Data.CreatedBy = UserId;
                objModel.Data.UpdatedBy = UserId;
                return await _IUnitOfWorkService.AdminDepartmentServiceBus.AddEditAdmDep(objModel.Data, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditAdmDep", ex.Message, ex.StackTrace, ex.Source, "MasterService/AdminDepartmentController/AddEditAdmDep");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> ActiveDeactiveAdmDep(AdminActiveDeactiveModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.UpdatedBy = UserId;
                return await _IUnitOfWorkService.AdminDepartmentServiceBus.ActiveDeactiveAdmDep(objModel, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveAdmDep", ex.Message, ex.StackTrace, ex.Source, "MasterService/AdminDepartmentController/ActiveDeactiveAdmDep");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


    }
}
