namespace Api_SAAS_legumfrut.Entities
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public int IdEmpresa { get; set; }

        public string Nombre { get; set; } = null!;
        public string? Telefono { get; set; }
        public string? CedulaRuc { get; set; }
        public string? Email { get; set; }
        public string? Tipo { get; set; }
 
        public bool Estado { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public DateTime FechaCreacion { get; set; }
        //public DateTime FechaActualizacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public int? IdUsuarioEliminacion { get; set; }

        public decimal LimiteCredito { get; set; }
        public int DiasCredito { get; set; }

        public string? Aux1 { get; set; }
        public string? Aux2 { get; set; }
        public string? Aux3 { get; set; }
    }
}
