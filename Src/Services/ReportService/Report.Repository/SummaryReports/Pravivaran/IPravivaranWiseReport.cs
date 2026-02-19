using Report.Dto.DetailReports;
using Report.Dto.SummaryReports.PravivaranWise;


namespace Report.Repository.SummaryReports.Pravivaran
{
    public interface IPravivaranWiseReport
    {
        Task<PravivaranResponseModel> GetPravivaran2(Pravivaran_2Model objModel);
        Task<PravivaranResponseModel> GetPravivaran3(Pravivaran_2Model objModel);
        Task<PravivaranResponseModel> GetPravivaran3K(Pravivaran_2Model objModel);
        Task<PravivaranResponseModel> GetPravivaran3Kha(Pravivaran_2Model objModel);
        Task<PravivaranResponseModel> GetPravivaran7(Pravivaran_2Model objModel);
        Task<PravivaranResponseModel> GetReturn4(Pravivaran_2Model objModel);
    }
}
