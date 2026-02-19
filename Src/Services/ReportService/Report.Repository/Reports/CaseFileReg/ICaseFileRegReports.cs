using Report.Dto.Reports;

namespace Report.Repository.Reports.CaseFileReg
{
    public interface ICaseFileRegReports
    {
        Task<ReportsResponseModel> GetCaseFileRegReports(CaseFileRegModel objModel);
    }


}
