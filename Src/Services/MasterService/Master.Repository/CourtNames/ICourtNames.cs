using Master.Dto.Masters;
using Master.Dto.Shared;

namespace Master.Repository.CourtName;

public interface ICourtNames
{
    Task<ResponseModel> GetCourtNames(CourtNamesFilterModel objModel);
    Task<ResponseWithoutPaginationModel> GetCourtNamesDropdownList(int JCourtId);
    Task<ResponseModel> AddEditCourtNames(CourtNamesModel objModel, int UserId);
    Task<ResponseModel> ActiveDeactiveCourtNames(CourtNamesActiveDeactiveModel objModel, int UserId);
}
