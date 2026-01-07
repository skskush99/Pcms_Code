using System.Data;

namespace Common.Dapper
{
    public interface IGenericRepository : IDisposable
    {
        IDbConnection GetOpenConnection();
        void DisposeCurrentSqlConnection();
    }
}
