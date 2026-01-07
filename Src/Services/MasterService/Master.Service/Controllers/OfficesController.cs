using Master.Dto.Masters;
using Master.Dto.Shared;
using Master.ServiceBus.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Master.Service.Middleware;
using Common.Repository;

namespace Master.Service.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class OfficesController : ControllerBase
    {
        private readonly IUnitOfWorkService _IUnitOfWorkService;
        private readonly LogsService _logsService;
        public OfficesController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            _IUnitOfWorkService = unitOfWorkService;
            _logsService = logsService;
        }

        [HttpPost]
        public async Task<ResponseModel> GetOfficesList(OfficesFilterModel objModel)
        {
            try
            {
                var loginUserData = UserSession.Current;
                //if (loginUserData.DepartmentId > 0)
                //    objModel.AdmDeptId = loginUserData.DepartmentId;
                //if (loginUserData.UnitId > 0)
                //    objModel.UnitId = loginUserData.UnitId;
                return await _IUnitOfWorkService.OfficeServiceBus.GetOffices(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetOfficesList", ex.Message, ex.StackTrace, ex.Source, "MasterService/OfficesController/GetOfficesList");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetOfficesDropdownList(int OfficeId = 0)
        {
            try
            {
                var loginUserData = UserSession.Current;
                //if (loginUserData.UnitId > 0)
                //    UnitId = loginUserData.UnitId;
                return await _IUnitOfWorkService.OfficeServiceBus.GetOfficesDropdownList(OfficeId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetOfficesDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/OfficesController/GetOfficesDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> AddEditOffices(OfficesRequestModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.Data.CreatedBy = UserId;
                objModel.Data.UpdatedBy = UserId;
                return await _IUnitOfWorkService.OfficeServiceBus.AddEditOffices(objModel.Data, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditOffices", ex.Message, ex.StackTrace, ex.Source, "MasterService/OfficesController/AddEditOffices");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> ActiveDeactiveOffices(OfficesActiveDeactiveModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.UpdatedBy = UserId;
                return await _IUnitOfWorkService.OfficeServiceBus.ActiveDeactiveOffices(objModel, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveOffices", ex.Message, ex.StackTrace, ex.Source, "MasterService/OfficesController/ActiveDeactiveOffices");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

    }
}
