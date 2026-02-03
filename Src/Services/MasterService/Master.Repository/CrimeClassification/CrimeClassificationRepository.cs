using Common.Dapper;
using Common.Repository;
using Dapper;
using Master.Dto.Masters;
using Master.Dto.Shared;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Master.Repository.CrimeClassification
{
    public class CrimeClassificationRepository : SqlRepository, ICrimeClassification
    {
        private readonly LogsService _logsService;
        private readonly System.Data.IDbConnection Con;
        public CrimeClassificationRepository(IConfiguration configuration, LogsService logsService) : base(configuration)
        {
            _logsService = logsService;
        }

        public async Task<ResponseModel> GetCrimeClassification(CrimeClassificationFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetCrimeClassification");
                    parmeters.Add("@PageNo", objModel.PageNo);
                    parmeters.Add("@Pagesize", objModel.PageSize);
                    parmeters.Add("@SortBy", objModel.SortBy == null ? "" : objModel.SortBy);
                    parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
                    var objResult = await Con.QueryMultipleAsync("spMstCrimeClassification", parmeters, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetCrimeClassification", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/CrimeClassificationRepository/GetCrimeClassification");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> GetCrimeClassificationDropdownList()
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetCrimeClassificationDropdownList");
                    var objData = await Con.QueryAsync<DropdownlistModel>("spMstCrimeClassification", parmeters, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetCrimeClassificationDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/CrimeClassificationRepository/GetCrimeClassificationDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseModel> AddEditCrimeClassification(AddEditCrimeClassificationModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddEditCrimeClassification");
                    parmeters.Add("@CrimeClsId", objModel.CrimeClsId);
                    parmeters.Add("@CrimeClsNameEnglish", objModel.CrimeClsNameEnglish);
                    parmeters.Add("@CrimeClsNameHindi", objModel.CrimeClsNameHindi);
                    parmeters.Add("@CreatedBy", UserId);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseModel>("spMstCrimeClassification", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditCrimeClassification", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/CrimeClassificationRepository/AddEditCrimeClassification");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseModel> ActiveDeactiveCrimeClassification(ActiveDeactiveCrimeClassificationModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "ActiveDeactiveCrimeClassification");
                    parmeters.Add("@CrimeClsId", objModel.CrimeClsId);
                    parmeters.Add("@IsActive", objModel.IsActive == true ? 1 : 0);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseModel>("spMstCrimeClassification", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveCrimeClassification", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/CrimeClassificationRepository/ActiveDeactiveCrimeClassification");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }





    }
}
