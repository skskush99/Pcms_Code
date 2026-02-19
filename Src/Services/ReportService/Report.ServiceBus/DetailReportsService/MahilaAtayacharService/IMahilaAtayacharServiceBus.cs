using Report.Dto.DetailReports;

namespace Report.ServiceBus.DetailReportsService.MahilaAtayacharService
{
    public interface IMahilaAtayacharServiceBus
    {
        Task<DetailReportsResponseModel> GetMahilaAtayacharIPCReport(MahilaAtayacharIPCModel objModel);
        Task<DetailReportsResponseModel> GetMahilaAtayacharBNSReport(MahilaAtayacharBNSModel objModel);
    }
}
