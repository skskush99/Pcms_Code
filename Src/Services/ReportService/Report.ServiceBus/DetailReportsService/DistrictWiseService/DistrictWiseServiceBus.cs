using Report.Dto.DetailReports;
using Report.Repository.UnitOfwork;
using static Core.Common;

namespace Report.ServiceBus.DetailReportsService.DistrictWiseService
{
    public class DistrictWiseServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : IDistrictWiseServiceBus
    {
        private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
        private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

        public async Task<DetailReportsResponseModel> GetDistrictWiseReport(DistrictWiseModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.DistrictWiseReport.GetDistrictWiseReport(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }





    }    

}
