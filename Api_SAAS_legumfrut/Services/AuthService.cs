using Api_SAAS_legumfrut.Dtos.Login;
using Api_SAAS_legumfrut.Repository;
using BCrypt.Net;
using Azure.Core;
using Api_SAAS_legumfrut.Entities;
using Api_SAAS_legumfrut.Utils;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace Api_SAAS_legumfrut.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _config;

        public AuthService(IAuthRepository authRepository, IConfiguration config) { 
            _authRepository = authRepository;
            _config = config;
        }
        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto loginRequest, CancellationToken ct = default)
        {
            // buscamos Usuario
            var data = await _authRepository.GetByEmailWithEmpresaAsync(loginRequest.Email, ct);
            if (data == null) {
                throw new Exception("Usuario no encontrado o no existe XD");
            }

            var userObtenido = data.user;

            // Validar estado
            if (!userObtenido.Estado || userObtenido.IsDeleted) {
                throw new Exception("el usuario esta inactivo XD");
            }

            

            if (!string.IsNullOrEmpty(loginRequest.NombreEmpresa) && loginRequest.NombreEmpresa != data.NombreEmpresa) {
                throw new Exception("La emprese es incorrecta OJO");
            }

            /*if (userObtenido.IdEmpresa != loginRequest.IdEmpresa) {
                throw new Exception("La empresa es incorrecta OJO");
            }
            */
            var hash = BCrypt.Net.BCrypt.HashPassword("texto plano");
            Console.WriteLine(hash);


            if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, userObtenido.PassHash))
                throw new Exception("Credenciales incorrectas OJO");

            // Generar token
            var expiration = DateTime.UtcNow.AddHours(12);
            var token = GenerarToken(userObtenido, expiration);

            return AuthMapper.ToLoginResponse(
                userObtenido,
                token,
                expiration,
                data.NombreEmpresa
                );


        }

        private string GenerarToken(User userObtenido, DateTime expiration)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userObtenido.IdUser.ToString()),
                new Claim(ClaimTypes.Name, userObtenido.Nombre),
                new Claim(ClaimTypes.Email, userObtenido.Email),
                new Claim(ClaimTypes.Role, userObtenido.Rol),
                new Claim("empresaId", userObtenido.IdEmpresa.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"])
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
