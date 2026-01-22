using Master.Dto.Masters;
using Master.Dto.Shared;
using Master.Repository.UnitOfwork;
using static Core.Common;

namespace Master.ServiceBus.CrimeSubActService
{
    public class CrimeSubActServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : ICrimeSubActServiceBus
    {
        private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
        private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

        public async Task<ResponseModel> GetCrimeSubAct(CrimeSubActFilterModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CrimeSubAct.GetCrimeSubAct(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetCrimeSubActDropdownList(int CrimeActId, int CrimeClsId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CrimeSubAct.GetCrimeSubActDropdownList(CrimeActId, CrimeClsId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseModel> AddEditCrimeSubAct(AddEditCrimeSubActModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CrimeSubAct.AddEditCrimeSubAct(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseModel> ActiveDeactiveCrimeSubAct(ActiveDeactiveCrimeSubActModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CrimeSubAct.ActiveDeactiveCrimeSubAct(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
