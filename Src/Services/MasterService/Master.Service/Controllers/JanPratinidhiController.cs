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
    public class JanPratinidhiController : ControllerBase
    {
        private readonly LogsService _logsService;
        private readonly IUnitOfWorkService _IUnitOfWorkService;

        public JanPratinidhiController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            _IUnitOfWorkService = unitOfWorkService;
            _logsService = logsService;
        }
        [HttpPost]
        public async Task<ResponseModel> GetJanPratinidhi(JanPratinidhiFilterModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                return await _IUnitOfWorkService.JanPratinidhiServiceBus.GetJanPratinidhi(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetJanPratinidhi", ex.Message, ex.StackTrace, ex.Source, "MasterService/JanPratinidhiController/GetJanPratinidhi");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetJanPratinidhiDropdownList()
        {
            try
            {
                return await _IUnitOfWorkService.JanPratinidhiServiceBus.GetJanPratinidhiDropdownList();
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetJanPratinidhiDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/JanPratinidhiController/GetJanPratinidhiDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        [HttpPost]
        public async Task<ResponseModel> AddEditJanPratinidhi(JanPratinidhiRequestModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.Data.CreatedBy = UserId;
                objModel.Data.UpdatedBy = UserId;
                return await _IUnitOfWorkService.JanPratinidhiServiceBus.AddEditJanPratinidhi(objModel.Data, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditJanPratinidhi", ex.Message, ex.StackTrace, ex.Source, "MasterService/JanPratinidhiController/AddEditJanPratinidhi");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> ActiveDeactiveJanPratinidhi(JanPratinidhiActiveDeactiveModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.UpdatedBy = UserId;
                return await _IUnitOfWorkService.JanPratinidhiServiceBus.ActiveDeactiveJanPratinidhi(objModel, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveJanPratinidhi", ex.Message, ex.StackTrace, ex.Source, "MasterService/JanPratinidhiController/ActiveDeactiveJanPratinidhi");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }



    }
}
