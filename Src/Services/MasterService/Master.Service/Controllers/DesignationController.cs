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
    public class DesignationController : ControllerBase
    {
        private readonly LogsService _logsService;
        private readonly IUnitOfWorkService _IUnitOfWorkService;
        public DesignationController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            _IUnitOfWorkService = unitOfWorkService;
            _logsService = logsService;
        }

        [HttpPost]
        public async Task<ResponseModel> GetDesignationList(DesignationFilterModel objModel)
        {
            try
            {
                return await _IUnitOfWorkService.DesignationServiceBus.GetDesignation(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDesignationList", ex.Message, ex.StackTrace, ex.Source, "MasterService/DesignationController/GetDesignationList");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetDesignationDropdownList()
        {
            try
            {
                return await _IUnitOfWorkService.DesignationServiceBus.GetDesignationDropdownList();
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDesignationDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/DesignationController/GetDesignationDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> AddEditDesignation(DesignationRequestModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.Data.CreatedBy = UserId;
                objModel.Data.UpdatedBy = UserId;
                return await _IUnitOfWorkService.DesignationServiceBus.AddEditDesignation(objModel.Data, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDesignation", ex.Message, ex.StackTrace, ex.Source, "MasterService/DesignationController/AddEditDesignation");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> ActiveDeactiveDesignation(DesignationActiveDeactiveModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.UpdatedBy = UserId;
                return await _IUnitOfWorkService.DesignationServiceBus.ActiveDeactiveDesignation(objModel, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveDesignation", ex.Message, ex.StackTrace, ex.Source, "MasterService/DesignationController/ActiveDeactiveDesignation");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        //[HttpPost]
        //public async Task<ResponseModel> GetDesignationRajmaster(DesignationFilterModel objModel)
        //{
        //    try
        //    {
        //        return await _IUnitOfWorkService.DesignationServiceBus.GetDesignationRajmaster(objModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "GetDesignationRajmaster", ex.Message, ex.StackTrace, ex.Source, "MasterService/DesignationController/GetDesignationRajmaster");
        //        return new ResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}

        //[HttpGet]
        //public async Task<ResponseWithoutPaginationModel> GetDesignationRajmasterDropdownList()
        //{
        //    try
        //    {
        //        return await _IUnitOfWorkService.DesignationServiceBus.GetDesignationRajmasterDropdownList();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "GetDesignationRajmasterDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/DesignationController/GetDesignationRajmasterDropdownList");
        //        return new ResponseWithoutPaginationModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}

        /////////// OISc Designation Mapping Start
        //[HttpPost]
        //public async Task<ResponseModel> GetOICSDesigMapping(OICSDesigMappingFilterModel objModel)
        //{
        //    try
        //    {
        //        var loginUserData = UserSession.Current;
        //        if (loginUserData.DepartmentId > 0)
        //            objModel.AdminDeptId = loginUserData.DepartmentId;
        //        if (loginUserData.UnitId > 0)
        //            objModel.UnitId = loginUserData.UnitId;
        //        return await _IUnitOfWorkService.DesignationServiceBus.GetOICSDesigMapping(objModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "GetOICSDesigMapping", ex.Message, ex.StackTrace, ex.Source, "MasterService/DesignationController/GetOICSDesigMapping");
        //        return new ResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}

        //[HttpGet]
        //public async Task<ResponseWithoutPaginationModel> GetOICSDesigMappingDropdownList(int AdmDeptId = 0, int UnitId = 0)
        //{
        //    try
        //    {
        //        var loginUserData = UserSession.Current;
        //        if (loginUserData.DepartmentId > 0)
        //            AdmDeptId = loginUserData.DepartmentId;
        //        if (loginUserData.UnitId > 0)
        //            UnitId = loginUserData.UnitId;
        //        return await _IUnitOfWorkService.DesignationServiceBus.GetOICSDesigMappingDropdownList(AdmDeptId, UnitId);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "GetOICSDesigMappingDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/DesignationController/GetOICSDesigMappingDropdownList");
        //        return new ResponseWithoutPaginationModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}

        //[HttpPost]
        //public async Task<ResponseModel> AddEditOICSDesigMapping(OICSDesigMappingModel objModel)
        //{
        //    try
        //    {
        //        var UserId = UserSession.Current.UserId;
        //        objModel.CreatedBy = UserId;
        //        objModel.UpdatedBy = UserId;
        //        return await _IUnitOfWorkService.DesignationServiceBus.AddEditOICSDesigMapping(objModel, UserId);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "AddEditOICSDesigMapping", ex.Message, ex.StackTrace, ex.Source, "MasterService/DesignationController/AddEditOICSDesigMapping");
        //        return new ResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}

        //[HttpPost]
        //public async Task<ResponseModel> ActiveDeactiveOICSDesigMapping(OICsDesigActiveDeactiveModel objModel)
        //{
        //    try
        //    {
        //        var UserId = UserSession.Current.UserId;
        //        objModel.UpdatedBy = UserId;
        //        return await _IUnitOfWorkService.DesignationServiceBus.ActiveDeactiveOICSDesigMapping(objModel, UserId);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "ActiveDeactiveOICSDesigMapping", ex.Message, ex.StackTrace, ex.Source, "MasterService/DesignationController/ActiveDeactiveOICSDesigMapping");
        //        return new ResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}

        /////////// OISc Designation Mapping End        



        ////////////// OISc Designation Section Start
        //[HttpPost]
        //public async Task<ResponseModel> GetSection(SectionFilterModel objModel)
        //{
        //    try
        //    {
        //        var loginUserData = UserSession.Current;
        //        if (loginUserData.DepartmentId > 0)
        //            objModel.AdmDeptId = loginUserData.DepartmentId;
        //        if (loginUserData.UnitId > 0)
        //            objModel.UnitId = loginUserData.UnitId;
        //        return await _IUnitOfWorkService.DesignationServiceBus.GetSection(objModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "GetSection", ex.Message, ex.StackTrace, ex.Source, "MasterService/DesignationController/GetSection");
        //        return new ResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}

        //[HttpGet]
        //public async Task<ResponseWithoutPaginationModel> GetSectionDropdownList(int AdminDeptId = 0, int UnitId = 0)
        //{
        //    try
        //    {
        //        var loginUserData = UserSession.Current;
        //        if (loginUserData.DepartmentId > 0)
        //            AdminDeptId = loginUserData.DepartmentId;
        //        if (loginUserData.UnitId > 0)
        //            UnitId = loginUserData.UnitId;
        //        return await _IUnitOfWorkService.DesignationServiceBus.GetSectionDropdownList(AdminDeptId, UnitId);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "GetSectionDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/DesignationController/GetSectionDropdownList");
        //        return new ResponseWithoutPaginationModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}

        //[HttpPost]
        //public async Task<ResponseModel> AddEditSection(SectionModel objModel)
        //{
        //    try
        //    {
        //        var UserId = UserSession.Current.UserId;
        //        objModel.CreatedBy = UserId;
        //        objModel.UpdatedBy = UserId;
        //        return await _IUnitOfWorkService.DesignationServiceBus.AddEditSection(objModel, UserId);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "AddEditSection", ex.Message, ex.StackTrace, ex.Source, "MasterService/DesignationController/AddEditSection");
        //        return new ResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}

        //[HttpPost]
        //public async Task<ResponseModel> ActiveDeactiveSection(SectionActiveDeactiveModel objModel)
        //{
        //    try
        //    {
        //        var UserId = UserSession.Current.UserId;
        //        objModel.UpdatedBy = UserId;
        //        objModel.DeleteBy = UserId;
        //        return await _IUnitOfWorkService.DesignationServiceBus.ActiveDeactiveSection(objModel, UserId);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "ActiveDeactiveSection", ex.Message, ex.StackTrace, ex.Source, "MasterService/DesignationController/ActiveDeactiveSection");
        //        return new ResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}

        ///////// OISc Designation Section End
    }
}
