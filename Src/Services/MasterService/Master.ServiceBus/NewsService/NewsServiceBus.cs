using Master.Dto.Masters;
using Master.Dto.Shared;
using Master.Repository.UnitOfwork;
using static Core.Common;

namespace Master.ServiceBus.NewsService;

public class NewsServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : INewsServiceBus
{
    private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
    private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

    public async Task<ResponseModel> GetNews(NewsFilterModel objModel)
    {
        try
        {
            var data = _IUnitOfWorkRepository.News.GetNews(objModel);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseWithoutPaginationModel> GetNewsDropdownList()
    {
        try
        {
            var data = _IUnitOfWorkRepository.News.GetNewsDropdownList();
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseModel> AddEditNews(NewsModel objModel, int UserId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.News.AddEditNews(objModel, UserId);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseModel> ActiveDeactiveNews(NewsActiveDeactiveModel objModel, int UserId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.News.ActiveDeactiveNews(objModel, UserId);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }

}
