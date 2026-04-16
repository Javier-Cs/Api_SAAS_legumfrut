using System.Data;

namespace Api_SAAS_legumfrut.Data
{
    public interface ISqlConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
