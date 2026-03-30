using Case.Dto.DierRegistrations_New;
using Case.Dto.Shared;

namespace Case.Repository.DierRegistrations_New
{
    public interface IDierRegistrations_NewRepository
    {
        Task<ResponseModel> GetDierList(Dier_NewListFilterModel objModel);
        Task<DierRegistrations_NewResponseModel> AddEditDierRegistrationsSteps1(DierRegistrations_NewSteps1Model objModel, int UserId);
        Task<DierRegistrations_NewResponseModel> AddEditDierRegistrationsSteps2(DierRegistrations_NewSteps2Model objModel, int UserId);
        Task<DierRegistrations_NewResponseModel> AddEditDierRegistrationsSteps3(DierRegistrations_NewSteps3Model objModel, int UserId);
        Task<DierRegistrations_NewResponseModel> AddEditDierRegistrationsSteps4(DierRegistrations_NewSteps4Model objModel, int UserId);

        Task<ResponseWithoutPaginationModel> AddEditDierRegistrations(DierRegistrations_New_OldModel objModel, int UserId);


        Task<ResponseWithoutPaginationModel> GetDierAccused(long AccusedGroupNo);
        Task<ResponseWithoutPaginationModel> AddEditDierAccused(Dier_NewAccusedModel objModel, int UserId);
        Task<ResponseWithoutPaginationModel> DeleteDierAccused(long AccusedId, int UserId);


        Task<ResponseWithoutPaginationModel> GetDierVictimWitness(long GroupNo);
        Task<ResponseWithoutPaginationModel> AddEditDierVictimWitness(Dier_NewVictimWitnessModel objModel, int UserId);
        Task<ResponseWithoutPaginationModel> DeleteDierVictimWitness(long Id, int UserId);


        Task<ResponseWithoutPaginationModel> GetDierInvestigation(long InvestGroupNo);
        Task<ResponseWithoutPaginationModel> AddEditDierInvestigation(Dier_NewInvestigationModel objModel, int UserId);
        Task<ResponseWithoutPaginationModel> DeleteDierInvestigation(long InvestId, int UserId);

        Task<ResponseWithoutPaginationModel> GetDierComplaintAgainstPerson(long ComplaintPerGroupNo);
        Task<ResponseWithoutPaginationModel> AddEditDierComplaintAgainstPerson(Dier_NewComplaintAgainstPersonModel objModel, int UserId);
        Task<ResponseWithoutPaginationModel> DeleteDierComplaintAgainstPerson(long ComplaintPerId, int UserId);

        Task<ResponseWithoutPaginationModel> GetOffenceClassification(long OffenceClassifGroupNo);
        Task<DierRegistrations_NewResponseModel> AddEditOffenceClassification(OffenceClassification_NewModel objModel, int UserId);
        Task<DierRegistrations_NewResponseModel> DeleteOffenceClassification(long OffenceClassifId, int UserId);

        Task<DierRegistrations_NewResponseModel> AddEditCompleteDierRegistrationsSteps2(DierRegistrations_NewSteps2Model objModel, int UserId);
        Task<DierRegistrations_NewResponseModel> AddEditCompleteDierRegistrationsStep3(DierRegistrations_NewSteps3Model objModel, int UserId);
        Task<DierRegistrations_NewResponseModel> AddEditCompleteDierRegistrationsFinal(DierRegistrations_NewModel objModel, int UserId);
    }
}
