using Api_SAAS_legumfrut.Entities;

namespace Api_SAAS_legumfrut.Auth
{
    public class UserWithEmpresa
    {
        public User user { get; set;}
        public string NombreEmpresa { get; set;}
    }
}
