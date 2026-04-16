using Microsoft.Data.SqlClient;
using System.Data;

namespace Api_SAAS_legumfrut.Data
{
    public class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly string _connectionString;

        public SqlConnectionFactory(IConfiguration config) {
            _connectionString = config.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Falta ConnectionString 'DefaultConnection' XD");
        }

        public IDbConnection CreateConnection(){
            var connection = new SqlConnection(_connectionString);
            // connection.Open();
            return connection;
        }
        
            
    }
}
