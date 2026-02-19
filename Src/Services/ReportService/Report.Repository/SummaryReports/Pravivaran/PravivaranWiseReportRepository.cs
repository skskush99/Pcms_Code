using Common.Dapper;
using Common.Repository;
using Dapper;
using Microsoft.Extensions.Configuration;
using Report.Dto.Global;
using Report.Dto.SummaryReports.PravivaranWise;
using System.Data;

namespace Report.Repository.SummaryReports.Pravivaran
{
    public class PravivaranWiseReportRepository : SqlRepository, IPravivaranWiseReport
    {
        private readonly System.Data.IDbConnection Con;
        private readonly LogsService _logsService;
        public PravivaranWiseReportRepository(IConfiguration Configuration, LogsService logsService) : base(Configuration)
        {
            _logsService = logsService;
        }

        public async Task<PravivaranResponseModel> GetPravivaran2(Pravivaran_2Model objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetPravivaran2");
                    parmeters.Add("@DepartmentId", objModel.DepartmentId);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@FromDate", objModel.FromDate);
                    parmeters.Add("@ToDate", objModel.ToDate);
                    parmeters.Add("@PageNumber", objModel.PageNumber);
                    parmeters.Add("@PageSize", objModel.PageSize);

                    var objResult = await Con.QueryMultipleAsync("spRptPravivaran", parmeters, commandType: CommandType.StoredProcedure);

                    PravivaranResponseModel objResut = new()
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
                _logsService.Logs("Error", "GetPravivaran2", ex.Message, ex.StackTrace, ex.Source, "ReportService/ReportService.Repository/SummaryReports/PravivaranWiseReportRepository/GetPravivaran2");
                return new PravivaranResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<PravivaranResponseModel> GetPravivaran3(Pravivaran_2Model objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetPravivaran3");
                    parmeters.Add("@DepartmentId", objModel.DepartmentId);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@FromDate", objModel.FromDate);
                    parmeters.Add("@ToDate", objModel.ToDate);
                    parmeters.Add("@PageNumber", objModel.PageNumber);
                    parmeters.Add("@PageSize", objModel.PageSize);

                    var objResult = await Con.QueryMultipleAsync("spRptPravivaran", parmeters, commandType: CommandType.StoredProcedure);

                    PravivaranResponseModel objResut = new()
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
                _logsService.Logs("Error", "GetPravivaran3", ex.Message, ex.StackTrace, ex.Source, "ReportService/ReportService.Repository/SummaryReports/PravivaranWiseReportRepository/GetPravivaran3");
                return new PravivaranResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<PravivaranResponseModel> GetPravivaran3K(Pravivaran_2Model objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetPravivaran3K");
                    parmeters.Add("@DepartmentId", objModel.DepartmentId);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@FromDate", objModel.FromDate);
                    parmeters.Add("@ToDate", objModel.ToDate);
                    parmeters.Add("@PageNumber", objModel.PageNumber);
                    parmeters.Add("@PageSize", objModel.PageSize);

                    var objResult = await Con.QueryMultipleAsync("spRptPravivaran", parmeters, commandType: CommandType.StoredProcedure);

                    PravivaranResponseModel objResut = new()
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
                _logsService.Logs("Error", "GetPravivaran3K", ex.Message, ex.StackTrace, ex.Source, "ReportService/ReportService.Repository/SummaryReports/PravivaranWiseReportRepository/GetPravivaran3K");
                return new PravivaranResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<PravivaranResponseModel> GetPravivaran3Kha(Pravivaran_2Model objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetPravivaran3Kha");
                    parmeters.Add("@DepartmentId", objModel.DepartmentId);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@FromDate", objModel.FromDate);
                    parmeters.Add("@ToDate", objModel.ToDate);
                    parmeters.Add("@PageNumber", objModel.PageNumber);
                    parmeters.Add("@PageSize", objModel.PageSize);

                    var objResult = await Con.QueryMultipleAsync("spRptPravivaran", parmeters, commandType: CommandType.StoredProcedure);

                    PravivaranResponseModel objResut = new()
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
                _logsService.Logs("Error", "GetPravivaran3Kha", ex.Message, ex.StackTrace, ex.Source, "ReportService/ReportService.Repository/SummaryReports/PravivaranWiseReportRepository/GetPravivaran3Kha");
                return new PravivaranResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<PravivaranResponseModel> GetPravivaran7(Pravivaran_2Model objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetPravivaran7");
                    parmeters.Add("@DepartmentId", objModel.DepartmentId);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@FromDate", objModel.FromDate);
                    parmeters.Add("@ToDate", objModel.ToDate);
                    parmeters.Add("@PageNumber", objModel.PageNumber);
                    parmeters.Add("@PageSize", objModel.PageSize);

                    var objResult = await Con.QueryMultipleAsync("spRptPravivaran", parmeters, commandType: CommandType.StoredProcedure);

                    PravivaranResponseModel objResut = new()
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
                _logsService.Logs("Error", "GetPravivaran7", ex.Message, ex.StackTrace, ex.Source, "ReportService/ReportService.Repository/SummaryReports/PravivaranWiseReportRepository/GetPravivaran7");
                return new PravivaranResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<PravivaranResponseModel> GetReturn4(Pravivaran_2Model objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetReturn4");
                    parmeters.Add("@DepartmentId", objModel.DepartmentId);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@FromDate", objModel.FromDate);
                    parmeters.Add("@ToDate", objModel.ToDate);
                    parmeters.Add("@PageNumber", objModel.PageNumber);
                    parmeters.Add("@PageSize", objModel.PageSize);

                    var objResult = await Con.QueryMultipleAsync("spRptPravivaran", parmeters, commandType: CommandType.StoredProcedure);

                    PravivaranResponseModel objResut = new()
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
                _logsService.Logs("Error", "GetReturn4", ex.Message, ex.StackTrace, ex.Source, "ReportService/ReportService.Repository/SummaryReports/PravivaranWiseReportRepository/GetReturn4");
                return new PravivaranResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        






    }
}
