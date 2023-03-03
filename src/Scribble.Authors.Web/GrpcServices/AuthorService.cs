using Grpc.Core;
using MediatR;
using Scribble.Authors.Contracts.Proto;
using Scribble.Authors.Infrastructure.Features.Queries;

namespace Scribble.Authors.Web.GrpcServices;

public class AuthorService : AuthorsProtoService.AuthorsProtoServiceBase
{
    private readonly IMediator _mediator;

    public AuthorService(IMediator mediator) => _mediator = mediator;

    public override async Task<AuthorModel?> GetAuthorById(GetAuthorByIdRequest request, ServerCallContext context)
    {
        if (Guid.TryParse(request.Id, out var result)) 
            return default;

        return await _mediator.Send(new GetAuthorByIdQuery(result), context.CancellationToken)
            .ConfigureAwait(false);
    }

    public override async Task<ExistsByAuthorIdResponse> ExistsById(ExistsByAuthorIdRequest request, ServerCallContext context)
    {
        if (Guid.TryParse(request.Id, out var result)) 
            return new ExistsByAuthorIdResponse { Status = false };

        var model = await _mediator.Send(new GetAuthorByIdQuery(result), context.CancellationToken)
            .ConfigureAwait(false);

        return new ExistsByAuthorIdResponse { Status = model is not null };
    }
}