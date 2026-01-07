using Common.Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using Master.Dto.Masters;
using Master.Dto.Shared;
using Dapper;
using Master.Repository.Offices;
using Common.Repository;

namespace PcmsMasterMicroServices.Repository
{
    public class OfficesRepository : SqlRepository, IOffices
    {
        private readonly System.Data.IDbConnection Con;
        private readonly LogsService _logsService;
        public OfficesRepository(IConfiguration Configuration, LogsService logsService) : base(Configuration)
        {
            _logsService = logsService;
        }
        public async Task<ResponseModel> GetOffices(OfficesFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetOffices");
                    parmeters.Add("@OfficeId", objModel.OfficeId);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@IsActive", objModel.IsActive);
                    parmeters.Add("@PageNo", objModel.PageNo);
                    parmeters.Add("@PageSize", objModel.PageSize);
                    parmeters.Add("@SortBy", objModel.SortBy == null ? "" : objModel.SortBy);
                    parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
                    var objResult = await Con.QueryMultipleAsync("spMstOffices", parmeters, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetOffices", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/OfficesRepository/GetOffices");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> GetOfficesDropdownList(int OfficeId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetOfficesDropdownList");
                    parmeters.Add("@OfficeId", OfficeId);
                    var objData = await Con.QueryAsync<DropdownlistModel>("spMstOffices", parmeters, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetOfficesDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/OfficesRepository/GetOfficesDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseModel> AddEditOffices(OfficesModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddEditOffices");
                    parmeters.Add("@OfficeId", objModel.OfficeId);
                    parmeters.Add("@OfficeEng", objModel.OfficeEng);
                    parmeters.Add("@OfficeHindi", objModel.OfficeHindi);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@IsActive", objModel.IsActive);
                    parmeters.Add("@CreatedBy", UserId);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseModel>("spMstOffices", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditOffices", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/OfficesRepository/AddEditOffices");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseModel> ActiveDeactiveOffices(OfficesActiveDeactiveModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "ActiveDeactiveOffices");
                    parmeters.Add("@OfficeId", objModel.OfficeId);
                    parmeters.Add("@IsActive", objModel.IsActive);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseModel>("spMstOffices", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveOffices", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/OfficesRepository/ActiveDeactiveOffices");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
