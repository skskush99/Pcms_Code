using Case.Dto.CaseRegistrations;
using Case.Dto.Shared;

namespace Case.ServiceBus.CaseRegistrations
{
    public interface ICaseRegistrationsServiceBus
    {
        Task<ResponseModel> GetCaseList(CaseListFilterModel objModel);
        Task<CaseRegistrationsResponseModel> AddEditCaseRegistrations(CaseRegistrationsModel objModel, int UserId);
        Task<CaseRegistrationsResponseModel> DeleteCase(long CaseId, string DeleteMobileNo, string Reason, int UserId);
       
        Task<CaseRegistrationsResponseModel> AddCaseGroup(AddCaseGroupModel objModel, int UserId);
        Task<CaseRegistrationsResponseModel> AddCaseLinking(AddCaseLinkingModel objModel, int UserId);
        Task<CaseRegistrationsResponseModel> AddCaseRemand(AddCaseRemandModel objModel, int UserId);
        
        Task<ResponseWithoutPaginationModel> GetCaseAppellantsList(long CaseId);
        Task<ResponseWithoutPaginationModel> AddEditCaseAppellants(CaseAppellantsModel objModel, int UserId);
        Task<ResponseWithoutPaginationModel> DeleteCaseAppellants(long CaseAppellantId, int UserId);
        
        Task<ResponseWithoutPaginationModel> GetCaseRespondentsList(long CaseId);
        Task<ResponseWithoutPaginationModel> AddEditCaseRespondents(CaseRespondentsModel objModel, int UserId);
        Task<ResponseWithoutPaginationModel> DeleteCaseRespondents(long RespondentId, int UserId);

        Task<ResponseWithoutPaginationModel> GetCaseDocumentsList(long CaseId);
        Task<CaseRegistrationsResponseModel> AddCaseDocuments(CaseAddDocumentModel objModel, int UserId);
        Task<ResponseWithoutPaginationModel> DeleteCaseDocuments(long CaseDocumentId, int UserId);

        Task<ResponseModel> GetCaseListWithoutCaseNo(CaseListFilterModel objModel);
        Task<CaseRegistrationsResponseModel> AddEditCaseWithoutCaseNo(CaseWithoutCaseNoModel objModel, int UserId);
        Task<CaseRegistrationsResponseModel> GetCaseRegistrationDataByCaseId(long CaseId);

        Task<CaseRegistrationsResponseModel> CheckCaseEntry(CheckCaseEntryModel objModel);
        Task<ResponseWithoutPaginationModel> GetLinkCaseList(long LinkCaseId);

        // Add sandeep 25/07/2025
        Task<ResponseModel> GetCaseRegistrationGovtEmpList(CaseRegistrationGovtEmpListFilterModel objModel);
        Task<ResponseWithoutPaginationModel> AddEditCaseRegistrationGovtEmp(CaseRegistrationGovtEmpModel objModel, int UserId);
        Task<ResponseWithoutPaginationModel> DeactiveCaseRegistrationGovtEmp(int CRGEId, int UserId);

        // Add sandeep 25/07/2025
    }
}
