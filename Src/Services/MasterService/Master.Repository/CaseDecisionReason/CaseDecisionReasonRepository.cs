using Common.Dapper;
using Common.Repository;
using Dapper;
using Master.Dto.Masters;
using Master.Dto.Shared;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Repository.CaseDecisionReason
{
    public class CaseDecisionReasonRepository : SqlRepository , ICaseDecisionReason
    {
        private readonly LogsService _logsService;
        private readonly System.Data.IDbConnection Con;
        public CaseDecisionReasonRepository(IConfiguration configuration, LogsService logsService) : base(configuration)
        {
            _logsService = logsService;
        }

        public async Task<ResponseModel> GetDecisionReason(CaseDecisionReasonFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetDecisionReason");
                    parmeters.Add("@DecisionTypeId", objModel.DecisionTypeId);
                    parmeters.Add("@PageNo", objModel.PageNo);
                    parmeters.Add("@Pagesize", objModel.PageSize);
                    parmeters.Add("@SortBy", objModel.SortBy == null ? "" : objModel.SortBy);
                    parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
                    var objResult = await Con.QueryMultipleAsync("spMstCaseDecisionReason", parmeters, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetDecisionReason", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/CaseDecisionReason/CaseDecisionReasonRepository/GetDecisionReason");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetDecisionReasonDropdownList(int DecisionTypeId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetDecisionReasonDropdownList");
                    parmeters.Add("@DecisionTypeId", DecisionTypeId);
                    var objData = await Con.QueryAsync<DropdownlistModel>("spMstCaseDecisionReason", parmeters, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetDecisionReason", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/CaseDecisionReason/CaseDecisionReasonRepository/GetDecisionReason");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


        public async Task<ResponseModel> AddEditDecisionReason(AddEditCaseDecisionReasonModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddEditDecisionReason");
                    parmeters.Add("@DecisionReasonId", objModel.DecisionReasonId);
                    parmeters.Add("@DecisionReasonEnglish", objModel.DecisionReasonEnglish);
                    parmeters.Add("@DecisionReasonHindi", objModel.DecisionReasonHindi);
                    parmeters.Add("@DecisionTypeId", objModel.DecisionTypeId);
                    parmeters.Add("@CreatedBy", UserId);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseModel>("spMstCaseDecisionReason", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDecisionReason", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/CaseDecisionReason/CaseDecisionReasonRepository/AddEditDecisionReason");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


        
        public async Task<ResponseModel> ActiveDeactiveDecisionReason(ActiveDeactiveCaseDecisionReasonModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "ActiveDeactiveDecisionReason");
                    parmeters.Add("@DecisionReasonId", objModel.DecisionReasonId);
                    parmeters.Add("@IsActive", objModel.IsActive == true ? 1 : 0);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseModel>("spMstCaseDecisionReason", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveDecisionReason", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/CaseDecisionReason/CaseDecisionReasonRepository/ActiveDeactiveDecisionReason");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

    }
}
