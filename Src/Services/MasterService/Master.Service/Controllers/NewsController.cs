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
    public class NewsController : ControllerBase
    {
        private readonly LogsService _logsService;
        private readonly IUnitOfWorkService _IUnitOfWorkService;
        
        public NewsController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            _IUnitOfWorkService = unitOfWorkService;
            _logsService = logsService;
        }

        [HttpPost]
        public async Task<ResponseModel> GetNewsList(NewsFilterModel objModel)
        {
            try
            {
                return await _IUnitOfWorkService.NewsServiceBus.GetNews(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetNewsList", ex.Message, ex.StackTrace, ex.Source, "MasterService/NewsController/GetNewsList");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetNewsDropdownList()
        {
            try
            {
                return await _IUnitOfWorkService.NewsServiceBus.GetNewsDropdownList();
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetNewsDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/NewsController/GetNewsDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> AddEditNews(NewsRequestModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.Data.CreatedBy = UserId;
                objModel.Data.UpdatedBy = UserId;
                return await _IUnitOfWorkService.NewsServiceBus.AddEditNews(objModel.Data, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditNews", ex.Message, ex.StackTrace, ex.Source, "MasterService/NewsController/AddEditNews");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> ActiveDeactiveNews(NewsActiveDeactiveModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.UpdatedBy = UserId;
                return await _IUnitOfWorkService.NewsServiceBus.ActiveDeactiveNews(objModel, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveNews", ex.Message, ex.StackTrace, ex.Source, "MasterService/NewsController/ActiveDeactiveNews");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

    }
}
