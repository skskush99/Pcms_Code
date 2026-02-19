using Report.Dto.MISReport.FormatWise;
using Report.Repository.UnitOfwork;
using static Core.Common;

namespace Report.ServiceBus.MISReportService.FormatWiseService
{
    public class FormatWiseServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : IFormatWiseServiceBus
    {
        private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
        private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

        public async Task<FormatWiseReportsModel> GetFormat_AReport(Format_AReportModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.FormatWiseReport.GetFormat_AReport(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<FormatWiseReportsModel> GetFormat_BReport(Format_BReportModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.FormatWiseReport.GetFormat_BReport(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
