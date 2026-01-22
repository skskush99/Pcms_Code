using Master.Dto.Masters;
using Master.Dto.Shared;
using Master.Repository.UnitOfwork;
using static Core.Common;

namespace Master.ServiceBus.FirStatusService
{
    public class FirStatusServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : IFirStatusServiceBus
    {
        private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
        private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

        public async Task<ResponseModel> GetFirStatus(FIRStatusFilterModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.FirStatus.GetFirStatus(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetFirStatusDropdownList()
        {
            try
            {
                var data = _IUnitOfWorkRepository.FirStatus.GetFirStatusDropdownList();
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseModel> AddEditFirStatus(AddEditFIRStatusModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.FirStatus.AddEditFirStatus(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseModel> ActiveDeactiveFirStatus(ActiveDeactiveFIRStatusModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.FirStatus.ActiveDeactiveFirStatus(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }



    }
}
