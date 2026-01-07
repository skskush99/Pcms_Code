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
    public class UnitsDepartmentController : ControllerBase
    {
        private readonly IUnitOfWorkService _IUnitOfWorkService;
        private readonly LogsService _logsService;
        public UnitsDepartmentController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            _IUnitOfWorkService = unitOfWorkService;
            _logsService = logsService;
        }

        [HttpPost]
        public async Task<ResponseModel> GetUnitDepartment(UnitsDepartmentFilterModel objModel)
        {
            try
            {
                var loginUserData = UserSession.Current;
                if (loginUserData.DepartmentId > 0)
                    objModel.AdmDeptId = loginUserData.DepartmentId;
                return await _IUnitOfWorkService.UnitsDepartmentServiceBus.GetUnitDepartment(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetUnitDepartment", ex.Message, ex.StackTrace, ex.Source, "MasterService/UnitsDepartmentController/GetUnitDepartment");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> GetUnitDepartmentRajMaster(UnitsDepartmentFilterModel objModel)
        {
            try
            {
                var loginUserData = UserSession.Current;
                if (loginUserData.DepartmentId > 0)
                    objModel.AdmDeptId = loginUserData.DepartmentId;
                return await _IUnitOfWorkService.UnitsDepartmentServiceBus.GetUnitDepartmentRajMaster(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetUnitDepartmentRajMaster", ex.Message, ex.StackTrace, ex.Source, "MasterService/UnitsDepartmentController/GetUnitDepartmentRajMaster");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetUnitDepartmentDropdownList(int AdmDptID)
        {
            try
            {
                var loginUserData = UserSession.Current;
                if (loginUserData.DepartmentId > 0)
                    AdmDptID = loginUserData.DepartmentId;
                return await _IUnitOfWorkService.UnitsDepartmentServiceBus.GetUnitDepartmentDropdownList(AdmDptID, loginUserData.UnitId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetUnitDepartmentDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/UnitsDepartmentController/GetUnitDepartmentDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetUnitDepartmentRajMasterDropdownList(int AdmDptID)
        {
            try
            {
                var loginUserData = UserSession.Current;
                if (loginUserData.DepartmentId > 0)
                    AdmDptID = loginUserData.DepartmentId;
                return await _IUnitOfWorkService.UnitsDepartmentServiceBus.GetUnitDepartmentRajMasterDropdownList(AdmDptID, loginUserData.UnitId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetUnitDepartmentRajMasterDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/UnitsDepartmentController/GetUnitDepartmentRajMasterDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetDepartmentWiseUnitDropdownList(int AdmDptID = 0)
        {
            try
            {
                return await _IUnitOfWorkService.UnitsDepartmentServiceBus.GetDepartmentWiseUnitDropdownList(AdmDptID);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDepartmentWiseUnitDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/UnitsDepartmentController/GetDepartmentWiseUnitDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetDepartmentWiseUnitRajMasterDropdownList(int AdmDptID = 0)
        {
            try
            {
                return await _IUnitOfWorkService.UnitsDepartmentServiceBus.GetDepartmentWiseUnitRajMasterDropdownList(AdmDptID);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDepartmentWiseUnitRajMasterDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/UnitsDepartmentController/GetDepartmentWiseUnitRajMasterDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> AddEditUnitDepartment(UnitsDepartmentModel objModel, int UnitId)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.CreatedBy = UserId;
                objModel.UpdatedBy = UserId;
                return await _IUnitOfWorkService.UnitsDepartmentServiceBus.AddEditUnitDepartment(objModel, UnitId, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditUnitDepartment", ex.Message, ex.StackTrace, ex.Source, "MasterService/UnitsDepartmentController/AddEditUnitDepartment");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> ActiveDeactiveUnitDepartment(UnitsDepartmentActiveDeactiveModel objModel, int UnitId)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.UpdatedBy = UserId;
                return await _IUnitOfWorkService.UnitsDepartmentServiceBus.ActiveDeactiveUnitDepartment(objModel, UnitId, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveUnitDepartment", ex.Message, ex.StackTrace, ex.Source, "MasterService/UnitsDepartmentController/ActiveDeactiveUnitDepartment");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> ActiveDeactiveUnitDepartmentRajMaster(UnitsDepartmentActiveDeactiveModel objModel, int UnitId)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.UpdatedBy = UserId;
                return await _IUnitOfWorkService.UnitsDepartmentServiceBus.ActiveDeactiveUnitDepartmentRajMaster(objModel, UnitId, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveUnitDepartmentRajMaster", ex.Message, ex.StackTrace, ex.Source, "MasterService/UnitsDepartmentController/ActiveDeactiveUnitDepartmentRajMaster");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

    }
}
