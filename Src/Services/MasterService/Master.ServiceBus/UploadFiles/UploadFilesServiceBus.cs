using Master.Dto.UploadFiles;
using Master.Dto.Shared;
using Master.Repository.UnitOfwork;
using static Core.Common;

namespace Master.ServiceBus.UploadFiles
{
    public class UploadFilesServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : IUploadFilesServiceBus
    {
        private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
        private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;
        public async Task<ResponseWithoutPaginationModel> GetUploadFileCategoryList()
        {
            try
            {
                var data = _IUnitOfWorkRepository.UploadFiles.GetUploadFileCategoryList();
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseModel> GetUploadFilesList(UploadFilesFIlterModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.UploadFiles.GetUploadFilesList(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> UploadFile(UploadFilesModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.UploadFiles.UploadFile(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> DeleteFile(int FileId, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.UploadFiles.DeleteFile(FileId, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseModel> GetUserManualList(UserManualFIlterModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.UploadFiles.GetUserManualList(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseWithoutPaginationModel> UploadUserManual(UserManualAddEditModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.UploadFiles.UploadUserManual(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> DeleteUserManual(int Id, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.UploadFiles.DeleteUserManual(Id, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
