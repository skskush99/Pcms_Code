using Master.Dto.Masters;
using Master.Dto.Shared;

namespace Master.ServiceBus.CourtTypesService
{
    public interface ICourtTypesServiceBus
    {
        Task<ResponseModel> GetCourtTypes(CourtTypesFilterModel objModel);
        Task<ResponseWithoutPaginationModel> GetCourtTypesDropdownList(int CourtTypeId);
        Task<ResponseModel> AddEditCourtTypes(CourtTypesModel objModel, int UserId);
        Task<ResponseModel> ActiveDeactiveCourtTypes(CourtTypesActiveDeactiveModel objModel, int UserId);
    }
}
