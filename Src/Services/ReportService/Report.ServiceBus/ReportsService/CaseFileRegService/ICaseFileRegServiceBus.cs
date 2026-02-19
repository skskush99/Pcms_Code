using Report.Dto.Reports;

namespace Report.ServiceBus.ReportsService.CaseFileRegService
{
    public interface ICaseFileRegServiceBus
    {
        Task<ReportsResponseModel> GetCaseFileRegReports(CaseFileRegModel objModel);
    }
}
