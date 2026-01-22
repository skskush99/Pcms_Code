using Case.Dto.CaseFileRegister;
using Case.Dto.Shared;
using Common.Dapper;
using Common.Repository;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Case.Repository.CaseFileRegister
{
    public class CaseFileRegisterRepository : SqlRepository, ICaseFileRegisterRepository
    {
        private readonly LogsService _logsService;
        private readonly System.Data.IDbConnection Con;
        public CaseFileRegisterRepository(IConfiguration Configuration, LogsService logsService) : base(Configuration)
        {
            _logsService = logsService;
        }

        public async Task<ResponseModel> GetCaseFileRegisterList(CaseFileRegisterFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetCaseFileRegisterList");
                    parmeters.Add("@PageNo", objModel.PageNo);
                    parmeters.Add("@Pagesize", objModel.PageSize);
                    parmeters.Add("@AdmDepttId", objModel.AdmDepttId);
                    parmeters.Add("@Cell", objModel.Cell);
                    parmeters.Add("@HeadId", objModel.HeadId);
                    parmeters.Add("@Court", objModel.Court);
                    parmeters.Add("@CaseNo", objModel.CaseNo);
                    parmeters.Add("@CaseRegistorYear", objModel.CaseRegistorYear);
                    parmeters.Add("@AbbrevationId", objModel.AbbrevationId);
                    parmeters.Add("@Banch", objModel.Banch);
                    parmeters.Add("@CsIsParty", objModel.CsIsParty);
                    parmeters.Add("@CourtType", objModel.CourtType);
                    parmeters.Add("@HasConnectedCase", objModel.HasConnectedCase);
                    var objResult = await Con.QueryMultipleAsync("spTrn_CaseFileRegister", parmeters, commandType: CommandType.StoredProcedure);
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
                _logsService.Logs("Error", "GetCaseFileRegisterList", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseFileRegisterRepository/GetCaseFileRegisterList");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> GetLawDeptFileNoCount(CaseFileRegisterCountFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetLawDeptFileNoCount"); 
                    parmeters.Add("@Cell", objModel.Cell);
                    parmeters.Add("@HeadId", objModel.HeadId);
                    parmeters.Add("@CourtType", objModel.CourtType);
                    parmeters.Add("@Court", objModel.Court);
                    var objResult = await Con.QueryMultipleAsync("spTrn_CaseFileRegister", parmeters, commandType: CommandType.StoredProcedure);
                    ResponseWithoutPaginationModel objResut = new()
                    {
                        Status = true,
                        Message = "",
                        Data = objResult.Read<object>(),
                    };
                    DisposeCurrentSqlConnection();

                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetLawDeptFileNoCount", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseFileRegisterRepository/GetLawDeptFileNoCount");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> AddEditCaseFileRegister(CaseFileRegisterModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    if (objModel.CaseFileRegistorId > 0)
                    {
                        parmeters.Add("@Action", "EditCaseFileRegister");
                        parmeters.Add("@LastUpdatedBy", UserId);
                        parmeters.Add("@CaseFileRegistorId", objModel.CaseFileRegistorId);
                    }
                    else
                    {
                        parmeters.Add("@Action", "AddCaseFileRegister");
                        parmeters.Add("@CreatedBy", UserId);
                    }
                    parmeters.Add("@Cell", objModel.Cell);
                    parmeters.Add("@HeadId", objModel.HeadId);
                    parmeters.Add("@Court", objModel.Court);
                    parmeters.Add("@CourtType", objModel.CourtType);
                    parmeters.Add("@CaseNo", objModel.CaseNo);
                    parmeters.Add("@Title", objModel.Title);
                    parmeters.Add("@Respondents", objModel.Respondents);
                    parmeters.Add("@CaseRegistorYear", objModel.CaseRegistorYear);
                    parmeters.Add("@AbbrevationId", objModel.AbbrevationId);
                    parmeters.Add("@Banch", objModel.Banch);
                    parmeters.Add("@AdmDepttId", objModel.AdmDepttId);
                    parmeters.Add("@AdmDepttFileNo", objModel.AdmDepttFileNo);
                    parmeters.Add("@AdmDepttPartFileNo", objModel.AdmDepttPartFileNo);
                    parmeters.Add("@ConnectedCaseNo", objModel.ConnectedCaseNo);
                    parmeters.Add("@CnnectedTitle", objModel.CnnectedTitle);
                    parmeters.Add("@ConnectedRespondents", objModel.ConnectedRespondents);
                    parmeters.Add("@ConnectedYear", objModel.ConnectedYear);
                    parmeters.Add("@ConnectedAbbrevationId", objModel.ConnectedAbbrevationId);
                    parmeters.Add("@ConnectedBanch", objModel.ConnectedBanch);
                    parmeters.Add("@LawDeptFileNo", objModel.LawDeptFileNo);
                    parmeters.Add("@LawDeptPartFileNo", objModel.LawDeptPartFileNo);
                    parmeters.Add("@CsIsParty", objModel.CsIsParty);
                    parmeters.Add("@ConnectedCaseStatus", objModel.ConnectedCaseStatus);
                    parmeters.Add("@ConnectedCaseFileRegId", objModel.ConnectedCaseFileRegId);
                    parmeters.Add("@LawCCsId", objModel.LawCCsId);
                    parmeters.Add("@LawOtherSignatureAuthorityId", objModel.LawOtherSignatureAuthorityId);
                    parmeters.Add("@LawOtherSignatureAuthoritysId", objModel.LawOtherSignatureAuthoritysId);
                    parmeters.Add("@maintext", objModel.maintext);
                    parmeters.Add("@maintextnext", objModel.maintextnext);
                    parmeters.Add("@textname1", objModel.textname1);
                    parmeters.Add("@textname2", objModel.textname2);
                    parmeters.Add("@textname3", objModel.textname3);
                    parmeters.Add("@AddressGenLetter", objModel.AddressGenLetter);
                    parmeters.Add("@LawCCsIdGenLetter", objModel.LawCCsIdGenLetter);
                    parmeters.Add("@Address1GenLetter", objModel.Address1GenLetter);
                    parmeters.Add("@Address2GenLetter", objModel.Address2GenLetter);
                    parmeters.Add("@Address3GenLetter", objModel.Address3GenLetter);
                    parmeters.Add("@MiddleContGenLetter", objModel.MiddleContGenLetter);
                    parmeters.Add("@LawOtherSignatureAuthorityIdGenLetter", objModel.LawOtherSignatureAuthorityIdGenLetter);
                    parmeters.Add("@LawOtherSignatureAuthoritysIdGenLetter", objModel.LawOtherSignatureAuthoritysIdGenLetter);
                    parmeters.Add("@ShowCC", objModel.ShowCC);
                    parmeters.Add("@HasConnectedCase", objModel.HasConnectedCase);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spTrn_CaseFileRegister", parmeters, commandType: CommandType.StoredProcedure);
                    var objResult = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResult != null ? objResult : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditCaseFileRegister", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseFileRegisterRepository/AddEditCaseFileRegister");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseModel> GetConnectedCaseList(ConnectedCaseFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetConnectedCaseList");
                    parmeters.Add("@PageNo", objModel.PageNo);
                    parmeters.Add("@Pagesize", objModel.PageSize);
                    parmeters.Add("@AdmDepttId", objModel.AdmDepttId);
                    parmeters.Add("@Cell", objModel.Cell);
                    parmeters.Add("@HeadId", objModel.HeadId);
                    parmeters.Add("@Court", objModel.Court);
                    parmeters.Add("@CaseNo", objModel.CaseNo);
                    parmeters.Add("@CaseRegistorYear", objModel.CaseRegistorYear);
                    parmeters.Add("@AbbrevationId", objModel.AbbrevationId);
                    parmeters.Add("@Banch", objModel.Banch);
                    parmeters.Add("@CsIsParty", objModel.CsIsParty);
                    parmeters.Add("@CourtType", objModel.CourtType);
                    var objResult = await Con.QueryMultipleAsync("spTrn_CaseFileRegister", parmeters, commandType: CommandType.StoredProcedure);
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
                _logsService.Logs("Error", "GetConnectedCaseList", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseFileRegisterRepository/GetConnectedCaseList");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> AddEditConnectedCase(CaseFileRegisterModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    
                    if (objModel.ConnectedCaseFileRegId == 0)
                        parmeters.Add("@Action", "AddConnectedCase");
                    else
                        parmeters.Add("@Action", "EditConnectedCase");

                    parmeters.Add("@CreatedBy", UserId);
                    parmeters.Add("@LastUpdatedBy", UserId);
                    parmeters.Add("@CaseFileRegistorId", objModel.CaseFileRegistorId);
                    parmeters.Add("@Cell", objModel.Cell);
                    parmeters.Add("@HeadId", objModel.HeadId);
                    parmeters.Add("@Court", objModel.Court);
                    parmeters.Add("@CourtType", objModel.CourtType);
                    parmeters.Add("@CaseNo", objModel.CaseNo);
                    parmeters.Add("@Title", objModel.Title);
                    parmeters.Add("@Respondents", objModel.Respondents);
                    parmeters.Add("@CaseRegistorYear", objModel.CaseRegistorYear);
                    parmeters.Add("@AbbrevationId", objModel.AbbrevationId);
                    parmeters.Add("@Banch", objModel.Banch);
                    parmeters.Add("@AdmDepttId", objModel.AdmDepttId);
                    parmeters.Add("@AdmDepttFileNo", objModel.AdmDepttFileNo);
                    parmeters.Add("@AdmDepttPartFileNo", objModel.AdmDepttPartFileNo);
                    parmeters.Add("@ConnectedCaseNo", objModel.ConnectedCaseNo);
                    parmeters.Add("@CnnectedTitle", objModel.CnnectedTitle);
                    parmeters.Add("@ConnectedRespondents", objModel.ConnectedRespondents);
                    parmeters.Add("@ConnectedYear", objModel.ConnectedYear);
                    parmeters.Add("@ConnectedAbbrevationId", objModel.ConnectedAbbrevationId);
                    parmeters.Add("@ConnectedBanch", objModel.ConnectedBanch);
                    parmeters.Add("@LawDeptFileNo", objModel.LawDeptFileNo);
                    parmeters.Add("@LawDeptPartFileNo", objModel.LawDeptPartFileNo);
                    parmeters.Add("@CsIsParty", objModel.CsIsParty);
                    parmeters.Add("@ConnectedCaseStatus", objModel.ConnectedCaseStatus);
                    parmeters.Add("@ConnectedCaseFileRegId", objModel.ConnectedCaseFileRegId);
                    parmeters.Add("@LawCCsId", objModel.LawCCsId);
                    parmeters.Add("@LawOtherSignatureAuthorityId", objModel.LawOtherSignatureAuthorityId);
                    parmeters.Add("@LawOtherSignatureAuthoritysId", objModel.LawOtherSignatureAuthoritysId);
                    parmeters.Add("@maintext", objModel.maintext);
                    parmeters.Add("@maintextnext", objModel.maintextnext);
                    parmeters.Add("@textname1", objModel.textname1);
                    parmeters.Add("@textname2", objModel.textname2);
                    parmeters.Add("@textname3", objModel.textname3);
                    parmeters.Add("@AddressGenLetter", objModel.AddressGenLetter);
                    parmeters.Add("@LawCCsIdGenLetter", objModel.LawCCsIdGenLetter);
                    parmeters.Add("@Address1GenLetter", objModel.Address1GenLetter);
                    parmeters.Add("@Address2GenLetter", objModel.Address2GenLetter);
                    parmeters.Add("@Address3GenLetter", objModel.Address3GenLetter);
                    parmeters.Add("@MiddleContGenLetter", objModel.MiddleContGenLetter);
                    parmeters.Add("@LawOtherSignatureAuthorityIdGenLetter", objModel.LawOtherSignatureAuthorityIdGenLetter);
                    parmeters.Add("@LawOtherSignatureAuthoritysIdGenLetter", objModel.LawOtherSignatureAuthoritysIdGenLetter);
                    parmeters.Add("@ShowCC", objModel.ShowCC);
                    parmeters.Add("@HasConnectedCase", objModel.HasConnectedCase);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spTrn_CaseFileRegister", parmeters, commandType: CommandType.StoredProcedure);
                    var objResult = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResult != null ? objResult : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditConnectedCase", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseFileRegisterRepository/AddEditConnectedCase");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> DeleteConnectedCase(int CaseFileRegistorId, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "DeleteConnectedCase");
                    parmeters.Add("@LastUpdatedBy", UserId);
                    parmeters.Add("@CaseFileRegistorId", CaseFileRegistorId);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spTrn_CaseFileRegister", parmeters, commandType: CommandType.StoredProcedure);
                    var objResult = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResult != null ? objResult : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteConnectedCase", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseFileRegisterRepository/DeleteConnectedCase");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> GetConnectedCaseListByCaseFileRegistorId(int caseFileRegistorId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var objData = await Con.QueryAsync<object>("spTrn_CaseFileRegister", 
                        new { 
                                Action = "GetConnectedCaseListByCaseFileRegistorId", 
                                CaseFileRegistorId= caseFileRegistorId 
                            }, commandType: CommandType.StoredProcedure);
                    var objResult = new ResponseWithoutPaginationModel()
                    {
                        Status = true,
                        Message = "",
                        Data = objData
                    };
                    DisposeCurrentSqlConnection();
                    return objResult;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetConnectedCaseListByCaseFileRegistorId", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseFileRegisterRepository/GetConnectedCaseListByCaseFileRegistorId");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseModel> GetUploadDocumentList(int PageNo, int PageSize)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetUploadCaseFileRegistor");
                    parmeters.Add("@PageNo", PageNo);
                    parmeters.Add("@Pagesize", PageSize);
                    var objResult = await Con.QueryMultipleAsync("spTrn_UploadCaseFileRegistor", parmeters, commandType: CommandType.StoredProcedure);
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
                _logsService.Logs("Error", "GetUploadDocumentList", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseFileRegisterRepository/GetUploadDocumentList");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> AddUploadDocument(AddCaseFileRegisterUploadDocumentModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddUploadCaseFileRegistor");
                    parmeters.Add("@DocumentName", objModel.DocumentName);
                    parmeters.Add("@DocumentFile", objModel.DocumentFile);
                    parmeters.Add("@CaseNo", objModel.CaseNo);
                    parmeters.Add("@CaseFileRegistorId", objModel.CaseFileRegistorId);
                    parmeters.Add("@CreatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spTrn_UploadCaseFileRegistor", parmeters, commandType: CommandType.StoredProcedure);
                    var objResult = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResult != null ? objResult : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddUploadDocument", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseFileRegisterRepository/AddUploadDocument");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> DeactiveUploadDocument(DeactiveCaseFileRegisterUploadDocumentModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "DeActiveUploadCaseFileRegistor");
                    parmeters.Add("@Id", objModel.Id);
                    parmeters.Add("@Active", objModel.Active);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spTrn_UploadCaseFileRegistor", parmeters, commandType: CommandType.StoredProcedure);
                    var objResult = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResult != null ? objResult : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeactiveUploadDocument", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseFileRegisterRepository/DeactiveUploadDocument");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

    }
}
