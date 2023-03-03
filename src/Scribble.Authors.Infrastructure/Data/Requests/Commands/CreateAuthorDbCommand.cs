using System.Data;
using Dapper;
using Scribble.Shared.Infrastructure;

namespace Scribble.Authors.Infrastructure.Data.Requests.Commands;

public class CreateAuthorDbCommand : IDbRequest
{
    private readonly object _parameters;
    private const string Query = """
         INSERT INTO Authors (IdentityId, FirstName, LastName) 
         VALUES (@IdentityId, @FirstName, @LastName)
         """;

    public CreateAuthorDbCommand(object parameters) => _parameters = parameters;

    public async Task ExecuteAsync(IDbConnection connection, IDbTransaction? transaction,
        CancellationToken token = default)
    {
        await connection.ExecuteAsync(Query, _parameters, transaction)
            .ConfigureAwait(false);
    }
}