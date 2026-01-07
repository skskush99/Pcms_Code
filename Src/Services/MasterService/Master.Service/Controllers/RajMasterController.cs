using Common.Repository;
using Master.Dto.Masters;
using Master.Dto.Shared;
using Master.Service.Middleware;
using Master.ServiceBus.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Master.Service.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class RajMasterController : ControllerBase
    {
        private readonly LogsService _logsService;
        private readonly IUnitOfWorkService _IUnitOfWorkService;
        public RajMasterController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            _logsService = logsService;
            _IUnitOfWorkService = unitOfWorkService;
        }

        [HttpPost]
        public async Task<ResponseModel> AddStateRajMaster(RajMasterModel objModel, int MasterDataID = 17)
        {
            try
            {
                return await _IUnitOfWorkService.RajMasterServiceBus.AddStateRajMaster(objModel, MasterDataID);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddStateRajMaster", ex.Message, ex.StackTrace, ex.Source, "MasterService/RajMasterController/AddStateRajMaster");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> AddDivisionRajMaster(RajMasterModel objModel, int MasterDataID = 57)
        {
            try
            {
                return await _IUnitOfWorkService.RajMasterServiceBus.AddDivisionRajMaster(objModel, MasterDataID);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddDivisionRajMaster", ex.Message, ex.StackTrace, ex.Source, "MasterService/RajMasterController/AddDivisionRajMaster");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
       
        [HttpPost]
        public async Task<ResponseModel> AddDistrictRajMaster(RajMasterModel objModel, int MasterDataID = 56)
        {
            try
            {
                return await _IUnitOfWorkService.RajMasterServiceBus.AddDistrictRajMaster(objModel, MasterDataID);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddDistrictRajMaster", ex.Message, ex.StackTrace, ex.Source, "MasterService/RajMasterController/AddDistrictRajMaster");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> AddPoliceRangeRajMaster(RajMasterModel objModel, int MasterDataID = 88)
        {
            try
            {
                return await _IUnitOfWorkService.RajMasterServiceBus.AddPoliceRangeRajMaster(objModel, MasterDataID);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddPoliceRangeRajMaster", ex.Message, ex.StackTrace, ex.Source, "MasterService/RajMasterController/AddPoliceRangeRajMaster");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> AddPoliceDistrictRajMaster(RajMasterModel objModel, int MasterDataID = 89)
        {
            try
            {
                return await _IUnitOfWorkService.RajMasterServiceBus.AddPoliceDistrictRajMaster(objModel, MasterDataID);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddPoliceDistrictRajMaster", ex.Message, ex.StackTrace, ex.Source, "MasterService/RajMasterController/AddPoliceDistrictRajMaster");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> AddPoliceCircleRajMaster(RajMasterModel objModel, int MasterDataID = 90)
        {
            try
            {
                return await _IUnitOfWorkService.RajMasterServiceBus.AddPoliceCircleRajMaster(objModel, MasterDataID);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddPoliceCircleRajMaster", ex.Message, ex.StackTrace, ex.Source, "MasterService/RajMasterController/AddPoliceCircleRajMaster");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> AddPoliceStationRajMaster(RajMasterModel objModel, int MasterDataID = 91)
        {
            try
            {
                return await _IUnitOfWorkService.RajMasterServiceBus.AddPoliceStationRajMaster(objModel, MasterDataID);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddPoliceStationRajMaster", ex.Message, ex.StackTrace, ex.Source, "MasterService/RajMasterController/AddPoliceStationRajMaster");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        









        //[HttpPost]
        //public async Task<ResponseModel> AddCityRajMaster(RajMasterModel objModel, int MasterDataID = 3)
        //{
        //    try
        //    {
        //        return await _IUnitOfWorkService.RajMasterServiceBus.AddCityRajMaster(objModel, MasterDataID);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "AddCityRajMaster", ex.Message, ex.StackTrace, ex.Source, "MasterService/RajMasterController/AddCityRajMaster");
        //        return new ResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}

        //[HttpPost]
        //public async Task<ResponseModel> AddSubDivisionRajMaster(RajMasterModel objModel, int MasterDataID = 71)
        //{
        //    try
        //    {
        //        return await _IUnitOfWorkService.RajMasterServiceBus.AddSubDivisionRajMaster(objModel, MasterDataID);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "AddSubDivisionRajMaster", ex.Message, ex.StackTrace, ex.Source, "MasterService/RajMasterController/AddSubDivisionRajMaster");
        //        return new ResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}

        //[HttpPost]
        //public async Task<ResponseModel> AddTehsilRajMaster(RajMasterModel objModel, int MasterDataID = 62)
        //{
        //    try
        //    {
        //        return await _IUnitOfWorkService.RajMasterServiceBus.AddTehsilRajMaster(objModel, MasterDataID);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "AddTehsilRajMaster", ex.Message, ex.StackTrace, ex.Source, "MasterService/RajMasterController/AddTehsilRajMaster");
        //        return new ResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}

        //[HttpPost]
        //public async Task<ResponseModel> AddDesignationRajMaster(RajMasterModel objModel, int MasterDataID = 107)
        //{
        //    try
        //    {
        //        return await _IUnitOfWorkService.RajMasterServiceBus.AddDesignationRajMaster(objModel, MasterDataID);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "AddDesignationRajMaster", ex.Message, ex.StackTrace, ex.Source, "MasterService/RajMasterController/AddDesignationRajMaster");
        //        return new ResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}

        //[HttpPost]
        //public async Task<ResponseModel> AddAdminDepartmentRajMaster(RajMasterModel objModel, int MasterDataID = 81)
        //{
        //    try
        //    {
        //        return await _IUnitOfWorkService.RajMasterServiceBus.AddAdminDepartmentRajMaster(objModel, MasterDataID);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "AddAdminDepartmentRajMaster", ex.Message, ex.StackTrace, ex.Source, "MasterService/RajMasterController/AddAdminDepartmentRajMaster");
        //        return new ResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}

        //[HttpPost]
        //public async Task<ResponseModel> AddAdminUnitsDepartmentRajMaster(RajMasterModel objModel, int MasterDataID = 82)
        //{
        //    try
        //    {
        //        return await _IUnitOfWorkService.RajMasterServiceBus.AddAdminUnitsDepartmentRajMaster(objModel, MasterDataID);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "AddAdminDepartmentRajMaster", ex.Message, ex.StackTrace, ex.Source, "MasterService/RajMasterController/AddAdminDepartmentRajMaster");
        //        return new ResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}



    }
}
