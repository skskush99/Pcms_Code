using Common.Dapper;
using Common.Repository;
using Dapper;
using Microsoft.Extensions.Configuration;
using Report.Dto.DetailReports;
using System.Data;
using Report.Dto.Global;

namespace Report.Repository.DetailReports.MahilaAtayachar
{
    public class MahilaAtayacharReportRepository: SqlRepository, IMahilaAtayacharReport
    {
        private readonly System.Data.IDbConnection Con;
        private readonly LogsService _logsService;
        public MahilaAtayacharReportRepository(IConfiguration Configuration, LogsService logsService) : base(Configuration)
        {
            _logsService = logsService;
        }

        public async Task<DetailReportsResponseModel> GetMahilaAtayacharIPCReport(MahilaAtayacharIPCModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetMahilaAtayacharIPC");
                    parmeters.Add("@FromDate", objModel.FromDate);
                    parmeters.Add("@ToDate", objModel.ToDate);
                    parmeters.Add("@DepartmentId", objModel.DepartmentId);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@PageNumber", objModel.PageNumber);
                    parmeters.Add("@PageSize", objModel.PageSize);

                    var objResult = await Con.QueryMultipleAsync("spRptMahilaAtayacharIPC", parmeters, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetMahilaAtayacharIPCReport", ex.Message, ex.StackTrace, ex.Source, "ReportService/ReportService.Repository/DetailReports/MahilaAtayacharReportRepository/GetMahilaAtayacharIPCReport");
                return new DetailReportsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<DetailReportsResponseModel> GetMahilaAtayacharBNSReport(MahilaAtayacharBNSModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetMahilaAtayacharBNS");
                    parmeters.Add("@FromDate", objModel.FromDate);
                    parmeters.Add("@ToDate", objModel.ToDate);
                    parmeters.Add("@DepartmentId", objModel.DepartmentId);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@PageNumber", objModel.PageNumber);
                    parmeters.Add("@PageSize", objModel.PageSize);

                    var objResult = await Con.QueryMultipleAsync("spRptMahilaAtayacharBNS", parmeters, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetMahilaAtayacharBNSReport", ex.Message, ex.StackTrace, ex.Source, "ReportService/ReportService.Repository/DetailReports/MahilaAtayacharReportRepository/GetMahilaAtayacharBNSReport");
                return new DetailReportsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }




    }
}
