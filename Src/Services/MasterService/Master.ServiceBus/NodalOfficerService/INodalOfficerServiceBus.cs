using Master.Dto.Masters;
using Master.Dto.Shared;

namespace Master.ServiceBus.NodalOfficerService
{
    public interface INodalOfficerServiceBus
    {
        Task<ResponseModel> GetNodalOfficer(NodalOfficerFilterModel objModel, int LoginRoleId);
        Task<ResponseWithoutPaginationModel> GetNodalOfficerDropdownList();
        Task<ResponseWithoutPaginationModel> AddEditNodalOfficer(NodalOfficerModel objModel);
        Task<ResponseWithoutPaginationModel> ActiveDeactiveNodalOfficer(NodalOfficerActiveDeactiveModel objModel,int UserId);

    }
}
