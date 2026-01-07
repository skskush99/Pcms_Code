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
    public class CourtNamesController : ControllerBase
    {
        private readonly LogsService _logsService;
        private readonly IUnitOfWorkService _IUnitOfWorkService;

        public CourtNamesController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            _IUnitOfWorkService = unitOfWorkService;
            _logsService = logsService;
            //_logger = logger;
        }

        [HttpPost]
        public async Task<ResponseModel> GetCourtNamesList(CourtNamesFilterModel objModel)
        {
            try
            {
                return await _IUnitOfWorkService.CourtNamesServiceBus.GetCourtNames(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCourtNamesList", ex.Message, ex.StackTrace, ex.Source, "MasterService/CourtNamesController/GetCourtNamesList");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetCourtNamesDropdownList(int JCourtId = 0)
        {
            try
            {
                return await _IUnitOfWorkService.CourtNamesServiceBus.GetCourtNamesDropdownList(JCourtId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCourtNamesDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/CourtNamesController/GetCourtNamesDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }            
        }


        [HttpPost]
        public async Task<ResponseModel> AddEditCourtNames(CourtNamesRequestModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.Data.CreatedBy = UserId;
                objModel.Data.UpdatedBy = UserId;
                return await _IUnitOfWorkService.CourtNamesServiceBus.AddEditCourtNames(objModel.Data, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditCourtNames", ex.Message, ex.StackTrace, ex.Source, "MasterService/CourtNamesController/AddEditCourtNames");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }

        }

        [HttpPost]
        public async Task<ResponseModel> ActiveDeactiveCourtNames(CourtNamesActiveDeactiveModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.UpdatedBy = UserId;
                return await _IUnitOfWorkService.CourtNamesServiceBus.ActiveDeactiveCourtNames(objModel, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveCourtNames", ex.Message, ex.StackTrace, ex.Source, "MasterService/CourtNamesController/ActiveDeactiveCourtNames");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
