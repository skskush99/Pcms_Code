using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authentication.Dto.Shared;

namespace Authentication.ServiceBus.DropDownsService
{
    public interface IDropDownsServiceBus
    {
        Task<ResponseWithoutPaginationModel> GetLevelDropdownList();
        Task<ResponseWithoutPaginationModel> GetRolesDropdownList();
        Task<ResponseWithoutPaginationModel> GetDivisionsList();
        Task<ResponseWithoutPaginationModel> GetDistrictsList(int DivisionId, int StateId);
        Task<ResponseWithoutPaginationModel> GetOfficesDropdownList(int OfficeId);
        Task<ResponseWithoutPaginationModel> GetDesignationDropdownList();
        Task<ResponseWithoutPaginationModel> GetCourtNamesDropdownList(int JCourtId);
        Task<ResponseWithoutPaginationModel> AddEditUserMapReq(UserMapReqModel objModel, int UserId);

    }





}
