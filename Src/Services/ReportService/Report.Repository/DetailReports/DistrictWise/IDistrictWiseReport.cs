using Report.Dto.DetailReports;

namespace Report.Repository.DetailReports.DistrictWise
{
    public interface IDistrictWiseReport
    {
        Task<DetailReportsResponseModel> GetDistrictWiseReport(DistrictWiseModel objModel);

    }
}
