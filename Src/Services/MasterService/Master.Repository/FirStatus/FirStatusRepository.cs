using Common.Dapper;
using Common.Repository;
using Dapper;
using Master.Dto.Masters;
using Master.Dto.Shared;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Master.Repository.FirStatus
{
    public class FirStatusRepository : SqlRepository, IFirStatus
    {
        private readonly LogsService _logsService;
        private readonly System.Data.IDbConnection Con;
        public FirStatusRepository(IConfiguration configuration, LogsService logsService) : base(configuration)
        {
            _logsService = logsService;
        }

        public async Task<ResponseModel> GetFirStatus(FIRStatusFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetFirStatus");
                    parmeters.Add("@PageNo", objModel.PageNo);
                    parmeters.Add("@Pagesize", objModel.PageSize);
                    parmeters.Add("@SortBy", objModel.SortBy == null ? "" : objModel.SortBy);
                    parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
                    var objResult = await Con.QueryMultipleAsync("spMstFirStatus", parmeters, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetFirStatus", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/FirStatus/FirStatusRepository/GetFirStatus");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> GetFirStatusDropdownList()
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetFirStatusDropdownList");
                    var objData = await Con.QueryAsync<DropdownlistModel>("spMstFirStatus", parmeters, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetFirStatusDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/FirStatus/FirStatusRepository/GetFirStatusDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseModel> AddEditFirStatus(AddEditFIRStatusModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddEditFirStatus");
                    parmeters.Add("@FirStatusId", objModel.FirStatusId);
                    parmeters.Add("@FirStatusNameEnglish", objModel.FirStatusNameEnglish);
                    parmeters.Add("@FirStatusNameHindi", objModel.FirStatusNameHindi);
                    parmeters.Add("@CreatedBy", UserId);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseModel>("spMstFirStatus", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditFirStatus", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/FirStatus/FirStatusRepository/AddEditFirStatus");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseModel> ActiveDeactiveFirStatus(ActiveDeactiveFIRStatusModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "ActiveDeactiveFirStatus");
                    parmeters.Add("@FirStatusId", objModel.FirStatusId);
                    parmeters.Add("@IsActive", objModel.IsActive == true ? 1 : 0);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseModel>("spMstFirStatus", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveFirStatus", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/FirStatus/FirStatusRepository/ActiveDeactiveFirStatus");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }







    }
}
