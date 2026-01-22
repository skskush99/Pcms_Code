using Case.Dto.CaseHearings;
using Case.Dto.Shared;

namespace Case.Repository.CaseHearings
{
    public interface ICaseHearingsRepository
    {
        Task<ResponseWithoutPaginationModel> GetCaseHearingsList(long CaseId);
        Task<ResponseWithoutPaginationModel> AddEditCaseHearings(CaseHearingsModel objModel, int UserId);
        Task<ResponseWithoutPaginationModel> DeleteCaseHearings(long CaseHearingId, int UserId);
        Task<ResponseWithoutPaginationModel> GetReplyComplianceList(long CaseHearingId);
        Task<ResponseWithoutPaginationModel> AddEditReplyCompliance(CaseHearingDetailModel objModel, int UserId);
    }
}
