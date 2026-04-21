using MediatR;

namespace Api_SAAS_legumfrut.Dtos.cliente.Commands
{
    public record ClienteCreateCommand(
        string Nombre,
        string? Telefono,
        string? CedulaRuc,
        string? Email,
        string? Tipo
    ) : IRequest<int>;

    public record ClienteUpdateCommand(
        int IdCliente,
        string Nombre,
        string? Telefono,
        string? CedulaRuc,
        string? Email,
        string? Tipo,
        bool Estado,
        decimal LimiteCredito,
        int DiasCredito
    ) : IRequest<bool>;

    public record ClienteDeleteCommand(
        int IdCliente
    ) : IRequest<bool>;
}
