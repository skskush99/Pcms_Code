using Common.Repository;
using Master.Dto.Masters;
using Master.ServiceBus.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Master.Service.Middleware;
using Master.Dto.Shared;

namespace Master.Service.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]

    public class CaseDecisionTypeController : ControllerBase
    {
        private readonly LogsService _logsService;
        private readonly IUnitOfWorkService _IUnitOfWorkService;

        public CaseDecisionTypeController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            _IUnitOfWorkService = unitOfWorkService;
            _logsService = logsService;
        }

        [HttpPost]
        public async Task<ResponseModel> GetCaseDecisionType(CaseDecisionTypeFilterModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                return await _IUnitOfWorkService.CaseDecisionTypeServiceBus.GetCaseDecisionType(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCaseDecisionType", ex.Message, ex.StackTrace, ex.Source, "MasterService/CaseDecisionTypeController/GetCaseDecisionType");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetCaseDecisionTypeDropdownList()
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                return await _IUnitOfWorkService.CaseDecisionTypeServiceBus.GetCaseDecisionTypeDropdownList();
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCaseDecisionTypeDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/CaseDecisionTypeController/GetCaseDecisionTypeDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> AddEditCaseDecisionType(AddEditCaseDecisionTypeModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.CreatedBy = UserId;
                objModel.UpdatedBy = UserId;
                return await _IUnitOfWorkService.CaseDecisionTypeServiceBus.AddEditCaseDecisionType(objModel, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditCaseDecisionType", ex.Message, ex.StackTrace, ex.Source, "MasterService/CaseDecisionTypeController/AddEditCaseDecisionType");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> ActiveDeactiveCaseDecisionType(ActiveDeactiveCaseDecisionTypeModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.UpdatedBy = UserId;
                return await _IUnitOfWorkService.CaseDecisionTypeServiceBus.ActiveDeactiveCaseDecisionType(objModel, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveCaseDecisionType", ex.Message, ex.StackTrace, ex.Source, "MasterService/CaseDecisionTypeController/ActiveDeactiveCaseDecisionType");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


    }
}
