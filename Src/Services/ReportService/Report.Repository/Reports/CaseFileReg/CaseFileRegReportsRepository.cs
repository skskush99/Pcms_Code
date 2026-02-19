using Common.Dapper;
using Common.Repository;
using Dapper;
using Microsoft.Extensions.Configuration;
using Report.Dto.Global;
using Report.Dto.Reports;
using System.Data;

namespace Report.Repository.Reports.CaseFileReg
{
    public class CaseFileRegReportsRepository : SqlRepository, ICaseFileRegReports
    {
        private readonly System.Data.IDbConnection Con;
        private readonly LogsService _logsService;
        public CaseFileRegReportsRepository(IConfiguration Configuration, LogsService logsService) : base(Configuration)
        {
            _logsService = logsService;
        }
        public async Task<ReportsResponseModel> GetCaseFileRegReports(CaseFileRegModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@AdmDepttId", objModel.AdmDepttId);
                    parmeters.Add("@CellId", objModel.CellId);
                    parmeters.Add("@HeadId", objModel.HeadId);
                    parmeters.Add("@CourtId", objModel.CourtId);
                    parmeters.Add("@CaseNo", objModel.CaseNo);
                    parmeters.Add("@CaseRegistorYear", objModel.CaseRegistorYear);
                    parmeters.Add("@AbbreviationId", objModel.AbbreviationId);
                    parmeters.Add("@Banch", objModel.Banch);
                    parmeters.Add("@CsIsParty", objModel.CsIsParty);
                    parmeters.Add("@CourtType", objModel.CourtType);
                    parmeters.Add("@PageNumber", objModel.PageNo);
                    parmeters.Add("@PageSize", objModel.PageSize);

                    var objResult = await Con.QueryMultipleAsync("SpRptofCaseFileRegister", parmeters, commandType: CommandType.StoredProcedure);

                    ReportsResponseModel objResut = new()
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
                _logsService.Logs("Error", "GetCaseFileRegReports", ex.Message, ex.StackTrace, ex.Source, "ReportService/ReportService.Repository/Reports/CaseFileReg/CaseFileRegReportsRepository/GetCaseFileRegReports");
                return new ReportsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


    }

}
