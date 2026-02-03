using Common.Dapper;
using Common.Repository;
using Dapper;
using Master.Dto.Masters;
using Master.Dto.Shared;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Master.Repository.CrimeSubAct
{
    public class CrimeSubActRepository : SqlRepository, ICrimeSubAct
    {
        private readonly LogsService _logsService;
        private readonly System.Data.IDbConnection Con;
        public CrimeSubActRepository(IConfiguration configuration, LogsService logsService) : base(configuration)
        {
            _logsService = logsService;
        }

        public async Task<ResponseModel> GetCrimeSubAct(CrimeSubActFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetCrimeSubAct");
                    parmeters.Add("@CrimeActId", objModel.CrimeActId);
                    parmeters.Add("@CrimeClsId", objModel.CrimeClsId);
                    parmeters.Add("@PageNo", objModel.PageNo);
                    parmeters.Add("@Pagesize", objModel.PageSize);
                    parmeters.Add("@SortBy", objModel.SortBy == null ? "" : objModel.SortBy);
                    parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
                    var objResult = await Con.QueryMultipleAsync("spMstCrimeSubAct", parmeters, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetCrimeSubAct", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/CrimeSubAct/CrimeSubActRepository/GetCrimeSubAct");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> GetCrimeSubActDropdownList(int CrimeActId, int CrimeClsId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetCrimeSubActDropdownList");
                    parmeters.Add("@CrimeActId", CrimeActId);
                    parmeters.Add("@CrimeClsId", CrimeClsId);
                    var objData = await Con.QueryAsync<DropdownlistModel>("spMstCrimeSubAct", parmeters, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetCrimeSubActDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/CrimeSubAct/CrimeSubActRepository/GetCrimeSubActDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseModel> AddEditCrimeSubAct(AddEditCrimeSubActModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddEditCrimeSubAct");
                    parmeters.Add("@CrimeSubActId", objModel.CrimeSubActId);
                    parmeters.Add("@CrimeActId", objModel.CrimeActId);
                    parmeters.Add("@CrimeClsId", objModel.CrimeClsId);
                    parmeters.Add("@CrimeSubActNameEnglish", objModel.CrimeSubActNameEnglish);
                    parmeters.Add("@CrimeSubActNameHindi", objModel.CrimeSubActNameHindi);
                    parmeters.Add("@CrimeSubActShortName", objModel.CrimeSubActShortName);
                    parmeters.Add("@CrimeSubActDescription", objModel.CrimeSubActDescription);
                    parmeters.Add("@CreatedBy", UserId);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseModel>("spMstCrimeSubAct", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditCrimeSubAct", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/CrimeSubAct/CrimeSubActRepository/AddEditCrimeSubAct");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseModel> ActiveDeactiveCrimeSubAct(ActiveDeactiveCrimeSubActModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "ActiveDeactiveCrimeSubAct");
                    parmeters.Add("@CrimeSubActId", objModel.CrimeSubActId);
                    parmeters.Add("@IsActive", objModel.IsActive == true ? 1 : 0);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseModel>("spMstCrimeSubAct", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveCrimeSubAct", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/CrimeSubAct/CrimeSubActRepository/ActiveDeactiveCrimeSubAct");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }



    }
}
