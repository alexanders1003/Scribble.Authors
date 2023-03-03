using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scribble.Authors.Contracts.Proto;
using Scribble.Authors.Infrastructure.Features.Queries;

namespace Scribble.Authors.Web.Controllers;

[ApiController]
[Route("/api/v1/authors/{id:guid}")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AuthorsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthorsController(IMediator mediator) => _mediator = mediator;

    [HttpGet, AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(AuthorModel), StatusCodes.Status200OK)]
    public async Task<ActionResult<AuthorModel?>> GetAuthorByIdAsync(Guid id)
    {
        if (id.Equals(Guid.Empty))
            return new BadRequestResult();

        var model = await _mediator.Send(new GetAuthorByIdQuery(id), HttpContext.RequestAborted)
            .ConfigureAwait(false);

        if (model is not null)
            return new OkObjectResult(model);
        return new NotFoundResult();
    }

    [HttpGet, AllowAnonymous]
    public async Task<ActionResult<ExistsByAuthorIdResponse>> ExistsByAuthorId(Guid id)
    {
        var actionResult = await GetAuthorByIdAsync(id)
            .ConfigureAwait(false);

        return actionResult.Result switch
        {
            OkObjectResult =>
                new OkObjectResult(new ExistsByAuthorIdResponse { Status = true }),
            
            NotFoundResult =>
                new NotFoundObjectResult(new ExistsByAuthorIdResponse { Status = false }),
            
            _ => new BadRequestResult()
        };
    }
}