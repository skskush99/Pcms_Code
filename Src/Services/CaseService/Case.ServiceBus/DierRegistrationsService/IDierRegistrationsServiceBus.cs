using Case.Dto.DierRegistrations;
using Case.Dto.Shared;

namespace Case.ServiceBus.DierRegistrationsService
{
    public interface IDierRegistrationsServiceBus
    {
        Task<ResponseModel> GetDierList(DierListFilterModel objModel);
        Task<ResponseWithoutPaginationModel> AddEditDierRegistrations(DierRegistrationsModel objModel, int UserId);

        Task<ResponseWithoutPaginationModel> GetDierAccused(long AccusedGroupNo);
        Task<ResponseWithoutPaginationModel> AddEditDierAccused(DierAccusedModel objModel, int UserId);
        Task<ResponseWithoutPaginationModel> DeleteDierAccused(long AccusedId, int UserId);

        Task<ResponseWithoutPaginationModel> GetDierVictimWitness(long GroupNo);
        Task<ResponseWithoutPaginationModel> AddEditDierVictimWitness(DierVictimWitnessModel objModel, int UserId);
        Task<ResponseWithoutPaginationModel> DeleteDierVictimWitness(long Id, int UserId);

        Task<ResponseWithoutPaginationModel> GetDierVictim(long VictimGroupNo);
        Task<ResponseWithoutPaginationModel> AddEditDierVictim(DierVictimModel objModel, int UserId);
        Task<ResponseWithoutPaginationModel> DeleteDierVictim(long VictimId, int UserId);

        Task<ResponseWithoutPaginationModel> GetDierWitness(long WitnessGroupNo);
        Task<ResponseWithoutPaginationModel> AddEditDierWitness(DierWitnessModel objModel, int UserId);
        Task<ResponseWithoutPaginationModel> DeleteDierWitness(long WitnessId, int UserId);

        Task<ResponseWithoutPaginationModel> GetDierInvestigation(long InvestGroupNo);
        Task<ResponseWithoutPaginationModel> AddEditDierInvestigation(DierInvestigationModel objModel, int UserId);
        Task<ResponseWithoutPaginationModel> DeleteDierInvestigation(long InvestId, int UserId);

        Task<ResponseWithoutPaginationModel> GetDierComplaintAgainstPerson(long ComplaintPerGroupNo);
        Task<ResponseWithoutPaginationModel> AddEditDierComplaintAgainstPerson(DierComplaintAgainstPersonModel objModel, int UserId);
        Task<ResponseWithoutPaginationModel> DeleteDierComplaintAgainstPerson(long ComplaintPerId, int UserId);


    }



}
