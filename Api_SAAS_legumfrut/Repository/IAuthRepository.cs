using Api_SAAS_legumfrut.Auth;
using Api_SAAS_legumfrut.Entities;

namespace Api_SAAS_legumfrut.Repository
{
    public interface IAuthRepository
    {
        Task<UserWithEmpresa?> GetByEmailWithEmpresaAsync(string email, CancellationToken ct);
    }
}
