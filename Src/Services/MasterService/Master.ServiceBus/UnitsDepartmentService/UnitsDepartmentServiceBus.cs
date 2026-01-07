using Master.Dto.Masters;
using Master.Dto.Shared;
using Master.Repository.UnitOfwork;
using static Core.Common;


namespace Master.ServiceBus.UnitsDepartmentService;

public class UnitsDepartmentServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : IUnitsDepartmentServiceBus
{
    private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
    private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

    public async Task<ResponseModel> GetUnitDepartment(UnitsDepartmentFilterModel objModel)
    {
        try
        {
            var data = _IUnitOfWorkRepository.UnitsDepartment.GetUnitDepartment(objModel);
            return await data;
        }
        catch (Exception)
        {

            throw;
        }
    }
    public async Task<ResponseModel> GetUnitDepartmentRajMaster(UnitsDepartmentFilterModel objModel)
    {
        try
        {
            var data = _IUnitOfWorkRepository.UnitsDepartment.GetUnitDepartmentRajMaster(objModel);
            return await data;
        }
        catch (Exception)
        {

            throw;
        }
    }
    public async Task<ResponseWithoutPaginationModel> GetUnitDepartmentDropdownList(int AdmDptID, int UnitId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.UnitsDepartment.GetUnitDepartmentDropdownList(AdmDptID, UnitId);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseWithoutPaginationModel> GetUnitDepartmentRajMasterDropdownList(int AdmDptID, int UnitId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.UnitsDepartment.GetUnitDepartmentRajMasterDropdownList(AdmDptID, UnitId);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseWithoutPaginationModel> GetDepartmentWiseUnitDropdownList(int AdmDptID)
    {
        try
        {
            var data = _IUnitOfWorkRepository.UnitsDepartment.GetDepartmentWiseUnitDropdownList(AdmDptID);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseWithoutPaginationModel> GetDepartmentWiseUnitRajMasterDropdownList(int AdmDptID)
    {
        try
        {
            var data = _IUnitOfWorkRepository.UnitsDepartment.GetDepartmentWiseUnitRajMasterDropdownList(AdmDptID);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseWithoutPaginationModel> AddEditUnitDepartment(UnitsDepartmentModel objModel, int UnitId, int UserId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.UnitsDepartment.AddEditUnitDepartment(objModel, UnitId, UserId);
            return await data;
        }
        catch (Exception)
        {

            throw;
        }
    }
    public async Task<ResponseWithoutPaginationModel> ActiveDeactiveUnitDepartment(UnitsDepartmentActiveDeactiveModel objModel, int UnitId, int UserId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.UnitsDepartment.ActiveDeactiveUnitDepartment(objModel, UnitId, UserId);
            return await data;
        }
        catch (Exception)
        {

            throw;
        }
    }
    public async Task<ResponseWithoutPaginationModel> ActiveDeactiveUnitDepartmentRajMaster(UnitsDepartmentActiveDeactiveModel objModel, int UnitId, int UserId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.UnitsDepartment.ActiveDeactiveUnitDepartmentRajMaster(objModel, UnitId, UserId);
            return await data;
        }
        catch (Exception)
        {

            throw;
        }
    }
}
