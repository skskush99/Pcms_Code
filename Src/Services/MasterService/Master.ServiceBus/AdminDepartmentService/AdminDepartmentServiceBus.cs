

using Master.Dto.Masters;
using Master.Dto.Shared;
using Master.Repository.UnitOfwork;
using static Core.Common;

namespace Master.ServiceBus.AdminDepartmentService;

public class AdminDepartmentServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : IAdminDepartmentServiceBus
{
    private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
    private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

    public async Task<ResponseModel> GetAdmDep(AdminRequestFilterModel objModel)
    {
        try
        {
            var data = _IUnitOfWorkRepository.AdminDepartments.GetAdmDep(objModel);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseWithoutPaginationModel> GetAdmDepDropdownList(int AdmDeptId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.AdminDepartments.GetAdmDepDropdownList(AdmDeptId);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseModel> AddEditAdmDep(AdminDepartmentModel objModel, int UserId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.AdminDepartments.AddEditAdmDep(objModel, UserId);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseModel> ActiveDeactiveAdmDep(AdminActiveDeactiveModel objModel, int UserId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.AdminDepartments.ActiveDeactiveAdmDep(objModel, UserId);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }


}
