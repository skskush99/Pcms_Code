using Case.Dto.CaseDecision;
using Case.Dto.Shared;
using Case.Repository.UnitOfwork;
using static Core.Common;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Case.ServiceBus.CaseDecision
{
    public class CaseDecisionServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) :ICaseDecisionServiceBus
    {
        private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
        private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;
        public async Task<ResponseWithoutPaginationModel> GetCaseDecisionList(long CaseId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseDecision.GetCaseDecisionList(CaseId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CaseDecisionResponseModel> AddEditCaseDecision(CaseDecisionModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseDecision.AddEditCaseDecision(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> DeleteCaseDecision(long DecisionId, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseDecision.DeleteCaseDecision(DecisionId, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetCaseDecisionPamcList(long CaseId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseDecision.GetCaseDecisionPamcList(CaseId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CaseDecisionResponseModel> AddEditCaseDecisionPamc(CaseDecisionPamcAddModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseDecision.AddEditCaseDecisionPamc(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> DeactiveCaseDecisionPamc(long PamcId, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseDecision.DeactiveCaseDecisionPamc(PamcId, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> DeleteFromCaseDecisionUpdateList(long caseId, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseDecision.DeleteFromCaseDecisionUpdateList(caseId, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
