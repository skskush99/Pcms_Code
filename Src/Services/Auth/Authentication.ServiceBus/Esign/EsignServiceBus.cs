using Authentication.Dto.Shared;
using Authentication.Repository.UnitOfwork;
using static Core.Common;
namespace Authentication.ServiceBus.Esign
{
    public class EsignServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : IEsignServiceBus
    {
        private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
        private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

        public async Task<object> AddEsignData(string txn, string esignData)
        {
            try
            {
                var data = _IUnitOfWorkRepository.Esign.AddEsignData(txn, esignData);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
