namespace Api_SAAS_legumfrut.Entities
{
    public class User
    {
        public int IdUser { get; set; }
        public int IdEmpresa { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PassHash { get; set; } = string.Empty; // nunca se expone
        public string Cedula { get; set; }
        public string NumeroTelefono { get; set; }
        public string Rol { get; set; }
        public bool Estado { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }

    }
}
