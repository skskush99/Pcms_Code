using Master.Dto.Masters;
using Master.Dto.Shared;
using Master.Repository.UnitOfwork;
using static Core.Common;

namespace Master.ServiceBus.CaseDecisionTypeService
{
    public class CaseDecisionTypeServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : ICaseDecisionTypeServiceBus
    {
        private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
        private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

        public async Task<ResponseModel> GetCaseDecisionType(CaseDecisionTypeFilterModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseDecisionType.GetCaseDecisionType(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetCaseDecisionTypeDropdownList()
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseDecisionType.GetCaseDecisionTypeDropdownList();
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseModel> AddEditCaseDecisionType(AddEditCaseDecisionTypeModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseDecisionType.AddEditCaseDecisionType(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseModel> ActiveDeactiveCaseDecisionType(ActiveDeactiveCaseDecisionTypeModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseDecisionType.ActiveDeactiveCaseDecisionType(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }







    }
}
