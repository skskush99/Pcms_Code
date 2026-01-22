using Case.Dto.CaseHearings;
using Case.Dto.Shared;
using Case.Repository.UnitOfwork;
using static Core.Common;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Case.ServiceBus.CaseHearings
{
    public class CaseHearingsServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) :ICaseHearingsServiceBus
    {
        private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
        private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;
        public async Task<ResponseWithoutPaginationModel> GetCaseHearingsList(long CaseId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseHearings.GetCaseHearingsList(CaseId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> AddEditCaseHearings(CaseHearingsModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseHearings.AddEditCaseHearings(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> DeleteCaseHearings(long CaseHearingId, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseHearings.DeleteCaseHearings(CaseHearingId, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetReplyComplianceList(long CaseHearingId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseHearings.GetReplyComplianceList(CaseHearingId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> AddEditReplyCompliance(CaseHearingDetailModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseHearings.AddEditReplyCompliance(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
