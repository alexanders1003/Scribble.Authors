using System.Data;
using Dapper;
using Scribble.Authors.Contracts.Proto;
using Scribble.Shared.Infrastructure;

namespace Scribble.Authors.Infrastructure.Data.Requests.Queries;

public class GetAuthorByIdDbQuery : IDbRequest<AuthorModel?>
{
    private readonly object _parameters;
    private const string Query = "SELECT * FROM Authors WHERE IdentityId = @IdentityId";

    public GetAuthorByIdDbQuery(object parameters) => _parameters = parameters;

    public async Task<AuthorModel?> ExecuteAsync(IDbConnection connection, IDbTransaction? transaction,
        CancellationToken token = default)
    {
        return await connection
            .QuerySingleOrDefaultAsync<AuthorModel>(Query, _parameters, transaction)
            .ConfigureAwait(false);
    }
}