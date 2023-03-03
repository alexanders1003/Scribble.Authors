using MediatR;
using Scribble.Authors.Contracts.Proto;
using Scribble.Authors.Infrastructure.Data.Requests.Commands;
using Scribble.Shared.Infrastructure.Factories;

namespace Scribble.Authors.Infrastructure.Features.Commands;

public class UpdateAuthorCommand : IRequest
{
    public UpdateAuthorCommand(AuthorModel model) => Model = model;
    public AuthorModel Model { get; }
}

public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand>
{
    private readonly IUnitOfWorkFactory _factory;

    public UpdateAuthorCommandHandler(IUnitOfWorkFactory factory) => _factory = factory;

    public async Task Handle(UpdateAuthorCommand request, CancellationToken token)
    {
        using var unitOfWork = await _factory.CreateAsync(true, token)
            .ConfigureAwait(false);

        await unitOfWork.ExecuteAsync(new UpdateAuthorDbCommand(request), token)
            .ConfigureAwait(false);
        
        unitOfWork.Commit();
    }
}