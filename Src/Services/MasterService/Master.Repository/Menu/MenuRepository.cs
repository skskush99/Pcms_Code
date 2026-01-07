using System.Data;
using Master.Dto.Menu;
using Master.Dto.Shared;
using Dapper;
using Common.Dapper;
using Microsoft.Extensions.Configuration;
using System.Text;
using Common.Repository;

namespace Master.Repository.Menu
{
    public class MenuRepository : SqlRepository, IMenu
    {
        private readonly LogsService _logsService;
        private readonly System.Data.IDbConnection Con;
        public MenuRepository(IConfiguration Configuration, LogsService logsService) : base(Configuration)
        {
            
        }

        public async Task<ResponseModel> GetMenu(int PageNo, int PageSize)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetMenus");
                    parmeters.Add("@PageNo", PageNo);
                    parmeters.Add("@Pagesize", PageSize);
                    var objResult = await Con.QueryMultipleAsync("spUsr_Menu", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseModel objResut = new()
                    {
                        Status = true,
                        Message = "",
                        Data = objResult.Read<object>(),
                        Pagination = objResult.Read<PaginationModel>()
                    };
                    DisposeCurrentSqlConnection();

                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetMenu", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/MenuRepository/GetMenu");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetParentMenusDropdownList()
        {           
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetParentMenusDropdownList");
                    var objData = await Con.QueryAsync<DropdownlistModel>("spUsr_Menu", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseWithoutPaginationModel objResut = new()
                    {
                        Status = true,
                        Message = "",
                        Data = objData
                    };
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetParentMenusDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/MenuRepository/GetParentMenusDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> AddMenu(MenuModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddMenu");
                    parmeters.Add("@Id", objModel.Id);
                    parmeters.Add("@PerentId", objModel.ParentId);
                    parmeters.Add("@EnglishName", objModel.EnglishName);
                    parmeters.Add("@HindiName", objModel.HindiName);
                    parmeters.Add("@LinkPage", objModel.LinkPage);
                    parmeters.Add("@Icon", objModel.Icon);
                    parmeters.Add("@IsDisplay", objModel.IsDisplay == true ? 1 : 0);
                    parmeters.Add("@DisplayOrder", objModel.DisplayOrder);
                    parmeters.Add("@IsActive", objModel.IsActive == true ? 1 : 0);
                    parmeters.Add("@CreatedBy", UserId);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spUsr_Menu", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddMenu", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/MenuRepository/AddMenu");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> ActiveDeactiveMenu(MenuActiveDeactiveModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "ActiveDeactiveMenu");
                    parmeters.Add("@Id", objModel.Id);
                    parmeters.Add("@Active", objModel.Active == true ? 1 : 0);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spUsr_Menu", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveMenu", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/MenuRepository/ActiveDeactiveMenu");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetMenuPageLink(MenuPageLinkFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetMenuPageLink");
                    parmeters.Add("@RoleId", objModel.RoleId);
                    parmeters.Add("@MenuId", objModel.MenuId);
                    var objData = await Con.QueryAsync<object>("spUsr_Menu", parmeters, commandType: CommandType.StoredProcedure);
                    ResponseWithoutPaginationModel objResut = new()
                    {
                        Status = true,
                        Message = "",
                        Data = objData
                    };
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetMenuPageLink", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/MenuRepository/GetMenuPageLink");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetMenuMapping(int RoleId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetMenuMapping");
                    parmeters.Add("@RoleId", RoleId);
                    var objResult = await Con.QueryMultipleAsync("spUsr_Menu", parmeters, commandType: CommandType.StoredProcedure);

                    var mainMenu = objResult.Read<MenuMappingModel>();
                    var subMenu = objResult.Read<SubMenuModel>();
                    foreach (var item in mainMenu)
                        item.SubMenus = subMenu.Where(x => x.ParentId == item.Id);

                    ResponseWithoutPaginationModel objResut = new ResponseWithoutPaginationModel();
                    objResut.Status = true;
                    objResut.Message = "";
                    objResut.Data = mainMenu;
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetMenuMapping", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/MenuRepository/GetMenuMapping");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> AddEditMenuMapping(IEnumerable<MenuMappingModel> objModel, int RoleId, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddEditMenuMapping");
                    parmeters.Add("@RoleId", RoleId);
                    parmeters.Add("@MenuMappingXML", GetMenuMappingModelXML(objModel));
                    parmeters.Add("@CreatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spUsr_Menu", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditMenuMapping", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/MenuRepository/AddEditMenuMapping");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetMenuMappingUser(int RoleId, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetMenuMappingUser");
                    parmeters.Add("@RoleId", RoleId);
                    parmeters.Add("@UserId", UserId);
                    var objResult = await Con.QueryMultipleAsync("spUsr_Menu", parmeters, commandType: CommandType.StoredProcedure);

                    var mainMenu = objResult.Read<MenuMappingModel>();
                    var subMenu = objResult.Read<SubMenuModel>();
                    foreach (var item in mainMenu)
                        item.SubMenus = subMenu.Where(x => x.ParentId == item.Id);

                    ResponseWithoutPaginationModel objResut = new ResponseWithoutPaginationModel();
                    objResut.Status = true;
                    objResut.Message = "";
                    objResut.Data = mainMenu;
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetMenuMappingUser", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/MenuRepository/GetMenuMappingUser");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> AddEditMenuMappingUser(IEnumerable<MenuMappingModel> objModel, int RoleId, int UserId, int ActionBy)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddEditMenuMappingUser");
                    parmeters.Add("@RoleId", RoleId);
                    parmeters.Add("@UserId", UserId);
                    parmeters.Add("@MenuMappingXML", GetMenuMappingModelXML(objModel));
                    parmeters.Add("@CreatedBy", ActionBy);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spUsr_Menu", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditMenuMappingUser", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/MenuRepository/AddEditMenuMappingUser");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        private string? GetMenuMappingModelXML(dynamic model)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<MenuMappingModel>");
            foreach (var menu in model)
            {
                sb.Append("<Mapping>");
                sb.Append(string.Format(@"<MenuId>{0}</MenuId><IsAddPermission>{1}</IsAddPermission><IsEditPermission>{2}</IsEditPermission><IsDeletePermission>{3}</IsDeletePermission>", menu.Id, menu.IsAddPermission == true ? 1 : 0, menu.IsEditPermission == true ? 1 : 0, menu.IsDeletePermission == true ? 1 : 0));
                sb.Append("</Mapping>");
                foreach (var subMenu in menu.SubMenus)
                {
                    sb.Append("<Mapping>");
                    sb.Append(string.Format(@"<MenuId>{0}</MenuId><IsAddPermission>{1}</IsAddPermission><IsEditPermission>{2}</IsEditPermission><IsDeletePermission>{3}</IsDeletePermission>", subMenu.Id, subMenu.IsAddPermission == true ? 1 : 0, subMenu.IsEditPermission == true ? 1 : 0, subMenu.IsDeletePermission == true ? 1 : 0));
                    sb.Append("</Mapping>");
                }
            }
            sb.Append("</MenuMappingModel>");
            return Convert.ToString(sb);
        }
    }
}
