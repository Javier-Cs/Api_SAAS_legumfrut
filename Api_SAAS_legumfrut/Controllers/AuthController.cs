using Api_SAAS_legumfrut.Dtos.Login;
using Api_SAAS_legumfrut.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Api_SAAS_legumfrut.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config, AuthService authService) {
            _authService = authService;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginRequestDto requestDto,
            CancellationToken ct
            )
        {
            try
            {
                var result = await _authService.LoginAsync(requestDto);
                return Ok(result);
            }
            catch (Exception ex) {
                return StatusCode(500, new {
                    mensaje = "Error de conexion de base de datos.",
                    detalle = ex.Message,
                });
            }
        }


        [HttpGet("test-db")]
        public async Task<IActionResult> TestDb()
        {
            try
            {
                using var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
                await conn.OpenAsync();
                return Ok("Conexion exitosa");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    
    }
}
