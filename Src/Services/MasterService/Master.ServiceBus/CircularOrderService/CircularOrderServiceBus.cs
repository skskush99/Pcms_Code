using Master.Dto.Masters;
using Master.Dto.Shared;
using Master.Repository.UnitOfwork;
using static Core.Common;

namespace Master.ServiceBus.CircularOrderService;

public class CircularOrderServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : ICircularOrderServiceBus
{
    private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
    private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

    public async Task<ResponseModel> GetCircularOrders(CircularOrderFilterModel objModel)
    {
        try
        {
            var data = _IUnitOfWorkRepository.CircularOrder.GetCircularOrders(objModel);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseWithoutPaginationModel> AddEditCircularOrder(CircularOrderModel objModel, int UserId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.CircularOrder.AddEditCircularOrder(objModel, UserId);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseModel> ActiveDeactiveCircularOrder(CircularOrderActiveDeactiveModel objModel, int UserId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.CircularOrder.ActiveDeactiveCircularOrder(objModel, UserId);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }

}
