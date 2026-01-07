using Common.Dapper;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Repository.Esign
{
    internal class EsignRepository : SqlRepository, IEsignRepository
    {
        private readonly System.Data.IDbConnection Con;
        public EsignRepository(IConfiguration Configuration) : base(Configuration)
        {
        }

        public async Task<object> AddEsignData(string txn, string esignData)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddEsignData");
                    parmeters.Add("@txn", txn);
                    parmeters.Add("@esignData", esignData);
                    var objResult = await Con.QueryAsync("spTrn_EsignData", parmeters, commandType: CommandType.StoredProcedure);
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
