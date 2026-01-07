using Master.Dto.Masters;
using Master.Dto.Shared;

namespace Master.ServiceBus.UnitsDepartmentService
{
    public interface IUnitsDepartmentServiceBus
    {
        Task<ResponseModel> GetUnitDepartment(UnitsDepartmentFilterModel objModel);
        Task<ResponseModel> GetUnitDepartmentRajMaster(UnitsDepartmentFilterModel objModel);
        Task<ResponseWithoutPaginationModel> GetUnitDepartmentDropdownList(int AdmDptID, int UnitId);
        Task<ResponseWithoutPaginationModel> GetUnitDepartmentRajMasterDropdownList(int AdmDptID, int UnitId);
        Task<ResponseWithoutPaginationModel> GetDepartmentWiseUnitDropdownList(int AdmDptID);
        Task<ResponseWithoutPaginationModel> GetDepartmentWiseUnitRajMasterDropdownList(int AdmDptID);
        Task<ResponseWithoutPaginationModel> AddEditUnitDepartment(UnitsDepartmentModel objModel, int UnitId, int UserId);
        Task<ResponseWithoutPaginationModel> ActiveDeactiveUnitDepartment(UnitsDepartmentActiveDeactiveModel objModel, int UnitId, int UserId);
        Task<ResponseWithoutPaginationModel> ActiveDeactiveUnitDepartmentRajMaster(UnitsDepartmentActiveDeactiveModel objModel, int UnitId, int UserId);

    }
}
