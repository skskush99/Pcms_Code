using Common.Dapper;
using Common.Repository;
using Dapper;
using Microsoft.Extensions.Configuration;
using Report.Dto.Global;
using Report.Dto.MISReport.FormatWise;
using System.Data;

namespace Report.Repository.MISReport.FormatWise
{
    public class FormatWiseReportRepository : SqlRepository, IFormatWiseReport
    {
        private readonly System.Data.IDbConnection Con;
        private readonly LogsService _logsService;
        public FormatWiseReportRepository(IConfiguration Configuration, LogsService logsService) : base(Configuration)
        {
            _logsService = logsService;
        }

        public async Task<FormatWiseReportsModel> GetFormat_AReport(Format_AReportModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetFormat_AReport");
                    parmeters.Add("@CaseId", objModel.CaseId);
                    parmeters.Add("@CNRNumber", objModel.CNRNumber);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@DepartmentId", objModel.DepartmentId);
                    parmeters.Add("@FromDate", objModel.FromDate);
                    parmeters.Add("@ToDate", objModel.ToDate);
                    parmeters.Add("@PageNumber", objModel.PageNumber);
                    parmeters.Add("@PageSize", objModel.PageSize);

                    var objResult = await Con.QueryMultipleAsync("spRptFormatWise", parmeters, commandType: CommandType.StoredProcedure);

                    FormatWiseReportsModel objResut = new()
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
                _logsService.Logs("Error", "GetFormat_AReport", ex.Message, ex.StackTrace, ex.Source, "ReportService/ReportService.Repository/MISReports/FormatWiseReportRepository/GetFormat_AReport");
                return new FormatWiseReportsModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<FormatWiseReportsModel> GetFormat_BReport(Format_BReportModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetFormat_BReport");
                    parmeters.Add("@CaseId", objModel.CaseId);
                    parmeters.Add("@CNRNumber", objModel.CNRNumber);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@DepartmentId", objModel.DepartmentId);
                    parmeters.Add("@FromDate", objModel.FromDate);
                    parmeters.Add("@ToDate", objModel.ToDate);
                    parmeters.Add("@PageNumber", objModel.PageNumber);
                    parmeters.Add("@PageSize", objModel.PageSize);

                    var objResult = await Con.QueryMultipleAsync("spRptFormatWise", parmeters, commandType: CommandType.StoredProcedure);

                    FormatWiseReportsModel objResut = new()
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
                _logsService.Logs("Error", "GetFormat_BReport", ex.Message, ex.StackTrace, ex.Source, "ReportService/ReportService.Repository/MISReports/FormatWiseReportRepository/GetFormat_BReport");
                return new FormatWiseReportsModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }



    }
}
