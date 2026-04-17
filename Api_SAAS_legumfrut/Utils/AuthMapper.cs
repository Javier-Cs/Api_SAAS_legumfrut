using Api_SAAS_legumfrut.Dtos.Login;
using Api_SAAS_legumfrut.Entities;

namespace Api_SAAS_legumfrut.Utils
{
    public static class AuthMapper
    {
        

        public static LoginResponseDto ToLoginResponse(
            User user,
            string token,
            DateTime expiration,
            string nombreEmpresa
            )
        {
            return new LoginResponseDto { 
                Token = token,
                Expiration = expiration,
                IdEmpresa = user.IdEmpresa,
                NombreEmpresa = nombreEmpresa,
                IdUser = user.IdUser,
                Rol = user.Rol,
                Usuario = user.Nombre,
                Estado = user.Estado
            };
        }
    }
}
