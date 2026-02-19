using Common.Dapper;
using Dapper;
using Report.Dto.DetailReports;
using Microsoft.Extensions.Configuration;
using System.Data;
using Report.Dto.Global;
using Common.Repository;

namespace Report.Repository.DetailReports.DistrictWise
{
    public class DistrictWiseReportRepository : SqlRepository, IDistrictWiseReport
    {
        private readonly System.Data.IDbConnection Con;
        private readonly LogsService _logsService;
        public DistrictWiseReportRepository(IConfiguration Configuration, LogsService logsService) : base(Configuration)
        {
            _logsService = logsService;
        }
        public async Task<DetailReportsResponseModel> GetDistrictWiseReport(DistrictWiseModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetDistrictWiseDetail");
                    parmeters.Add("@FromDate", objModel.FromDate);
                    parmeters.Add("@ToDate", objModel.ToDate);
                    parmeters.Add("@DepartmentId", objModel.DepartmentId);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@Status", objModel.Status);
                    parmeters.Add("@PageNumber", objModel.PageNumber);
                    parmeters.Add("@PageSize", objModel.PageSize);

                    var objResult = await Con.QueryMultipleAsync("spRptDistrictWiseDetail", parmeters, commandType: CommandType.StoredProcedure);

                    DetailReportsResponseModel objResut = new()
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
                _logsService.Logs("Error", "GetDistrictWiseReport", ex.Message, ex.StackTrace, ex.Source, "ReportService/ReportService.Repository/DetailReports/DistrictWiseReportRepository/GetDistrictWiseReport");
                return new DetailReportsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


    }
}
