using Case.Dto.CaseDecision;
using Case.Dto.Shared;

namespace Case.ServiceBus.CaseDecision
{
    public interface ICaseDecisionServiceBus
    {
        Task<ResponseWithoutPaginationModel> GetCaseDecisionList(long CaseId);
        Task<CaseDecisionResponseModel> AddEditCaseDecision(CaseDecisionModel objModel, int UserId);
        Task<ResponseWithoutPaginationModel> DeleteCaseDecision(long DecisionId, int UserId);
        Task<ResponseWithoutPaginationModel> GetCaseDecisionPamcList(long CaseId);
        Task<CaseDecisionResponseModel> AddEditCaseDecisionPamc(CaseDecisionPamcAddModel objModel, int UserId);
        Task<ResponseWithoutPaginationModel> DeactiveCaseDecisionPamc(long PamcId, int UserId);
        Task<ResponseWithoutPaginationModel> DeleteFromCaseDecisionUpdateList(long caseId, int UserId);
    }
}
