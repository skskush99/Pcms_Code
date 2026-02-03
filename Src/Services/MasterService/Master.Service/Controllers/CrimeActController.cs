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

    public class CrimeActController : ControllerBase
    {
        private readonly LogsService _logsService;
        private readonly IUnitOfWorkService _IUnitOfWorkService;

        public CrimeActController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            _IUnitOfWorkService = unitOfWorkService;
            _logsService = logsService;
        }

        [HttpPost]
        public async Task<ResponseModel> GetCrimeAct(CrimeActFilterModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                return await _IUnitOfWorkService.CrimeActServiceBus.GetCrimeAct(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCrimeAct", ex.Message, ex.StackTrace, ex.Source, "MasterService/CrimeActController/GetCrimeAct");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetCrimeActDropdownList(int CrimeClsId)
        {
            try
            {
                return await _IUnitOfWorkService.CrimeActServiceBus.GetCrimeActDropdownList(CrimeClsId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCrimeActDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/CrimeActController/GetCrimeActDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        [HttpPost]
        public async Task<ResponseModel> AddEditCrimeAct(AddEditCrimeActModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.CreatedBy = UserId;
                objModel.UpdatedBy = UserId;
                return await _IUnitOfWorkService.CrimeActServiceBus.AddEditCrimeAct(objModel, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditCrimeAct", ex.Message, ex.StackTrace, ex.Source, "MasterService/CrimeActController/AddEditCrimeAct");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> ActiveDeactiveCrimeAct(ActiveDeactiveCrimeActModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.UpdatedBy = UserId;
                return await _IUnitOfWorkService.CrimeActServiceBus.ActiveDeactiveCrimeAct(objModel, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveCrimeAct", ex.Message, ex.StackTrace, ex.Source, "MasterService/CrimeActController/ActiveDeactiveCrimeAct");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

    }
}
