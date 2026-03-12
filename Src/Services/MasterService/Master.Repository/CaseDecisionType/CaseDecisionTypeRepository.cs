using Common.Dapper;
using Common.Repository;
using Dapper;
using Master.Dto.Masters;
using Master.Dto.Shared;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Master.Repository.CaseDecisionType
{
    public class CaseDecisionTypeRepository : SqlRepository, ICaseDecisionType
    {
        private readonly LogsService _logsService;
        private readonly System.Data.IDbConnection Con;
        public CaseDecisionTypeRepository(IConfiguration configuration, LogsService logsService) : base(configuration)
        {
            _logsService = logsService;
        }

        public async Task<ResponseModel> GetCaseDecisionType(CaseDecisionTypeFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetDecisionType");
                    parmeters.Add("@PageNo", objModel.PageNo);
                    parmeters.Add("@Pagesize", objModel.PageSize);
                    parmeters.Add("@SortBy", objModel.SortBy == null ? "" : objModel.SortBy);
                    parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
                    var objResult = await Con.QueryMultipleAsync("spMstCaseDecisionType", parmeters, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetCaseDecisionType", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/CaseDecisionType/CaseDecisionTypeRepository/GetCaseDecisionType");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetCaseDecisionTypeDropdownList()
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetDecisionTypeDropdownList");
                    var objData = await Con.QueryAsync<DropdownlistModel>("spMstCaseDecisionType", parmeters, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetCaseDecisionTypeDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/CaseDecisionType/CaseDecisionTypeRepository/GetCaseDecisionTypeDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        

        public async Task<ResponseModel> AddEditCaseDecisionType(AddEditCaseDecisionTypeModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddEditDecisionType");
                    parmeters.Add("@DecisionTypeId", objModel.DecisionTypeId);
                    parmeters.Add("@DecisionTypeEnglish", objModel.DecisionTypeEnglish);
                    parmeters.Add("@DecisionTypeHindi", objModel.DecisionTypeHindi);
                    parmeters.Add("@CreatedBy", UserId);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseModel>("spMstCaseDecisionType", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditCaseDecisionType", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/CaseDecisionType/CaseDecisionTypeRepository/AddEditCaseDecisionType");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseModel> ActiveDeactiveCaseDecisionType(ActiveDeactiveCaseDecisionTypeModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "ActiveDeactiveDecisionType");
                    parmeters.Add("@DecisionTypeId", objModel.DecisionTypeId);
                    parmeters.Add("@IsActive", objModel.IsActive == true ? 1 : 0);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseModel>("spMstCaseDecisionType", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveCaseDecisionType", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/CaseDecisionType/CaseDecisionTypeRepository/ActiveDeactiveCaseDecisionType");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
