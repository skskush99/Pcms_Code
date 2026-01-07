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
    public class LevelController : ControllerBase
    {
        private readonly LogsService _logsService;
        private readonly IUnitOfWorkService _IUnitOfWorkService;

        public LevelController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            _IUnitOfWorkService = unitOfWorkService;
            _logsService = logsService;
        }

        [HttpPost]
        public async Task<ResponseModel> GetLevel(LevelModelFilterModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                return await _IUnitOfWorkService.LevelServiceBus.GetLevel(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetLevel", ex.Message, ex.StackTrace, ex.Source, "MasterService/LevelController/GetLevel");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetLevelDropdownList()
        {
            try
            {
                return await _IUnitOfWorkService.LevelServiceBus.GetLevelDropdownList();
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetLevelDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/LevelController/GetLevelDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        [HttpPost]
        public async Task<ResponseModel> AddEditLevel(LevelRequestModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.Data.CreatedBy = UserId;
                objModel.Data.UpdatedBy = UserId;
                return await _IUnitOfWorkService.LevelServiceBus.AddEditLevel(objModel.Data, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditLevel", ex.Message, ex.StackTrace, ex.Source, "MasterService/LevelController/AddEditLevel");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> ActiveDeactiveLevel(LevelActiveDeactiveModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.UpdatedBy = UserId;
                return await _IUnitOfWorkService.LevelServiceBus.ActiveDeactiveLevel(objModel, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveLevel", ex.Message, ex.StackTrace, ex.Source, "MasterService/LevelController/ActiveDeactiveLevel");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


    }
}
