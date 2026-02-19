using Report.Dto.Reports;
using Report.Repository.UnitOfwork;
using static Core.Common;

namespace Report.ServiceBus.ReportsService.CaseFileRegService
{
    public class CaseFileRegServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : ICaseFileRegServiceBus
    {
        private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
        private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

        public async Task<ReportsResponseModel> GetCaseFileRegReports(CaseFileRegModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseFileRegReports.GetCaseFileRegReports(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }





    }    

}
