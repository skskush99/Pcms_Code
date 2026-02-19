using Common.Dapper;
using Common.Repository;
using Dapper;
using Microsoft.Extensions.Configuration;
using Report.Dto.Global;
using Report.Dto.MISReport;
using Report.Dto.MISReport.Login;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Utils.LogUtility;

namespace Report.Repository.MISReport.Login
{
    public class LoginReportRepository : SqlRepository, ILoginReportRepository
    {
        private readonly System.Data.IDbConnection Con;
        private readonly LogsService _logsService;
        public LoginReportRepository(IConfiguration configuration, LogsService logsService) : base(configuration)
        {
            _logsService = logsService;
        }

        public ResponseModel GetLoginDetailReport(Nullable<System.DateTime> fromDate, Nullable<System.DateTime> toDate, Nullable<int> departmentId, Nullable<int> unitId, Nullable<int> officeId, Nullable<int> oicId, int pageSize, int currentPage)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@fromDate", fromDate);
                    parmeters.Add("@toDate", toDate);
                    parmeters.Add("@departmentId", departmentId);
                    parmeters.Add("@unitId", unitId);
                    parmeters.Add("@officeId", officeId);
                    parmeters.Add("@oicId", oicId);
                    parmeters.Add("@PageNumber", currentPage);
                    parmeters.Add("@PageSize", pageSize);

                    var objResult = Con.QueryMultiple("sp_LoginDetailReport", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseModel objResut = new()
                    {
                        Status = true,
                        Message = "",
                        Data = objResult.Read<LoginDetailReport>(),
                        Pagination = objResult.Read<PaginationModel>()
                    };

                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetLoginDetailReport", ex.Message, ex.StackTrace, ex.Source, "ReportService/ReportService.Repository/MISReport/LoginReportRepository/GetLoginDetailReport");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public ResponseModel GetLogReport(LogReportFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetLogsList");
                    parmeters.Add("@LogType", objModel.LogType);
                    parmeters.Add("@FromDate", objModel.FromDate);
                    parmeters.Add("@ToDate", objModel.ToDate);
                    parmeters.Add("@PageNo", objModel.PageNo);
                    parmeters.Add("@PageSize", objModel.PageSize);

                    var objResult = Con.QueryMultiple("spTrn_Logs", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetLoginDetailReport", ex.Message, ex.StackTrace, ex.Source, "ReportService/ReportService.Repository/MISReport/LoginReportRepository/GetLoginDetailReport");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


    }
}
