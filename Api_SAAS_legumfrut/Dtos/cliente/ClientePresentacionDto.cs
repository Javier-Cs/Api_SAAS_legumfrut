namespace Api_SAAS_legumfrut.Dtos.cliente
{
    public class ClientePresentacionDto
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Email { get; set; }
        public string? Tipo { get; set; }
        public bool Estado { get; set; } = true;
        public DateTime FechaCreacion { get; set; }
    }
}
