using Api_SAAS_legumfrut.Data;
using Api_SAAS_legumfrut.Dtos.cliente;
using Api_SAAS_legumfrut.Entities;
using Dapper;

namespace Api_SAAS_legumfrut.Repository.repoCliente
{
    public class ClienteRepos : IClienteRepos
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public ClienteRepos(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        
        public async Task<IEnumerable<ClientePresentacionDto>> GetAllCliAsync(int idEmpresa, int page, int pageSize, CancellationToken cancellationToken)
        {
            var sql = @"
                SELECT
                    IdCliente,
                    Nombre,
                    email,
                    Tipo,
                    Estado,
                    FechaCreacion
                FROM Clientes
                WHERE IdEmpresa = @idEmpresa
                    AND IsDeleted = 0
                ORDER BY FechaCreacion DESC
                OFFSET @Offset ROWS FETCH NEXT @pageSize ROWS ONLY;";
            using var connection = _connectionFactory.CreateConnection();

            var result = await connection.QueryAsync<ClientePresentacionDto>(
                new CommandDefinition(
                    sql,
                    new { 
                        idEmpresa,
                        Offset = (page - 1) * pageSize,
                        pageSize 
                    },
                    cancellationToken: cancellationToken

                    )
            );

            return result;
        }
        
        public async Task<Cliente?> GetCliIdAsync(int idCliente, int idEmpresa, CancellationToken cancellationToken)
        {
            var sql = @"
                SELECT *
                FROM Clientes

                WHERE IdCliente = @idCliente
                    AND IdEmpresa = @idEmpresa
                    AND IsDeleted = 0;  

            ";
            using var connection = _connectionFactory.CreateConnection();

            var resultado =  await connection.QueryFirstOrDefaultAsync<Cliente>(
                new CommandDefinition(
                    sql,
                    new {
                        idCliente,
                        idEmpresa
                    },
                    cancellationToken: cancellationToken
                 )
             );

            return resultado;
        }

        public async Task<Cliente> CreateCliAsync(Cliente cliente, int idEmpresa, CancellationToken cancellationToken)
        {
            var sql = @"
                INSERT INTO Clientes
                (
                    IdEmpresa,
                    Nombre,
                    Telefono,
                    CedulaRuc,
                    Email,
                    Tipo,
                    Estado,
                    FechaCreacion,
                    LimiteCredito,
                    DiasCredito
                )
                VALUES
                (
                    @IdEmpresa,
                    @Nombre,
                    @Telefono,
                    @CedulaRuc,
                    @Email,
                    @Tipo,
                    1,
                    GETUTCDATE(),
                    @LimiteCredito,
                    @DiasCredito
                );

                SELECT CAST(SCOPE_IDENTITY() as int);";

            using var connection = _connectionFactory.CreateConnection();

            var id = await connection.ExecuteScalarAsync<int>(
                new CommandDefinition(
                    sql,
                    new {
                        IdEmpresa = idEmpresa,
                        cliente.Nombre,
                        cliente.Telefono,
                        cliente.CedulaRuc,
                        cliente.Email,
                        cliente.Tipo,
                        cliente.LimiteCredito,
                        cliente.DiasCredito
                    },
                    cancellationToken: cancellationToken
                    )
               );
            cliente.IdCliente = id;
            return cliente;
        }

        public async Task<bool> DeleteCliAsync(int idCliente, int idEmpresa, int idUsuarioEliminacion, CancellationToken cancellationToken)
        {
            var sql = @"
                UPDATE Clientes
                SET 
                    IsDeleted = 1,
                    Estado = 0,
                    fechaEliminacion = GETUTCDATE(),
                    IdUsuarioEliminacion = @IdUsuario
                WHERE IdCliente = @IdCliente
                    AND IdEmpresa = @IdEmpresa
                    AND IsDeleted = 0;
                    
            ";

            using var connection = _connectionFactory.CreateConnection();

            var resultado = await connection.ExecuteAsync(
                new CommandDefinition(
                    sql,
                    new 
                    {
                        IdCliente = idCliente,
                        IdEmpresa = idEmpresa,
                        IdUsuario = idUsuarioEliminacion
                    },
                    cancellationToken: cancellationToken
                 )
             );

            return resultado > 0;
        }

        

        

        public  async  Task<bool> UpdateCliAsync(Cliente cliente, int idEmpresa, CancellationToken cancellationToken)
        {
            var sql = @"
                UPDATE Clientes
                SET 
                    Nombre = @Nombre,
                    Telefono = @Telefono,
                    CedulaRuc = @CedulaRuc,
                    Email = @Email,
                    Tipo = @Tipo,
                    LimiteCredito = @LimiteCredito,
                    DiasCredito = @DiasCredito
                WHERE IdCliente = @IdCliente
                    AND IdEmpresa = @IdEmpresa
                    AND IsDeleted = 0;
            ";

            using var connection = _connectionFactory.CreateConnection();
            var resultado = await connection.ExecuteAsync(
                new CommandDefinition(
                    sql,
                    new {
                        cliente.IdCliente,
                        idEmpresa = idEmpresa,
                        cliente.Nombre,
                        cliente.Telefono,
                        cliente.Email,
                        cliente.Tipo,
                        cliente.CedulaRuc,
                        cliente.LimiteCredito,
                        cliente.DiasCredito

                    },
                    cancellationToken: cancellationToken
                )
            );
            return resultado > 0;
        }
    }
}
