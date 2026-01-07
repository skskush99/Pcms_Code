using System.Data.SqlClient;
using System.Data;
using Core;

namespace Common.Dapper
{
    public class DbConnectionFactory
    {
        public static IDbConnection GetDbConnection(EDbConnectionTypes dbType, string connectionString)
        {
            IDbConnection connection = null;

            try
            {
                switch (dbType)
                {
                    case EDbConnectionTypes.SQL:
                        connection = new SqlConnection(connectionString);
                        break;
                    case EDbConnectionTypes.XML:
                        // TODO: Implement XML Connection (path name)
                        break;
                    case EDbConnectionTypes.DOCUMENT:
                        // TODO: Implement Document DB connection
                        break;
                    default:
                        connection = null;
                        break;
                }
                connection.Open();
            }
            catch (Exception)
            {
                connection.Close();
            }
            return connection;
        }
    }
}
