using Common.Dapper;
using Common.Repository;
using Dapper;
using Microsoft.Extensions.Configuration;
using Report.Dto.Global;
using Report.Dto.SummaryReports.Action;
using System.Data;

namespace Report.Repository.SummaryReports.Action
{
    public class ActionTakenRepository : SqlRepository, IActionTakenRepository
    {
        private readonly System.Data.IDbConnection Con;
        private readonly LogsService _logsService;
        public ActionTakenRepository(IConfiguration configuration, LogsService logsService) : base(configuration)
        {
            _logsService = logsService;
        }

        public ResponseModel GetActionToBeTakenReport(Nullable<int> deptId, Nullable<int> unitId, Nullable<int> officeId, Nullable<int> districtId, Nullable<int> oicId, Nullable<int> level, Nullable<int> roleid, string main_Party, int pageSize, int currentPage)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@deptId", deptId);
                    parmeters.Add("@unitId", unitId);
                    parmeters.Add("@officeId", officeId);
                    parmeters.Add("@districtId", districtId);
                    parmeters.Add("@oicId", oicId);
                    parmeters.Add("@level", level);
                    parmeters.Add("@roleid", roleid);
                    parmeters.Add("@main_Party", main_Party);
                    parmeters.Add("@PageNumber", currentPage);
                    parmeters.Add("@PageSize", pageSize);

                    var objResult = Con.QueryMultiple("sp_DashBoardActionTobeTaken", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseModel objResut = new()
                    {
                        Status = true,
                        Message = "",
                        Data = objResult.Read<DashBoardActionTobeTaken>(),
                        Pagination = objResult.Read<PaginationModel>()
                    };

                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetActionToBeTakenReport", ex.Message, ex.StackTrace, ex.Source, "ReportService/ReportService.Repository/SummaryReports/Action/ActionTakenRepository/GetActionToBeTakenReport");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public ResponseModel ActionTobeTakenGridNew(Nullable<int> deptId, Nullable<int> unitId, Nullable<int> officeId, Nullable<int> districtId, Nullable<int> oicId, Nullable<int> level, Nullable<int> roleid, string main_Party, int pageSize, int currentPage)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@deptId", deptId);
                    parmeters.Add("@unitId", unitId);
                    parmeters.Add("@officeId", officeId);
                    parmeters.Add("@districtId", districtId);
                    parmeters.Add("@oicId", oicId);
                    parmeters.Add("@level", level);
                    parmeters.Add("@roleid", roleid);
                    parmeters.Add("@main_Party", main_Party);
                    parmeters.Add("@PageNumber", currentPage);
                    parmeters.Add("@PageSize", pageSize);

                    var objResult = Con.QueryMultiple("sp_DashBoardActionTobeTakenNew", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseModel objResut = new()
                    {
                        Status = true,
                        Message = "",
                        Data = objResult.Read<DashBoardActionTobeTakenNew>(),
                        Pagination = objResult.Read<PaginationModel>()
                    };

                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActionTobeTakenGridNew", ex.Message, ex.StackTrace, ex.Source, "ReportService/ReportService.Repository/SummaryReports/Action/ActionTakenRepository/ActionTobeTakenGridNew");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

    }
}
