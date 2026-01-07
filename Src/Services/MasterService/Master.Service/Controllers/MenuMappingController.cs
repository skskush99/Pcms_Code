using Common.Repository;
using Master.Dto.Menu;
using Master.Dto.Shared;
using Master.Service.Middleware;
using Master.ServiceBus.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PcmsUserManagementMicroServices.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class MenuMappingController : ControllerBase
    {
        private readonly LogsService _logsService;
        private readonly IUnitOfWorkService unitOfWork;
        public MenuMappingController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            _logsService = logsService;
            unitOfWork = unitOfWorkService;
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetMenuMapping(int RoleId)
        {
            try
            {
                return await unitOfWork.Menu.GetMenuMapping(RoleId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetMenuMapping", ex.Message, ex.StackTrace, ex.Source, "MasterService/MenuMappingController/GetMenuMapping");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> AddEditMenuMapping(MenuMappingRequestModel objModel)
        {
            try
            {
                return await unitOfWork.Menu.AddEditMenuMapping(objModel.Data, objModel.RoleId, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditMenuMapping", ex.Message, ex.StackTrace, ex.Source, "MasterService/MenuMappingController/AddEditMenuMapping");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetMenuMappingUser(int RoleId, int UserId)
        {
            try
            {
                return await unitOfWork.Menu.GetMenuMappingUser(RoleId, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetMenuMappingUser", ex.Message, ex.StackTrace, ex.Source, "MasterService/MenuMappingController/GetMenuMappingUser");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> AddEditMenuMappingUser(MenuMappingRequestUserModel objModel)
        {
            try
            {
                return await unitOfWork.Menu.AddEditMenuMappingUser(objModel.Data, objModel.RoleId, objModel.UserId, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditMenuMappingUser", ex.Message, ex.StackTrace, ex.Source, "MasterService/MenuMappingController/AddEditMenuMappingUser");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
