using Master.Dto.Masters;
using Master.Dto.Shared;

namespace Master.ServiceBus.LevelService
{
    public interface ILevelServiceBus
    {
        Task<ResponseModel> GetLevel(LevelModelFilterModel objModel);
        Task<ResponseWithoutPaginationModel> GetLevelDropdownList();
        Task<ResponseModel> AddEditLevel(LevelModel objModel, int UserId);
        Task<ResponseModel> ActiveDeactiveLevel(LevelActiveDeactiveModel objModel, int UserId);

    }
}
