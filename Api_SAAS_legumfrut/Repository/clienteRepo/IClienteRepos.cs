using Api_SAAS_legumfrut.Dtos.cliente;
using Api_SAAS_legumfrut.Entities;

namespace Api_SAAS_legumfrut.Repository.repoCliente
{
    public interface IClienteRepos
    {
        Task<IEnumerable<ClientePresentacionDto>> GetAllCliAsync(int idEmpresa,int page, int pageSize, CancellationToken cancellationToken);
        Task<Cliente?> GetCliIdAsync(int idCliente, int idEmpresa, CancellationToken cancellationToken);
        Task<Cliente> CreateCliAsync(Cliente cliente, int idEmpresa, CancellationToken cancellationToken);
        Task<bool> UpdateCliAsync(Cliente cliente, int idEmpresa, CancellationToken cancellationToken);
        Task<bool> DeleteCliAsync(int idCliente, int idEmpresa, int idUsuarioEliminacion, CancellationToken cancellationToken);


    }
}
