using Common.Repository;
using Master.Dto.Menu;
using Master.Dto.Shared;
using Master.Service.Middleware;
using Master.ServiceBus.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace PcmsUserManagementMicroServices.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class MenuController : ControllerBase
    {
        private readonly LogsService _logsService;
        private readonly IUnitOfWorkService unitOfWork;
        public MenuController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            unitOfWork = unitOfWorkService;
            _logsService = logsService;
        }

        [HttpGet]
        public async Task<ResponseModel> GetMenus(int PageNo, int PageSize)
        {
            try
            {
                return await unitOfWork.Menu.GetMenu(PageNo, PageSize);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetMenus", ex.Message, ex.StackTrace, ex.Source, "MasterService/MenuController/GetMenus");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> ParentMenusDropdownList()
        {
            try
            {
                return await unitOfWork.Menu.GetParentMenusDropdownList();
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ParentMenusDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/MenuController/ParentMenusDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> AddMenu(MenuRequestModel objModel)
        {
            try
            {
                return await unitOfWork.Menu.AddMenu(objModel.Data, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ParentMenusDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/MenuController/ParentMenusDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> ActiveDeactiveMenu(MenuActiveDeactiveModel objModel)
        {
            try
            {
                return await unitOfWork.Menu.ActiveDeactiveMenu(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveMenu", ex.Message, ex.StackTrace, ex.Source, "MasterService/MenuController/ActiveDeactiveMenu");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> GetDashboardReportLink()
        {
            try
            {
                var objModel = new MenuPageLinkFilterModel()
                {
                    RoleId = UserSession.Current.RoleId,
                    MenuId = 1
                };
                return await unitOfWork.Menu.GetMenuPageLink(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDashboardReportLink", ex.Message, ex.StackTrace, ex.Source, "MasterService/MenuController/GetDashboardReportLink");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

    }
}
