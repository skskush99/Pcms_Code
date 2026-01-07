using Master.Dto.Masters;
using Master.Dto.Shared;

namespace Master.Repository.CourtPlaces;


public interface ICourtPlaces
{
    Task<ResponseModel> GetCourtPlaces(CourtPlacesFilterModel objModel);
    Task<ResponseWithoutPaginationModel> GetCourtPlacesDropdownList(int CourtTypeId, int TehsilId);
    Task<ResponseModel> AddEditCourtPlaces(CourtPlacesModel objModel, int UserId);
    Task<ResponseModel> ActiveDeactiveCourtPlaces(CourtPlacesActiveDeactiveModel objModel, int UserId);

}
