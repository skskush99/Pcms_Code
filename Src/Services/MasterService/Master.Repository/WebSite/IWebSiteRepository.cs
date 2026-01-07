using Master.Dto.WebSite;
using Master.Dto.Shared;

namespace Master.Repository.WebSite
{
    public interface IWebSiteRepository
    {
        Task<ResponseModel> GetWebSiteUploadFilesList(WebSitesFIlterModel objModel);
        Task<ResponseWithoutPaginationModel> WebSiteUploadFile(WebSitesModel objModel, int UserId);
        Task<ResponseWithoutPaginationModel> WebSiteContact(WebSitesContactAddModel objModel, int UserId);
        Task<ResponseWithoutPaginationModel> WebSiteActiveDeActiveFile(int Id, int Active, int UserId);
    
    }
}
