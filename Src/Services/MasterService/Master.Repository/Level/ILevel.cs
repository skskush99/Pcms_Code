using Master.Dto.Masters;
using Master.Dto.Shared;

namespace Master.Repository.Level;

public interface ILevel
{
    Task<ResponseModel> GetLevel(LevelModelFilterModel objModel);
    Task<ResponseWithoutPaginationModel> GetLevelDropdownList();
    Task<ResponseModel> AddEditLevel(LevelModel objModel, int UserId);
    Task<ResponseModel> ActiveDeactiveLevel(LevelActiveDeactiveModel objModel, int UserId);
}
