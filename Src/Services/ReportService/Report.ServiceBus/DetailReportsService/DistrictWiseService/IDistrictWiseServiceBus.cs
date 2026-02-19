using Report.Dto.DetailReports;

namespace Report.ServiceBus.DetailReportsService.DistrictWiseService
{
    public interface IDistrictWiseServiceBus
    {
        Task<DetailReportsResponseModel> GetDistrictWiseReport(DistrictWiseModel objModel);
    }
}
