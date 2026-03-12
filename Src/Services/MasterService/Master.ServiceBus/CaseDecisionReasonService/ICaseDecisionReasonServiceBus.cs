using Master.Dto.Masters;
using Master.Dto.Shared;

namespace Master.ServiceBus.CaseDecisionReasonService
{
    public interface ICaseDecisionReasonServiceBus
    {
        Task<ResponseModel> GetDecisionReason(CaseDecisionReasonFilterModel objModel);
        Task<ResponseWithoutPaginationModel> GetDecisionReasonDropdownList(int DecisionTypeId);
        Task<ResponseModel> AddEditDecisionReason(AddEditCaseDecisionReasonModel objModel, int UserId);
        Task<ResponseModel> ActiveDeactiveDecisionReason(ActiveDeactiveCaseDecisionReasonModel objModel, int UserId);
    }
}
