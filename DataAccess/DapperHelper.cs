using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

namespace Paragwan.DataAccess
{
    public class DapperHelper
    {
        private readonly string _connectionString;

        public DapperHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public IEnumerable<T> Query<T>(string action, object parameters = null)
        {
            using (var connection = GetConnection())
            {
                var dynamicParams = new DynamicParameters(parameters);
                dynamicParams.Add("@Action", action);
                return connection.Query<T>("spManageData", dynamicParams, commandType: CommandType.StoredProcedure);
            }
        }

        public T QuerySingle<T>(string action, object parameters = null)
        {
            using (var connection = GetConnection())
            {
                var dynamicParams = new DynamicParameters(parameters);
                dynamicParams.Add("@Action", action);
                return connection.QuerySingleOrDefault<T>("spManageData", dynamicParams, commandType: CommandType.StoredProcedure);
            }
        }

        public void Execute(string action, object parameters = null)
        {
            using (var connection = GetConnection())
            {
                var dynamicParams = new DynamicParameters(parameters);
                dynamicParams.Add("@Action", action);
                connection.Execute("spManageData", dynamicParams, commandType: CommandType.StoredProcedure);
            }
        }
    }
}