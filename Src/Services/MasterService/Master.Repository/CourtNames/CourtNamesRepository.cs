using Common.Dapper;
using Common.Repository;
using Dapper;
using Master.Dto.Masters;
using Master.Dto.Shared;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Master.Repository.CourtName
{
    public class CourtNamesRepository : SqlRepository, ICourtNames
    {
        private readonly LogsService _logsService;
        private readonly System.Data.IDbConnection Con;
        public CourtNamesRepository(IConfiguration configuration, LogsService logsService) : base(configuration)
        {
            _logsService = logsService;
        }
        public async Task<ResponseModel> GetCourtNames(CourtNamesFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {

                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetCourtNames");
                    parmeters.Add("@JCourtId", objModel.JCourtId);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@DivisionId", objModel.DivisionId);
                    parmeters.Add("@PageNo", objModel.PageNo);
                    parmeters.Add("@Pagesize", objModel.PageSize);
                    parmeters.Add("@SortBy", objModel.SortBy == null ? "" : objModel.SortBy);
                    parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
                    var objResult = await Con.QueryMultipleAsync("spMstCourtNames", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseModel objResut = new()
                    {
                        Status = true,
                        Message = "",
                        //Data = objResult.Read<CourtNamesListModel>(),
                        Data = objResult.Read<object> (),
                        Pagination = objResult.Read<PaginationModel>()
                    };
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCourtNames", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/CourtNamesRepository/GetCourtNames");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> GetCourtNamesDropdownList(int JCourtId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetCourtNamesDropdownList");
                    parmeters.Add("@JCourtId", JCourtId);
                    var objData = await Con.QueryAsync<DropdownlistModel>("spMstCourtNames", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseWithoutPaginationModel objResut = new ResponseWithoutPaginationModel();
                    {
                        objResut.Status = true;
                        objResut.Message = "";
                        objResut.Data = objData;
                    }
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCourtNamesDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/CourtNamesRepository/GetCourtNamesDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseModel> AddEditCourtNames(CourtNamesModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddEditCourtNames");
                    parmeters.Add("@JCourtId", objModel.JCourtId);
                    parmeters.Add("@JCourtCode", objModel.JCourtCode);
                    parmeters.Add("@JCourtEng", objModel.JCourtEng);
                    parmeters.Add("@JCourtHindi", objModel.JCourtHindi);
                    parmeters.Add("@DivisionId", objModel.DivisionId);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@OfficeId", objModel.OfficeId);
                    parmeters.Add("@IsActive", objModel.IsActive == true ? 1 : 0);
                    parmeters.Add("@CreatedBy", UserId);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseModel>("spMstCourtNames", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditCourtNames", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/CourtNamesRepository/AddEditCourtNames");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseModel> ActiveDeactiveCourtNames(CourtNamesActiveDeactiveModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "ActiveDeactiveCourtNames");
                    parmeters.Add("@JCourtId", objModel.JCourtId);
                    parmeters.Add("@IsActive", objModel.IsActive == true ? 1 : 0);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseModel>("spMstCourtNames", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveCourtNames", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/CourtNamesRepository/ActiveDeactiveCourtNames");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }



    }
}
