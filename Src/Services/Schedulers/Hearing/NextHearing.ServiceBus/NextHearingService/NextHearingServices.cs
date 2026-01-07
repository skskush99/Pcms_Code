using Common.Dapper;
using Common.Repository;
using Dapper;
using Microsoft.Extensions.Configuration;
using NextHearing.Dto.NextHearingModel;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NextHearing.ServiceBus.NextHearingService
{
    public class NextHearingServices : SqlRepository, INextHearingService
    {
        private readonly LogsService _logsService;
        private readonly System.Data.IDbConnection Con;
        public NextHearingServices(IConfiguration Configuration, LogsService logsService) : base(Configuration)
        {
            _logsService = logsService;
        }

        public async Task<List<NextHearingData>> GetNextHearingListforUpdate()
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    var objData = await Con.QueryAsync<NextHearingData>("usp_NextHearingListForUpdate", parmeters, commandType: CommandType.StoredProcedure);
                    DisposeCurrentSqlConnection();
                    return objData != null ? objData.ToList() : new List<NextHearingData>();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "NextHearingScheduler", ex.Message, ex.StackTrace, ex.Source, "NextHearingScheduler/NextHearing.ServiceBus.NextHearingService/NextHearingServices/GetNextHearingListforUpdate");
                return new List<NextHearingData>();
            }
        }

        public async Task<NextHearingResponseData?> UpdateDecideDateUsingCNR(string cnr, string DecidedDate, Int64 caseid, int hearing_SNo=1)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@CRNNumber", cnr);
                    parmeters.Add("@DecideDate", DecidedDate);
                    parmeters.Add("@CaseId", caseid);
                    parmeters.Add("@Hearing_SNo", hearing_SNo);
                    var objData = await Con.QueryAsync<NextHearingResponseData>("usp_UpdateDecideDate_UsingCNR", parmeters, commandType: CommandType.StoredProcedure);
                    DisposeCurrentSqlConnection();
                    return objData != null ? objData.FirstOrDefault() : new NextHearingResponseData(); 
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "NextHearingScheduler", ex.Message, ex.StackTrace, ex.Source, "NextHearingScheduler/NextHearing.ServiceBus.NextHearingService/NextHearingServices/UpdateDecideDateUsingCNR");
                return new NextHearingResponseData();
            }
        }

        public async Task<NextHearingResponseData1?> UpdateDecideDateUsingCNR1(string cnr, string DecidedDate, Int64 caseid, int hearing_SNo = 1)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@CRNNumber", cnr);
                    parmeters.Add("@NextHearing", DecidedDate);
                    parmeters.Add("@CaseId", caseid);
                    parmeters.Add("@Hearing_SNo", hearing_SNo);
                    var objData = await Con.QueryAsync<NextHearingResponseData1>("sp_UpdateNextHearing_UsingCNR1", parmeters, commandType: CommandType.StoredProcedure);
                    DisposeCurrentSqlConnection();
                    return objData != null ? objData.FirstOrDefault() : new NextHearingResponseData1();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "NextHearingScheduler", ex.Message, ex.StackTrace, ex.Source, "NextHearingScheduler/NextHearing.ServiceBus.NextHearingService/NextHearingServices/UpdateDecideDateUsingCNR1");
                return new NextHearingResponseData1();
            }
        }
    }
}
