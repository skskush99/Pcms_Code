using Case.Dto.CaseFileRegister;
using Case.Dto.Shared;
using Case.Repository.UnitOfwork;
using static Core.Common;

namespace Case.ServiceBus.CaseFileRegister
{
    public class CaseFileRegisterServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : ICaseFileRegisterServiceBus
    {
        private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
        private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

        public async Task<ResponseModel> GetCaseFileRegisterList(CaseFileRegisterFilterModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseFileRegister.GetCaseFileRegisterList(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetLawDeptFileNoCount(CaseFileRegisterCountFilterModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseFileRegister.GetLawDeptFileNoCount(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> AddEditCaseFileRegister(CaseFileRegisterModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseFileRegister.AddEditCaseFileRegister(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseModel> GetConnectedCaseList(ConnectedCaseFilterModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseFileRegister.GetConnectedCaseList(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseWithoutPaginationModel> AddEditConnectedCase(CaseFileRegisterModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseFileRegister.AddEditConnectedCase(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseWithoutPaginationModel> DeleteConnectedCase(int CaseFileRegistorId, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseFileRegister.DeleteConnectedCase(CaseFileRegistorId, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseWithoutPaginationModel> GetConnectedCaseListByCaseFileRegistorId(int CaseFileRegistorId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseFileRegister.GetConnectedCaseListByCaseFileRegistorId(CaseFileRegistorId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseModel> GetUploadDocumentList(int PageNo, int PageSize)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseFileRegister.GetUploadDocumentList(PageNo, PageSize);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseWithoutPaginationModel> AddUploadDocument(AddCaseFileRegisterUploadDocumentModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseFileRegister.AddUploadDocument(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseWithoutPaginationModel> DeactiveUploadDocument(DeactiveCaseFileRegisterUploadDocumentModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseFileRegister.DeactiveUploadDocument(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
