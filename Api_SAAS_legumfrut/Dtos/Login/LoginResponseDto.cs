namespace Api_SAAS_legumfrut.Dtos.Login
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }

        // Contexto multiempresa
        public int IdEmpresa { get; set; }
        public string NombreEmpresa { get; set; } = string.Empty;

        // Contexto de seguridad
        public string Rol { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
        public bool Estado { get; set; }
    }
}
