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
    public class CourtPlacesController : ControllerBase
    {
        private readonly IUnitOfWorkService _IUnitOfWorkService;
        private readonly LogsService _logsService;
        //private readonly ILogger<CourtPlacesController> _logger;
        //public CourtPlacesController(IUnitOfWorkService unitOfWorkService, ILogger<CourtPlacesController> logger)
        public CourtPlacesController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            _IUnitOfWorkService = unitOfWorkService;
            _logsService = logsService;
            //_logger = logger;
        }

        [HttpPost]
        public async Task<ResponseModel> GetCourtPlacesList(CourtPlacesFilterModel objModel)
        {
            try
            {
                return await _IUnitOfWorkService.CourtPlacesServiceBus.GetCourtPlaces(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCourtPlacesList", ex.Message, ex.StackTrace, ex.Source, "MasterService/CourtPlacesController/GetCourtPlacesList");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }            
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetCourtPlacesDropdownList(int CourtTypeId= 0, int TehsilId = 0)
        {
            try
            {
                return await _IUnitOfWorkService.CourtPlacesServiceBus.GetCourtPlacesDropdownList(CourtTypeId, TehsilId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCourtPlacesDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/CourtPlacesController/GetCourtPlacesDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }            
        }

        [HttpPost]
        public async Task<ResponseModel> AddEditCourtPlaces(CourtPlacesRequestModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.Data.CreatedBy = UserId;
                objModel.Data.UpdatedBy = UserId;
                return await _IUnitOfWorkService.CourtPlacesServiceBus.AddEditCourtPlaces(objModel.Data, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditCourtPlaces", ex.Message, ex.StackTrace, ex.Source, "MasterService/CourtPlacesController/AddEditCourtPlaces");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> ActiveDeactiveCourtPlaces(CourtPlacesActiveDeactiveModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.UpdatedBy = UserId;
                return await _IUnitOfWorkService.CourtPlacesServiceBus.ActiveDeactiveCourtPlaces(objModel, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveCourtPlaces", ex.Message, ex.StackTrace, ex.Source, "MasterService/CourtPlacesController/ActiveDeactiveCourtPlaces");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }

        }

    }
}
