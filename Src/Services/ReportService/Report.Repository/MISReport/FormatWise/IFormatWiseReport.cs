using Report.Dto.MISReport.FormatWise;

namespace Report.Repository.MISReport.FormatWise
{
    public interface IFormatWiseReport
    {
        Task<FormatWiseReportsModel> GetFormat_AReport(Format_AReportModel objModel);
        Task<FormatWiseReportsModel> GetFormat_BReport(Format_BReportModel objModel);
    }
}
