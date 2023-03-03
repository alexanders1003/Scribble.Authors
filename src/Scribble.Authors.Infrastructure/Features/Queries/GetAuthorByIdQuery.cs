using MediatR;
using Scribble.Authors.Contracts.Proto;
using Scribble.Authors.Infrastructure.Data.Requests.Queries;
using Scribble.Shared.Infrastructure.Factories;

namespace Scribble.Authors.Infrastructure.Features.Queries;

public class GetAuthorByIdQuery : IRequest<AuthorModel?>
{
    public GetAuthorByIdQuery(Guid id) => Id = id;
    public Guid Id { get; }
}

public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, AuthorModel?>
{
    private readonly IUnitOfWorkFactory _factory;

    public GetAuthorByIdQueryHandler(IUnitOfWorkFactory factory) => _factory = factory;

    public async Task<AuthorModel?> Handle(GetAuthorByIdQuery request, CancellationToken token)
    {
        using var unitOfWork = await _factory.CreateAsync(false, token)
            .ConfigureAwait(false);

        return await unitOfWork.ExecuteAsync(new GetAuthorByIdDbQuery(request), token)
            .ConfigureAwait(false);
    }
}