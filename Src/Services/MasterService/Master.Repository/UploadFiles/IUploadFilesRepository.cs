using Master.Dto.UploadFiles;
using Master.Dto.Shared;

namespace Master.Repository.UploadFiles
{
    public interface IUploadFilesRepository
    {
        Task<ResponseWithoutPaginationModel> GetUploadFileCategoryList();
        Task<ResponseModel> GetUploadFilesList(UploadFilesFIlterModel objModel);
        Task<ResponseWithoutPaginationModel> UploadFile(UploadFilesModel objModel, int UserId);
        Task<ResponseWithoutPaginationModel> DeleteFile(int FileId, int UserId);
        Task<ResponseModel> GetUserManualList(UserManualFIlterModel objModel);
        Task<ResponseWithoutPaginationModel> UploadUserManual(UserManualAddEditModel objModel, int UserId);
        Task<ResponseWithoutPaginationModel> DeleteUserManual(int Id, int UserId);
    }
}
