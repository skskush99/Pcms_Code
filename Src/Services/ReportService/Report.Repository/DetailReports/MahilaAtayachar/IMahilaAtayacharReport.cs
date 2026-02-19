using Report.Dto.DetailReports;

namespace Report.Repository.DetailReports.MahilaAtayachar
{
    public interface IMahilaAtayacharReport
    {
        Task<DetailReportsResponseModel> GetMahilaAtayacharIPCReport(MahilaAtayacharIPCModel objModel);
        Task<DetailReportsResponseModel> GetMahilaAtayacharBNSReport(MahilaAtayacharBNSModel objModel);

    }
}
