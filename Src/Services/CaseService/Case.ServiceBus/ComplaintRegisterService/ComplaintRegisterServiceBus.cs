using Case.Dto.ComplaintRegister;
using Case.Dto.DierRegistrations;
using Case.Dto.Shared;
using Case.Repository.UnitOfwork;
using static Core.Common;


namespace Case.ServiceBus.ComplaintRegisterService
{
    public class ComplaintRegisterServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : IComplaintRegisterServiceBus
    {

        private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
        private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

        public async Task<ResponseModel> GetComplaintList(ComplaintListFilterModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.ComplaintRegister.GetComplaintList(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ComplaintRegisterResponseModel> AddEditComplaintRegister(ComplaintRegisterModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.ComplaintRegister.AddEditComplaintRegister(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetPersonAgainstDetails(long ComplaintRegId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.ComplaintRegister.GetPersonAgainstDetails(ComplaintRegId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ComplaintRegisterResponseModel> AddEditPersonAgainstDetails(PersonAgainstDetailsModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.ComplaintRegister.AddEditPersonAgainstDetails(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ComplaintRegisterResponseModel> DeletePersonAgainstDetails(long PersonAgainstId, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.ComplaintRegister.DeletePersonAgainstDetails(PersonAgainstId, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }


}
