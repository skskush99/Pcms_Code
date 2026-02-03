using Master.Dto.Masters;
using Master.Dto.Shared;
using Master.Repository.UnitOfwork;
using static Core.Common;

namespace Master.ServiceBus.JanPratinidhiService
{
    public class JanPratinidhiServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : IJanPratinidhiServiceBus
    {
        private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
        private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

        public async Task<ResponseModel> GetJanPratinidhi(JanPratinidhiFilterModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.JanPratinidhi.GetJanPratinidhi(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseWithoutPaginationModel> GetJanPratinidhiDropdownList()
        {
            try
            {
                var data = _IUnitOfWorkRepository.JanPratinidhi.GetJanPratinidhiDropdownList();
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseModel> AddEditJanPratinidhi(JanPratinidhiModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.JanPratinidhi.AddEditJanPratinidhi(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseModel> ActiveDeactiveJanPratinidhi(JanPratinidhiActiveDeactiveModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.JanPratinidhi.ActiveDeactiveJanPratinidhi(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }



    }


}
