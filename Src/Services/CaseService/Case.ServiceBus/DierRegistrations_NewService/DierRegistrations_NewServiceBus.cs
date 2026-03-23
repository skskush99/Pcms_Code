using Case.Dto.DierRegistrations_New;
using Case.Dto.Shared;
using Case.Repository.UnitOfwork;
using static Core.Common;

namespace Case.ServiceBus.DierRegistrations_NewService
{
    public class DierRegistrations_NewServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : IDierRegistrations_NewServiceBus
    {

        private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
        private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;


        public async Task<ResponseModel> GetDierList(Dier_NewListFilterModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.DierRegistrations_New.GetDierList(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<DierRegistrations_NewResponseModel> AddEditDierRegistrationsSteps1(DierRegistrations_NewSteps1Model objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.DierRegistrations_New.AddEditDierRegistrationsSteps1(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<DierRegistrations_NewResponseModel> AddEditDierRegistrationsSteps2(DierRegistrations_NewSteps2Model objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.DierRegistrations_New.AddEditDierRegistrationsSteps2(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<DierRegistrations_NewResponseModel> AddEditDierRegistrationsSteps3(DierRegistrations_NewSteps3Model objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.DierRegistrations_New.AddEditDierRegistrationsSteps3(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<DierRegistrations_NewResponseModel> AddEditDierRegistrationsSteps4(DierRegistrations_NewSteps4Model objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.DierRegistrations_New.AddEditDierRegistrationsSteps4(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseWithoutPaginationModel> AddEditDierRegistrations(DierRegistrations_New_OldModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.DierRegistrations_New.AddEditDierRegistrations(objModel, UserId);
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
                var data = _IUnitOfWorkRepository.DierRegistrations_New.GetDierAccused(AccusedGroupNo);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseWithoutPaginationModel> AddEditDierAccused(Dier_NewAccusedModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.DierRegistrations_New.AddEditDierAccused(objModel, UserId);
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
                var data = _IUnitOfWorkRepository.DierRegistrations_New.DeleteDierAccused(AccusedId, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<ResponseWithoutPaginationModel> GetDierVictimWitness(long GroupNo)
        {
            try
            {
                var data = _IUnitOfWorkRepository.DierRegistrations_New.GetDierVictimWitness(GroupNo);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseWithoutPaginationModel> AddEditDierVictimWitness(Dier_NewVictimWitnessModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.DierRegistrations_New.AddEditDierVictimWitness(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseWithoutPaginationModel> DeleteDierVictimWitness(long Id, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.DierRegistrations_New.DeleteDierVictimWitness(Id, UserId);
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
                var data = _IUnitOfWorkRepository.DierRegistrations_New.GetDierInvestigation(InvestGroupNo);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseWithoutPaginationModel> AddEditDierInvestigation(Dier_NewInvestigationModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.DierRegistrations_New.AddEditDierInvestigation(objModel, UserId);
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
                var data = _IUnitOfWorkRepository.DierRegistrations_New.DeleteDierInvestigation(InvestId, UserId);
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
                var data = _IUnitOfWorkRepository.DierRegistrations_New.GetDierComplaintAgainstPerson(ComplaintPerGroupNo);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseWithoutPaginationModel> AddEditDierComplaintAgainstPerson(Dier_NewComplaintAgainstPersonModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.DierRegistrations_New.AddEditDierComplaintAgainstPerson(objModel, UserId);
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
                var data = _IUnitOfWorkRepository.DierRegistrations_New.DeleteDierComplaintAgainstPerson(ComplaintPerId, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetOffenceClassification(long OffenceClassifGroupNo)
        {
            try
            {
                var data = _IUnitOfWorkRepository.DierRegistrations_New.GetOffenceClassification(OffenceClassifGroupNo);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<DierRegistrations_NewResponseModel> AddEditOffenceClassification(OffenceClassification_NewModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.DierRegistrations_New.AddEditOffenceClassification(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<DierRegistrations_NewResponseModel> DeleteOffenceClassification(long OffenceClassifId, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.DierRegistrations_New.DeleteOffenceClassification(OffenceClassifId, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DierRegistrations_NewResponseModel> AddEditCompleteDierRegistrationsSteps2(DierRegistrations_NewSteps2Model objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.DierRegistrations_New.AddEditCompleteDierRegistrationsSteps2(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<DierRegistrations_NewResponseModel> AddEditCompleteDierRegistrationsStep3(DierRegistrations_NewSteps3Model objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.DierRegistrations_New.AddEditCompleteDierRegistrationsStep3(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<DierRegistrations_NewResponseModel> AddEditCompleteDierRegistrationsFinal(DierRegistrations_NewModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.DierRegistrations_New.AddEditCompleteDierRegistrationsFinal(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
