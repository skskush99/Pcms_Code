using Master.Dto.Menu;
using Master.Dto.Shared;

namespace Master.Repository.Menu
{
    public interface IMenu
    {
        Task<ResponseModel> GetMenu(int PageNo, int PageSize);
        Task<ResponseWithoutPaginationModel> GetParentMenusDropdownList();
        Task<ResponseWithoutPaginationModel> AddMenu(MenuModel objModel, int UserId);
        Task<ResponseWithoutPaginationModel> ActiveDeactiveMenu(MenuActiveDeactiveModel objModel, int UserId);
        Task<ResponseWithoutPaginationModel> GetMenuPageLink(MenuPageLinkFilterModel objModel);
        Task<ResponseWithoutPaginationModel> GetMenuMapping(int RoleId);
        Task<ResponseWithoutPaginationModel> AddEditMenuMapping(IEnumerable<MenuMappingModel> objModel, int RoleId, int UserId);

        Task<ResponseWithoutPaginationModel> GetMenuMappingUser(int RoleId, int UserId);
        Task<ResponseWithoutPaginationModel> AddEditMenuMappingUser(IEnumerable<MenuMappingModel> objModel, int RoleId, int UserId, int ActionBy);
    }
}
