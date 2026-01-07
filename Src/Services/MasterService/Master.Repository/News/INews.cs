using Master.Dto.Masters;
using Master.Dto.Shared;

namespace Master.Repository.News;
public interface INews
    {
        Task<ResponseModel> GetNews(NewsFilterModel objModel);
        Task<ResponseWithoutPaginationModel> GetNewsDropdownList();
        Task<ResponseModel> AddEditNews(NewsModel objModel, int UserId);
        Task<ResponseModel> ActiveDeactiveNews(NewsActiveDeactiveModel objModel, int UserId);

    }

