using Common.Dapper;
using Common.Repository;
using Dapper;
using Master.Dto.Masters;
using Master.Dto.Shared;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Master.Repository.News
{
    public class NewsRepository : SqlRepository, INews
    {
        private readonly LogsService _logsService;
        private readonly System.Data.IDbConnection Con;
        public NewsRepository(IConfiguration configuration, LogsService logsService) : base(configuration)
        {
            _logsService = logsService;
        }

        public async Task<ResponseModel> GetNews(NewsFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetNews");
                    parmeters.Add("@PageNo", objModel.PageNo);
                    parmeters.Add("@Pagesize", objModel.PageSize);
                    parmeters.Add("@SortBy", objModel.SortBy == null ? "" : objModel.SortBy);
                    parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
                    var objResult = await Con.QueryMultipleAsync("spMstNews", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseModel objResut = new()
                    {
                        Status = true,
                        Message = "",
                        //Data = objResult.Read<NewsModel>(),
                        Data = objResult.Read<object>(),
                        Pagination = objResult.Read<PaginationModel>()
                    };
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetNews", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/NewsRepository/GetNews");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetNewsDropdownList()
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "NewsDropdownList");
                    var objData = await Con.QueryAsync<DropdownlistModel>("spMstNews", parmeters, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetNewsDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/NewsRepository/GetNewsDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseModel> AddEditNews(NewsModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddEditNews");
                    parmeters.Add("@NewsId", objModel.NewsId);
                    parmeters.Add("@Title", objModel.Title);
                    parmeters.Add("@Description", objModel.Description);
                    parmeters.Add("@FromDate", objModel.FromDate);
                    parmeters.Add("@ToDate", objModel.ToDate);
                    parmeters.Add("@Active", objModel.Active == true ? 1 : 0);
                    parmeters.Add("@CreatedBy", UserId);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseModel>("spMstNews", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditNews", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/NewsRepository/AddEditNews");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseModel> ActiveDeactiveNews(NewsActiveDeactiveModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "ActiveDeactiveNews");
                    parmeters.Add("@NewsId", objModel.NewsId);
                    parmeters.Add("@Active", objModel.Active);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseModel>("spMstNews", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveNews", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/NewsRepository/ActiveDeactiveNews");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


    }
}
