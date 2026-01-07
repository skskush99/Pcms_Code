using Master.Dto.WebSite;
using Master.Dto.Shared;
using Master.Repository.UnitOfwork;
using static Core.Common;

namespace Master.ServiceBus.WebSiteService
{
    public class WebSiteServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : IWebSiteServiceBus
    {
        private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
        private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

        public async Task<ResponseModel> GetWebSiteUploadFilesList(WebSitesFIlterModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.WebSite.GetWebSiteUploadFilesList(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseWithoutPaginationModel> WebSiteUploadFile(WebSitesModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.WebSite.WebSiteUploadFile(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseWithoutPaginationModel> WebSiteContact(WebSitesContactAddModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.WebSite.WebSiteContact(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseWithoutPaginationModel> WebSiteActiveDeActiveFile(int Id, int Active, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.WebSite.WebSiteActiveDeActiveFile(Id, Active, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        


    }
}
