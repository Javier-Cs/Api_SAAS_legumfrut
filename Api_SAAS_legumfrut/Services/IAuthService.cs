using Api_SAAS_legumfrut.Dtos.Login;
using Api_SAAS_legumfrut.Repository;
using Microsoft.AspNetCore.Identity.Data;

namespace Api_SAAS_legumfrut.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto loginRequest, CancellationToken ct);
    }
}
