using Api_SAAS_legumfrut.Dtos.Login;
using Api_SAAS_legumfrut.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api_SAAS_legumfrut.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService) {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto requestDto) 
        {
            try
            {
                var result = await _authService.LoginAsync(requestDto);
                return Ok(result);
            }
            catch (Exception ex) {
                return Unauthorized( new { 
                    mensaje = ex.Message,
                });
            }
        }
    }
}
