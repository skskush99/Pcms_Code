using Master.Dto.Masters;
using Master.Dto.Shared;
using Master.Repository.UnitOfwork;
using static Core.Common;


namespace Master.ServiceBus.CrimeActService
{
    public class CrimeActServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : ICrimeActServiceBus
    {
        private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
        private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;


        public async Task<ResponseModel> GetCrimeAct(CrimeActFilterModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CrimeAct.GetCrimeAct(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetCrimeActDropdownList(int CrimeClsId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CrimeAct.GetCrimeActDropdownList(CrimeClsId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseModel> AddEditCrimeAct(AddEditCrimeActModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CrimeAct.AddEditCrimeAct(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseModel> ActiveDeactiveCrimeAct(ActiveDeactiveCrimeActModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CrimeAct.ActiveDeactiveCrimeAct(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }




    }
}
