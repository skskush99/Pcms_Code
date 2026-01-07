using Master.Dto.Roles;
using Master.Dto.Shared;

namespace Master.ServiceBus.Roles
{
    public interface IRolesServiceBus
    {
        Task<ResponseModel> GetRoles(int PageNo, int PageSize);
        Task<ResponseWithoutPaginationModel> GetRolesDropdownList();
        Task<ResponseWithoutPaginationModel> GetRolesNodelOfficerDropdownList(int RoleId);
        Task<ResponseWithoutPaginationModel> AddRole(RoleModel objModel,int UserId);
        Task<ResponseWithoutPaginationModel> ActiveDeactiveRole(RoleActiveDeactiveModel objModel, int UserId);
        Task<ResponseWithoutPaginationModel> DBAction(DBActionModel objModel);
    }
}
