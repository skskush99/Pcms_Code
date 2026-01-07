using Common.Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using Master.Dto.Masters;
using Master.Dto.Shared;
using Dapper;
using Master.Repository.UnitsDepartment;
using Common.Repository;

namespace PcmsMasterMicroServices.Repository
{
    public class UnitsDepartmentRepository : SqlRepository, IUnitsDepartment
    {
        private readonly LogsService _logsService;
        private readonly System.Data.IDbConnection Con;
        public UnitsDepartmentRepository(IConfiguration Configuration, LogsService logsService) : base(Configuration)
        {
            _logsService = logsService;
        }
        public async Task<ResponseModel> GetUnitDepartment(UnitsDepartmentFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetUnitDepartment");
                    parmeters.Add("@ActiveFilter", objModel.ActiveFilter);
                    parmeters.Add("@AdmDeptId", objModel.AdmDeptId);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@PageNo", objModel.PageNo);
                    parmeters.Add("@Pagesize", objModel.PageSize);
                    parmeters.Add("@SortBy", objModel.SortBy == null ? "" : objModel.SortBy);
                    parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
                    var objResult = await Con.QueryMultipleAsync("spMstUnitDepartment", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseModel objResut = new()
                    {
                        Status = true,
                        Message = "",
                        //Data = objResult.Read<UnitsDepartmentModel>(),
                        Data = objResult.Read<object>(),
                        Pagination = objResult.Read<PaginationModel>()
                    };
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetUnitDepartment", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/UnitsDepartmentRepository/GetUnitDepartment");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseModel> GetUnitDepartmentRajMaster(UnitsDepartmentFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetUnitDepartmentRajMaster");
                    parmeters.Add("@ActiveFilter", objModel.ActiveFilter);
                    parmeters.Add("@AdmDeptId", objModel.AdmDeptId);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@PageNo", objModel.PageNo);
                    parmeters.Add("@Pagesize", objModel.PageSize);
                    parmeters.Add("@SortBy", objModel.SortBy == null ? "" : objModel.SortBy);
                    parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
                    var objResult = await Con.QueryMultipleAsync("spMstUnitDepartment", parmeters, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetUnitDepartmentRajMaster", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/UnitsDepartmentRepository/GetUnitDepartmentRajMaster");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> GetUnitDepartmentDropdownList(int AdmDptID, int UnitId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetUnitDepartmentDropdownList");
                    parmeters.Add("@AdmDeptId", AdmDptID);
                    parmeters.Add("@UnitId", UnitId);
                    var objData = await Con.QueryAsync<DropdownlistModel>("spMstUnitDepartment", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseWithoutPaginationModel objResut = new ResponseWithoutPaginationModel();
                    {
                        objResut.Status = true;
                        objResut.Message = "";
                        objResut.Data = objData;
                    };
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetUnitDepartmentDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/UnitsDepartmentRepository/GetUnitDepartmentDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> GetUnitDepartmentRajMasterDropdownList(int AdmDptID, int UnitId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetUnitDepartmentRajMasterDropdownList");
                    parmeters.Add("@AdmDeptId", AdmDptID);
                    parmeters.Add("@UnitId", UnitId);
                    var objData = await Con.QueryAsync<DropdownlistModel>("spMstUnitDepartment", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseWithoutPaginationModel objResut = new ResponseWithoutPaginationModel();
                    {
                        objResut.Status = true;
                        objResut.Message = "";
                        objResut.Data = objData;
                    };
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetUnitDepartmentRajMasterDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/UnitsDepartmentRepository/GetUnitDepartmentRajMasterDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> GetDepartmentWiseUnitDropdownList(int AdmDptID)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetDepartmentWiseUnitDropdownList");
                    parmeters.Add("@AdmDeptId", AdmDptID);
                    var objData = await Con.QueryAsync<DropdownlistModel>("spMstUnitDepartment", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseWithoutPaginationModel objResut = new ResponseWithoutPaginationModel();
                    {
                        objResut.Status = true;
                        objResut.Message = "";
                        objResut.Data = objData;
                    };
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDepartmentWiseUnitDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/UnitsDepartmentRepository/GetDepartmentWiseUnitDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> GetDepartmentWiseUnitRajMasterDropdownList(int AdmDptID)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetDepartmentWiseUnitRajMasterDropdownList");
                    parmeters.Add("@AdmDeptId", AdmDptID);
                    var objData = await Con.QueryAsync<DropdownlistModel>("spMstUnitDepartment", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseWithoutPaginationModel objResut = new ResponseWithoutPaginationModel();
                    {
                        objResut.Status = true;
                        objResut.Message = "";
                        objResut.Data = objData;
                    };
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDepartmentWiseUnitRajMasterDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/UnitsDepartmentRepository/GetDepartmentWiseUnitRajMasterDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> AddEditUnitDepartment(UnitsDepartmentModel objModel, int UnitId, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddEditUnitDepartment");
                    parmeters.Add("@UnitId", objModel.UnitId);
                    parmeters.Add("@UnitName", objModel.UnitName);
                    parmeters.Add("@UnitShortName", objModel.UnitShortName);
                    parmeters.Add("@AdmDeptId", objModel.AdmDeptId);
                    parmeters.Add("@NicUnitId", objModel.NicUnitId);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@Active", objModel.Active == true ? 1 : 0);
                    parmeters.Add("@CreatedBy", UserId);
                    parmeters.Add("@UpdatedBy", UserId);
                    parmeters.Add("@DeleteBy", UserId);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spMstUnitDepartment", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditUnitDepartment", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/UnitsDepartmentRepository/AddEditUnitDepartment");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> ActiveDeactiveUnitDepartment(UnitsDepartmentActiveDeactiveModel objModel, int UnitId, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "ActiveDeactiveUnitDepartment");
                    parmeters.Add("@UnitId", objModel.UnitId);
                    parmeters.Add("@Active", objModel.Active);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spMstUnitDepartment", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveUnitDepartment", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/UnitsDepartmentRepository/ActiveDeactiveUnitDepartment");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> ActiveDeactiveUnitDepartmentRajMaster(UnitsDepartmentActiveDeactiveModel objModel, int UnitId, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "ActiveDeactiveUnitDepartmentRajMaster");
                    parmeters.Add("@UnitId", objModel.UnitId);
                    parmeters.Add("@Active", objModel.Active);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spMstUnitDepartment", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveUnitDepartmentRajMaster", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/UnitsDepartmentRepository/ActiveDeactiveUnitDepartmentRajMaster");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

    }
}
