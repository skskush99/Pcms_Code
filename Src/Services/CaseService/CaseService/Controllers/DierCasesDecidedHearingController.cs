using Case.Dto.CaseRegistrations;
using Case.Dto.CasesDecidedOnIstHearing;
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
    public class DierCasesDecidedHearingController : ControllerBase
    {
        private readonly LogsService _logsService;
        private readonly IUnitOfWorkService unitOfWork;
        public DierCasesDecidedHearingController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            _logsService = logsService;
            unitOfWork = unitOfWorkService;
        }

        [HttpPost]
        public async Task<ResponseModel> GetCaseList(CasesDecidedOnIstHearingFilterModel objModel)
        {
            try
            {
                var loginUserData = UserSession.Current;
                if (loginUserData.DepartmentId > 0)
                    objModel.AdmDepttId = loginUserData.DepartmentId;
                if (loginUserData.UnitId > 0)
                    objModel.UnitId = loginUserData.UnitId;
                if (loginUserData.OfficeId > 0)
                    objModel.OfficeId = loginUserData.OfficeId;
                return await unitOfWork.CasesDecidedOnIstHearing.GetCaseList(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCaseList", ex.Message, ex.StackTrace, ex.Source, "CaseService/CasesDecidedOnIstHearing/GetCaseList");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<CaseRegistrationsResponseModel> AddCase(CasesDecidedOnIstHearingModel objModel)
        {
            try
            {
                return await unitOfWork.CasesDecidedOnIstHearing.AddCase(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddCase", ex.Message, ex.StackTrace, ex.Source, "CaseService/CasesDecidedOnIstHearing/AddCase");
                return new CaseRegistrationsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
