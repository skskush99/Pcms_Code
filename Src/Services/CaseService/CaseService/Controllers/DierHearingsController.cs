using Case.Dto.CaseHearings;
using Case.Dto.Shared;
using Case.ServiceBus.UnitOfWork;
using CaseService.Middleware;
using Common.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaseService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class DierHearingsController : ControllerBase
    {
        private readonly LogsService _logsService;
        private readonly IUnitOfWorkService unitOfWork;
        public DierHearingsController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            _logsService = logsService;
            unitOfWork = unitOfWorkService;
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetCaseHearingsList(long CaseId)
        {
            try
            {
                return await unitOfWork.CaseHearings.GetCaseHearingsList(CaseId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCaseHearingsList", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseHearingsController/GetCaseHearingsList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> AddEditCaseHearings(CaseHearingsModel objModel)
        {
            try
            {
                return await unitOfWork.CaseHearings.AddEditCaseHearings(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditCaseHearings", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseHearingsController/AddEditCaseHearings");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> DeleteCaseHearings(long CaseHearingId)
        {
            try
            {
                return await unitOfWork.CaseHearings.DeleteCaseHearings(CaseHearingId, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteCaseHearings", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseHearingsController/DeleteCaseHearings");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetReplyComplianceList(long CaseHearingId)
        {
            try
            {
                return await unitOfWork.CaseHearings.GetReplyComplianceList(CaseHearingId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetReplyComplianceList", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseHearingsController/GetReplyComplianceList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> AddEditReplyCompliance(CaseHearingDetailModel objModel)
        {
            try
            {
                return await unitOfWork.CaseHearings.AddEditReplyCompliance(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditReplyCompliance", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseHearingsController/AddEditReplyCompliance");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
