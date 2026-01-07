using Master.Dto.Masters;
using Master.Dto.Shared;

namespace Master.ServiceBus.AdminDepartmentService
{
    public interface IAdminDepartmentServiceBus
    {
        Task<ResponseModel> GetAdmDep(AdminRequestFilterModel objModel);
        Task<ResponseWithoutPaginationModel> GetAdmDepDropdownList(int AdmDeptId);
        Task<ResponseModel> AddEditAdmDep(AdminDepartmentModel objModel, int UserId);
        Task<ResponseModel> ActiveDeactiveAdmDep(AdminActiveDeactiveModel objModel, int UserId);

    }
}
