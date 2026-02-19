using Report.Dto.MISReport.FormatWise;

namespace Report.ServiceBus.MISReportService.FormatWiseService
{
    public interface IFormatWiseServiceBus
    {
        Task<FormatWiseReportsModel> GetFormat_AReport(Format_AReportModel objModel);
        Task<FormatWiseReportsModel> GetFormat_BReport(Format_BReportModel objModel);
    }
}
