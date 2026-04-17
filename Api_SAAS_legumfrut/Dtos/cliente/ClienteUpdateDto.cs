namespace Api_SAAS_legumfrut.Dtos.cliente
{
    public class ClienteUpdateDto
    {

        public string Nombre { get; set; } = null!;
        public string? Telefono { get; set; }
        public string? CedulaRuc { get; set; }
        public string? Email { get; set; }
        public string? Tipo { get; set; }

        public bool Estado { get; set; }

        public decimal LimiteCredito { get; set; }
        public int DiasCredito { get; set; }

    }
}
