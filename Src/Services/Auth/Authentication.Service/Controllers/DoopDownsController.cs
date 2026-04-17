using Authentication.Dto.Shared;
using Authentication.ServiceBus.UnitOfWork;
using Core.Models.User;
using Core.Utils;
using JwtAuthenticationManager;
using JwtAuthenticationManager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Text;
using System.Text.Json;
using Common.Repository;
using Core.SsoEncryption;


namespace Authentication.Service.Controllers
{
    [Route("api/[controller]")]
    //[Route("api/[controller]/[action]")]
    [ApiController]
    public class DoopDownsController : ControllerBase
    {
        private readonly LogsService _logsService;
        private readonly IUnitOfWorkService _IUnitOfWorkService;

        public DoopDownsController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            _IUnitOfWorkService = unitOfWorkService;
            _logsService = logsService;
        }

        [HttpGet]
        [Route("GetLevelDropdownList")]
        public async Task<ResponseWithoutPaginationModel> GetLevelDropdownList()
        {
            try
            {
                return await _IUnitOfWorkService.DropDownsServiceBus.GetLevelDropdownList();
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetLevelDropdownList", ex.Message, ex.StackTrace, ex.Source, "Authentication.Service/DoopDownsController/GetLevelDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        [Route("RolesDropdownList")]
        public async Task<ResponseWithoutPaginationModel> RolesDropdownList()
        {
            try
            {
                return await _IUnitOfWorkService.DropDownsServiceBus.GetRolesDropdownList();
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "RolesDropdownList", ex.Message, ex.StackTrace, ex.Source, "Authentication.Service/DoopDownsController/RolesDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        [Route("GetDivisionsList")]
        public async Task<ResponseWithoutPaginationModel> GetDivisionsList()
        {
            try
            {
                return await _IUnitOfWorkService.DropDownsServiceBus.GetDivisionsList();
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDivisionsList", ex.Message, ex.StackTrace, ex.Source, "Authentication.Service/DoopDownsController/GetDivisionsList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        [Route("GetDistrictsList")]
        public async Task<ResponseWithoutPaginationModel> GetDistrictsList(int DivisionId, int StateId=6)
        {
            try
            {
                return await _IUnitOfWorkService.DropDownsServiceBus.GetDistrictsList(DivisionId, StateId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDistrictsList", ex.Message, ex.StackTrace, ex.Source, "Authentication.Service/DoopDownsController/GetDistrictsList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        [Route("GetOfficesDropdownList")]
        public async Task<ResponseWithoutPaginationModel> GetOfficesDropdownList(int OfficeId = 0)
        {
            try
            {
                return await _IUnitOfWorkService.DropDownsServiceBus.GetOfficesDropdownList(OfficeId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetOfficesDropdownList", ex.Message, ex.StackTrace, ex.Source, "Authentication.Service/DoopDownsController/GetOfficesDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        [HttpGet]
        [Route("GetOfficesByDistrictIdDropdownList")]
        public async Task<ResponseWithoutPaginationModel> GetOfficesByDistrictIdDropdownList(int DistrictId)
        {
            try
            {
                return await _IUnitOfWorkService.DropDownsServiceBus.GetOfficesByDistrictIdDropdownList(DistrictId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetOfficesByDistrictIdDropdownList", ex.Message, ex.StackTrace, ex.Source, "Authentication.Service/DoopDownsController/GetOfficesByDistrictIdDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        [Route("GetDesignationDropdownList")]
        public async Task<ResponseWithoutPaginationModel> GetDesignationDropdownList()
        {
            try
            {
                return await _IUnitOfWorkService.DropDownsServiceBus.GetDesignationDropdownList();
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDesignationDropdownList", ex.Message, ex.StackTrace, ex.Source, "Authentication.Service/DoopDownsController/GetDesignationDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        [HttpGet]
        [Route("GetDesignationByRoleIdDropdownList")]
        public async Task<ResponseWithoutPaginationModel> GetDesignationByRoleIdDropdownList(int RoleId)
        {
            try
            {
                return await _IUnitOfWorkService.DropDownsServiceBus.GetDesignationByRoleIdDropdownList(RoleId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDesignationByRoleIdDropdownList", ex.Message, ex.StackTrace, ex.Source, "Authentication.Service/DoopDownsController/GetDesignationByRoleIdDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        [Route("GetCourtNamesDropdownList")]
        public async Task<ResponseWithoutPaginationModel> GetCourtNamesDropdownList(int JCourtId = 0)
        {
            try
            {
                return await _IUnitOfWorkService.DropDownsServiceBus.GetCourtNamesDropdownList(JCourtId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCourtNamesDropdownList", ex.Message, ex.StackTrace, ex.Source, "Authentication.Service/DoopDownsController/GetCourtNamesDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        [Route("GetCourtNamesByOfficeIdDropdownList")]
        public async Task<ResponseWithoutPaginationModel> GetCourtNamesByOfficeIdDropdownList(int OfficeId)
        {
            try
            {
                return await _IUnitOfWorkService.DropDownsServiceBus.GetCourtNamesByOfficeIdDropdownList(OfficeId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCourtNamesByOfficeIdDropdownList", ex.Message, ex.StackTrace, ex.Source, "Authentication.Service/DoopDownsController/GetCourtNamesByOfficeIdDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        [Route("AddEditUserMapReq")]
        public async Task<ResponseWithoutPaginationModel> AddEditUserMapReq(UserMapReqAddEditModel objModel, int UserId)
        {
            try
            {
                return await _IUnitOfWorkService.DropDownsServiceBus.AddEditUserMapReq(objModel.Data, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditUserMapReq", ex.Message, ex.StackTrace, ex.Source, "Authentication.Service/DoopDownsController/AddEditUser");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }




    }
}
