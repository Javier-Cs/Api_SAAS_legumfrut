namespace Api_SAAS_legumfrut.Dtos.cliente
{
    public class ClienteCreateDto
    {
        public int IdEmpresa { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Telefono { get; set; }
        public string? CedulaRuc { get; set; }
        public string? Email { get; set; }

        public string? Tipo { get; set; }

    }
}
