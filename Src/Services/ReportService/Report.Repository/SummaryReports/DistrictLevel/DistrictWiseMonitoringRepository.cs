using Common.Dapper;
using Dapper;
using Microsoft.Extensions.Configuration;
using Report.Dto.SummaryReports.DistrictLevel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Repository.SummaryReports.DistrictLevel
{
    public class DistrictWiseMonitoringRepository : SqlRepository, IDistrictWiseMonitoringRepository
    {
        private readonly System.Data.IDbConnection Con;
        public DistrictWiseMonitoringRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public IEnumerable<DistrictWiseMonitoring> GetDistrictWiseMonitoringReport(Nullable<int> deptId, Nullable<int> unitId, Nullable<int> officeId, Nullable<int> districtId, Nullable<int> status, Nullable<int> level)
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
                    parmeters.Add("@status", status);
                    parmeters.Add("@level", level);

                    var objResult = Con.Query<DistrictWiseMonitoring>("sp_Summary_DistrictWiseMonitoringReport", parmeters, commandType: CommandType.StoredProcedure);

                    DisposeCurrentSqlConnection();
                    return objResult;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
