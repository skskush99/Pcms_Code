using Common.Dapper;
using Common.Repository;
using Dapper;
using Microsoft.Extensions.Configuration;
using Sms.Dto.SmsModel;
using System.Data;

namespace Sms.ServiceBus.SmsService
{
    public class SmsServices : SqlRepository, ISmsService
    {
        private readonly LogsService _logsService;
        private readonly System.Data.IDbConnection Con;
        public SmsServices(IConfiguration Configuration, LogsService logsService) : base(Configuration)
        {
            _logsService = logsService;
        }

        public async Task<List<SmsListModel>> GetSmsRequestList(SmsRequestModel data)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetSmsHistory");
                    parmeters.Add("@Date", data.Date);

                    var objData = await Con.QueryAsync<SmsListModel>("sp_SMSHistory", parmeters, commandType: CommandType.StoredProcedure, commandTimeout: 6000);
                    DisposeCurrentSqlConnection();
                    return objData != null ? objData.ToList() : new List<SmsListModel>();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "SmsScheduler", ex.Message, ex.StackTrace, ex.Source, "SmsServiceScheduler/Sms.ServiceBus.SmsService/SmsServices/GetSmsRequestList");
                return new List<SmsListModel>();
            }
        }

        public async Task RunSmsSender()
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    var objData = await Con.QueryAsync("sp_TodaySmsSender", parmeters, commandType: CommandType.StoredProcedure, commandTimeout: 6000);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "SmsScheduler", ex.Message, ex.StackTrace, ex.Source, "SmsServiceScheduler/Sms.ServiceBus.SmsService/SmsServices/RunSmsSender");
            }
        }

        public async Task RunSmsSenderNodalOfficer()
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    var objData = await Con.QueryAsync("sp_SmsSenderNodalOfficer", parmeters, commandType: CommandType.StoredProcedure, commandTimeout: 6000);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "SmsScheduler", ex.Message, ex.StackTrace, ex.Source, "SmsServiceScheduler/Sms.ServiceBus.SmsService/SmsServices/RunSmsSenderNodalOfficer");
            }
        }
    }
}
