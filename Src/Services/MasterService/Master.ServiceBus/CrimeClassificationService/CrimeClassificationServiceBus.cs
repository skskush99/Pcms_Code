using Master.Dto.Masters;
using Master.Dto.Shared;
using Master.Repository.UnitOfwork;
using static Core.Common;

namespace Master.ServiceBus.CrimeClassificationService
{
    public class CrimeClassificationServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : ICrimeClassificationServiceBus
    {
        private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
        private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;


        public async Task<ResponseModel> GetCrimeClassification(CrimeClassificationFilterModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CrimeClassification.GetCrimeClassification(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetCrimeClassificationDropdownList()
        {
            try
            {
                var data = _IUnitOfWorkRepository.CrimeClassification.GetCrimeClassificationDropdownList();
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseModel> AddEditCrimeClassification(AddEditCrimeClassificationModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CrimeClassification.AddEditCrimeClassification(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseModel> ActiveDeactiveCrimeClassification(ActiveDeactiveCrimeClassificationModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CrimeClassification.ActiveDeactiveCrimeClassification(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }


}
