using Master.Dto.Masters;
using Master.Dto.Shared;

namespace Master.Repository.Offices;
public interface IOffices
    {
        //Task<ResponseModel> GetOffices(int PageNo, int PageSize, int AdmDeptId, int UnitId, int DistrictId, int ActiveFilter);
        Task<ResponseModel> GetOffices(OfficesFilterModel objModel);
        Task<ResponseWithoutPaginationModel> GetOfficesDropdownList(int OfficeId);
        Task<ResponseModel> AddEditOffices(OfficesModel objModel, int UserId);
        Task<ResponseModel> ActiveDeactiveOffices(OfficesActiveDeactiveModel objModel, int UserId);
    }

