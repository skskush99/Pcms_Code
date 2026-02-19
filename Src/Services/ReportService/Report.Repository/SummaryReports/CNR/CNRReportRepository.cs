using Common.Dapper;
using Common.Repository;
using Dapper;
using Microsoft.Extensions.Configuration;
using Report.Dto.Global;
using Report.Dto.SummaryReports.CNR;
using System.Data;

namespace Report.Repository.SummaryReports.CNR
{
    public class CNRReportRepository : SqlRepository, ICNRReportRepository
    {
        private readonly System.Data.IDbConnection Con;
        private readonly LogsService _logsService;
        public CNRReportRepository(IConfiguration configuration, LogsService logsService) : base(configuration)
        {
            _logsService = logsService;
        }

        public ResponseModel GetCNRReport(Nullable<int> admDepttId, Nullable<int> districtId, Nullable<int> unitId, Nullable<int> officeId, Nullable<int> lavelId, int pageSize, int currentPage)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@AdmDepttId", admDepttId);
                    parmeters.Add("@DistrictId", districtId);
                    parmeters.Add("@UnitId", unitId);
                    parmeters.Add("@OfficeId", officeId);
                    parmeters.Add("@LavelId", lavelId);
                    parmeters.Add("@PageNumber", currentPage);
                    parmeters.Add("@PageSize", pageSize);

                    var objResult = Con.QueryMultiple("usp_CNR_Report", parmeters, commandType: CommandType.StoredProcedure);
                    ResponseModel objResut = new()
                    {
                        Status = true,
                        Message = "",
                        Data = objResult.Read<CNR_Report>(),
                        Pagination = objResult.Read<PaginationModel>()
                    };

                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCNRReport", ex.Message, ex.StackTrace, ex.Source, "ReportService/ReportService.Repository/SummaryReports/CNR/CNRReportRepository/GetCNRReport");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public ResponseModel GetCNRListSadReport(ref DataPagingModel TablePaging)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    int AdmDepttIdParameter = 0;
                    int UnitIdParameter = 0;
                    int OfficeIdParameter = 0;
                    int districtidParameter = 0;
                    int StatusParameter = 0;
                    string PrimarySecondaryParameter = string.Empty;
                    int LavelIdParameter = 0;


                    foreach (var item in TablePaging.SearchParameter)
                    {
                        string value = item.Value.Trim();
                        if (item.Key.ToLower() == "admdepttid" && !String.IsNullOrEmpty(value))
                        {
                            AdmDepttIdParameter = Convert.ToInt32(value);
                        }
                        if (item.Key.ToLower() == "unitid" && !String.IsNullOrEmpty(value))
                        {
                            UnitIdParameter = Convert.ToInt32(value);
                        }
                        if (item.Key.ToLower() == "officeid" && !String.IsNullOrEmpty(value))
                        {
                            OfficeIdParameter = Convert.ToInt32(value);
                        }
                        if (item.Key.ToLower() == "districtid" && !String.IsNullOrEmpty(value))
                        {
                            districtidParameter = Convert.ToInt32(value);
                        }
                        if (item.Key.ToLower() == "status" && !String.IsNullOrEmpty(value))
                        {
                            StatusParameter = Convert.ToInt32(value);
                        }
                        if (item.Key.ToLower() == "primarysecondary" && !String.IsNullOrEmpty(value))
                        {
                            PrimarySecondaryParameter = value;
                        }
                        if (item.Key.ToLower() == "lavelid" && !String.IsNullOrEmpty(value))
                        {
                            LavelIdParameter = Convert.ToInt32(value);
                        }
                    }

                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "CNRList");
                    parmeters.Add("@deptId", AdmDepttIdParameter);
                    parmeters.Add("@unitId", UnitIdParameter);
                    parmeters.Add("@officeId", OfficeIdParameter);
                    parmeters.Add("@districtId", districtidParameter);
                    parmeters.Add("@oicId", StatusParameter);
                    parmeters.Add("@status", PrimarySecondaryParameter);
                    parmeters.Add("@status", LavelIdParameter);
                    parmeters.Add("@PageNumber", TablePaging.StartPageNumber);
                    parmeters.Add("@PageSize", TablePaging.PageSize);

