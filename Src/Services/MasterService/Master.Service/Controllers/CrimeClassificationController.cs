using Common.Repository;
using Master.Dto.Masters;
using Master.Service.Middleware;
using Master.ServiceBus.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Master.Dto.Shared;
namespace Master.Service.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CrimeClassificationController : ControllerBase
    {
        private readonly LogsService _logsService;
        private readonly IUnitOfWorkService _IUnitOfWorkService;

        public CrimeClassificationController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            _IUnitOfWorkService = unitOfWorkService;
            _logsService = logsService;
        }

        [HttpPost]
        public async Task<ResponseModel> GetCrimeClassification(CrimeClassificationFilterModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                return await _IUnitOfWorkService.CrimeClassificationServiceBus.GetCrimeClassification(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCrimeClassification", ex.Message, ex.StackTrace, ex.Source, "MasterService/CrimeClassificationController/GetCrimeClassification");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetCrimeClassificationDropdownList()
        {
            try
            {
                return await _IUnitOfWorkService.CrimeClassificationServiceBus.GetCrimeClassificationDropdownList();
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCrimeClassificationDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/CrimeClassificationController/GetCrimeClassificationDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        [HttpPost]
        public async Task<ResponseModel> AddEditCrimeClassification(AddEditCrimeClassificationModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.CreatedBy = UserId;
                objModel.UpdatedBy = UserId;
                return await _IUnitOfWorkService.CrimeClassificationServiceBus.AddEditCrimeClassification(objModel, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCrimeClassification", ex.Message, ex.StackTrace, ex.Source, "MasterService/CrimeClassificationController/GetCrimeClassification");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> ActiveDeactiveCrimeClassification(ActiveDeactiveCrimeClassificationModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.UpdatedBy = UserId;
                return await _IUnitOfWorkService.CrimeClassificationServiceBus.ActiveDeactiveCrimeClassification(objModel, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveCrimeClassification", ex.Message, ex.StackTrace, ex.Source, "MasterService/CrimeClassificationController/ActiveDeactiveCrimeClassification");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }



    }
}
