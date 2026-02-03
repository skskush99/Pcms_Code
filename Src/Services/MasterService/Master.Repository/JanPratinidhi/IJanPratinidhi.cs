using Master.Dto.Masters;
using Master.Dto.Shared;

namespace Master.Repository.JanPratinidhi
{
    public interface IJanPratinidhi
    {
        Task<ResponseModel> GetJanPratinidhi(JanPratinidhiFilterModel objModel);
        Task<ResponseWithoutPaginationModel> GetJanPratinidhiDropdownList();
        Task<ResponseModel> AddEditJanPratinidhi(JanPratinidhiModel objModel, int UserId);
        Task<ResponseModel> ActiveDeactiveJanPratinidhi(JanPratinidhiActiveDeactiveModel objModel, int UserId);

    }
}
