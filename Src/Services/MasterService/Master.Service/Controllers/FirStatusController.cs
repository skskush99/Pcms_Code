using Common.Repository;
using Master.Dto.Masters;
using Master.Service.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Master.ServiceBus.UnitOfWork;
using Master.Dto.Shared;

namespace Master.Service.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]

    public class FirStatusController : ControllerBase
    {
        private readonly LogsService _logsService;
        private readonly IUnitOfWorkService _IUnitOfWorkService;

        public FirStatusController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            _IUnitOfWorkService = unitOfWorkService;
            _logsService = logsService;
        }

        [HttpPost]
        public async Task<ResponseModel> GetFirStatus(FIRStatusFilterModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                return await _IUnitOfWorkService.FirStatusServiceBus.GetFirStatus(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetFirStatus", ex.Message, ex.StackTrace, ex.Source, "MasterService/FirStatusController/GetFirStatus");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetFirStatusDropdownList()
        {
            try
            {
                return await _IUnitOfWorkService.FirStatusServiceBus.GetFirStatusDropdownList();
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetFirStatusDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/FirStatusController/GetFirStatusDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        [HttpPost]
        public async Task<ResponseModel> AddEditFirStatus(AddEditFIRStatusModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.CreatedBy = UserId;
                objModel.UpdatedBy = UserId;
                return await _IUnitOfWorkService.FirStatusServiceBus.AddEditFirStatus(objModel, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditFirStatus", ex.Message, ex.StackTrace, ex.Source, "MasterService/FirStatusController/AddEditFirStatus");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> ActiveDeactiveFirStatus(ActiveDeactiveFIRStatusModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.UpdatedBy = UserId;
                return await _IUnitOfWorkService.FirStatusServiceBus.ActiveDeactiveFirStatus(objModel, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveFirStatus", ex.Message, ex.StackTrace, ex.Source, "MasterService/FirStatusController/ActiveDeactiveFirStatus");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }




    }
}
