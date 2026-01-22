using Case.Dto.CaseHearings;
using Case.Dto.Shared;
using Common.Dapper;
using Common.Repository;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Case.Repository.CaseHearings
{
    public class CaseHearingsRepository : SqlRepository,ICaseHearingsRepository
    {
        private readonly LogsService _logsService;
        private readonly System.Data.IDbConnection Con;
        public CaseHearingsRepository(IConfiguration Configuration, LogsService logsService) : base(Configuration)
        {
            _logsService = logsService;
        }

        public async Task<ResponseWithoutPaginationModel> GetCaseHearingsList(long CaseId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetCaseHearingsList");
                    parmeters.Add("@CaseId", CaseId);
                    var objResult = await Con.QueryAsync("spTrn_CaseHearings", parmeters, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetCaseHearingsList", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseHearingsRepository/GetCaseHearingsList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> AddEditCaseHearings(CaseHearingsModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    if (objModel.CaseHearingId > 0)
                    {
                        parmeters.Add("@Action", "EditCaseHearings");
                        parmeters.Add("@UpdatedBy", UserId);
                    }
                    else
                    {
                        parmeters.Add("@Action", "AddCaseHearings");
                        parmeters.Add("@CreatedBy", UserId);
                    }                    
                    parmeters.Add("@CaseHearingId", objModel.CaseHearingId);
                    parmeters.Add("@CaseId", objModel.CaseId);
                    parmeters.Add("@LawyerId", objModel.LawyerId);
                    parmeters.Add("@HearingDate", objModel.HearingDate);
                    parmeters.Add("@OICId", objModel.OICId);
                    parmeters.Add("@Judgment_PR", objModel.Judgment_PR);
                    parmeters.Add("@ArgumentOver_YN", objModel.ArgumentOver_YN == true ? 1 : 0);
                    parmeters.Add("@HC_Admitted_YNA", objModel.HC_Admitted_YNA);
                    parmeters.Add("@HC_StayGranted_YN", objModel.HC_StayGranted_YN == true ? 1 : 0);
                    parmeters.Add("@HC_AnyMiscAppfiled_YN", objModel.HC_AnyMiscAppfiled_YN == true ? 1 : 0);
                    parmeters.Add("@HC_Sup_InwordNo", objModel.HC_Sup_InwordNo);
                    parmeters.Add("@HC_Sup_InwordRegion", objModel.HC_Sup_InwordRegion);
                    parmeters.Add("@HC_Sup_InwordDate", objModel.HC_Sup_InwordDate);
                    parmeters.Add("@HC_Replyfiled_YN", objModel.HC_Replyfiled_YN);
                    parmeters.Add("@StayOrder_FA", objModel.StayOrder_FA == true ? 1 : 0);
                    parmeters.Add("@StayFinishDate", objModel.StayFinishDate);
                    parmeters.Add("@InterimOrder_YN", objModel.InterimOrder_YN == true ? 1 : 0);
                    parmeters.Add("@Interim_Order_Date", objModel.Interim_Order_Date);
                    parmeters.Add("@Interim_Order_No", objModel.Interim_Order_No);
                    parmeters.Add("@SupplementaryFactul_YN", objModel.SupplementaryFactul_YN == true ? 1 : 0);
                    parmeters.Add("@SupplementaryInwordNo", objModel.SupplementaryInwordNo);
                    parmeters.Add("@SupplementaryInwordDate", objModel.SupplementaryInwordDate);
                    parmeters.Add("@ApplVactingStay_YN", objModel.ApplVactingStay_YN == true ? 1 : 0);
                    parmeters.Add("@ApplVactingInwordNo", objModel.ApplVactingInwordNo);
                    parmeters.Add("@ApplVactingInwordDate", objModel.ApplVactingInwordDate);
                    parmeters.Add("@ReplayFildInwordNo", objModel.ReplayFildInwordNo);
                    parmeters.Add("@ReplayFildInwordDate", objModel.ReplayFildInwordDate);
                    parmeters.Add("@Adjourned_YN", objModel.Adjourned_YN == true ? 1 : 0);
                    parmeters.Add("@ReplyFileDate", objModel.ReplyFileDate);
                    parmeters.Add("@AdjournmentByCourt_YN", objModel.AdjournmentByCourt_YN == true ? 1 : 0);
                    parmeters.Add("@AdjournmentByPertitnor_YN", objModel.AdjournmentByPertitnor_YN == true ? 1 : 0);
                    parmeters.Add("@AdjournmentByResponent_YN", objModel.AdjournmentByResponent_YN == true ? 1 : 0);
                    parmeters.Add("@AdjournmentDate", objModel.AdjournmentDate);
                    parmeters.Add("@AdjournmentRegion", objModel.AdjournmentRegion);
                    parmeters.Add("@SpecialAppearance", objModel.SpecialAppearance);
                    parmeters.Add("@FactualReportDate", objModel.FactualReportDate);
                    parmeters.Add("@Next_HearingYN", objModel.Next_HearingYN == true ? 1 : 0);
                    parmeters.Add("@NextHearing_Date", objModel.NextHearing_Date);
                    parmeters.Add("@IsExPartyStay", objModel.IsExPartyStay == true ? 1 : 0);
                    parmeters.Add("@ExPartyStayDate", objModel.ExPartyStayDate);
                    parmeters.Add("@Remark", objModel.Remark);
                    parmeters.Add("@DueCourse", objModel.DueCourse);
                    parmeters.Add("@Decided", objModel.Decided == true ? 1 : 0);
                    parmeters.Add("@DateCaseFillingDeptToAG_AAG", objModel.DateCaseFillingDeptToAG_AAG);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spTrn_CaseHearings", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditCaseHearings", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseHearingsRepository/AddEditCaseHearings");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> DeleteCaseHearings(long CaseHearingId, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "DeleteCaseHearings");
                    parmeters.Add("@DeleteBy", UserId);
                    parmeters.Add("@CaseHearingId", CaseHearingId);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spTrn_CaseHearings", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteCaseHearings", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseHearingsRepository/DeleteCaseHearings");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetReplyComplianceList(long CaseHearingId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetReplyComplianceList");
                    parmeters.Add("@CaseHearingId", CaseHearingId);
                    var objResult = await Con.QueryAsync("spTrn_CaseHearings", parmeters, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetReplyComplianceList", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseHearingsRepository/GetReplyComplianceList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> AddEditReplyCompliance(CaseHearingDetailModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    if (objModel.HearingDetailId > 0)
                    {
                        parmeters.Add("@Action", "EditReplyCompliance");
                        parmeters.Add("@UpdatedBy", UserId);
                    }
                    else
                    {
                        parmeters.Add("@Action", "AddReplyCompliance");
                        parmeters.Add("@CreatedBy", UserId);
                    }
                    parmeters.Add("@HearingDetailId", objModel.HearingDetailId);
                    parmeters.Add("@CaseId", objModel.CaseId);
                    parmeters.Add("@CaseHearingId", objModel.CaseHearingId);
                    parmeters.Add("@ReplyStatus", objModel.ReplyStatus);
                    parmeters.Add("@OrderDetail", objModel.OrderDetail);
                    parmeters.Add("@ComplianceFiled", objModel.ComplianceFiled == true ? 1 : 0);
                    parmeters.Add("@ComplianceFiledDate", objModel.ComplianceFiledDate);
                    parmeters.Add("@ComplianceDetail", objModel.ComplianceDetail);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spTrn_CaseHearings", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditReplyCompliance", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseHearingsRepository/AddEditReplyCompliance");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
