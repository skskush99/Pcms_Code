using Case.Dto.CaseRegistrations;
using Case.Dto.Shared;
using Common.Dapper;
using Common.Repository;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Case.Repository.CaseRegistrations
{
    public class CaseRegistrationsRepository : SqlRepository, ICaseRegistrationsRepository
    {
        private readonly LogsService _logsService;
        private readonly System.Data.IDbConnection Con;
        public CaseRegistrationsRepository(IConfiguration Configuration, LogsService logsService) : base(Configuration)
        {
            _logsService = logsService;
        }

        public async Task<ResponseModel> GetCaseList(CaseListFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetCaseList");
                    parmeters.Add("@AdmDepttId", objModel.AdmDepttId);
                    parmeters.Add("@UnitId", objModel.UnitId);
                    parmeters.Add("@OfficeId", objModel.OfficeId);
                    parmeters.Add("@CourtTypeId", objModel.CourtTypeId);
                    parmeters.Add("@AbbreviationId", objModel.AbbreviationId);
                    parmeters.Add("@CaseYear", objModel.CaseYear);
                    parmeters.Add("@GroupingId", objModel.GroupingId);
                    parmeters.Add("@DecisionCount", objModel.CaseStatus);
                    parmeters.Add("@PrimarySecondary", objModel.PrimarySecondary ?? "");
                    parmeters.Add("@CRNNumber", objModel.CRNNumber);
                    parmeters.Add("@CaseNo", objModel.CaseNo == null ? 0 : objModel.CaseNo);
                    parmeters.Add("@RoleId", objModel.RoleId);
                    parmeters.Add("@OICId", objModel.OICId);
                    parmeters.Add("@LawyerId", objModel.LawyerId);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@SortBy", objModel.SortBy ?? "");
                    parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
                    parmeters.Add("@PageNo", objModel.PageNo);
                    parmeters.Add("@Pagesize", objModel.PageSize);
                    //parmeters.Add("@CaseType", Core.CaseType.CaseRegistered);
                    parmeters.Add("@CaseType", objModel.CaseType);
                    var objResult = await Con.QueryMultipleAsync("spTrn_CaseRegistrations", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetCaseList", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseRegistrationsRepository/GetCaseList");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<CaseRegistrationsResponseModel> AddEditCaseRegistrations(CaseRegistrationsModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();

                    if (objModel.CaseId > 0)
                    {
                        parmeters.Add("@Action", "EditCase");
                        parmeters.Add("@LastUpdatedBy", UserId);
                    }
                    else
                    {
                        parmeters.Add("@Action", "AddCase");
                        parmeters.Add("@CreatedBy", UserId);
                    }
                    parmeters.Add("@CaseId", objModel.CaseId);
                    parmeters.Add("@CRNNumber", objModel.CRNNumber);
                    parmeters.Add("@AdmDepttId", objModel.AdmDepttId);
                    parmeters.Add("@UnitId", objModel.UnitId);
                    parmeters.Add("@OfficeId", objModel.OfficeId);
                    parmeters.Add("@PlaceId", objModel.PlaceId);
                    parmeters.Add("@CourtId", objModel.CourtId);
                    parmeters.Add("@AbbreviationId", objModel.AbbreviationId);
                    parmeters.Add("@CaseYear", objModel.CaseYear);
                    parmeters.Add("@CaseNo", objModel.CaseNo);
                    parmeters.Add("@FileNo", objModel.FileNo);
                    parmeters.Add("@SubjectSubCategoryId", objModel.SubjectSubCategoryId);
                    parmeters.Add("@SubjectSubMatterId", objModel.SubjectSubMatterId);
                    parmeters.Add("@AppellantOrResponded", objModel.AppellantOrResponded);
                    parmeters.Add("@R_E_Implication", objModel.R_E_Implication);
                    parmeters.Add("@Does_P_O_A", objModel.Does_P_O_A == true ? 1 : 0);
                    parmeters.Add("@Does_P_A_PD", objModel.Does_P_A_PD == true ? 1 : 0);
                    parmeters.Add("@PriorityCode", objModel.PriorityCode);
                    parmeters.Add("@PriorityId", objModel.PriorityId);
                    parmeters.Add("@SubPriorityId", objModel.SubPriorityId);
                    parmeters.Add("@CaseRegistrationDate", objModel.CaseRegistrationDate);
                    parmeters.Add("@Bench", objModel.Bench);
                    parmeters.Add("@WACPNo", objModel.WACPNo);
                    parmeters.Add("@PrimarySecondary", objModel.PrimarySecondary);
                    parmeters.Add("@GroupingId", objModel.GroupingId);
                    parmeters.Add("@DateCaseFillingDeptToAG_AAG", objModel.DateCaseFillingDeptToAG_AAG);
                    parmeters.Add("@DateFillingCaseCourtByAG_AAG", objModel.DateFillingCaseCourtByAG_AAG);
                    parmeters.Add("@ApplicationUnderSec5FiledYN", objModel.ApplicationUnderSec5FiledYN == true ? 1 : 0);
                    parmeters.Add("@Remark", objModel.Remark);
                    parmeters.Add("@LinkCaseId", objModel.LinkCaseId);
                    parmeters.Add("@IsEmployee", objModel.IsEmployee == true ? 1 : 0);
                    parmeters.Add("@EmployeeCode", objModel.EmployeeCode);
                    parmeters.Add("@EmployeeId", objModel.EmployeeId);
                    parmeters.Add("@EmployeeName", objModel.EmployeeName);
                    parmeters.Add("@EmployeeDesignation", objModel.EmployeeDesignation);
                    parmeters.Add("@EmployeeSSOID", objModel.EmployeeSSOID);
                    parmeters.Add("@CaseStatus", objModel.ImportantCase == true ? 1 : 0);
                    parmeters.Add("@CaseType", Core.CaseType.CaseRegistered);

                    var objData = await Con.QueryAsync<CaseRegistrationsResponseModel>("spTrn_CaseRegistrations", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new CaseRegistrationsResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditCaseRegistrations", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseRegistrationsRepository/AddEditCaseRegistrations");
                return new CaseRegistrationsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<CaseRegistrationsResponseModel> DeleteCase(long CaseId, string DeleteMobileNo, string Reason, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "DeleteCase");
                    parmeters.Add("@DeleteBy", UserId);
                    parmeters.Add("@CaseId", CaseId);
                    parmeters.Add("@DeleteMobileNo", DeleteMobileNo);
                    parmeters.Add("@Reason", Reason);

                    var objData = await Con.QueryAsync<CaseRegistrationsResponseModel>("spTrn_CaseRegistrations", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new CaseRegistrationsResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteCase", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseRegistrationsRepository/DeleteCase");
                return new CaseRegistrationsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<CaseRegistrationsResponseModel> AddCaseGroup(AddCaseGroupModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddCaseGroup");
                    parmeters.Add("@CaseId", objModel.CaseId);
                    parmeters.Add("@GroupingId", objModel.GroupingId);
                    parmeters.Add("@LastUpdatedBy", UserId);

                    var objData = await Con.QueryAsync<CaseRegistrationsResponseModel>("spTrn_CaseRegistrations", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new CaseRegistrationsResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddCaseGroup", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseRegistrationsRepository/AddCaseGroup");
                return new CaseRegistrationsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<CaseRegistrationsResponseModel> AddCaseLinking(AddCaseLinkingModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddCaseLinking");
                    parmeters.Add("@CaseId", objModel.CaseId);
                    parmeters.Add("@LinkCaseId", objModel.LinkCaseId);
                    parmeters.Add("@LastUpdatedBy", UserId);

                    var objData = await Con.QueryAsync<CaseRegistrationsResponseModel>("spTrn_CaseRegistrations", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new CaseRegistrationsResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddCaseLinking", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseRegistrationsRepository/AddCaseLinking");
                return new CaseRegistrationsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<CaseRegistrationsResponseModel> AddCaseRemand(AddCaseRemandModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddCaseRemand");
                    parmeters.Add("@CaseId", objModel.CaseId);
                    parmeters.Add("@RemandId", objModel.RemandId);
                    parmeters.Add("@LastUpdatedBy", UserId);

                    var objData = await Con.QueryAsync<CaseRegistrationsResponseModel>("spTrn_CaseRegistrations", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new CaseRegistrationsResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddCaseRemand", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseRegistrationsRepository/AddCaseRemand");
                return new CaseRegistrationsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetCaseAppellantsList(long CaseId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetCaseAppellantsList");
                    parmeters.Add("@CaseId", CaseId);
                    var objResult = await Con.QueryAsync("spTrn_CaseAppellants", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetCaseAppellantsList", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseRegistrationsRepository/GetCaseAppellantsList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> AddEditCaseAppellants(CaseAppellantsModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();

                    if (objModel.CaseAppellantId > 0)
                    {
                        parmeters.Add("@Action", "EditCaseAppellant");
                        parmeters.Add("@LastUpdatedBy", UserId);
                    }
                    else
                    {
                        parmeters.Add("@Action", "AddCaseAppellant");
                        parmeters.Add("@CreatedBy", UserId);
                    }
                    parmeters.Add("@CaseAppellantId", objModel.CaseAppellantId);
                    parmeters.Add("@CaseId", objModel.CaseId);
                    parmeters.Add("@Name", objModel.Name);
                    parmeters.Add("@Designation", objModel.Designation);
                    parmeters.Add("@Address1", objModel.Address1);
                    parmeters.Add("@Address2", objModel.Address2);
                    parmeters.Add("@ContactNo", objModel.ContactNo);
                    parmeters.Add("@MobileNo", objModel.MobileNo);
                    parmeters.Add("@EmailId", objModel.EmailId);
                    parmeters.Add("@Appellant_SrNo", objModel.Appellant_SrNo);

                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spTrn_CaseAppellants", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResult = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResult != null ? objResult : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditCaseAppellants", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseRegistrationsRepository/AddEditCaseAppellants");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> DeleteCaseAppellants(long CaseAppellantId, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "DeleteCaseAppellant");
                    parmeters.Add("@DeleteBy", UserId);
                    parmeters.Add("@CaseAppellantId", CaseAppellantId);

                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spTrn_CaseAppellants", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteCaseAppellants", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseRegistrationsRepository/DeleteCaseAppellants");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetCaseRespondentsList(long CaseId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetCaseRespondentsList");
                    parmeters.Add("@CaseId", CaseId);
                    var objResult = await Con.QueryAsync("spTrn_CaseRespondents", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetCaseRespondentsList", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseRegistrationsRepository/GetCaseRespondentsList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> AddEditCaseRespondents(CaseRespondentsModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();

                    if (objModel.RespondentId > 0)
                    {
                        parmeters.Add("@Action", "EditCaseRespondent");
                        parmeters.Add("@LastUpdatedBy", UserId);
                    }
                    else
                    {
                        parmeters.Add("@Action", "AddCaseRespondent");
                        parmeters.Add("@CreatedBy", UserId);
                    }
                    parmeters.Add("@RespondentId", objModel.RespondentId);
                    parmeters.Add("@CaseId", objModel.CaseId);
                    parmeters.Add("@Name", objModel.Name);
                    parmeters.Add("@Designation", objModel.Designation);
                    parmeters.Add("@Address1", objModel.Address1);
                    parmeters.Add("@Address2", objModel.Address2);
                    parmeters.Add("@ContactNo", objModel.ContactNo);
                    parmeters.Add("@MobileNo", objModel.MobileNo);
                    parmeters.Add("@EmailId", objModel.EmailId);
                    parmeters.Add("@Respondant_SrNo", objModel.Respondant_SrNo);

                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spTrn_CaseRespondents", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditCaseRespondents", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseRegistrationsRepository/AddEditCaseRespondents");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> DeleteCaseRespondents(long RespondentId, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "DeleteCaseRespondent");
                    parmeters.Add("@DeleteBy", UserId);
                    parmeters.Add("@RespondentId", RespondentId);

                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spTrn_CaseRespondents", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteCaseRespondents", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseRegistrationsRepository/DeleteCaseRespondents");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetCaseDocumentsList(long CaseId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetCaseDocumentsList");
                    parmeters.Add("@CaseId", CaseId);
                    var objResult = await Con.QueryAsync("spTrn_CaseDocuments", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetCaseDocumentsList", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseRegistrationsRepository/GetCaseDocumentsList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<CaseRegistrationsResponseModel> AddCaseDocuments(CaseAddDocumentModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddCaseDocument");
                    parmeters.Add("@CreatedBy", UserId);
                    parmeters.Add("@CaseId", objModel.CaseId);
                    parmeters.Add("@DocType", objModel.DocType);
                    parmeters.Add("@DocumentName", objModel.DocumentName);
                    parmeters.Add("@DocumentFile", objModel.DocumentFile);
                    var objData = await Con.QueryAsync<CaseRegistrationsResponseModel>("spTrn_CaseDocuments", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new CaseRegistrationsResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddCaseDocuments", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseRegistrationsRepository/AddCaseDocuments");
                return new CaseRegistrationsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> DeleteCaseDocuments(long CaseDocumentId, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "DeleteCaseDocument");
                    parmeters.Add("@DeleteBy", UserId);
                    parmeters.Add("@CaseDocumentId", CaseDocumentId);

                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spTrn_CaseDocuments", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteCaseDocuments", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseRegistrationsRepository/DeleteCaseDocuments");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseModel> GetCaseListWithoutCaseNo(CaseListFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetCaseListWithoutCaseNo"); ;
                    parmeters.Add("@AdmDepttId", objModel.AdmDepttId);
                    parmeters.Add("@UnitId", objModel.UnitId);
                    parmeters.Add("@OfficeId", objModel.OfficeId);
                    parmeters.Add("@CourtTypeId", objModel.CourtTypeId);
                    parmeters.Add("@AbbreviationId", objModel.AbbreviationId);
                    parmeters.Add("@CaseYear", objModel.CaseYear);
                    parmeters.Add("@GroupingId", objModel.GroupingId);
                    parmeters.Add("@DecisionCount", objModel.CaseStatus);
                    parmeters.Add("@PrimarySecondary", objModel.PrimarySecondary ?? "");
                    parmeters.Add("@CRNNumber", objModel.CRNNumber ?? "");
                    parmeters.Add("@CaseNo", objModel.CaseNo == null ? 0 : objModel.CaseNo);
                    parmeters.Add("@RoleId", objModel.RoleId);
                    parmeters.Add("@OICId", objModel.OICId);
                    parmeters.Add("@LawyerId", objModel.LawyerId);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@SortBy", objModel.SortBy == null ? "" : objModel.SortBy);
                    parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
                    parmeters.Add("@PageNo", objModel.PageNo);
                    parmeters.Add("@Pagesize", objModel.PageSize);
                    parmeters.Add("@CaseType", Core.CaseType.CaseWithoutCaseNo);
                    var objResult = await Con.QueryMultipleAsync("spTrn_CaseRegistrations", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetCaseListWithoutCaseNo", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseRegistrationsRepository/GetCaseListWithoutCaseNo");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<CaseRegistrationsResponseModel> AddEditCaseWithoutCaseNo(CaseWithoutCaseNoModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();

                    if (objModel.CaseId > 0)
                    {
                        parmeters.Add("@Action", "EditCaseWithoutCaseNo");
                        parmeters.Add("@LastUpdatedBy", UserId);
                    }
                    else
                    {
                        parmeters.Add("@Action", "AddCaseWithoutCaseNo");
                        parmeters.Add("@CreatedBy", UserId);
                    }
                    parmeters.Add("@CaseId", objModel.CaseId);
                    parmeters.Add("@AdmDepttId", objModel.AdmDepttId);
                    parmeters.Add("@UnitId", objModel.UnitId);
                    parmeters.Add("@OfficeId", objModel.OfficeId);
                    parmeters.Add("@PlaceId", objModel.PlaceId);
                    parmeters.Add("@CourtId", objModel.CourtId);
                    parmeters.Add("@AbbreviationId", objModel.AbbreviationId);
                    parmeters.Add("@CaseYear", objModel.CaseYear);
                    parmeters.Add("@PreCaseNo", objModel.PreCaseNo);
                    parmeters.Add("@FileNo", objModel.FileNo);
                    parmeters.Add("@SubjectSubCategoryId", objModel.SubjectSubCategoryId);
                    parmeters.Add("@SubjectSubMatterId", objModel.SubjectSubMatterId);
                    parmeters.Add("@AppellantOrResponded", objModel.AppellantOrResponded);
                    parmeters.Add("@R_E_Implication", objModel.R_E_Implication);
                    parmeters.Add("@Does_P_O_A", objModel.Does_P_O_A == true ? 1 : 0);
                    parmeters.Add("@Does_P_A_PD", objModel.Does_P_A_PD == true ? 1 : 0);
                    parmeters.Add("@PriorityCode", objModel.PriorityCode);
                    parmeters.Add("@PriorityId", objModel.PriorityId);
                    parmeters.Add("@SubPriorityId", objModel.SubPriorityId);
                    parmeters.Add("@CaseRegistrationDate", objModel.CaseRegistrationDate);
                    parmeters.Add("@Bench", objModel.Bench);
                    parmeters.Add("@WACPNo", objModel.WACPNo);
                    parmeters.Add("@PrimarySecondary", objModel.PrimarySecondary);
                    parmeters.Add("@GroupingId", objModel.GroupingId);
                    parmeters.Add("@DateCaseFillingDeptToAG_AAG", objModel.DateCaseFillingDeptToAG_AAG);
                    parmeters.Add("@DateFillingCaseCourtByAG_AAG", objModel.DateFillingCaseCourtByAG_AAG);
                    parmeters.Add("@ApplicationUnderSec5FiledYN", objModel.ApplicationUnderSec5FiledYN == true ? 1 : 0);
                    parmeters.Add("@Remark", objModel.Remark);
                    parmeters.Add("@LinkCaseId", objModel.LinkCaseId);
                    parmeters.Add("@CaseType", Core.CaseType.CaseWithoutCaseNo);

                    var objData = await Con.QueryAsync<CaseRegistrationsResponseModel>("spTrn_CaseRegistrations", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new CaseRegistrationsResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditCaseWithoutCaseNo", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseRegistrationsRepository/AddEditCaseWithoutCaseNo");
                return new CaseRegistrationsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<CaseRegistrationsResponseModel> GetCaseRegistrationDataByCaseId(long CaseId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetCaseRegistrationDataByCaseId");
                    parmeters.Add("@CaseId", CaseId);
                    var objData = await Con.QueryMultipleAsync("spTrn_CaseRegistrations", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResult = objData.Read<object>().FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    CaseRegistrationsResponseModel objReturn = new()
                    {
                        Status = true,
                        Message = "",
                        Data = objResult
                    };                                        
                    return objReturn;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCaseRegistrationDataByCaseId", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseRegistrationsRepository/GetCaseRegistrationDataByCaseId");
                return new CaseRegistrationsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<CaseRegistrationsResponseModel> CheckCaseEntry(CheckCaseEntryModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "CheckCaseEntry");
                    parmeters.Add("@CRNNumber", objModel.CRNNumber);
                    parmeters.Add("@PrimarySecondary", objModel.PrimarySecondary);
                    parmeters.Add("@CaseNo", objModel.CaseNo);
                    parmeters.Add("@CaseYear", objModel.CaseYear);
                    parmeters.Add("@AbbreviationId", objModel.AbbreviationId);
                    parmeters.Add("@CourtId", objModel.CourtId);
                    var objData = await Con.QueryAsync<CaseRegistrationsResponseModel>("spTrn_CaseRegistrations", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new CaseRegistrationsResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "CheckCaseEntry", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseRegistrationsRepository/CheckCaseEntry");
                return new CaseRegistrationsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetLinkCaseList(long LinkCaseId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetLinkCaseList");
                    parmeters.Add("@LinkCaseId", LinkCaseId);
                    var objResult = await Con.QueryAsync("spTrn_CaseRegistrations", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    DisposeCurrentSqlConnection();
                    ResponseWithoutPaginationModel objReturn = new()
                    {
                        Status = true,
                        Message = "",
                        Data = objResult
                    };
                    return objReturn;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetLinkCaseList", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseRegistrationsRepository/GetLinkCaseList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        // Add sandeep 25/07/2025
        public async Task<ResponseModel> GetCaseRegistrationGovtEmpList(CaseRegistrationGovtEmpListFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetCaseGovtEmp");
                    parmeters.Add("@CaseId", objModel.CaseId);
                    parmeters.Add("@CRNNumber", objModel.CRNNumber);
                    parmeters.Add("@SortBy", objModel.SortBy ?? "");
                    parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
                    parmeters.Add("@PageNo", objModel.PageNo);
                    parmeters.Add("@Pagesize", objModel.PageSize);
                    var objResult = await Con.QueryMultipleAsync("spTrn_CaseRegistrationGovtEmp", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetCaseRegistrationGovtEmpList", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseRegistrationsRepository/GetCaseRegistrationGovtEmpList");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> AddEditCaseRegistrationGovtEmp(CaseRegistrationGovtEmpModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();

                    if (objModel.CRGEId > 0)
                    {
                        parmeters.Add("@Action", "EditCaseGovtEmp");
                        parmeters.Add("@LastUpdatedBy", UserId);
                    }
                    else
                    {
                        parmeters.Add("@Action", "AddCaseGovtEmp");
                        parmeters.Add("@CreatedBy", UserId);
                    }
                    parmeters.Add("@CRGEId", objModel.CRGEId);
                    parmeters.Add("@CaseId", objModel.CaseId);
                    parmeters.Add("@CRNNumber", objModel.CRNNumber);
                    parmeters.Add("@EmployeeSSOID", objModel.EmployeeSSOID);
                    parmeters.Add("@EmployeeName", objModel.EmployeeName);
                    parmeters.Add("@EmployeeDesignation", objModel.EmployeeDesignation);
                    parmeters.Add("@EmployeeId", objModel.EmployeeId);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spTrn_CaseRegistrationGovtEmp", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditCaseRegistrationGovtEmp", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseRegistrationsRepository/AddEditCaseRegistrationGovtEmp");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> DeactiveCaseRegistrationGovtEmp(int CRGEId, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "DeactiveCaseGovtEmp");
                    parmeters.Add("@DeleteBy", UserId);
                    parmeters.Add("@CRGEId", CRGEId);

                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spTrn_CaseRegistrationGovtEmp", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeactiveCaseRegistrationGovtEmp", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CaseRegistrationsRepository/DeactiveCaseRegistrationGovtEmp");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        // Add sandeep 25/07/2025
    }
}
