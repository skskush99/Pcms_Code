using Case.Dto.DierRegistrations;
using Case.Dto.Shared;

namespace Case.ServiceBus.DierRegistrationsService
{
    public interface IDierRegistrationsServiceBus
    {
        Task<ResponseModel> GetDierList(DierListFilterModel objModel);
        Task<ResponseModel> GetDisposalList(DierListFilterModel objModel);
        Task<DierRegistrationsResponseModel> AddEditDierRegistrationsSteps1(DierRegistrationsSteps1Model objModel, int UserId);
        Task<DierRegistrationsResponseModel> AddEditDierRegistrationsSteps2(DierRegistrationsSteps2Model objModel, int UserId);
        Task<DierRegistrationsResponseModel> AddEditDierRegistrationsSteps3(DierRegistrationsSteps3Model objModel, int UserId);
        Task<DierRegistrationsResponseModel> AddEditDierRegistrationsSteps4(DierRegistrationsSteps4Model objModel, int UserId);
        Task<DisposalRegistrationsResponseModel> AddEditDierDisposalRegistrationsSteps5(FinalDisposalRegister objModel, int UserId);

        Task<ResponseWithoutPaginationModel> AddEditDierRegistrations(DierRegistrations_OldModel objModel, int UserId);

        Task<ResponseWithoutPaginationModel> GetDierAccused(long AccusedGroupNo);
        Task<ResponseWithoutPaginationModel> AddEditDierAccused(DierAccusedModel objModel, int UserId);
        Task<ResponseWithoutPaginationModel> DeleteDierAccused(long AccusedId, int UserId);

        Task<ResponseWithoutPaginationModel> GetDierVictimWitness(long GroupNo);
        Task<ResponseWithoutPaginationModel> AddEditDierVictimWitness(DierVictimWitnessModel objModel, int UserId);
        Task<ResponseWithoutPaginationModel> DeleteDierVictimWitness(long Id, int UserId);


        Task<ResponseWithoutPaginationModel> GetDierInvestigation(long InvestGroupNo);
        Task<ResponseWithoutPaginationModel> AddEditDierInvestigation(DierInvestigationModel objModel, int UserId);
        Task<ResponseWithoutPaginationModel> DeleteDierInvestigation(long InvestId, int UserId);

        Task<ResponseWithoutPaginationModel> GetDierComplaintAgainstPerson(long ComplaintPerGroupNo);
        Task<ResponseWithoutPaginationModel> AddEditDierComplaintAgainstPerson(DierComplaintAgainstPersonModel objModel, int UserId);
        Task<ResponseWithoutPaginationModel> DeleteDierComplaintAgainstPerson(long ComplaintPerId, int UserId);

        Task<ResponseWithoutPaginationModel> GetOffenceClassification(long OffenceClassifGroupNo);
        Task<DierRegistrationsResponseModel> AddEditOffenceClassification(OffenceClassificationModel objModel, int UserId);
        Task<DierRegistrationsResponseModel> DeleteOffenceClassification(long OffenceClassifId, int UserId);

        Task<ResponseWithoutPaginationModel> GetDisposalSentence(long DisposalGroupNo);
        Task<DisposalRegistrationsResponseModel> AddEditDisposalSentence(DisposalSentenceModel objModel, int UserId);
        Task<DisposalRegistrationsResponseModel> DeleteDisposalSentence(long SentenceId, int UserId);

        Task<ResponseWithoutPaginationModel> GetWitnessesAttendanceList(long DirRegId);
        Task<DisposalRegistrationsResponseModel> AddEditWitnessesAttendance(WitnessesAttendanceModel objModel, int UserId);
        Task<DisposalRegistrationsResponseModel> DeleteWitnessesAttendance(long Id, int UserId);


        //Task<ResponseWithoutPaginationModel> GetDierVictim(long VictimGroupNo);
        //Task<ResponseWithoutPaginationModel> AddEditDierVictim(DierVictimModel objModel, int UserId);
        //Task<ResponseWithoutPaginationModel> DeleteDierVictim(long VictimId, int UserId);

        //Task<ResponseWithoutPaginationModel> GetDierWitness(long WitnessGroupNo);
        //Task<ResponseWithoutPaginationModel> AddEditDierWitness(DierWitnessModel objModel, int UserId);
        //Task<ResponseWithoutPaginationModel> DeleteDierWitness(long WitnessId, int UserId);

    }



}
