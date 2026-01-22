using Case.Dto.CaseDecision;
using Case.Dto.Shared;
using Common.Dapper;
using Common.Repository;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Case.Repository.CaseDecision
{
    public class CaseDecisionRepository : SqlRepository,ICaseDecisionRepository
    {
        private readonly LogsService _logsService;
        private readonly System.Data.IDbConnection Con;
        public CaseDecisionRepository(IConfiguration Configuration, LogsService logsService) : base(Configuration)
        {
            _logsService = logsService;
        }
        public async Task<ResponseWithoutPaginationModel> GetCaseDecisionList(long CaseId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetCaseDecisionList");
                    parmeters.Add("@CaseId", CaseId);
                    var objResult = await Con.QueryAsync("spTrn_CaseDecision", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseWithoutPaginationModel objResut = new()
                    {
                        Status = true,
                        Message = "",
                        Data = objResult,
                    };
                    DisposeCurrentSqlConnection();

                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCaseDecisionList", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseDecisionRepository/GetCaseDecisionList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<CaseDecisionResponseModel> AddEditCaseDecision(CaseDecisionModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    if (objModel.DecisionId > 0)
                    {
                        parmeters.Add("@Action", "EditCaseDecision");
                        parmeters.Add("@UpdatedBy", UserId);
                    }
                    else
                    {
                        parmeters.Add("@Action", "AddCaseDecision");
                        parmeters.Add("@CreatedBy", UserId);
                    }
                    parmeters.Add("@DecisionId", objModel.DecisionId);
                    parmeters.Add("@CaseId", objModel.CaseId);
                    parmeters.Add("@DecisionDate", objModel.DecisionDate);
                    parmeters.Add("@Decision_Comp_Date", objModel.Decision_Comp_Date);
                    parmeters.Add("@Decision_Detail", objModel.Decision_Detail);
                    parmeters.Add("@Decision_FA", objModel.Decision_FA == true ? 1 : 0);
                    parmeters.Add("@Web_copy_order_obtained", objModel.Web_copy_order_obtained == true ? 1 : 0);
                    parmeters.Add("@Web_obtained_date", objModel.Web_obtained_date);
                    parmeters.Add("@DocumentName", objModel.DocumentName);
                    parmeters.Add("@Implementation_required", objModel.Implementation_required == true ? 1 : 0);
                    parmeters.Add("@Implementation_required_OrNo", objModel.Implementation_required_OrNo);
                    parmeters.Add("@Implementation_required_date", objModel.Implementation_required_date);
                    parmeters.Add("@AppliedForCertifiedCopy_YN", objModel.AppliedForCertifiedCopy_YN == true ? 1 : 0);
                    parmeters.Add("@AppliedForCertifiedCopyInwordNo", objModel.AppliedForCertifiedCopyInwordNo);
                    parmeters.Add("@AppliedForCertifiedCopyDate", objModel.AppliedForCertifiedCopyDate);
                    parmeters.Add("@CopyReceived_YN", objModel.CopyReceived_YN == true ? 1 : 0);
                    parmeters.Add("@CopyForwordedOfOic_Hod_YN", objModel.CopyForwordedOfOic_Hod_YN == true ? 1 : 0);
                    parmeters.Add("@OpinionProvidedToOic_Hod_YN", objModel.OpinionProvidedToOic_Hod_YN == true ? 1 : 0);
                    parmeters.Add("@PD_DecisionCopyRecYN", objModel.PD_DecisionCopyRecYN == true ? 1 : 0);
                    parmeters.Add("@PD_DecisionCopyRecDate", objModel.PD_DecisionCopyRecDate);
                    parmeters.Add("@PD_DecisionSenttoHOOYN", objModel.PD_DecisionSenttoHOOYN == true ? 1 : 0);
                    parmeters.Add("@PD_DecisionSenttoHOODate", objModel.PD_DecisionSenttoHOODate);
                    parmeters.Add("@PD_DecisionSenttoGovtYN", objModel.PD_DecisionSenttoGovtYN == true ? 1 : 0);
                    parmeters.Add("@PD_DecisionSenttoGovtDate", objModel.PD_DecisionSenttoGovtDate);
                    parmeters.Add("@PD_StayGrantedYN", objModel.PD_StayGrantedYN == true ? 1 : 0);
                    parmeters.Add("@PD_StayGrantedDate", objModel.PD_StayGrantedDate);
                    parmeters.Add("@PD_LawyerOpenionYN", objModel.PD_LawyerOpenionYN == true ? 1 : 0);
                    parmeters.Add("@PD_DateoffilingAppeal", objModel.PD_DateoffilingAppeal);
                    parmeters.Add("@Remark", objModel.Remark);
                    parmeters.Add("@DateoSendingCertifiedCopyYN", objModel.DateoSendingCertifiedCopyYN == true ? 1 : 0);
                    parmeters.Add("@DateoSendingCertifiedCopy", objModel.DateoSendingCertifiedCopy);
                    parmeters.Add("@PD_AppealFilingDate", objModel.PD_AppealFilingDate);
                    parmeters.Add("@PD_DecisionSenttoHODYN", objModel.PD_DecisionSenttoHODYN == true ? 1 : 0);
                    parmeters.Add("@PD_DecisionSenttoHODDate", objModel.PD_DecisionSenttoHODDate);
                    parmeters.Add("@PD_FinalDecisionofGovtYN", objModel.PD_FinalDecisionofGovtYN == true ? 1 : 0);
                    parmeters.Add("@PD_FinalDecisionofGovtDate", objModel.PD_FinalDecisionofGovtDate);
                    parmeters.Add("@PD_DecisionCompliedYN", objModel.PD_DecisionCompliedYN == true ? 1 : 0);
                    parmeters.Add("@PD_DecisionCompliedDate", objModel.PD_DecisionCompliedDate);
                    parmeters.Add("@PD_DepttOpenionYN", objModel.PD_DepttOpenionYN == true ? 1 : 0);
                    parmeters.Add("@PD_AppealNo", objModel.PD_AppealNo);
                    parmeters.Add("@IsExParty", objModel.IsExParty == true ? 1 : 0);
                    parmeters.Add("@ExPartyDate", objModel.ExPartyDate);
                    parmeters.Add("@DataSendCommYN", objModel.DataSendCommYN == true ? 1 : 0);
                    parmeters.Add("@Date_Sending_Comment", objModel.Date_Sending_Comment);
                    parmeters.Add("@DateoSendingCertifiedCopyFileType", objModel.DateoSendingCertifiedCopyFileType);
                    parmeters.Add("@PLC_Date", objModel.PLC_Date);
                    parmeters.Add("@PLC_Document", objModel.PLC_Document);
                    parmeters.Add("@CopyOfDecisionReceivedDocs", objModel.CopyOfDecisionReceivedDocs);
                    parmeters.Add("@OpinionOfOic_YN", objModel.OpinionOfOic_YN == true ? 1 : 0);
                    parmeters.Add("@OpinionOfOicDocs", objModel.OpinionOfOicDocs);
                    parmeters.Add("@PD_DecisionNonCompliedReason", objModel.PD_DecisionNonCompliedReason);
                    var objData = await Con.QueryAsync<CaseDecisionResponseModel>("spTrn_CaseDecision", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new CaseDecisionResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditCaseDecision", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseDecisionRepository/AddEditCaseDecision");
                return new CaseDecisionResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> DeleteCaseDecision(long DecisionId, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "DeleteCaseDecision");
                    parmeters.Add("@DeleteBy", UserId);
                    parmeters.Add("@DecisionId", DecisionId);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spTrn_CaseDecision", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteCaseDecision", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseDecisionRepository/DeleteCaseDecision");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> GetCaseDecisionPamcList(long CaseId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetCaseDecisionPamcList");
                    parmeters.Add("@CaseId", CaseId);
                    var objResult = await Con.QueryAsync("spTrn_CaseDecision", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseWithoutPaginationModel objResut = new()
                    {
                        Status = true,
                        Message = "",
                        Data = objResult,
                    };
                    DisposeCurrentSqlConnection();

                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCaseDecisionPamcList", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseDecisionRepository/GetCaseDecisionPamcList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<CaseDecisionResponseModel> AddEditCaseDecisionPamc(CaseDecisionPamcAddModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    if (objModel.PamcId > 0)
                    {
                        parmeters.Add("@Action", "EditCaseDecisionPamc");
                        parmeters.Add("@c", UserId);
                    }
                    else
                    {
                        parmeters.Add("@Action", "AddCaseDecisionPamc");
                        parmeters.Add("@CreatedBy", UserId);
                    }
                    parmeters.Add("@PamcId", objModel.PamcId);
                    parmeters.Add("@CaseId", objModel.CaseId);
                    parmeters.Add("@DecisionId", objModel.DecisionId);                    
                    parmeters.Add("@PamcDate", objModel.PamcDate);
                    parmeters.Add("@PamcDocs", objModel.PamcDocs);
                    parmeters.Add("@CopyOfPamcDecision", objModel.CopyOfPamcDecision);
                    parmeters.Add("@MeetingConducted", objModel.MeetingConducted);
                    parmeters.Add("@MeetingStatus", objModel.MeetingStatus);
                    var objData = await Con.QueryAsync<CaseDecisionResponseModel>("spTrn_CaseDecision", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new CaseDecisionResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditCaseDecisionPamc", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseDecisionRepository/AddEditCaseDecisionPamc");
                return new CaseDecisionResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> DeactiveCaseDecisionPamc(long PamcId, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "DeactiveCaseDecisionPamc");
                    parmeters.Add("@PamcId", PamcId);
                    parmeters.Add("@UpdatedBy", UserId);
                    parmeters.Add("@DeleteBy", UserId);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spTrn_CaseDecision", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeactiveCaseDecisionPamc", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseDecisionRepository/DeactiveCaseDecisionPamc");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> DeleteFromCaseDecisionUpdateList(long caseId, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "DeleteFromCaseDecisionUpdateList");
                    parmeters.Add("@CaseId", caseId);
                    parmeters.Add("@DeleteBy", UserId);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spTrn_CaseDecision", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteFromCaseDecisionUpdateList", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseDecisionRepository/DeleteFromCaseDecisionUpdateList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
