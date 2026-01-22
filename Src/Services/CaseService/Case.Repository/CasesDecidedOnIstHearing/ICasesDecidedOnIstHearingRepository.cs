using Case.Dto.CaseRegistrations;
using Case.Dto.CasesDecidedOnIstHearing;
using Case.Dto.Shared;

namespace Case.Repository.CasesDecidedOnIstHearing
{
    public interface ICasesDecidedOnIstHearingRepository
    {
        Task<ResponseModel> GetCaseList(CasesDecidedOnIstHearingFilterModel objModel);
        Task<CaseRegistrationsResponseModel> AddCase(CasesDecidedOnIstHearingModel objModel, int UserId);
    }
}
