using MediatR;
using Scribble.Authors.Contracts.Proto;
using Scribble.Authors.Infrastructure.Data.Requests.Commands;
using Scribble.Shared.Infrastructure.Factories;

namespace Scribble.Authors.Infrastructure.Features.Commands;

public class CreateAuthorCommand : IRequest
{
    public CreateAuthorCommand(AuthorModel model) => Model = model;
    public AuthorModel Model { get; }
}

public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand>
{
    private readonly IUnitOfWorkFactory _factory;

    public CreateAuthorCommandHandler(IUnitOfWorkFactory factory) => _factory = factory;

    public async Task Handle(CreateAuthorCommand request, CancellationToken token)
    {
        using var unitOfWork = await _factory.CreateAsync(true, token)
            .ConfigureAwait(false);

        await unitOfWork.ExecuteAsync(new CreateAuthorDbCommand(request), token)
            .ConfigureAwait(false);
        
        unitOfWork.Commit();
    }
}