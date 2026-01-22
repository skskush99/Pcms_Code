using Case.Dto.DierRegistrations;
using Case.Dto.Shared;
using Case.Repository.UnitOfwork;
using static Core.Common;


namespace Case.ServiceBus.DierRegistrationsService
{
    public class DierRegistrationsServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : IDierRegistrationsServiceBus
    {

        private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
        private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;


        public async Task<ResponseModel> GetDierList(DierListFilterModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.DierRegistrations.GetDierList(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DierRegistrationsResponseModel> AddEditDierRegistrations(DierRegistrationsModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.DierRegistrations.AddEditDierRegistrations(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetDierAccused(long AccusedGroupNo)
        {
            try
            {
                var data = _IUnitOfWorkRepository.DierRegistrations.GetDierAccused(AccusedGroupNo);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseWithoutPaginationModel> AddEditDierAccused(DierAccusedModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.DierRegistrations.AddEditDierAccused(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseWithoutPaginationModel> DeleteDierAccused(long AccusedId, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.DierRegistrations.DeleteDierAccused(AccusedId, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<ResponseWithoutPaginationModel> GetDierVictim(long VictimGroupNo)
        {
            try
            {
                var data = _IUnitOfWorkRepository.DierRegistrations.GetDierVictim(VictimGroupNo);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseWithoutPaginationModel> AddEditDierVictim(DierVictimModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.DierRegistrations.AddEditDierVictim(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseWithoutPaginationModel> DeleteDierVictim(long VictimId, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.DierRegistrations.DeleteDierVictim(VictimId, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetDierWitness(long WitnessGroupNo)
        {
            try
            {
                var data = _IUnitOfWorkRepository.DierRegistrations.GetDierWitness(WitnessGroupNo);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseWithoutPaginationModel> AddEditDierWitness(DierWitnessModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.DierRegistrations.AddEditDierWitness(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseWithoutPaginationModel> DeleteDierWitness(long WitnessId, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.DierRegistrations.DeleteDierWitness(WitnessId, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<ResponseWithoutPaginationModel> GetDierInvestigation(long InvestGroupNo)
        {
            try
            {
                var data = _IUnitOfWorkRepository.DierRegistrations.GetDierInvestigation(InvestGroupNo);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseWithoutPaginationModel> AddEditDierInvestigation(DierInvestigationModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.DierRegistrations.AddEditDierInvestigation(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseWithoutPaginationModel> DeleteDierInvestigation(long InvestId, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.DierRegistrations.DeleteDierInvestigation(InvestId, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetDierComplaintAgainstPerson(long ComplaintPerGroupNo)
        {
            try
            {
                var data = _IUnitOfWorkRepository.DierRegistrations.GetDierComplaintAgainstPerson(ComplaintPerGroupNo);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseWithoutPaginationModel> AddEditDierComplaintAgainstPerson(DierComplaintAgainstPersonModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.DierRegistrations.AddEditDierComplaintAgainstPerson(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseWithoutPaginationModel> DeleteDierComplaintAgainstPerson(long ComplaintPerId, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.DierRegistrations.DeleteDierComplaintAgainstPerson(ComplaintPerId, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }



    }


}
