using Core;
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
    public class CourtTypesController : ControllerBase
    {
        private readonly LogsService _logsService;
        private readonly IUnitOfWorkService _IUnitOfWorkService;
        //private readonly ILogger<CourtTypesController> _logger;
        //public CourtTypesController(IUnitOfWorkService unitOfWorkService, ILogger<CourtTypesController> logger)
        public CourtTypesController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            _IUnitOfWorkService = unitOfWorkService;
            //_logger = logger;
            _logsService = logsService;
        }

        [HttpPost]
        public async Task<ResponseModel> GetCourtTypesList(CourtTypesFilterModel objModel)
        {
            try
            {
                return await _IUnitOfWorkService.CourtTypesServiceBus.GetCourtTypes(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCourtTypesList", ex.Message, ex.StackTrace, ex.Source, "MasterService/CourtTypesController/GetCourtTypesList");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }

        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetCourtTypesDropdownList(int CourtTypeId=0)
        {
            try
            {
                return await _IUnitOfWorkService.CourtTypesServiceBus.GetCourtTypesDropdownList(CourtTypeId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCourtTypesDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/CourtTypesController/GetCourtTypesDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
            
        }

        [HttpPost]
        public async Task<ResponseModel> AddEditCourtTypes(CourtTypesRequestModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.Data.CreatedBy = UserId;
                objModel.Data.UpdatedBy = UserId;
                return await _IUnitOfWorkService.CourtTypesServiceBus.AddEditCourtTypes(objModel.Data, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditCourtTypes", ex.Message, ex.StackTrace, ex.Source, "MasterService/CourtTypesController/AddEditCourtTypes");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> ActiveDeactiveCourtTypes(CourtTypesActiveDeactiveModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.UpdatedBy = UserId;
                return await _IUnitOfWorkService.CourtTypesServiceBus.ActiveDeactiveCourtTypes(objModel, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveCourtTypes", ex.Message, ex.StackTrace, ex.Source, "MasterService/CourtTypesController/ActiveDeactiveCourtTypes");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }

        }
    }
}
