using Case.Dto.CaseRegistrations;
using Case.Dto.CasesDecidedOnIstHearing;
using Case.Dto.Shared;
using Case.Repository.UnitOfwork;
using Case.ServiceBus.CaseRegistrations;
using static Core.Common;

namespace Case.ServiceBus.CasesDecidedOnIstHearing
{
    public class CasesDecidedOnIstHearingServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : ICasesDecidedOnIstHearingServiceBus
    {
        private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
        private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

        public async Task<ResponseModel> GetCaseList(CasesDecidedOnIstHearingFilterModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CasesDecidedOnIstHearing.GetCaseList(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CaseRegistrationsResponseModel> AddCase(CasesDecidedOnIstHearingModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CasesDecidedOnIstHearing.AddCase(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
