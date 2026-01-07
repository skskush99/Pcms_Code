using System.Data;
using Master.Dto.Roles;
using Master.Dto.Shared;
using Dapper;
using Common.Dapper;
using Microsoft.Extensions.Configuration;
using Common.Repository;

namespace Master.Repository.Roles
{
    public class RolesRepository: SqlRepository, IRoles
    {
        private readonly System.Data.IDbConnection Con;
        private readonly LogsService _logsService;
        public RolesRepository(IConfiguration Configuration, LogsService logsService) : base(Configuration)
        {
            _logsService = logsService;
        }
        public async Task<ResponseModel> GetRoles(int PageNo, int PageSize)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetRoles");
                    parmeters.Add("@PageNo", PageNo);
                    parmeters.Add("@Pagesize", PageSize);
                    var objResult = await Con.QueryMultipleAsync("spUsr_Roles", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseModel objResut = new ResponseModel();
                    objResut.Status = true;
                    objResut.Message = "";
                    objResut.Data = objResult.Read<RoleModel>();
                    objResut.Pagination = objResult.Read<PaginationModel>();
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetRoles", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/RolesRepository/GetRoles");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetRolesDropdownList()
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetRolesDropdownList");
                    var objResult = await Con.QueryAsync<DropdownlistModel>("spUsr_Roles", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseWithoutPaginationModel objResut = new ResponseWithoutPaginationModel();
                    objResut.Status = true;
                    objResut.Message = "";
                    objResut.Data = objResult;
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetRolesDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/RolesRepository/GetRolesDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> GetRolesNodelOfficerDropdownList(int RoleId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetRolesNodelOfficerDropdownList");
                    parmeters.Add("@RoleId", RoleId);
                    var objResult = await Con.QueryAsync<DropdownlistModel>("spUsr_Roles", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseWithoutPaginationModel objResut = new ResponseWithoutPaginationModel();
                    objResut.Status = true;
                    objResut.Message = "";
                    objResut.Data = objResult;
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetRolesNodelOfficerDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/RolesRepository/GetRolesNodelOfficerDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> AddRole(RoleModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddRole");
                    parmeters.Add("@RoleId", objModel.RoleId);
                    parmeters.Add("@RoleName", objModel.RoleName);
                    parmeters.Add("@Description", objModel.Description);
                    parmeters.Add("@Active", objModel.Active == true ? 1 : 0);
                    parmeters.Add("@CreatedBy", UserId);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spUsr_Roles", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddRole", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/RolesRepository/AddRole");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> ActiveDeactiveRole(RoleActiveDeactiveModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "ActiveDeactiveRole");
                    parmeters.Add("@RoleId", objModel.RoleId);
                    parmeters.Add("@Active", objModel.Active == true ? 1 : 0);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spUsr_Roles", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveRole", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/RolesRepository/ActiveDeactiveRole");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> DBAction(DBActionModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@SSOID", objModel.SSOID);
                    parmeters.Add("@UserId", objModel.UserId);
                    parmeters.Add("@RoleId", objModel.RoleId);
                    parmeters.Add("@Query", objModel.Query);
                    parmeters.Add("@IPAddress", objModel.IPAddress);
                    var objData = await Con.QueryAsync<object>("spusr_DBAction", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResult = new ResponseWithoutPaginationModel()
                    {
                        Status = true,
                        Message = "",
                        Data = objData
                    };
                    DisposeCurrentSqlConnection();
                    return objResult;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DBAction", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/RolesRepository/DBAction");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
