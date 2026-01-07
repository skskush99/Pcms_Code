using Common.Dapper;
using Common.Repository;
using Dapper;
using Master.Dto.Masters;
using Master.Dto.Shared;
using Microsoft.Extensions.Configuration;
using System.Data;


namespace Master.Repository.CourtTypes
{
    public class CourtTypesRepository : SqlRepository, ICourtTypes
    {
        private readonly LogsService _logsService;
        private readonly System.Data.IDbConnection Con;
        public CourtTypesRepository(IConfiguration configuration, LogsService logsService) : base(configuration)
        {
            _logsService = logsService;
        }

        public async Task<ResponseModel> GetCourtTypes(CourtTypesFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetCourtTypes");
                    parmeters.Add("@PageNo", objModel.PageNo);
                    parmeters.Add("@Pagesize", objModel.PageSize);
                    parmeters.Add("@SortBy", objModel.SortBy == null ? "" : objModel.SortBy);
                    parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
                    var objResult = await Con.QueryMultipleAsync("spMstCourtTypes", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseModel objResut = new()
                    {
                        Status = true,
                        Message = "",
                        Data = objResult.Read<CourtTypesModel>(),
                        Pagination = objResult.Read<PaginationModel>()
                    };
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCourtTypes", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/CourtTypesRepository/GetCourtTypes");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> GetCourtTypesDropdownList(int CourtTypeId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetCourtTypesDropdownList");
                    parmeters.Add("@CourtTypeId", CourtTypeId);
                    var objData = await Con.QueryAsync<DropdownlistModel>("spMstCourtTypes", parmeters, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetCourtTypesDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/CourtTypesRepository/GetCourtTypesDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseModel> AddEditCourtTypes(CourtTypesModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddEditCourtTypes");
                    parmeters.Add("@CourtTypeId", objModel.CourtTypeId);
                    parmeters.Add("@CourtTypeName", objModel.CourtTypeName);
                    parmeters.Add("@CourtTypeShortName", objModel.CourtTypeShortName);
                    parmeters.Add("@OrderNo", objModel.OrderNo);
                    parmeters.Add("@Active", objModel.Active == true ? 1 : 0);
                    parmeters.Add("@CreatedBy", UserId);
                    parmeters.Add("@UpdatedBy", UserId);
                    parmeters.Add("@DeleteBy", UserId);
                    var objData = await Con.QueryAsync<ResponseModel>("spMstCourtTypes", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditCourtTypes", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/CourtTypesRepository/AddEditCourtTypes");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseModel> ActiveDeactiveCourtTypes(CourtTypesActiveDeactiveModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "ActiveDeactiveCourtTypes");
                    parmeters.Add("@CourtTypeId", objModel.CourtTypeId);
                    parmeters.Add("@Active", objModel.Active);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseModel>("spMstCourtTypes", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveCourtTypes", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/CourtTypesRepository/ActiveDeactiveCourtTypes");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


    }
}
