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
    public class PoliceThanaController : ControllerBase
    {
        private readonly LogsService _logsService;
        private readonly IUnitOfWorkService _IUnitOfWorkService;

        public PoliceThanaController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            _IUnitOfWorkService = unitOfWorkService;
            _logsService = logsService;
        }

        [HttpPost]
        public async Task<ResponseModel> GetPoliceRange(PoliceRangeFilterModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                return await _IUnitOfWorkService.PoliceThanaServiceBus.GetPoliceRange(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetPoliceRange", ex.Message, ex.StackTrace, ex.Source, "MasterService/PoliceThanaController/GetPoliceRange");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> PoliceRangeDropdownList(int DistrictId)
        {
            try
            {
                //return await _IUnitOfWorkService.PoliceThanaServiceBus.PoliceRangeDropdownList(UserSession.Current.DepartmentId);
                return await _IUnitOfWorkService.PoliceThanaServiceBus.PoliceRangeDropdownList(DistrictId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "PoliceRangeDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/PoliceThanaController/PoliceRangeDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> GetPoliceDistrict(PoliceDistrictFilterModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                return await _IUnitOfWorkService.PoliceThanaServiceBus.GetPoliceDistrict(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetPoliceDistrict", ex.Message, ex.StackTrace, ex.Source, "MasterService/PoliceThanaController/GetPoliceDistrict");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> PoliceDistrictDropdownList(int DistrictId, int RangeId)
        {
            try
            {
                return await _IUnitOfWorkService.PoliceThanaServiceBus.PoliceDistrictDropdownList(DistrictId, RangeId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "PoliceDistrictDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/PoliceThanaController/PoliceDistrictDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> GetPoliceCircle(PoliceCircleFilterModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                return await _IUnitOfWorkService.PoliceThanaServiceBus.GetPoliceCircle(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetPoliceCircle", ex.Message, ex.StackTrace, ex.Source, "MasterService/PoliceThanaController/GetPoliceCircle");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> PoliceCircleDropdownList(int PdId)
        {
            try
            {
                return await _IUnitOfWorkService.PoliceThanaServiceBus.PoliceCircleDropdownList(PdId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "PoliceCircleDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/PoliceThanaController/PoliceCircleDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> GetPoliceStation(PoliceStationFilterModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                return await _IUnitOfWorkService.PoliceThanaServiceBus.GetPoliceStation(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetPoliceStation", ex.Message, ex.StackTrace, ex.Source, "MasterService/PoliceThanaController/GetPoliceStation");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> PoliceStationDropdownList(int PcId)
        {
            try
            {
                return await _IUnitOfWorkService.PoliceThanaServiceBus.PoliceStationDropdownList(PcId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "PoliceStationDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/PoliceThanaController/PoliceStationDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
