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

    public class CaseDecisionReasonController : ControllerBase
    {
        private readonly LogsService _logsService;
        private readonly IUnitOfWorkService _IUnitOfWorkService;

        public CaseDecisionReasonController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            _IUnitOfWorkService = unitOfWorkService;
            _logsService = logsService;
        }

        [HttpPost]
        public async Task<ResponseModel> GetDecisionReason(CaseDecisionReasonFilterModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                return await _IUnitOfWorkService.CaseDecisionReasonServiceBus.GetDecisionReason(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDecisionReason", ex.Message, ex.StackTrace, ex.Source, "MasterService/CaseDecisionReasonController/GetDecisionReason");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetDecisionReasonDropdownList(int DecisionTypeId)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                return await _IUnitOfWorkService.CaseDecisionReasonServiceBus.GetDecisionReasonDropdownList(DecisionTypeId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDecisionReasonDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/CaseDecisionReasonController/GetDecisionReasonDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> AddEditDecisionReason(AddEditCaseDecisionReasonModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.CreatedBy = UserId;
                objModel.UpdatedBy = UserId;
                return await _IUnitOfWorkService.CaseDecisionReasonServiceBus.AddEditDecisionReason(objModel, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDecisionReason", ex.Message, ex.StackTrace, ex.Source, "MasterService/CaseDecisionReasonController/AddEditDecisionReason");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> ActiveDeactiveDecisionReason(ActiveDeactiveCaseDecisionReasonModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.UpdatedBy = UserId;
                return await _IUnitOfWorkService.CaseDecisionReasonServiceBus.ActiveDeactiveDecisionReason(objModel, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveDecisionReason", ex.Message, ex.StackTrace, ex.Source, "MasterService/CaseDecisionReasonController/ActiveDeactiveDecisionReason");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }



    }
}
