using Master.Dto.Masters;
using Master.Dto.Shared;

namespace Master.ServiceBus.CircularOrderService
{
    public interface ICircularOrderServiceBus
    {
        Task<ResponseModel> GetCircularOrders(CircularOrderFilterModel objModel);
        //Task<ResponseModel> AddEditCircularOrder(CircularOrderModel objModel);
        Task<ResponseWithoutPaginationModel> AddEditCircularOrder(CircularOrderModel objModel, int UserId);
        Task<ResponseModel> ActiveDeactiveCircularOrder(CircularOrderActiveDeactiveModel objModel, int UserId);
    }
}
