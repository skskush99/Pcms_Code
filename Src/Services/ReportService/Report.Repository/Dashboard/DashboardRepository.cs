using Common.Dapper;
using Common.Repository;
using Dapper;
using Microsoft.Extensions.Configuration;
using Report.Dto.Dashboard;
using Report.Repository.Global;
using System.Data;
namespace Report.Repository.Dashboard
{
    public class DashboardRepository : SqlRepository, IDashboardRepository
    {
        private readonly System.Data.IDbConnection Con;
        private readonly LogsService _logsService;
        public DashboardRepository(IConfiguration Configuration, LogsService logsService) : base(Configuration)
        {
            _logsService = logsService;
        }

        public async Task<DashboardDataResponseModel> GetDashboardData(DashboardFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetDashboardData");
                    parmeters.Add("@AdmDepttId", objModel.AdmDepttId);
                    parmeters.Add("@UnitId", objModel.UnitId);
                    parmeters.Add("@OfficeId", objModel.OfficeId);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@OICId", objModel.OICId);
                    parmeters.Add("@LawyerId", objModel.LawyerId);
                    parmeters.Add("@Status", objModel.Status);
                    parmeters.Add("@PrimarySecondary", objModel.PrimarySecondary);
                    parmeters.Add("@RoleId", objModel.RoleId);
                    var objResult = await Con.QueryMultipleAsync("spDashboard", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objCaseData = objResult.Read<object>();
                    var objCaseEntryStatusData = objResult.Read<object>();
                    var objCasePriorityWiseData = objResult.Read<object>();
                    var objCaseCourtWiseData = objResult.Read<object>();
                    DashboardDataResponseModel objResut = new()
                    {
                        Status = true,
                        Message = "",
                        CaseData = objCaseData.FirstOrDefault(),
                        CaseEntryStatusData = objCaseEntryStatusData.FirstOrDefault(),
                        CasePriorityWiseData = objCasePriorityWiseData.FirstOrDefault(),
                        CaseCourtWiseData = objCaseCourtWiseData.FirstOrDefault(),
                    };
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDashboardData", ex.Message, ex.StackTrace, ex.Source, "ReportService/ReportService.Repository/DashboardRepository/GetDashboardData");
                return new DashboardDataResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        
        public async Task<DashboardResponseModel> GetPendingReportCourtWise(DashboardFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetPendingReportCourtWise");
                    parmeters.Add("@AdmDepttId", objModel.AdmDepttId);
                    parmeters.Add("@UnitId", objModel.UnitId);
                    parmeters.Add("@OfficeId", objModel.OfficeId);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@OICId", objModel.OICId);
                    parmeters.Add("@LawyerId", objModel.LawyerId);
                    parmeters.Add("@Status", objModel.Status);
                    parmeters.Add("@PrimarySecondary", objModel.PrimarySecondary);
                    parmeters.Add("@RoleId", objModel.RoleId);
                    var objResult = await Con.QueryMultipleAsync("spDashboard", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    DashboardResponseModel objResut = new()
                    {
                        Status = true,
                        Message = "",
                        Data = objResult.Read<object>()
                    };
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetPendingReportCourtWise", ex.Message, ex.StackTrace, ex.Source, "ReportService/ReportService.Repository/DashboardRepository/GetPendingReportCourtWise");
                return new DashboardResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<DashboardResponseModel> GetPendingReportDistrictWise(DashboardFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetPendingReportDistrictWise");
                    parmeters.Add("@AdmDepttId", objModel.AdmDepttId);
                    parmeters.Add("@UnitId", objModel.UnitId);
                    parmeters.Add("@OfficeId", objModel.OfficeId);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@OICId", objModel.OICId);
                    parmeters.Add("@LawyerId", objModel.LawyerId);
                    parmeters.Add("@Status", objModel.Status);
                    parmeters.Add("@PrimarySecondary", objModel.PrimarySecondary);
                    parmeters.Add("@RoleId", objModel.RoleId);
                    var objResult = await Con.QueryMultipleAsync("spDashboard", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    DashboardResponseModel objResut = new()
                    {
                        Status = true,
                        Message = "",
                        Data = objResult.Read<object>()
                    };
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetPendingReportDistrictWise", ex.Message, ex.StackTrace, ex.Source, "ReportService/ReportService.Repository/DashboardRepository/GetPendingReportDistrictWise");
                return new DashboardResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<DashboardResponseModel> GetPendingReportDepartmentWise(DashboardFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetPendingReportDepartmentWise");
                    parmeters.Add("@AdmDepttId", objModel.AdmDepttId);
                    parmeters.Add("@UnitId", objModel.UnitId);
                    parmeters.Add("@OfficeId", objModel.OfficeId);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@OICId", objModel.OICId);
                    parmeters.Add("@LawyerId", objModel.LawyerId);
                    parmeters.Add("@Status", objModel.Status);
                    parmeters.Add("@PrimarySecondary", objModel.PrimarySecondary);
                    parmeters.Add("@RoleId", objModel.RoleId);
                    var objResult = await Con.QueryMultipleAsync("spDashboard", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    DashboardResponseModel objResut = new()
                    {
                        Status = true,
                        Message = "",
                        Data = objResult.Read<object>()
                    };
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetPendingReportDepartmentWise", ex.Message, ex.StackTrace, ex.Source, "ReportService/ReportService.Repository/DashboardRepository/GetPendingReportDepartmentWise");
                return new DashboardResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<DashboardResponseModel> GetPendingReportOfficeWise(DashboardFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetPendingReportOfficeWise");
                    parmeters.Add("@AdmDepttId", objModel.AdmDepttId);
                    parmeters.Add("@UnitId", objModel.UnitId);
                    parmeters.Add("@OfficeId", objModel.OfficeId);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@OICId", objModel.OICId);
                    parmeters.Add("@LawyerId", objModel.LawyerId);
                    parmeters.Add("@Status", objModel.Status);
                    parmeters.Add("@PrimarySecondary", objModel.PrimarySecondary);
                    parmeters.Add("@RoleId", objModel.RoleId);
                    var objResult = await Con.QueryMultipleAsync("spDashboard", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    DashboardResponseModel objResut = new()
                    {
                        Status = true,
                        Message = "",
                        Data = objResult.Read<object>()
                    };
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetPendingReportOfficeWise", ex.Message, ex.StackTrace, ex.Source, "ReportService/ReportService.Repository/DashboardRepository/GetPendingReportOfficeWise");
                return new DashboardResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        

        public Trn_CaseRegistrations GetCaseDetails(Nullable<int> CaseId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@CaseId", CaseId);
                    var objResult = Con.QueryMultiple("sp_GetCasesDetail", parameters, commandType: CommandType.StoredProcedure);

                    // Retrieve the main case registration information
                    var caseRegistration = objResult.ReadFirstOrDefault<Trn_CaseRegistrations>();

                    if (caseRegistration == null)
                    {
                        return new Trn_CaseRegistrations();
                    }

                    // Retrieve the case hearing list
                    var caseHearings = objResult.Read<CaseHearing>().ToList();

                    // Retrieve the case decision list
                    var caseDecisions = objResult.Read<CaseDecision>().ToList();

                    // Retrieve the case contempt list
                    var caseContempts = objResult.Read<CaseContempt>().ToList();

                    // Assign the lists to the case registration object (if necessary)
                    caseRegistration.CaseHearings = caseHearings;
                    caseRegistration.CaseDecisions = caseDecisions;
                    caseRegistration.CaseContempts = caseContempts;

                    DisposeCurrentSqlConnection();

                    return caseRegistration;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DashboardResponseModel> GetPendingDetailReport(PendingDetailReportFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetPendingDetailReport");
                    parmeters.Add("@AdmDepttId", objModel.AdmDepttId);
                    parmeters.Add("@UnitId", objModel.UnitId);
                    parmeters.Add("@OfficeId", objModel.OfficeId);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@OICId", objModel.OICId);
                    parmeters.Add("@LawyerId", objModel.LawyerId);
                    parmeters.Add("@CourtId", objModel.CourtId);
                    parmeters.Add("@Type", objModel.Type);
                    parmeters.Add("@PrimarySecondary", objModel.PrimarySecondary);
                    parmeters.Add("@RoleId", objModel.RoleId);
                    var objResult = await Con.QueryMultipleAsync("spDashboard", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    DashboardResponseModel objResut = new()
                    {
                        Status = true,
                        Message = "",
                        Data = objResult.Read<object>()
                    };
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetPendingDetailReport", ex.Message, ex.StackTrace, ex.Source, "ReportService/ReportService.Repository/DashboardRepository/GetPendingDetailReport");
                return new DashboardResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<DashboardResponseWithPaginationModel> GetDashboardPendencyReport(DashboardPendencyReportFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@AdmDepttId", objModel.AdmDepttId);
                    parmeters.Add("@UnitId", objModel.UnitId);
                    parmeters.Add("@OfficeId", objModel.OfficeId);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@OICId", objModel.OICId);
                    parmeters.Add("@LawyerId", objModel.LawyerId);
                    parmeters.Add("@Status", objModel.Status);
                    //parmeters.Add("@PrimarySecondary", objModel.PrimarySecondary);
                    parmeters.Add("@RoleId", objModel.RoleId);
                    parmeters.Add("@Level", objModel.Level);
                    parmeters.Add("@CourtTypeId", objModel.CourtTypeId);
                    parmeters.Add("@PlaceId", objModel.PlaceId);
                    parmeters.Add("@Bench", objModel.Bench);
                    parmeters.Add("@PageNumber", objModel.PageNo);
                    parmeters.Add("@PageSize", objModel.PageSize);
                    var objResult = await Con.QueryMultipleAsync("spDashboardPendencyReports", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    DashboardResponseWithPaginationModel objResut = new()
                    {
                        Status = true,
                        Message = "",
                        Data = objResult.Read<object>(),
                        Pagination = objResult.Read<object>()
                    };
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDashboardPendencyReport", ex.Message, ex.StackTrace, ex.Source, "ReportService/ReportService.Repository/DashboardRepository/GetDashboardPendencyReport");
                return new DashboardResponseWithPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

    
    }
}
