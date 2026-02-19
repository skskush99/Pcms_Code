using Common.Dapper;
using Common.Repository;
using Dapper;
using Microsoft.Extensions.Configuration;
using Report.Dto.Global;
using Report.Dto.SummaryReports.User;
using Report.Repository.UnitOfwork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Common;

namespace Report.Repository.SummaryReports.User
{
    public class UserRegistrationRepository : SqlRepository, IUserRegistrationRepository
    {
        private readonly System.Data.IDbConnection Con;
        private readonly LogsService _logsService;
        public UserRegistrationRepository(IConfiguration configuration, LogsService logsService) : base(configuration)
        {
            _logsService = logsService;
        }

        public ResponseModel GetUserRegistrationSummaryReport(Nullable<int> departmentId, Nullable<int> unitId, Nullable<int> office, Nullable<int> roleId, int pageSize, int currentPage)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@departmentId", departmentId);
                    parmeters.Add("@unitId", unitId);
                    parmeters.Add("@office", office);
                    parmeters.Add("@roleId", roleId);
                    parmeters.Add("@PageNumber", currentPage);
                    parmeters.Add("@PageSize", pageSize);

                    var objResult = Con.QueryMultiple("sp_UserRegistrationReport", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseModel objResut = new()
                    {
                        Status = true,
                        Message = "",
                        Data = objResult.Read<UserRegistrationReport>(),
                        Pagination = objResult.Read<PaginationModel>()
                    };

                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetUserRegistrationSummaryReport", ex.Message, ex.StackTrace, ex.Source, "ReportService/ReportService.Repository/SummaryReports/User/UserRegistrationRepository/GetUserRegistrationSummaryReport");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
