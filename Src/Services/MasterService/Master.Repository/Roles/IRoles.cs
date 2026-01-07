using Master.Dto.Roles;
using Master.Dto.Shared;

namespace Master.Repository.Roles
{
    public interface IRoles
    {
        Task<ResponseModel> GetRoles(int PageNo, int PageSize);
        Task<ResponseWithoutPaginationModel> GetRolesDropdownList();
        Task<ResponseWithoutPaginationModel> GetRolesNodelOfficerDropdownList(int RoleId);
        Task<ResponseWithoutPaginationModel> AddRole(RoleModel objModel, int UserId);
        Task<ResponseWithoutPaginationModel> ActiveDeactiveRole(RoleActiveDeactiveModel objModel, int UserId);
        Task<ResponseWithoutPaginationModel> DBAction(DBActionModel objModel);
    }
}
