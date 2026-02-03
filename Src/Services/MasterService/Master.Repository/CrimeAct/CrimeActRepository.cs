using Common.Dapper;
using Common.Repository;
using Dapper;
using Master.Dto.Masters;
using Master.Dto.Shared;
using Microsoft.Extensions.Configuration;
using System.Data;


namespace Master.Repository.CrimeAct
{
    public class CrimeActRepository : SqlRepository, ICrimeAct
    {
        private readonly LogsService _logsService;
        private readonly System.Data.IDbConnection Con;
        public CrimeActRepository(IConfiguration configuration, LogsService logsService) : base(configuration)
        {
            _logsService = logsService;
        }

        public async Task<ResponseModel> GetCrimeAct(CrimeActFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetCrimeAct");
                    parmeters.Add("@CrimeClsId", objModel.CrimeClsId);
                    parmeters.Add("@PageNo", objModel.PageNo);
                    parmeters.Add("@Pagesize", objModel.PageSize);
                    parmeters.Add("@SortBy", objModel.SortBy == null ? "" : objModel.SortBy);
                    parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
                    var objResult = await Con.QueryMultipleAsync("spMstCrimeAct", parmeters, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetCrimeAct", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/CrimeAct/CrimeActRepository/GetCrimeAct");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> GetCrimeActDropdownList(int CrimeClsId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetCrimeActDropdownList");
                    parmeters.Add("@CrimeClsId", CrimeClsId);
                    var objData = await Con.QueryAsync<DropdownlistModel>("spMstCrimeAct", parmeters, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetCrimeActDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/CrimeAct/CrimeActRepository/GetCrimeActDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseModel> AddEditCrimeAct(AddEditCrimeActModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddEditCrimeAct");
                    parmeters.Add("@CrimeActId", objModel.CrimeActId);
                    parmeters.Add("@CrimeClsId", objModel.CrimeClsId);
                    parmeters.Add("@CrimeActNameEnglish", objModel.CrimeActNameEnglish);
                    parmeters.Add("@CrimeActNameHindi", objModel.CrimeActNameHindi);
                    parmeters.Add("@CrimeActShortName", objModel.CrimeActShortName);
                    parmeters.Add("@CrimeActDescription", objModel.CrimeActDescription);
                    parmeters.Add("@CreatedBy", UserId);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseModel>("spMstCrimeAct", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditCrimeAct", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/CrimeAct/CrimeActRepository/AddEditCrimeAct");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseModel> ActiveDeactiveCrimeAct(ActiveDeactiveCrimeActModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "ActiveDeactiveCrimeAct");
                    parmeters.Add("@CrimeActId", objModel.CrimeActId);
                    parmeters.Add("@IsActive", objModel.IsActive == true ? 1 : 0);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseModel>("spMstCrimeAct", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveCrimeAct", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/CrimeAct/CrimeActRepository/ActiveDeactiveCrimeAct");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


    }

}
