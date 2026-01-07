using Master.Dto.Masters;
using Master.Dto.Shared;

namespace Master.ServiceBus.NewsService
{
    public interface INewsServiceBus
    {
        Task<ResponseModel> GetNews(NewsFilterModel objModel);
        Task<ResponseWithoutPaginationModel> GetNewsDropdownList();
        Task<ResponseModel> AddEditNews(NewsModel objModel, int UserId);
        Task<ResponseModel> ActiveDeactiveNews(NewsActiveDeactiveModel objModel, int UserId);
    }
}
