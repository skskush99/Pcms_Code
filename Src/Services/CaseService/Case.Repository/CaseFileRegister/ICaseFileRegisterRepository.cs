using Case.Dto.CaseFileRegister;
using Case.Dto.Shared;

namespace Case.Repository.CaseFileRegister
{
    public interface ICaseFileRegisterRepository
    {
        Task<ResponseModel> GetCaseFileRegisterList(CaseFileRegisterFilterModel objModel);
        Task<ResponseWithoutPaginationModel> GetLawDeptFileNoCount(CaseFileRegisterCountFilterModel objModel);
        Task<ResponseWithoutPaginationModel> AddEditCaseFileRegister(CaseFileRegisterModel objModel, int UserId);
        Task<ResponseModel> GetConnectedCaseList(ConnectedCaseFilterModel objModel);
        Task<ResponseWithoutPaginationModel> AddEditConnectedCase(CaseFileRegisterModel objModel, int UserId);
        Task<ResponseWithoutPaginationModel> DeleteConnectedCase(int CaseFileRegistorId, int UserId);
        Task<ResponseWithoutPaginationModel> GetConnectedCaseListByCaseFileRegistorId(int CaseFileRegistorId);
        Task<ResponseModel> GetUploadDocumentList(int PageNo, int PageSize);
        Task<ResponseWithoutPaginationModel> AddUploadDocument(AddCaseFileRegisterUploadDocumentModel objModel, int UserId);
        Task<ResponseWithoutPaginationModel> DeactiveUploadDocument(DeactiveCaseFileRegisterUploadDocumentModel objModel, int UserId);

    }
}
