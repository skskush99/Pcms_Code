using Master.Dto.Masters;
using Master.Dto.Shared;

namespace Master.ServiceBus.OfficeService
{
    public interface IOfficeServiceBus
    {
        Task<ResponseModel> GetOffices(OfficesFilterModel objModel);
        Task<ResponseWithoutPaginationModel> GetOfficesDropdownList(int OfficeId);
        Task<ResponseModel> AddEditOffices(OfficesModel objModel, int UserId);
        Task<ResponseModel> ActiveDeactiveOffices(OfficesActiveDeactiveModel objModel, int UserId);
    }
}
