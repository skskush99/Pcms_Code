using Master.Dto.Masters;
using Master.Dto.Shared;
using Master.Repository.UnitOfwork;
using static Core.Common;

namespace Master.ServiceBus.CaseDecisionReasonService
{
    public class CaseDecisionReasonServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : ICaseDecisionReasonServiceBus
    {
        private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
        private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

        public async Task<ResponseModel> GetDecisionReason(CaseDecisionReasonFilterModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseDecisionReason.GetDecisionReason(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetDecisionReasonDropdownList(int DecisionTypeId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseDecisionReason.GetDecisionReasonDropdownList(DecisionTypeId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseModel> AddEditDecisionReason(AddEditCaseDecisionReasonModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseDecisionReason.AddEditDecisionReason(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseModel> ActiveDeactiveDecisionReason(ActiveDeactiveCaseDecisionReasonModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseDecisionReason.ActiveDeactiveDecisionReason(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
