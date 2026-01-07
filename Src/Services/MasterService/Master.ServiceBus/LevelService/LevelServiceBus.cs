using Master.Dto.Masters;
using Master.Dto.Shared;
using Master.Repository.UnitOfwork;
using static Core.Common;

namespace Master.ServiceBus.LevelService;

public class LevelServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : ILevelServiceBus
{
    private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
    private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

    public async Task<ResponseModel> GetLevel(LevelModelFilterModel objModel)
    {
        try
        {
            var data = _IUnitOfWorkRepository.Level.GetLevel(objModel);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseWithoutPaginationModel> GetLevelDropdownList()
    {
        try
        {
            var data = _IUnitOfWorkRepository.Level.GetLevelDropdownList();
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseModel> AddEditLevel(LevelModel objModel, int UserId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.Level.AddEditLevel(objModel, UserId);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseModel> ActiveDeactiveLevel(LevelActiveDeactiveModel objModel, int UserId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.Level.ActiveDeactiveLevel(objModel, UserId);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }


}
