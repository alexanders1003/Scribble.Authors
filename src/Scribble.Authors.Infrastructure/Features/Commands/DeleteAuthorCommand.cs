using MediatR;
using Scribble.Authors.Infrastructure.Data.Requests.Commands;
using Scribble.Shared.Infrastructure.Factories;

namespace Scribble.Authors.Infrastructure.Features.Commands;

public class DeleteAuthorCommand : IRequest
{
    public DeleteAuthorCommand(Guid id) => Id = id;
    public Guid Id { get; }
}

public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand>
{
    private readonly IUnitOfWorkFactory _factory;

    public DeleteAuthorCommandHandler(IUnitOfWorkFactory factory) => _factory = factory;

    public async Task Handle(DeleteAuthorCommand request, CancellationToken token)
    {
        using var unitOfWork = await _factory.CreateAsync(true, token)
            .ConfigureAwait(false);

        await unitOfWork.ExecuteAsync(new DeleteAuthorDbCommand(request), token)
            .ConfigureAwait(false);
        
        unitOfWork.Commit();
    }
}