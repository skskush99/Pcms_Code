using Common.Dapper;
using Common.Repository;
using Dapper;
using Master.Dto.Masters;
using Master.Dto.Shared;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Master.Repository.JanPratinidhi
{
    public class JanPratinidhiRepository : SqlRepository, IJanPratinidhi
    {
        private readonly LogsService _logsService;
        private readonly System.Data.IDbConnection Con;
        public JanPratinidhiRepository(IConfiguration configuration, LogsService logsService) : base(configuration)
        {
            _logsService = logsService;
        }
        public async Task<ResponseModel> GetJanPratinidhi(JanPratinidhiFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetJanPratinidhi");
                    parmeters.Add("@PostId", objModel.PostId == null ? "" : objModel.PostId);
                    parmeters.Add("@PageNo", objModel.PageNo);
                    parmeters.Add("@Pagesize", objModel.PageSize);
                    parmeters.Add("@SortBy", objModel.SortBy == null ? "" : objModel.SortBy);
                    parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
                    var objResult = await Con.QueryMultipleAsync("spMstJanPratinidhi", parmeters, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetJanPratinidhi", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/JanPratinidhiRepository/GetJanPratinidhi");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> GetJanPratinidhiDropdownList()
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetJanPratinidhiDropdownList");
                    var objData = await Con.QueryAsync<DropdownlistModel>("spMstJanPratinidhi", parmeters, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetJanPratinidhiDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/JanPratinidhiRepository/GetJanPratinidhiDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseModel> AddEditJanPratinidhi(JanPratinidhiModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddEditJanPratinidhi");
                    parmeters.Add("@PostId", objModel.PostId);
                    parmeters.Add("@PostNameEnglish", objModel.PostNameEnglish);
                    parmeters.Add("@PostNameHindi", objModel.PostNameHindi);
                    parmeters.Add("@PostShortForm", objModel.PostShortForm);
                    parmeters.Add("@CreatedBy", UserId);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseModel>("spMstJanPratinidhi", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditJanPratinidhi", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/JanPratinidhiRepository/AddEditJanPratinidhi");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseModel> ActiveDeactiveJanPratinidhi(JanPratinidhiActiveDeactiveModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "ActiveDeactiveJanPratinidhi");
                    parmeters.Add("@PostId", objModel.PostId);
                    parmeters.Add("@IsActive", objModel.IsActive == true ? 1 : 0);
                    parmeters.Add("@UpdatedBy", UserId);
                    parmeters.Add("@DeletedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseModel>("spMstJanPratinidhi", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveJanPratinidhi", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/JanPratinidhiRepository/ActiveDeactiveJanPratinidhi");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }




    }
}
