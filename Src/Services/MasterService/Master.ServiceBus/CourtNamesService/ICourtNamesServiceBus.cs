using Master.Dto.Masters;
using Master.Dto.Shared;

namespace Master.ServiceBus.CourtNamesService
{
    public interface ICourtNamesServiceBus
    {
        Task<ResponseModel> GetCourtNames(CourtNamesFilterModel objModel);
        Task<ResponseWithoutPaginationModel> GetCourtNamesDropdownList(int JCourtId);
        Task<ResponseModel> AddEditCourtNames(CourtNamesModel objModel, int UserId);
        Task<ResponseModel> ActiveDeactiveCourtNames(CourtNamesActiveDeactiveModel objModel, int UserId);
    }
}
