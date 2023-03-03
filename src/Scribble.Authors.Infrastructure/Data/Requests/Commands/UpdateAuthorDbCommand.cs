using System.Data;
using Dapper;
using Scribble.Shared.Infrastructure;

namespace Scribble.Authors.Infrastructure.Data.Requests.Commands;

public class UpdateAuthorDbCommand : IDbRequest
{
    private readonly object _parameters;
    private const string Query = """
         UPDATE Authors 
         SET FirstName = @FirstName, LastName = @LastName
         WHERE Id = @Id;
         """;

    public UpdateAuthorDbCommand(object parameters) => _parameters = parameters;

    public async Task ExecuteAsync(IDbConnection connection, IDbTransaction? transaction,
        CancellationToken token = default)
    {
        await connection.ExecuteAsync(Query, _parameters, transaction)
            .ConfigureAwait(false);
    }
}