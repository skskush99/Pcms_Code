using Common.Dapper;
using Common.Repository;
using Dapper;
using Master.Dto.Masters;
using Master.Dto.Shared;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Master.Repository.CourtPlaces
{
    public class CourtPlacesRepository : SqlRepository, ICourtPlaces
    {
        private readonly LogsService _logsService;
        private readonly System.Data.IDbConnection Con;
        public CourtPlacesRepository(IConfiguration configuration, LogsService logsService) : base(configuration)
        {
            _logsService = logsService;
        }
        public async Task<ResponseModel> GetCourtPlaces(CourtPlacesFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetCourtPlaces");
                    parmeters.Add("@StateId", objModel.StateId);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@TehsilId", objModel.TehsilId);
                    parmeters.Add("@PageNo", objModel.PageNo);
                    parmeters.Add("@Pagesize", objModel.PageSize);
                    parmeters.Add("@SortBy", objModel.SortBy == null ? "" : objModel.SortBy);
                    parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
                    var objResult = await Con.QueryMultipleAsync("spMstCourtPlaces", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseModel objResut = new()
                    {
                        Status = true,
                        Message = "",
                        Data = objResult.Read<CourtPlacesModel>(),
                        Pagination = objResult.Read<PaginationModel>()
                    };
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCourtPlaces", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/CourtPlacesRepository/GetCourtPlaces");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> GetCourtPlacesDropdownList(int CourtTypeId, int TehsilId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetCourtPlacesDropdownList");
                    parmeters.Add("@CourtTypeId", CourtTypeId);
                    parmeters.Add("@TehsilId", TehsilId);
                    var objData = await Con.QueryAsync<DropdownlistModel>("spMstCourtPlaces", parmeters, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetCourtPlacesDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/CourtPlacesRepository/GetCourtPlacesDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseModel> AddEditCourtPlaces(CourtPlacesModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddEditCourtPlaces");
                    parmeters.Add("@PlaceId", objModel.PlaceId);
                    parmeters.Add("@PlaceName", objModel.PlaceName);
                    parmeters.Add("@TehsilId", objModel.TehsilId);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@Active", objModel.Active == true ? 1 : 0);
                    parmeters.Add("@CreatedBy", UserId);
                    parmeters.Add("@UpdatedBy", UserId);
                    parmeters.Add("@DeleteBy", UserId);
                    var objData = await Con.QueryAsync<ResponseModel>("spMstCourtPlaces", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditCourtPlaces", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/CourtPlacesRepository/AddEditCourtPlaces");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseModel> ActiveDeactiveCourtPlaces(CourtPlacesActiveDeactiveModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "ActiveDeactiveCourtPlaces");
                    parmeters.Add("@PlaceId", objModel.PlaceId);
                    parmeters.Add("@Active", objModel.Active);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseModel>("spMstCourtPlaces", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveCourtPlaces", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/CourtPlacesRepository/ActiveDeactiveCourtPlaces");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


    }
}
