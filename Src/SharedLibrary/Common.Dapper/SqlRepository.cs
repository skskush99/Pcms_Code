using Core;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Common.Dapper
{
    /// <summary>
    /// The concrete implementation of a SQL repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class SqlRepository(IConfiguration Configuration) : IGenericRepository
    {
        private IConfiguration _Configuration = Configuration;
        private EDbConnectionTypes _dbType = EDbConnectionTypes.SQL;
        private IDbConnection _IDbConnection;

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public IDbConnection GetOpenConnection()
        {
            _IDbConnection = DbConnectionFactory.GetDbConnection(_dbType, _Configuration.GetConnectionString("DefaultConnection"));
            return _IDbConnection;
        }

        public void DisposeCurrentSqlConnection()
        {
            _IDbConnection.Close();
            _IDbConnection.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
