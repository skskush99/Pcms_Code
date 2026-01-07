using Common.Dapper;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
namespace Common.Repository
{
    public class LogsService : SqlRepository
    {
        private readonly System.Data.IDbConnection Con;
        public LogsService(IConfiguration Configuration) : base(Configuration)
        {
        }
        public bool Logs(string LogType, string Method, string Message, string StackTrace = "", string Source = "", string Path = "")
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddLogsData");
                    parmeters.Add("@LogType", LogType);
                    parmeters.Add("@Method", Method);
                    parmeters.Add("@Message", Message);
                    parmeters.Add("@StackTrace", StackTrace);
                    parmeters.Add("@Source", Source);
                    parmeters.Add("@Path", Path);
                    var objData = Con.Query<object>("spTrn_Logs", parmeters, commandType: CommandType.StoredProcedure);
                    //var objResult = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
