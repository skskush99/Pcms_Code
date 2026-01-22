using Case.Dto.CaseRegistrations;
using Case.Dto.CasesDecidedOnIstHearing;
using Case.Dto.Shared;

namespace Case.ServiceBus.CasesDecidedOnIstHearing
{
    public interface ICasesDecidedOnIstHearingServiceBus
    {
        Task<ResponseModel> GetCaseList(CasesDecidedOnIstHearingFilterModel objModel);
        Task<CaseRegistrationsResponseModel> AddCase(CasesDecidedOnIstHearingModel objModel, int UserId);
    }
}
