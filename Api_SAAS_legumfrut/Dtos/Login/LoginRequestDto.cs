namespace Api_SAAS_legumfrut.Dtos.Login
{
    public class LoginRequestDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? NombreEmpresa { get; set; }
    }
}