                    var objResult = Con.QueryMultiple("usp_CNR_Report_Grid", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseModel objResut = new()
                    {
                        Status = true,
                        Message = "",
                        Data = objResult.Read<CnrModel>(),
                        Pagination = objResult.Read<PaginationModel>()
                    };

                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCNRListSadReport", ex.Message, ex.StackTrace, ex.Source, "ReportService/ReportService.Repository/SummaryReports/CNR/CNRReportRepository/GetCNRListSadReport");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public ResponseModel GetCNRListReport(ref DataPagingModel TablePaging, int DepartmentId, int UnitId, int OfficeId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    int AdmDepttIdParameter = 0;
                    int UnitIdParameter = 0;
                    int OfficeIdParameter = 0;
                    int districtidParameter = 0;
                    int StatusParameter = 0;
                    string PrimarySecondaryParameter = string.Empty;
                    int LavelIdParameter = 0;
                    int OicIdParameter = 0;

                    foreach (var item in TablePaging.SearchParameter)
                    {
                        string value = item.Value.Trim();
                        if (item.Key.ToLower() == "admdepttid" && !String.IsNullOrEmpty(value))
                        {
                            AdmDepttIdParameter = Convert.ToInt32(value);
                        }
                        if (item.Key.ToLower() == "unitid" && !String.IsNullOrEmpty(value))
                        {
                            UnitIdParameter = Convert.ToInt32(value);
                        }
                        if (item.Key.ToLower() == "officeid" && !String.IsNullOrEmpty(value))
                        {
                            OfficeIdParameter = Convert.ToInt32(value);
                        }
                        if (item.Key.ToLower() == "districtid" && !String.IsNullOrEmpty(value))
                        {
                            districtidParameter = Convert.ToInt32(value);
                        }
                        if (item.Key.ToLower() == "oicid" && !String.IsNullOrEmpty(value))
                        {
                            OicIdParameter = Convert.ToInt32(value);
                        }
                        if (item.Key.ToLower() == "status" && !String.IsNullOrEmpty(value))
                        {
                            StatusParameter = Convert.ToInt32(value);
                        }
                        if (item.Key.ToLower() == "primarysecondary" && !String.IsNullOrEmpty(value))
                        {
                            PrimarySecondaryParameter = value;
                        }
                        if (item.Key.ToLower() == "lavelid" && !String.IsNullOrEmpty(value))
                        {
                            LavelIdParameter = Convert.ToInt32(value);
                        }
                    }

                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "CNRList");
                    parmeters.Add("@deptId", AdmDepttIdParameter);
                    parmeters.Add("@unitId", UnitIdParameter);
                    parmeters.Add("@officeId", OfficeIdParameter);
                    parmeters.Add("@districtId", districtidParameter);
                    parmeters.Add("@oicId", OicIdParameter);
                    parmeters.Add("@status", StatusParameter);
                    parmeters.Add("@LavelId", LavelIdParameter);
                    parmeters.Add("@PrimarySecondary", PrimarySecondaryParameter);                    
                    parmeters.Add("@PageNumber", TablePaging.StartPageNumber);
                    parmeters.Add("@PageSize", TablePaging.PageSize);

                    var objResult = Con.QueryMultiple("usp_CNR_Report_Grid", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseModel objResut = new()
                    {
                        Status = true,
                        Message = "",
                        Data = objResult.Read<CnrModel>(),
                        Pagination = objResult.Read<PaginationModel>()
                    };

                    DisposeCurrentSqlConnection();
                    return objResut;

                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCNRListReport", ex.Message, ex.StackTrace, ex.Source, "ReportService/ReportService.Repository/SummaryReports/CNR/CNRReportRepository/GetCNRListReport");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
