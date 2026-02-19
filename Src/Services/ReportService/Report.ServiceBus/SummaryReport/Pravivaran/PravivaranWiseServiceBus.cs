using Report.Dto.SummaryReports.PravivaranWise;
using Report.Repository.UnitOfwork;
using static Core.Common;

namespace Report.ServiceBus.SummaryReport.Pravivaran
{
    public class PravivaranWiseServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : IPravivaranWiseServiceBus
    {

        private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
        private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

        public async Task<PravivaranResponseModel> GetPravivaran2(Pravivaran_2Model objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.PravivaranWiseReport.GetPravivaran2(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PravivaranResponseModel> GetPravivaran3(Pravivaran_2Model objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.PravivaranWiseReport.GetPravivaran3(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PravivaranResponseModel> GetPravivaran3K(Pravivaran_2Model objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.PravivaranWiseReport.GetPravivaran3K(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PravivaranResponseModel> GetPravivaran3Kha(Pravivaran_2Model objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.PravivaranWiseReport.GetPravivaran3Kha(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PravivaranResponseModel> GetPravivaran7(Pravivaran_2Model objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.PravivaranWiseReport.GetPravivaran7(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PravivaranResponseModel> GetReturn4(Pravivaran_2Model objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.PravivaranWiseReport.GetReturn4(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        











    }
}
