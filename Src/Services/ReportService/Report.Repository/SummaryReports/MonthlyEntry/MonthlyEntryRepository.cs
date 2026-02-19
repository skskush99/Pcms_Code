using Common.Dapper;
using Common.Repository;
using Dapper;
using Microsoft.Extensions.Configuration;
using Report.Dto.Global;
using Report.Dto.SummaryReports.MonthlyEntry;
using System.Data;

namespace Report.Repository.SummaryReports.MonthlyEntry
{
    public class MonthlyEntryRepository : SqlRepository, IMonthlyEntryRepository
    {
        private readonly System.Data.IDbConnection Con;
        private readonly LogsService _logsService;
        public MonthlyEntryRepository(IConfiguration configuration, LogsService logsService) : base(configuration)
        {
            _logsService = logsService;
        }

        public ResponseModel GetMonthlyEntryStatusReport(Nullable<int> departmentId, Nullable<int> unitId, Nullable<int> officeId, Nullable<int> month, Nullable<int> year, Nullable<int> districtId, Nullable<int> roleid, int pageSize, int currentPage)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@departmentId", departmentId);
                    parmeters.Add("@unitId", unitId);
                    parmeters.Add("@officeId", officeId);
                    parmeters.Add("@month", month);
                    parmeters.Add("@year", year);
                    parmeters.Add("@districtId", districtId);
                    parmeters.Add("@roleid", roleid);
                    parmeters.Add("@PageNumber", currentPage);
                    parmeters.Add("@PageSize", pageSize);

                    var objResult = Con.QueryMultiple("sp_Summary_MonthlyReportSummary", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseModel objResut = new()
                    {
                        Status = true,
                        Message = "",
                        Data = objResult.Read<MonthlyReportSummary>(),
                        Pagination = objResult.Read<PaginationModel>()
                    };

                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetMonthlyEntryStatusReport", ex.Message, ex.StackTrace, ex.Source, "ReportService/ReportService.Repository/SummaryReports/MonthlyEntry/MonthlyEntryRepository/GetMonthlyEntryStatusReport");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
