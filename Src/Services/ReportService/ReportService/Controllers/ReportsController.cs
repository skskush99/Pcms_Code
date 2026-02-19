using Report.ServiceBus.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Report.Dto.Reports;
using Microsoft.AspNetCore.Authorization;
using Common.Repository;

namespace ReportService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly IUnitOfWorkService unitOfWork;
        private readonly LogsService _logsService;
        public ReportsController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            unitOfWork = unitOfWorkService;
            _logsService = logsService;
        }       

        [HttpPost]
        public async Task<ReportsResponseModel> GetCaseFileRegReports(CaseFileRegModel objModel)
        {
            try
            {
                return await unitOfWork.CaseFileRegService.GetCaseFileRegReports(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCaseFileRegReports", ex.Message, ex.StackTrace, ex.Source, "ReportService/ReportsController/GetCaseFileRegReports");
                return new ReportsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

    }
}
