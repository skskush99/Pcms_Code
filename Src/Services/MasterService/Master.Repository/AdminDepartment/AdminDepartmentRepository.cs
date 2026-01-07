using Common.Dapper;
using Common.Repository;
using Dapper;
using Master.Dto.Masters;
using Master.Dto.Shared;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Master.Repository.AdminDepartment
{
    public class AdminDepartmentRepository : SqlRepository, IAdminDepartment
    {
        private readonly LogsService _logsService;
        private readonly System.Data.IDbConnection Con;
        public AdminDepartmentRepository(IConfiguration configuration, LogsService logsService) : base(configuration)
        {
            _logsService = logsService;
        }
        public async Task<ResponseModel> GetAdmDep(AdminRequestFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetAdminDepartment");
                    parmeters.Add("@MajorMinor", objModel.MajorMinor == null ? "" : objModel.MajorMinor);
                    parmeters.Add("@PageNo", objModel.PageNo);
                    parmeters.Add("@Pagesize", objModel.PageSize);
                    parmeters.Add("@SortBy", objModel.SortBy == null ? "" : objModel.SortBy);
                    parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
                    var objResult = await Con.QueryMultipleAsync("spMstAdmDep", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseModel objResut = new()
                    {
                        Status = true,
                        Message = "",
                        //Data = objResult.Read<AdminDepartmentModel>(),
                        Data = objResult.Read<object>(),
                        Pagination = objResult.Read<PaginationModel>()
                    };
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetAdmDep", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/AdminDepartmentRepository/GetAdmDep");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> GetAdmDepDropdownList(int AdmDeptId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetAdminDropdownList");
                    parmeters.Add("@AdmDeptId", AdmDeptId);
                    var objData = await Con.QueryAsync<DropdownlistModel>("spMstAdmDep", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseWithoutPaginationModel objResut = new ResponseWithoutPaginationModel();
                    {
                        objResut.Status = true;
                        objResut.Message = "";
                        objResut.Data = objData;
                    }
                    ;
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetAdmDepDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/AdminDepartmentRepository/GetAdmDepDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseModel> AddEditAdmDep(AdminDepartmentModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddEditAdminDepartment");
                    parmeters.Add("@AdmDeptId", objModel.AdmDeptId);
                    parmeters.Add("@AdmDeptName", objModel.AdmDeptName);
                    parmeters.Add("@AdmDeptShortName", objModel.AdmDeptShortName);
                    parmeters.Add("@MajorMinor", objModel.MajorMinor);
                    //parmeters.Add("@Active", objModel.Active == true ? 1 : 0);
                    parmeters.Add("@CreatedBy", UserId);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseModel>("spMstAdmDep", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditAdmDep", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/AdminDepartmentRepository/AddEditAdmDep");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseModel> ActiveDeactiveAdmDep(AdminActiveDeactiveModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "ActiveDeactiveAdminDepartment");
                    parmeters.Add("@AdmDeptId", objModel.AdmDeptId);
                    parmeters.Add("@Active", objModel.Active == true ? 1 : 0);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseModel>("spMstAdmDep", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveAdmDep", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/AdminDepartmentRepository/ActiveDeactiveAdmDep");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

    }
}
