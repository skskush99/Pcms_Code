using HighCourtRajCauseList.Dto.CauseListModel;
using HighCourtRajCauseList.Dto.shared;

namespace HighCourtRajCauseList.ServiceBus.HighCourtRajCauseList
{
    public interface IHighCourtRajCauseListService
    {
        Task<ResponseWithoutPaginationModel> AddHighCourtRajCauseList(CauseListRequestModel data);
        Task<ResponseWithoutPaginationModel> AddNewHighCourtRajCauseList(NewCauseListRequestModel data);
        Task<ResponseWithoutPaginationModel> JustDeptScheduler(string JsonData, string CourtType);
        Task<ResponseWithoutPaginationModel> CaseRegistrationHighCourtScheduler(string JsonData, string CourtType);
    }
}
