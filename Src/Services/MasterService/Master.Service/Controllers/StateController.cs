using Common.Repository;
using Master.Dto.Masters;
using Master.Dto.Shared;
using Master.ServiceBus.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Master.Service.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class StateController : ControllerBase
    {
        private readonly IUnitOfWorkService _IUnitOfWorkService;
        private readonly LogsService _logsService;
        public StateController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            _IUnitOfWorkService = unitOfWorkService;
            _logsService = logsService;
        }

        [HttpPost]
        public async Task<ResponseModel> StateList(StateFilterModel objModel)
        {
            try
            {
                return await _IUnitOfWorkService.StateServiceBus.StateList(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "StateList", ex.Message, ex.StackTrace, ex.Source, "MasterService/StateController/StateList");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetStateList()
        {
            try
            {
                return await _IUnitOfWorkService.StateServiceBus.GetStateList();
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetStateList", ex.Message, ex.StackTrace, ex.Source, "MasterService/StateController/GetStateList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> DivisionsList(StateFilterModel objModel)
        {
            try
            {
                return await _IUnitOfWorkService.StateServiceBus.DivisionsList(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DivisionsList", ex.Message, ex.StackTrace, ex.Source, "MasterService/StateController/DivisionsList");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> GetDivisionsList()
        {
            try
            {
                return await _IUnitOfWorkService.StateServiceBus.GetDivisionsList();
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDivisionsList", ex.Message, ex.StackTrace, ex.Source, "MasterService/StateController/GetDivisionsList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> DistrictsList(DistrictsFilterModel objModel)
        {
            try
            {
                return await _IUnitOfWorkService.StateServiceBus.DistrictsList(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DistrictsList", ex.Message, ex.StackTrace, ex.Source, "MasterService/StateController/DistrictsList");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetDistrictsList(int DivisionId, int StateId)
        {
            try
            {
                return await _IUnitOfWorkService.StateServiceBus.GetDistrictsList(DivisionId, StateId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDistrictsList", ex.Message, ex.StackTrace, ex.Source, "MasterService/StateController/GetDistrictsList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        //[HttpPost]
        //public async Task<ResponseModel> CityList(CityFilterModel objModel)
        //{
        //    try
        //    {
        //        return await _IUnitOfWorkService.StateServiceBus.CityList(objModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "CityList", ex.Message, ex.StackTrace, ex.Source, "MasterService/StateController/CityList");
        //        return new ResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}

        //[HttpGet]
        //public async Task<ResponseModel> GetCityList(int DistrictId)
        //{
        //    try
        //    {
        //        return await _IUnitOfWorkService.StateServiceBus.GetCityList(DistrictId);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "GetCityList", ex.Message, ex.StackTrace, ex.Source, "MasterService/StateController/GetCityList");
        //        return new ResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}

        //[HttpPost]
        //public async Task<ResponseModel> TehsilsList(CityFilterModel objModel)
        //{
        //    try
        //    {
        //        return await _IUnitOfWorkService.StateServiceBus.TehsilsList(objModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "TehsilsList", ex.Message, ex.StackTrace, ex.Source, "MasterService/StateController/TehsilsList");
        //        return new ResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}

        //[HttpPost]
        //public async Task<ResponseModel> GetTehsilsList(int DistrictId)
        //{
        //    try
        //    {
        //        return await _IUnitOfWorkService.StateServiceBus.GetTehsilsList(DistrictId);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "GetTehsilsList", ex.Message, ex.StackTrace, ex.Source, "MasterService/StateController/GetTehsilsList");
        //        return new ResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}



        //[HttpPost]
        //public async Task<ResponseModel> SubDivisionsList(SubDivisionsFilterModel objModel)
        //{
        //    try
        //    {
        //        return await _IUnitOfWorkService.StateServiceBus.SubDivisionsList(objModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "SubDivisionsList", ex.Message, ex.StackTrace, ex.Source, "MasterService/StateController/SubDivisionsList");
        //        return new ResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}
        //[HttpPost]
        //public async Task<ResponseWithoutPaginationModel> GetSubDivisionsList(int DivisionId, int DistrictId)
        //{
        //    try
        //    {
        //        return await _IUnitOfWorkService.StateServiceBus.GetSubDivisionsList(DivisionId, DistrictId);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "GetSubDivisionsList", ex.Message, ex.StackTrace, ex.Source, "MasterService/StateController/GetSubDivisionsList");
        //        return new ResponseWithoutPaginationModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }

        //}


    }
}
