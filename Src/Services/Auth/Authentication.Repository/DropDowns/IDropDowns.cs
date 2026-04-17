using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authentication.Dto.Shared;

namespace Authentication.Repository.DropDowns
{
    public interface IDropDowns
    {
        Task<ResponseWithoutPaginationModel> GetLevelDropdownList();
        Task<ResponseWithoutPaginationModel> GetRolesDropdownList();
        Task<ResponseWithoutPaginationModel> GetDivisionsList();
        Task<ResponseWithoutPaginationModel> GetDistrictsList(int DivisionId, int StateId);
        Task<ResponseWithoutPaginationModel> GetOfficesDropdownList(int OfficeId);
        Task<ResponseWithoutPaginationModel> GetOfficesByDistrictIdDropdownList(int DistrictId);
        Task<ResponseWithoutPaginationModel> GetDesignationDropdownList();
        Task<ResponseWithoutPaginationModel> GetDesignationByRoleIdDropdownList(int RoleId);
        Task<ResponseWithoutPaginationModel> GetCourtNamesDropdownList(int JCourtId);
        Task<ResponseWithoutPaginationModel> GetCourtNamesByOfficeIdDropdownList(int OfficeId);
        Task<ResponseWithoutPaginationModel> AddEditUserMapReq(UserMapReqModel objModel, int UserId);

    }
}
