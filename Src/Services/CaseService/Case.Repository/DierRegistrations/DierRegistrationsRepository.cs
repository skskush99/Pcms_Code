using Case.Dto.DierRegistrations;
using Case.Dto.Shared;
using Common.Dapper;
using Common.Repository;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Case.Repository.DierRegistrations
{
    public class DierRegistrationsRepository : SqlRepository, IDierRegistrationsRepository
    {
        private readonly LogsService _logsService;
        private readonly System.Data.IDbConnection Con;
        public DierRegistrationsRepository(IConfiguration Configuration, LogsService logsService) : base(Configuration)
        {
            _logsService = logsService;
        }

        public async Task<ResponseModel> GetDierList(DierListFilterModel objModel)
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
                _logsService.Logs("Error", "GetDierList", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrationsRepository/GetDierList");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<DierRegistrationsResponseModel> AddEditDierRegistrationsSteps1(DierRegistrationsSteps1Model objModel, int UserId)
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
                    var objData = await Con.QueryAsync<DierRegistrationsResponseModel>("spTrn_DierRegistrations", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new DierRegistrationsResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDierRegistrationsSteps1", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrationsRepository/AddEditDierRegistrationsSteps1");
                return new DierRegistrationsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<DierRegistrationsResponseModel> AddEditDierRegistrationsSteps2(DierRegistrationsSteps2Model objModel, int UserId)
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
                    var objData = await Con.QueryAsync<DierRegistrationsResponseModel>("spTrn_DierRegistrations", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new DierRegistrationsResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDierRegistrationsSteps2", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrationsRepository/AddEditDierRegistrationsSteps2");
                return new DierRegistrationsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<DierRegistrationsResponseModel> AddEditDierRegistrationsSteps3(DierRegistrationsSteps3Model objModel, int UserId)
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
                    var objData = await Con.QueryAsync<DierRegistrationsResponseModel>("spTrn_DierRegistrations", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new DierRegistrationsResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDierRegistrationsSteps3", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrationsRepository/AddEditDierRegistrationsSteps3");
                return new DierRegistrationsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<DierRegistrationsResponseModel> AddEditDierRegistrationsSteps4(DierRegistrationsSteps4Model objModel, int UserId)
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
                    var objData = await Con.QueryAsync<DierRegistrationsResponseModel>("spTrn_DierRegistrations", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new DierRegistrationsResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDierRegistrationsSteps4", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrationsRepository/AddEditDierRegistrationsSteps4");
                return new DierRegistrationsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> AddEditDierRegistrations(DierRegistrations_OldModel objModel, int UserId)
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
                _logsService.Logs("Error", "AddEditDierRegistrations", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrationsRepository/AddEditDierRegistrations");
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
                _logsService.Logs("Error", "GetDierAccused", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrationsRepository/GetDierAccused");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> AddEditDierAccused(DierAccusedModel objModel, int UserId)
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
                    parmeters.Add("@AccusedGroupNo", objModel.AccusedGroupNo);
                    parmeters.Add("@AccuseName", objModel.AccuseName);
                    parmeters.Add("@FatherName", objModel.FatherName);
                    parmeters.Add("@Gender", objModel.Gender);
                    parmeters.Add("@Address", objModel.Address);
                    parmeters.Add("@MobileNo", objModel.MobileNo);
                    parmeters.Add("@UIDNo", objModel.UIDNo);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@ThanaId", objModel.ThanaId);
                    parmeters.Add("@FIRStatusId", objModel.FIRStatusId);

                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spTrn_DierAccused", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResult = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResult != null ? objResult : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDierAccused", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrationsRepository/AddEditDierAccused");
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
                _logsService.Logs("Error", "DeleteDierAccused", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrationsRepository/DeleteDierAccused");
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
                _logsService.Logs("Error", "GetDierVictimWitness", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrationsRepository/GetDierVictimWitness");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> AddEditDierVictimWitness(DierVictimWitnessModel objModel, int UserId)
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
                _logsService.Logs("Error", "AddEditDierVictimWitness", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrationsRepository/AddEditDierVictimWitness");
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
                _logsService.Logs("Error", "DeleteDierVictimWitness", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrationsRepository/DeleteDierVictimWitness");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetDierVictim(long VictimGroupNo)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetDierVictim");
                    parmeters.Add("@VictimGroupNo", VictimGroupNo);
                    var objResult = await Con.QueryAsync("spTrn_DierVictim", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetDierVictim", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrationsRepository/GetDierVictim");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> AddEditDierVictim(DierVictimModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();

                    if (objModel.VictimId > 0)
                    {
                        parmeters.Add("@Action", "VictimEdit");
                        parmeters.Add("@UpdatedBy", UserId);
                    }
                    else
                    {
                        parmeters.Add("@Action", "VictimAdd");
                        parmeters.Add("@CreatedBy", UserId);
                    }
                    parmeters.Add("@VictimId", objModel.VictimId);
                    parmeters.Add("@VictimGroupNo", objModel.VictimGroupNo);
                    parmeters.Add("@VictimName", objModel.VictimName);
                    parmeters.Add("@FatherName", objModel.FatherName);
                    parmeters.Add("@Gender", objModel.Gender);
                    parmeters.Add("@Address", objModel.Address);
                    parmeters.Add("@MobileNo", objModel.MobileNo);
                    parmeters.Add("@UIDNo", objModel.UIDNo);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@ThanaId", objModel.ThanaId);
                    parmeters.Add("@VictimStatus", objModel.VictimStatus);

                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spTrn_DierVictim", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResult = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResult != null ? objResult : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDierVictim", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrationsRepository/AddEditDierVictim");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> DeleteDierVictim(long VictimId, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "DeleteDierVictim");
                    parmeters.Add("@DeletedBy", UserId);
                    parmeters.Add("@VictimId", VictimId);

                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spTrn_DierVictim", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteDierVictim", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrationsRepository/DeleteDierVictim");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetDierWitness(long WitnessGroupNo)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetDierWitness");
                    parmeters.Add("@WitnessGroupNo", WitnessGroupNo);
                    var objResult = await Con.QueryAsync("spTrn_DierWitness", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetDierWitness", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrationsRepository/GetDierWitness");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> AddEditDierWitness(DierWitnessModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();

                    if (objModel.WitnessId > 0)
                    {
                        parmeters.Add("@Action", "WitnessEdit");
                        parmeters.Add("@UpdatedBy", UserId);
                    }
                    else
                    {
                        parmeters.Add("@Action", "WitnessAdd");
                        parmeters.Add("@CreatedBy", UserId);
                    }
                    parmeters.Add("@WitnessId", objModel.WitnessId);
                    parmeters.Add("@WitnessGroupNo", objModel.WitnessGroupNo);
                    parmeters.Add("@WitnessName", objModel.WitnessName);
                    parmeters.Add("@FatherName", objModel.FatherName);
                    parmeters.Add("@Gender", objModel.Gender);
                    parmeters.Add("@Address", objModel.Address);
                    parmeters.Add("@MobileNo", objModel.MobileNo);
                    parmeters.Add("@UIDNo", objModel.UIDNo);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@ThanaId", objModel.ThanaId);
                    parmeters.Add("@WitnessStatus", objModel.WitnessStatus);

                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spTrn_DierWitness", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResult = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResult != null ? objResult : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDierWitness", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrationsRepository/AddEditDierWitness");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> DeleteDierWitness(long VictimId, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "DeleteDierWitness");
                    parmeters.Add("@DeletedBy", UserId);
                    parmeters.Add("@VictimId", VictimId);

                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spTrn_DierWitness", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteDierWitness", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrationsRepository/DeleteDierWitness");
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
                _logsService.Logs("Error", "GetDierInvestigation", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrationsRepository/GetDierInvestigation");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> AddEditDierInvestigation(DierInvestigationModel objModel, int UserId)
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
                _logsService.Logs("Error", "AddEditDierInvestigation", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrationsRepository/AddEditDierInvestigation");
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
                _logsService.Logs("Error", "DeleteDierInvestigation", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrationsRepository/DeleteDierInvestigation");
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
                _logsService.Logs("Error", "GetDierComplaintAgainstPerson", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrationsRepository/GetDierComplaintAgainstPerson");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> AddEditDierComplaintAgainstPerson(DierComplaintAgainstPersonModel objModel, int UserId)
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
                _logsService.Logs("Error", "AddEditDierComplaintAgainstPerson", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrationsRepository/AddEditDierComplaintAgainstPerson");
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
                _logsService.Logs("Error", "DeleteDierComplaintAgainstPerson", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/DierRegistrationsRepository/DeleteDierComplaintAgainstPerson");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


    }
}
