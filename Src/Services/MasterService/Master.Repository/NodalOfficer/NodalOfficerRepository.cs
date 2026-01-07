using Common.Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using Master.Dto.Masters;
using Master.Dto.Shared;
using Dapper;
using Master.Repository.NodalOfficer;
using Common.Repository;

namespace PcmsMasterMicroServices.Repository
{
    public class NodalOfficerRepository : SqlRepository, INodalOfficer
    {
        private readonly LogsService _logsService;
        private readonly System.Data.IDbConnection Con;
        public NodalOfficerRepository(IConfiguration Configuration, LogsService logsService) : base(Configuration)
        {
            _logsService = logsService;
        }
        public async Task<ResponseModel> GetNodalOfficer(NodalOfficerFilterModel objModel, int LoginRoleId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetNodalOfficer");
                    parmeters.Add("@AdmDeptId", objModel.AdmDeptId);
                    parmeters.Add("@UnitId", objModel.UnitId);
                    parmeters.Add("@Role", objModel.Role);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@PageNo", objModel.PageNo);
                    parmeters.Add("@Pagesize", objModel.PageSize);
                    parmeters.Add("@SortBy", objModel.SortBy == null ? "" : objModel.SortBy);
                    parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
                    parmeters.Add("@LoginRoleId", LoginRoleId);
                    var objResult = await Con.QueryMultipleAsync("spMstNodalOfficer", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseModel objResut = new()
                    {
                        Status = true,
                        Message = "",
                        //Data = objResult.Read<NodalOfficerModel>(),
                        Data = objResult.Read<object>(),
                        Pagination = objResult.Read<PaginationModel>()
                    };
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetNodalOfficer", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/NodalOfficerRepository/GetNodalOfficer");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> GetNodalOfficerDropdownList()
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetNodalOfficerList");
                    var objData = await Con.QueryAsync<DropdownlistModel>("spMstNodalOfficer", parmeters, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetNodalOfficerDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/NodalOfficerRepository/GetNodalOfficerDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> AddEditNodalOfficer(NodalOfficerModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    if (objModel.LicID > 0)
                    {
                        parmeters.Add("@Action", "EditNodalOfficer");
                        parmeters.Add("@UpdatedBy", objModel.UpdatedBy);
                        parmeters.Add("@LicID", objModel.LicID);
                    }
                    else
                    {
                        parmeters.Add("@Action", "AddNodalOfficer");
                        parmeters.Add("@CreatedBy", objModel.CreatedBy);
                    }
                    parmeters.Add("@AdmDeptId", objModel.AdmDeptId);
                    parmeters.Add("@UnitId", objModel.UnitId);
                    parmeters.Add("@Level", objModel.Level);
                    parmeters.Add("@Role", objModel.Role);
                    parmeters.Add("@Name", objModel.Name);
                    parmeters.Add("@Designation", objModel.Designation);
                    parmeters.Add("@Address1", objModel.Address1);
                    parmeters.Add("@Address2", objModel.Address2);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@City", objModel.City);
                    parmeters.Add("@Mobile", objModel.Mobile);
                    parmeters.Add("@Fax", objModel.Fax);
                    parmeters.Add("@Email", objModel.Email);
                    parmeters.Add("@FromDate", objModel.FromDate);
                    parmeters.Add("@ToDate", objModel.ToDate);
                    parmeters.Add("@Active", objModel.Active == true ? 1 : 0);
                    var objData = await Con.QueryMultipleAsync("spMstNodalOfficer", parmeters, commandType: CommandType.StoredProcedure);
                    var objResult = objData.Read<ResponseWithoutPaginationModel>().FirstOrDefault();
                    objResult.Data = objData.Read<object>();
                    DisposeCurrentSqlConnection();
                    
                    return objResult;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditNodalOfficer", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/NodalOfficerRepository/AddEditNodalOfficer");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> ActiveDeactiveNodalOfficer(NodalOfficerActiveDeactiveModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "DeactiveNodalOfficer");
                    parmeters.Add("@LicID", objModel.LicID);
                    parmeters.Add("@Active", objModel.Active);
                    parmeters.Add("@UpdatedBy", UserId);
                    parmeters.Add("@DeleteBy", UserId);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spMstNodalOfficer", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveNodalOfficer", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/NodalOfficerRepository/ActiveDeactiveNodalOfficer");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
