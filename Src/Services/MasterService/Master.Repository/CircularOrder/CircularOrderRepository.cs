using Common.Dapper;
using Common.Repository;
using Dapper;
using Master.Dto.Masters;
using Master.Dto.Shared;
using Master.Repository.CircularOrder;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Master.Repository.Documents
{
    public class CircularOrderRepository : SqlRepository, ICircularOrder
    {
        private readonly LogsService _logsService;
        private readonly System.Data.IDbConnection Con;
        public CircularOrderRepository(IConfiguration configuration, LogsService logsService) : base(configuration)
        {
            _logsService = logsService;
        }
        public async Task<ResponseModel> GetCircularOrders(CircularOrderFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetDocuments");
                    parmeters.Add("@PageNo", objModel.PageNo);
                    parmeters.Add("@Pagesize", objModel.PageSize);
                    parmeters.Add("@SortBy", objModel.SortBy == null ? "" : objModel.SortBy);
                    parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
                    var objResult = await Con.QueryMultipleAsync("spDocuments", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseModel objResut = new()
                    {
                        Status = true,
                        Message = "",
                        //Data = objResult.Read<CircularOrderModel>(),
                        Data = objResult.Read<object>(),
                        Pagination = objResult.Read<PaginationModel>()
                    };
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCircularOrders", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/CircularOrderRepository/GetCircularOrders");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> AddEditCircularOrder(CircularOrderModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddEditDocuments");
                    parmeters.Add("@Id", objModel.Id);
                    parmeters.Add("@Title", objModel.Title);
                    parmeters.Add("@FilePath", objModel.FilePath);
                    //parmeters.Add("@Active", objModel.Active);
                    //parmeters.Add("@Active", objModel.Active == true ? 1 : 0);
                    parmeters.Add("@UploadedBy", UserId);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spDocuments", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditCircularOrder", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/CircularOrderRepository/AddEditCircularOrder");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseModel> ActiveDeactiveCircularOrder(CircularOrderActiveDeactiveModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "ActiveDeactiveDocuments");
                    parmeters.Add("@Id", objModel.Id);
                    parmeters.Add("@Active", objModel.Active);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseModel>("spDocuments", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveCircularOrder", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/CircularOrderRepository/ActiveDeactiveCircularOrder");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

    }
}
