using Case.Dto.DierRegistrations_New;
using Case.Dto.Shared;
using Case.Repository.DierRegistrations_New;
using Common.Dapper;
using Common.Repository;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Case.Repository.DierRegistrations_New
{
    public class DierRegistrations_NewRepository : SqlRepository, IDierRegistrations_NewRepository
    {
        private readonly LogsService _logsService;
        private readonly System.Data.IDbConnection Con;
        public DierRegistrations_NewRepository(IConfiguration Configuration, LogsService logsService) : base(Configuration)
        {
            _logsService = logsService;
        }

        public async Task<ResponseModel> GetDierList(Dier_NewListFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetDierList");
                    parmeters.Add("@CNRNo", objModel.CNRNo);
                    parmeters.Add("@FIRNo", objModel.FIRNo);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@OfficeId", objModel.OfficeId);
                    parmeters.Add("@JCourtId", objModel.JCourtId);
                    parmeters.Add("@RegisterType", objModel.RegisterType);
                    parmeters.Add("@SortBy", objModel.SortBy ?? "");
                    parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
                    parmeters.Add("@PageNo", objModel.PageNo);
                    parmeters.Add("@Pagesize", objModel.PageSize);
                    var objResult = await Con.QueryMultipleAsync("spTrn_DierRegistrations", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetDierList", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrations_NewRepository/GetDierList");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<DierRegistrations_NewResponseModel> AddEditDierRegistrationsSteps1(DierRegistrations_NewSteps1Model objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();

                    if (objModel.DirRegId > 0)
                    {
                        parmeters.Add("@Action", "DierEdit1");
                        parmeters.Add("@UpdatedBy", UserId);
                        parmeters.Add("@DirRegId", objModel.DirRegId);
                    }
                    else
                    {
                        parmeters.Add("@Action", "DierAdd1");
                        parmeters.Add("@CreatedBy", UserId);
                    }

                    parmeters.Add("@Steps", objModel.Steps);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@OfficeId", objModel.OfficeId);
                    parmeters.Add("@JCourtId", objModel.JCourtId);
                    parmeters.Add("@RegisterType", objModel.RegisterType);
                    parmeters.Add("@SearchCaseVia", objModel.SearchCaseVia);
                    parmeters.Add("@DierNo", objModel.DierNo == null ? "" : objModel.DierNo);
                    parmeters.Add("@PoliceStationId", objModel.PoliceStationId);
                    parmeters.Add("@CNRNo", objModel.CNRNo == null ? "" : objModel.CNRNo);
                    parmeters.Add("@FIRNo", objModel.FIRNo == null ? "" : objModel.FIRNo);
                    parmeters.Add("@FIRYear", objModel.FIRYear);

                    var objData = await Con.QueryAsync<DierRegistrations_NewResponseModel>("spTrn_DierRegistrations", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new DierRegistrations_NewResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDierRegistrationsSteps1", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrations_NewRepository/AddEditDierRegistrationsSteps1");
                return new DierRegistrations_NewResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<DierRegistrations_NewResponseModel> AddEditDierRegistrationsSteps2(DierRegistrations_NewSteps2Model objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "DierEdit2");
                    parmeters.Add("@UpdatedBy", UserId);
                    parmeters.Add("@DirRegId", objModel.DirRegId);
                    parmeters.Add("@Steps", objModel.Steps);
                    parmeters.Add("@FIRNo", objModel.FIRNo == null ? "" : objModel.FIRNo);
                    parmeters.Add("@FIRDt", objModel.FIRDt);
                    parmeters.Add("@PSName", objModel.PSName);
                    parmeters.Add("@PSCode", objModel.PSCode);
                    parmeters.Add("@InvestGroupNo", objModel.InvestGroupNo);
                    parmeters.Add("@ChargeSheetNo", objModel.ChargeSheetNo);
                    parmeters.Add("@ChargeSheetDate", objModel.ChargeSheetDate);
                    parmeters.Add("@DateBeforeFillingCourt", objModel.DateBeforeFillingCourt);
                    parmeters.Add("@InvestigatingNameRank", objModel.InvestigatingNameRank);
                    parmeters.Add("@TitleOfCase", objModel.TitleOfCase);
                    parmeters.Add("@CClassificationId", objModel.CClassificationId);
                    parmeters.Add("@CrimeActId", objModel.CrimeActId);
                    parmeters.Add("@CrimeActSubId", objModel.CrimeActSubId);
                    parmeters.Add("@FRNo", objModel.FRNo == null ? "" : objModel.FRNo);
                    parmeters.Add("@FRDate", objModel.FRDate);
                    parmeters.Add("@CourtSubmissionDate", objModel.CourtSubmissionDate);
                    parmeters.Add("@FRStatusID", objModel.FRStatusID);
                    parmeters.Add("@FRStatusName", objModel.FRStatusName);
                    var objData = await Con.QueryAsync<DierRegistrations_NewResponseModel>("spTrn_DierRegistrations", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new DierRegistrations_NewResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDierRegistrationsSteps2", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrations_NewRepository/AddEditDierRegistrationsSteps2");
                return new DierRegistrations_NewResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<DierRegistrations_NewResponseModel> AddEditDierRegistrationsSteps3(DierRegistrations_NewSteps3Model objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "DierEdit3");
                    parmeters.Add("@UpdatedBy", UserId);
                    parmeters.Add("@DirRegId", objModel.DirRegId);
                    parmeters.Add("@Steps", objModel.Steps);
                    parmeters.Add("@IsAccusedType", objModel.IsAccusedType);
                    parmeters.Add("@AccusedGroupNo", objModel.AccusedGroupNo);
                    parmeters.Add("@VictimWitnessGroupNo", objModel.VictimWitnessGroupNo);
                    var objData = await Con.QueryAsync<DierRegistrations_NewResponseModel>("spTrn_DierRegistrations", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new DierRegistrations_NewResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDierRegistrationsSteps3", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrations_NewRepository/AddEditDierRegistrationsSteps3");
                return new DierRegistrations_NewResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<DierRegistrations_NewResponseModel> AddEditDierRegistrationsSteps4(DierRegistrations_NewSteps4Model objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();

                    parmeters.Add("@Action", "DierEdit4");
                    parmeters.Add("@UpdatedBy", UserId);
                    parmeters.Add("@DirRegId", objModel.DirRegId);
                    parmeters.Add("@Steps", objModel.Steps);
                    parmeters.Add("@Remarks", objModel.Remarks == null ? "" : objModel.Remarks);
                    parmeters.Add("@ChargeSheetDocs", objModel.ChargeSheetDocs == null ? "" : objModel.ChargeSheetDocs);
                    parmeters.Add("@FullChargeSheetDocs", objModel.FullChargeSheetDocs == null ? "" : objModel.FullChargeSheetDocs);
                    parmeters.Add("@OtherDocs", objModel.OtherDocs == null ? "" : objModel.OtherDocs);
                    parmeters.Add("@CaseStatus", objModel.CaseStatus == null ? "" : objModel.CaseStatus);
                    var objData = await Con.QueryAsync<DierRegistrations_NewResponseModel>("spTrn_DierRegistrations", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new DierRegistrations_NewResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDierRegistrationsSteps4", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrations_NewRepository/AddEditDierRegistrationsSteps4");
                return new DierRegistrations_NewResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> AddEditDierRegistrations(DierRegistrations_New_OldModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();

                    if (objModel.DirRegId > 0)
                    {
                        parmeters.Add("@Action", "DierEdit");
                        parmeters.Add("@UpdatedBy", UserId);
                        parmeters.Add("@DirRegId", objModel.DirRegId);
                    }
                    else
                    {
                        parmeters.Add("@Action", "DierAdd");
                        parmeters.Add("@CreatedBy", UserId);
                    }
                    parmeters.Add("@TitleOfCase", objModel.TitleOfCase == null ? "" : objModel.TitleOfCase);
                    parmeters.Add("@TitleOfCase", objModel.DierNo == null ? "" : objModel.DierNo);
                    parmeters.Add("@CNRNo", objModel.CNRNo == null ? "" : objModel.CNRNo);
                    parmeters.Add("@FIRNo", objModel.FIRNo == null ? "" : objModel.FIRNo);
                    parmeters.Add("@FIRYear", objModel.FIRYear);
                    parmeters.Add("@PoliceStationId", objModel.PoliceStationId);
                    parmeters.Add("@CClassificationId", objModel.CClassificationId);
                    parmeters.Add("@CrimeActId", objModel.CrimeActId);
                    parmeters.Add("@CrimeActSubId", objModel.CrimeActSubId);
                    parmeters.Add("@FirStatusId", objModel.FirStatusId);
                    parmeters.Add("@AccusedGroupNo", objModel.AccusedGroupNo);
                    parmeters.Add("@VictimGroupNo", objModel.VictimGroupNo);
                    parmeters.Add("@WitnessGroupNo", objModel.WitnessGroupNo);
                    parmeters.Add("@InvestigationDtId", objModel.InvestigationDtId);
                    parmeters.Add("@ChargeSheetNo", objModel.ChargeSheetNo == null ? "" : objModel.ChargeSheetNo);
                    parmeters.Add("@ChargeSheetDate", objModel.ChargeSheetDate == null ? "" : objModel.ChargeSheetDate);
                    parmeters.Add("@DateBeforeFillingCourt", objModel.DateBeforeFillingCourt == null ? "" : objModel.DateBeforeFillingCourt);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@OfficeId", objModel.OfficeId);
                    parmeters.Add("@JCourtId", objModel.JCourtId);
                    parmeters.Add("@IsGovtAccused", objModel.IsGovtAccused == true ? 1 : 0);
                    parmeters.Add("@GovtGroupId", objModel.GovtGroupId);
                    parmeters.Add("@IsConstitutionPost", objModel.IsConstitutionPost == true ? 1 : 0);
                    parmeters.Add("@ConsGroupId", objModel.ConsGroupId);
                    parmeters.Add("@Remarks", objModel.Remarks == null ? "" : objModel.Remarks);
                    parmeters.Add("@ChargeSheetDocs", objModel.ChargeSheetDocs == null ? "" : objModel.ChargeSheetDocs);
                    parmeters.Add("@FullChargeSheetDocs", objModel.FullChargeSheetDocs == null ? "" : objModel.FullChargeSheetDocs);
                    parmeters.Add("@OtherDocs", objModel.OtherDocs == null ? "" : objModel.OtherDocs);
                    parmeters.Add("@CaseStatus", objModel.CaseStatus == null ? "" : objModel.CaseStatus);
                    parmeters.Add("@Steps", objModel.Steps);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spTrn_DierRegistrations", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDierRegistrations", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrations_NewRepository/AddEditDierRegistrations");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


        public async Task<ResponseWithoutPaginationModel> GetDierAccused(long AccusedGroupNo)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetDierAccused");
                    parmeters.Add("@AccusedGroupNo", AccusedGroupNo);
                    var objResult = await Con.QueryAsync("spTrn_DierAccused", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetDierAccused", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrations_NewRepository/GetDierAccused");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> AddEditDierAccused(Dier_NewAccusedModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();

                    if (objModel.AccusedId > 0)
                    {
                        parmeters.Add("@Action", "AccusedEdit");
                        parmeters.Add("@UpdatedBy", UserId);
                    }
                    else
                    {
                        parmeters.Add("@Action", "AccusedAdd");
                        parmeters.Add("@CreatedBy", UserId);
                    }
                    parmeters.Add("@AccusedId", objModel.AccusedId);
                    parmeters.Add("@IsAccusedType", objModel.IsAccusedType);
                    parmeters.Add("@AccusedGroupNo", objModel.AccusedGroupNo);
                    parmeters.Add("@AccuseName", objModel.AccuseName);
                    parmeters.Add("@FatherName", objModel.FatherName);
                    parmeters.Add("@Address", objModel.Address);
                    parmeters.Add("@Age", objModel.Age);
                    parmeters.Add("@Gender", objModel.Gender);
                    parmeters.Add("@FIRStatusId", objModel.FIRStatusId);
                    parmeters.Add("@Remark", objModel.Remark);
                    parmeters.Add("@DepartmentId", objModel.DepartmentId);
                    parmeters.Add("@DepartmentName", objModel.DepartmentName);
                    parmeters.Add("@DesignationId", objModel.DesignationId);
                    parmeters.Add("@DesignationName", objModel.DesignationName);
                    parmeters.Add("@EmpID", objModel.EmpID);
                    parmeters.Add("@JanPratinidhiPostID", objModel.JanPratinidhiPostID);
                    parmeters.Add("@JanPratinidhiPostName", objModel.JanPratinidhiPostName);
                    parmeters.Add("@ConstitutionDT", objModel.ConstitutionDT);
                    parmeters.Add("@IsSanction", objModel.IsSanction == true ? 1 : 0);
                    parmeters.Add("@SanctionDocs", objModel.SanctionDocs);
                    parmeters.Add("@MobileNo", objModel.MobileNo);
                    parmeters.Add("@UIDNo", objModel.UIDNo);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@PsId", objModel.PsId);

                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spTrn_DierAccused", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResult = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResult != null ? objResult : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDierAccused", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrations_NewRepository/AddEditDierAccused");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> DeleteDierAccused(long AccusedId, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "DeleteDierAccused");
                    parmeters.Add("@DeletedBy", UserId);
                    parmeters.Add("@AccusedId", AccusedId);

                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spTrn_DierAccused", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteDierAccused", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrations_NewRepository/DeleteDierAccused");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


        public async Task<ResponseWithoutPaginationModel> GetDierVictimWitness(long GroupNo)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetDierVictimWitness");
                    parmeters.Add("@GroupNo", GroupNo);
                    var objResult = await Con.QueryAsync("spTrn_DierVictimWitness", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetDierVictimWitness", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrations_NewRepository/GetDierVictimWitness");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> AddEditDierVictimWitness(Dier_NewVictimWitnessModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();

                    if (objModel.Id > 0)
                    {
                        parmeters.Add("@Action", "VictimWitnessEdit");
                        parmeters.Add("@UpdatedBy", UserId);
                        parmeters.Add("@Id", objModel.Id);
                    }
                    else
                    {
                        parmeters.Add("@Action", "VictimWitnessAdd");
                        parmeters.Add("@CreatedBy", UserId);
                    }

                    parmeters.Add("@GroupNo", objModel.GroupNo);
                    parmeters.Add("@Name", objModel.Name);
                    parmeters.Add("@FatherName", objModel.FatherName);
                    parmeters.Add("@Gender", objModel.Gender);
                    parmeters.Add("@Address", objModel.Address);
                    parmeters.Add("@MobileNo", objModel.MobileNo);
                    parmeters.Add("@UIDNo", objModel.UIDNo);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@ThanaId", objModel.ThanaId);
                    parmeters.Add("@Status", objModel.Status);

                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spTrn_DierVictimWitness", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResult = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResult != null ? objResult : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDierVictimWitness", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrations_NewRepository/AddEditDierVictimWitness");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> DeleteDierVictimWitness(long Id, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "DeleteDierVictimWitness");
                    parmeters.Add("@DeletedBy", UserId);
                    parmeters.Add("@Id", Id);

                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spTrn_DierVictimWitness", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteDierVictimWitness", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrations_NewRepository/DeleteDierVictimWitness");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


        public async Task<ResponseWithoutPaginationModel> GetDierInvestigation(long InvestGroupNo)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetDierInvestigation");
                    parmeters.Add("@InvestGroupNo", InvestGroupNo);
                    var objResult = await Con.QueryAsync("spTrn_Investigation", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetDierInvestigation", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrations_NewRepository/GetDierInvestigation");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> AddEditDierInvestigation(Dier_NewInvestigationModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();

                    if (objModel.InvestId > 0)
                    {
                        parmeters.Add("@Action", "InvestigationEdit");
                        parmeters.Add("@UpdatedBy", UserId);
                    }
                    else
                    {
                        parmeters.Add("@Action", "InvestigationAdd");
                        parmeters.Add("@CreatedBy", UserId);
                    }
                    parmeters.Add("@InvestId", objModel.InvestId);
                    parmeters.Add("@InvestGroupNo", objModel.InvestGroupNo);
                    parmeters.Add("@InvestName", objModel.InvestName);
                    parmeters.Add("@FatherName", objModel.FatherName);
                    parmeters.Add("@RankName", objModel.RankName);
                    parmeters.Add("@PostingPlace", objModel.PostingPlace);
                    parmeters.Add("@Gender", objModel.Gender);
                    parmeters.Add("@MobileNo", objModel.MobileNo);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@ThanaId", objModel.ThanaId);
                    parmeters.Add("@InvestStatus", objModel.InvestStatus);

                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spTrn_Investigation", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResult = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResult != null ? objResult : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDierInvestigation", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrations_NewRepository/AddEditDierInvestigation");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> DeleteDierInvestigation(long InvestId, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "DeleteDierInvestigation");
                    parmeters.Add("@DeletedBy", UserId);
                    parmeters.Add("@InvestId", InvestId);

                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spTrn_Investigation", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteDierInvestigation", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrations_NewRepository/DeleteDierInvestigation");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


        public async Task<ResponseWithoutPaginationModel> GetDierComplaintAgainstPerson(long ComplaintPerGroupNo)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetDierComplaintAgainstPerson");
                    parmeters.Add("@ComplaintPerGroupNo", ComplaintPerGroupNo);
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
                _logsService.Logs("Error", "GetDierComplaintAgainstPerson", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrations_NewRepository/GetDierComplaintAgainstPerson");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> AddEditDierComplaintAgainstPerson(Dier_NewComplaintAgainstPersonModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();

                    if (objModel.ComplaintPerId > 0)
                    {
                        parmeters.Add("@Action", "ComplaintAgainstPersonEdit");
                        parmeters.Add("@UpdatedBy", UserId);
                    }
                    else
                    {
                        parmeters.Add("@Action", "ComplaintAgainstPersonAdd");
                        parmeters.Add("@CreatedBy", UserId);
                    }
                    parmeters.Add("@ComplaintPerId", objModel.ComplaintPerId);
                    parmeters.Add("@ComplaintPerGroupNo", objModel.ComplaintPerGroupNo);
                    parmeters.Add("@ComplaintPerName", objModel.ComplaintPerName);
                    parmeters.Add("@Address", objModel.Address);
                    parmeters.Add("@MobileNo", objModel.MobileNo);
                    parmeters.Add("@UIDNo", objModel.UIDNo);
                    parmeters.Add("@EmpID", objModel.EmpID);
                    parmeters.Add("@Institution", objModel.Institution);

                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spTrn_CaseAppellants", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResult = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResult != null ? objResult : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDierComplaintAgainstPerson", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrations_NewRepository/AddEditDierComplaintAgainstPerson");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> DeleteDierComplaintAgainstPerson(long ComplaintPerId, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "DeleteDierComplaintAgainstPerson");
                    parmeters.Add("@DeletedBy", UserId);
                    parmeters.Add("@ComplaintPerId", ComplaintPerId);

                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spTrn_CaseAppellants", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteDierComplaintAgainstPerson", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrations_NewRepository/DeleteDierComplaintAgainstPerson");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


        public async Task<ResponseWithoutPaginationModel> GetOffenceClassification(long OffenceClassifGroupNo)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetOffence");
                    parmeters.Add("@OffenceClassifGroupNo", OffenceClassifGroupNo);
                    var objResult = await Con.QueryAsync("spTrn_OffenceClassification", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetOffenceClassification", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrations_NewRepository/GetOffenceClassification");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<DierRegistrations_NewResponseModel> AddEditOffenceClassification(OffenceClassification_NewModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();

                    if (objModel.OffenceClassifId > 0)
                    {
                        parmeters.Add("@Action", "EditOffence");
                        parmeters.Add("@UpdatedBy", UserId);
                    }
                    else
                    {
                        parmeters.Add("@Action", "AddOffence");
                        parmeters.Add("@CreatedBy", UserId);
                    }
                    parmeters.Add("@OffenceClassifId", objModel.OffenceClassifId);
                    parmeters.Add("@OffenceClassifGroupNo", objModel.OffenceClassifGroupNo);
                    parmeters.Add("@IsCaseComplaintReg", objModel.IsCaseComplaintReg);
                    parmeters.Add("@ClassificationID", objModel.ClassificationID);
                    parmeters.Add("@ClassificationName", objModel.ClassificationName);
                    parmeters.Add("@ActsID", objModel.ActsID);
                    parmeters.Add("@ActsName", objModel.ActsName);
                    parmeters.Add("@SectionsID", objModel.SectionsID);
                    parmeters.Add("@SectionsName", objModel.SectionsName);

                    var objData = await Con.QueryAsync<DierRegistrations_NewResponseModel>("spTrn_OffenceClassification", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResult = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResult != null ? objResult : new DierRegistrations_NewResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditOffenceClassification", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrations_NewRepository/AddEditOffenceClassification");
                return new DierRegistrations_NewResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<DierRegistrations_NewResponseModel> DeleteOffenceClassification(long OffenceClassifId, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "DeleteOffence");
                    parmeters.Add("@DeletedBy", UserId);
                    parmeters.Add("@OffenceClassifId", OffenceClassifId);

                    var objData = await Con.QueryAsync<DierRegistrations_NewResponseModel>("spTrn_OffenceClassification", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new DierRegistrations_NewResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteOffenceClassification", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrations_NewRepository/DeleteOffenceClassification");
                return new DierRegistrations_NewResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


        public async Task<DierRegistrations_NewResponseModel> AddEditCompleteDierRegistrationsSteps2(DierRegistrations_NewSteps2Model objModel, int userId)
        {
            try
            {
                using (var con = GetOpenConnection())
                {
                    // STEP 1: Save Main Dier Step2 Data
                    var mainResult = await AddEditDierRegistrationsSteps2(objModel, userId);

                    if (mainResult == null || !mainResult.Status)
                    {
                        return new DierRegistrations_NewResponseModel
                        {
                            Status = false,
                            Message = "Error in saving main Dier data"
                        };
                    }

                    //long investGroupNo = objModel.InvestGroupNo ?? 0;
                    //long offenceGroupNo = 0;

                    // STEP 2: Save Investigation List
                    if (objModel.DierInvestigationDetails != null && objModel.DierInvestigationDetails.Count > 0)
                    {
                        foreach (var item in objModel.DierInvestigationDetails)
                        {
                            //item.InvestGroupNo = investGroupNo;

                            var investResult = await AddEditDierInvestigation(item, userId);

                            if (investResult == null || !investResult.Status)
                            {
                                return new DierRegistrations_NewResponseModel
                                {
                                    Status = false,
                                    Message = "Error in saving Investigation data"
                                };
                            }
                        }
                    }

                    // STEP 3: Save Offence Classification List
                    if (objModel.OffenceClassificationDetails != null && objModel.OffenceClassificationDetails.Count > 0)
                    {
                        foreach (var item in objModel.OffenceClassificationDetails)
                        {
                            //item.OffenceClassifGroupNo = offenceGroupNo;

                            var offenceResult = await AddEditOffenceClassification(item, userId);

                            if (offenceResult == null || !offenceResult.Status)
                            {
                                return new DierRegistrations_NewResponseModel
                                {
                                    Status = false,
                                    Message = "Error in saving Offence Classification data"
                                };
                            }
                        }
                    }
                    return mainResult != null ? mainResult : new DierRegistrations_NewResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditCompleteDierRegistrationsSteps2", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrations_NewRepository/AddEditCompleteDierRegistrationsSteps2");

                return new DierRegistrations_NewResponseModel
                {
                    Status = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<DierRegistrations_NewResponseModel> AddEditCompleteDierRegistrationsStep3(DierRegistrations_NewSteps3Model objModel, int userId)
        {
            try
            {
                using (var con = GetOpenConnection())
                {
                    // ================= STEP 1: Save Main Step3 =================
                    var mainResult = await AddEditDierRegistrationsSteps3(objModel, userId);

                    if (mainResult == null || !mainResult.Status)
                    {
                        return new DierRegistrations_NewResponseModel
                        {
                            Status = false,
                            Message = "Error in saving Step3 main data"
                        };
                    }

                    //long accusedGroupNo = objModel.AccusedGroupNo ?? 0;
                    //long victimGroupNo = objModel.VictimWitnessGroupNo ?? 0;

                    // ================= STEP 2: Save Accused List =================
                    if (objModel.DierAccusedModel != null && objModel.DierAccusedModel.Count > 0)
                    {
                        foreach (var item in objModel.DierAccusedModel)
                        {
                            //item.AccusedGroupNo = accusedGroupNo;
                            //item.IsAccusedType = objModel.IsAccusedType ?? 0;

                            var accusedResult = await AddEditDierAccused(item, userId);

                            if (accusedResult == null || !accusedResult.Status)
                            {
                                return new DierRegistrations_NewResponseModel
                                {
                                    Status = false,
                                    Message = "Error in saving Accused data"
                                };
                            }
                        }
                    }

                    // ================= STEP 3: Save Victim/Witness List =================
                    if (objModel.DierVictimWitnessDetails != null && objModel.DierVictimWitnessDetails.Count > 0)
                    {
                        foreach (var item in objModel.DierVictimWitnessDetails)
                        {
                            //item.GroupNo = victimGroupNo;

                            var victimResult = await AddEditDierVictimWitness(item, userId);

                            if (victimResult == null || !victimResult.Status)
                            {
                                return new DierRegistrations_NewResponseModel
                                {
                                    Status = false,
                                    Message = "Error in saving Victim/Witness data"
                                };
                            }
                        }
                    }
                    return mainResult != null ? mainResult : new DierRegistrations_NewResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditCompleteDierRegistrationsStep3", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrations_NewRepository/AddEditCompleteDierRegistrationsStep3");

                return new DierRegistrations_NewResponseModel
                {
                    Status = false,
                    Message = ex.Message
                };
            }
        }


        public async Task<DierRegistrations_NewResponseModel> AddEditCompleteDierRegistrationsFinal(DierRegistrations_NewModel objModel, int userId)
        {
            try
            {
                using (var con = GetOpenConnection())
                using (var transaction = con.BeginTransaction())
                {
                    try
                    {
                        var parameters = new DynamicParameters();

                        // ================= STEP 1 =================
                        if (objModel.DirRegId > 0)
                        {
                            parameters.Add("@Action", "DierEdit1");
                            parameters.Add("@UpdatedBy", userId);
                            parameters.Add("@DirRegId", objModel.DirRegId);
                        }
                        else
                        {
                            parameters.Add("@Action", "DierAdd1");
                            parameters.Add("@CreatedBy", userId);
                        }

                        parameters.Add("@Steps", objModel.Steps);
                        parameters.Add("@DistrictId", objModel.DistrictId);
                        parameters.Add("@OfficeId", objModel.OfficeId);
                        parameters.Add("@JCourtId", objModel.JCourtId);
                        parameters.Add("@RegisterType", objModel.RegisterType);
                        parameters.Add("@SearchCaseVia", objModel.SearchCaseVia);
                        parameters.Add("@DierNo", objModel.DierNo ?? "");
                        parameters.Add("@PoliceStationId", objModel.PoliceStationId);
                        parameters.Add("@CNRNo", objModel.CNRNo ?? "");
                        parameters.Add("@FIRNo", objModel.FIRNo ?? "");
                        parameters.Add("@FIRYear", objModel.FIRYear);

                        var step1 = (await con.QueryAsync<DierRegistrations_NewResponseModel>("spTrn_DierRegistrations", parameters, transaction, commandType: CommandType.StoredProcedure)).FirstOrDefault();

                        if (step1 == null || !step1.Status)
                            throw new Exception("Step1 failed");

                        long dirRegId = step1.ReturnID;

                        // ================= STEP 2 =================
                        parameters = new DynamicParameters();
                        parameters.Add("@Action", "DierEdit2");
                        parameters.Add("@UpdatedBy", userId);
                        parameters.Add("@DirRegId", dirRegId);
                        parameters.Add("@Steps", objModel.Steps);
                        parameters.Add("@FIRNo", objModel.FIRNo ?? "");
                        parameters.Add("@FIRDt", objModel.FIRDt);
                        parameters.Add("@PSName", objModel.PSName);
                        parameters.Add("@PSCode", objModel.PSCode);
                        parameters.Add("@InvestGroupNo", objModel.InvestGroupNo);
                        parameters.Add("@ChargeSheetNo", objModel.ChargeSheetNo);
                        parameters.Add("@ChargeSheetDate", objModel.ChargeSheetDate);
                        parameters.Add("@DateBeforeFillingCourt", objModel.DateBeforeFillingCourt);
                        parameters.Add("@InvestigatingNameRank", objModel.InvestigatingNameRank);
                        parameters.Add("@TitleOfCase", objModel.TitleOfCase);
                        parameters.Add("@CClassificationId", objModel.CClassificationId);
                        parameters.Add("@CrimeActId", objModel.CrimeActId);
                        parameters.Add("@CrimeActSubId", objModel.CrimeActSubId);
                        parameters.Add("@FRNo", objModel.FRNo ?? "");
                        parameters.Add("@FRDate", objModel.FRDate);
                        parameters.Add("@CourtSubmissionDate", objModel.CourtSubmissionDate);
                        parameters.Add("@FRStatusID", objModel.FRStatusID);
                        parameters.Add("@FRStatusName", objModel.FRStatusName);

                        var step2 = (await con.QueryAsync<DierRegistrations_NewResponseModel>("spTrn_DierRegistrations", parameters, transaction, commandType: CommandType.StoredProcedure)).FirstOrDefault();

                        if (step2 == null || !step2.Status)
                            throw new Exception("Step2 failed");

                        // ================= INVESTIGATION =================
                        if (objModel.DierInvestigationDetails != null)
                        {
                            foreach (var item in objModel.DierInvestigationDetails)
                            {
                                var p = new DynamicParameters();

                                p.Add("@Action", item.InvestId > 0 ? "InvestigationEdit" : "InvestigationAdd");
                                p.Add(item.InvestId > 0 ? "@UpdatedBy" : "@CreatedBy", userId);
                                p.Add("@InvestId", item.InvestId);
                                p.Add("@InvestGroupNo", item.InvestGroupNo);
                                p.Add("@InvestName", item.InvestName);
                                p.Add("@FatherName", item.FatherName);
                                p.Add("@RankName", item.RankName);
                                p.Add("@PostingPlace", item.PostingPlace);
                                p.Add("@Gender", item.Gender);
                                p.Add("@MobileNo", item.MobileNo);
                                p.Add("@DistrictId", item.DistrictId);
                                p.Add("@ThanaId", item.ThanaId);
                                p.Add("@InvestStatus", item.InvestStatus);

                                var res = (await con.QueryAsync<ResponseWithoutPaginationModel>("spTrn_Investigation", p, transaction, commandType: CommandType.StoredProcedure)).FirstOrDefault();

                                if (res == null || !res.Status)
                                    throw new Exception("Investigation failed");
                            }
                        }

                        // ================= OFFENCE =================
                        if (objModel.OffenceClassificationDetails != null)
                        {
                            foreach (var item in objModel.OffenceClassificationDetails)
                            {
                                var p = new DynamicParameters();

                                p.Add("@Action", item.OffenceClassifId > 0 ? "EditOffence" : "AddOffence");
                                p.Add(item.OffenceClassifId > 0 ? "@UpdatedBy" : "@CreatedBy", userId);
                                p.Add("@OffenceClassifId", item.OffenceClassifId);
                                p.Add("@OffenceClassifGroupNo", item.OffenceClassifGroupNo);
                                p.Add("@IsCaseComplaintReg", item.IsCaseComplaintReg);
                                p.Add("@ClassificationID", item.ClassificationID);
                                p.Add("@ClassificationName", item.ClassificationName);
                                p.Add("@ActsID", item.ActsID);
                                p.Add("@ActsName", item.ActsName);
                                p.Add("@SectionsID", item.SectionsID);
                                p.Add("@SectionsName", item.SectionsName);

                                var res = (await con.QueryAsync<ResponseWithoutPaginationModel>("spTrn_OffenceClassification", p, transaction, commandType: CommandType.StoredProcedure)).FirstOrDefault();

                                if (res == null || !res.Status)
                                    throw new Exception("Offence failed");
                            }
                        }

                        // ================= STEP 3 =================
                        parameters = new DynamicParameters();
                        parameters.Add("@Action", "DierEdit3");
                        parameters.Add("@UpdatedBy", userId);
                        parameters.Add("@DirRegId", dirRegId);
                        parameters.Add("@Steps", objModel.Steps);
                        parameters.Add("@IsAccusedType", objModel.IsAccusedType);
                        parameters.Add("@AccusedGroupNo", objModel.AccusedGroupNo);
                        parameters.Add("@VictimWitnessGroupNo", objModel.VictimWitnessGroupNo);

                        var step3 = (await con.QueryAsync<DierRegistrations_NewResponseModel>("spTrn_DierRegistrations", parameters, transaction, commandType: CommandType.StoredProcedure)).FirstOrDefault();

                        if (step3 == null || !step3.Status)
                            throw new Exception("Step3 failed");

                        // ================= ACCUSED =================
                        if (objModel.DierAccusedModel != null)
                        {
                            foreach (var item in objModel.DierAccusedModel)
                            {
                                var p = new DynamicParameters();

                                p.Add("@Action", item.AccusedId > 0 ? "AccusedEdit" : "AccusedAdd");
                                p.Add(item.AccusedId > 0 ? "@UpdatedBy" : "@CreatedBy", userId);
                                p.Add("@AccusedId", item.AccusedId);
                                p.Add("@IsAccusedType", item.IsAccusedType);
                                p.Add("@AccusedGroupNo", item.AccusedGroupNo);
                                p.Add("@AccuseName", item.AccuseName);
                                p.Add("@FatherName", item.FatherName);
                                p.Add("@Address", item.Address);
                                p.Add("@Age", item.Age);
                                p.Add("@Gender", item.Gender);
                                p.Add("@FIRStatusId", item.FIRStatusId);
                                p.Add("@Remark", item.Remark);
                                p.Add("@DepartmentId", item.DepartmentId);
                                p.Add("@DepartmentName", item.DepartmentName);
                                p.Add("@DesignationId", item.DesignationId);
                                p.Add("@DesignationName", item.DesignationName);
                                p.Add("@EmpID", item.EmpID);
                                p.Add("@JanPratinidhiPostID", item.JanPratinidhiPostID);
                                p.Add("@JanPratinidhiPostName", item.JanPratinidhiPostName);
                                p.Add("@ConstitutionDT", item.ConstitutionDT);
                                p.Add("@IsSanction", item.IsSanction == true ? 1 : 0);
                                p.Add("@SanctionDocs", item.SanctionDocs);
                                p.Add("@MobileNo", item.MobileNo);
                                p.Add("@UIDNo", item.UIDNo);
                                p.Add("@DistrictId", item.DistrictId);
                                p.Add("@PsId", item.PsId);

                                var res = (await con.QueryAsync<ResponseWithoutPaginationModel>("spTrn_DierAccused", p, transaction, commandType: CommandType.StoredProcedure)).FirstOrDefault();

                                if (res == null || !res.Status)
                                    throw new Exception("Accused failed");
                            }
                        }

                        // ================= VICTIM =================
                        if (objModel.DierVictimWitnessDetails != null)
                        {
                            foreach (var item in objModel.DierVictimWitnessDetails)
                            {
                                var p = new DynamicParameters();

                                p.Add("@Action", item.Id > 0 ? "VictimWitnessEdit" : "VictimWitnessAdd");
                                p.Add(item.Id > 0 ? "@UpdatedBy" : "@CreatedBy", userId);
                                p.Add("@Id", item.Id);
                                p.Add("@GroupNo", item.GroupNo);
                                p.Add("@Name", item.Name);
                                p.Add("@FatherName", item.FatherName);
                                p.Add("@Gender", item.Gender);
                                p.Add("@Address", item.Address);
                                p.Add("@MobileNo", item.MobileNo);
                                p.Add("@UIDNo", item.UIDNo);
                                p.Add("@DistrictId", item.DistrictId);
                                p.Add("@ThanaId", item.ThanaId);
                                p.Add("@Status", item.Status);

                                var res = (await con.QueryAsync<ResponseWithoutPaginationModel>("spTrn_DierVictimWitness", p, transaction, commandType: CommandType.StoredProcedure)).FirstOrDefault();

                                if (res == null || !res.Status)
                                    throw new Exception("Victim failed");
                            }
                        }

                        // ================= STEP 4 =================
                        parameters = new DynamicParameters();
                        parameters.Add("@Action", "DierEdit4");
                        parameters.Add("@UpdatedBy", userId);
                        parameters.Add("@DirRegId", dirRegId);
                        parameters.Add("@Steps", objModel.Steps);
                        parameters.Add("@Remarks", objModel.Remarks ?? "");
                        parameters.Add("@ChargeSheetDocs", objModel.ChargeSheetDocs ?? "");
                        parameters.Add("@FullChargeSheetDocs", objModel.FullChargeSheetDocs ?? "");
                        parameters.Add("@OtherDocs", objModel.OtherDocs ?? "");
                        parameters.Add("@CaseStatus", objModel.CaseStatus ?? "");

                        var step4 = (await con.QueryAsync<DierRegistrations_NewResponseModel>("spTrn_DierRegistrations", parameters, transaction, commandType: CommandType.StoredProcedure)).FirstOrDefault();

                        if (step4 == null || !step4.Status)
                            throw new Exception("Step4 failed");

                        transaction.Commit();
                        return step4 != null ? step4 : new DierRegistrations_NewResponseModel();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditCompleteDierRegistrationsFinal", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrations_NewRepository/AddEditCompleteDierRegistrationsFinal");

                return new DierRegistrations_NewResponseModel
                {
                    Status = false,
                    Message = ex.Message
                };
            }
        }

    }
}
