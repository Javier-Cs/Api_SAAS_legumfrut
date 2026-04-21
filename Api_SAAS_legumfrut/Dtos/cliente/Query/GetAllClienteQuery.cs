using MediatR;

namespace Api_SAAS_legumfrut.Dtos.cliente.Query
{
    public record GetAllClienteQuery(int page, int pageSize)
        : IRequest<IEnumerable<ClientePresentacionDto>>;


    public record GetClienteByIdQuery(int IdCliente)
        : IRequest<ClientePresentacionDto?>;

}
