using Case.Dto.CaseRegistrations;
using Case.Dto.Shared;
using Case.Repository.UnitOfwork;
using static Core.Common;

namespace Case.ServiceBus.CaseRegistrations
{
    public class CaseRegistrationsServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : ICaseRegistrationsServiceBus
    {

        private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
        private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

        public async Task<ResponseModel> GetCaseList(CaseListFilterModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseRegistrations.GetCaseList(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CaseRegistrationsResponseModel> AddEditCaseRegistrations(CaseRegistrationsModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseRegistrations.AddEditCaseRegistrations(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CaseRegistrationsResponseModel> DeleteCase(long CaseId, string DeleteMobileNo, string Reason, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseRegistrations.DeleteCase(CaseId, DeleteMobileNo, Reason, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CaseRegistrationsResponseModel> AddCaseGroup(AddCaseGroupModel objModel, int UserId)

        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseRegistrations.AddCaseGroup(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public async Task<CaseRegistrationsResponseModel> AddCaseLinking(AddCaseLinkingModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseRegistrations.AddCaseLinking(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public async Task<CaseRegistrationsResponseModel> AddCaseRemand(AddCaseRemandModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseRegistrations.AddCaseRemand(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public async Task<ResponseWithoutPaginationModel> GetCaseAppellantsList(long CaseId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseRegistrations.GetCaseAppellantsList(CaseId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public async Task<ResponseWithoutPaginationModel> AddEditCaseAppellants(CaseAppellantsModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseRegistrations.AddEditCaseAppellants(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public async Task<ResponseWithoutPaginationModel> DeleteCaseAppellants(long CaseAppellantId, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseRegistrations.DeleteCaseAppellants(CaseAppellantId, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public async Task<ResponseWithoutPaginationModel> GetCaseRespondentsList(long CaseId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseRegistrations.GetCaseRespondentsList(CaseId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public async Task<ResponseWithoutPaginationModel> AddEditCaseRespondents(CaseRespondentsModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseRegistrations.AddEditCaseRespondents(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public async Task<ResponseWithoutPaginationModel> DeleteCaseRespondents(long RespondentId, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseRegistrations.DeleteCaseRespondents(RespondentId, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetCaseDocumentsList(long CaseId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseRegistrations.GetCaseDocumentsList(CaseId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CaseRegistrationsResponseModel> AddCaseDocuments(CaseAddDocumentModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseRegistrations.AddCaseDocuments(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> DeleteCaseDocuments(long CaseDocumentId, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseRegistrations.DeleteCaseDocuments(CaseDocumentId, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseModel> GetCaseListWithoutCaseNo(CaseListFilterModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseRegistrations.GetCaseListWithoutCaseNo(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CaseRegistrationsResponseModel> AddEditCaseWithoutCaseNo(CaseWithoutCaseNoModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseRegistrations.AddEditCaseWithoutCaseNo(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CaseRegistrationsResponseModel> GetCaseRegistrationDataByCaseId(long CaseId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseRegistrations.GetCaseRegistrationDataByCaseId(CaseId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CaseRegistrationsResponseModel> CheckCaseEntry(CheckCaseEntryModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseRegistrations.CheckCaseEntry(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetLinkCaseList(long LinkCaseId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseRegistrations.GetLinkCaseList(LinkCaseId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Add sandeep 25/07/2025
        public async Task<ResponseModel> GetCaseRegistrationGovtEmpList(CaseRegistrationGovtEmpListFilterModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseRegistrations.GetCaseRegistrationGovtEmpList(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseWithoutPaginationModel> AddEditCaseRegistrationGovtEmp(CaseRegistrationGovtEmpModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseRegistrations.AddEditCaseRegistrationGovtEmp(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseWithoutPaginationModel> DeactiveCaseRegistrationGovtEmp(int CRGEId, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.CaseRegistrations.DeactiveCaseRegistrationGovtEmp(CRGEId, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        // Add sandeep 25/07/2025
    }
}
