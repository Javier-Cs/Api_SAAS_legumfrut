using Api_SAAS_legumfrut.Auth;
using Api_SAAS_legumfrut.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Api_SAAS_legumfrut.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IConfiguration _config;


        public AuthRepository(IConfiguration config) { 
            _config = config;
        }

        // esta es otra opcion de repositorio pero no usa el dto que recibe el controller, 
        // por que se usaria otro dto?
        public async Task<UserWithEmpresa?> GetByEmailWithEmpresaAsync(string email)
        {
            using IDbConnection db = new SqlConnection(
                _config.GetConnectionString("DefaultConnection")
            );

            var sql = @"
            SELECT 
                u.id_user AS IdUser,
                u.id_empresa AS IdEmpresa,
                u.nombre AS Nombre,
                u.email AS Email,
                u.pass_hash AS PassHash,
                u.rol AS Rol,
                u.estado AS Estado,
                u.is_deleted AS IsDeleted,

                e.nombre_empresa AS NombreEmpresa
            FROM usuarios u
            INNER JOIN empresas e ON e.id_empresa = u.id_empresa
            WHERE u.email = @Email
            ";

            var result = await db.QueryAsync<User, string, UserWithEmpresa>(
                sql,
                (user, nombreEmpresa) =>
                {
                    return new UserWithEmpresa
                    {
                        user = user,
                        NombreEmpresa = nombreEmpresa
                    }; 
                },
                new { Email = email },
                splitOn: "NombreEmpresa"
            );

            return result.FirstOrDefault();
        }
    }
}
