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

    public class CrimeSubActController : ControllerBase
    {
        private readonly LogsService _logsService;
        private readonly IUnitOfWorkService _IUnitOfWorkService;

        public CrimeSubActController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            _IUnitOfWorkService = unitOfWorkService;
            _logsService = logsService;
        }

        [HttpPost]
        public async Task<ResponseModel> GetCrimeSubAct(CrimeSubActFilterModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                return await _IUnitOfWorkService.CrimeSubActServiceBus.GetCrimeSubAct(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCrimeSubAct", ex.Message, ex.StackTrace, ex.Source, "MasterService/CrimeSubActController/GetCrimeSubAct");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetCrimeSubActDropdownList(int CrimeActId, int CrimeClsId)
        {
            try
            {
                return await _IUnitOfWorkService.CrimeSubActServiceBus.GetCrimeSubActDropdownList(CrimeActId, CrimeClsId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCrimeSubActDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/CrimeSubActController/GetCrimeSubActDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        [HttpPost]
        public async Task<ResponseModel> AddEditCrimeSubAct(AddEditCrimeSubActModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.CreatedBy = UserId;
                objModel.UpdatedBy = UserId;
                return await _IUnitOfWorkService.CrimeSubActServiceBus.AddEditCrimeSubAct(objModel, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditCrimeSubAct", ex.Message, ex.StackTrace, ex.Source, "MasterService/CrimeSubActController/AddEditCrimeSubAct");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> ActiveDeactiveCrimeSubAct(ActiveDeactiveCrimeSubActModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.UpdatedBy = UserId;
                return await _IUnitOfWorkService.CrimeSubActServiceBus.ActiveDeactiveCrimeSubAct(objModel, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveCrimeSubAct", ex.Message, ex.StackTrace, ex.Source, "MasterService/CrimeSubActController/ActiveDeactiveCrimeSubAct");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }



    }
}
