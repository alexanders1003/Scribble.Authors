using MassTransit;
using Scribble.Authors.Infrastructure.Data.Requests.Commands;
using Scribble.Identity.Contracts.Events;
using Scribble.Shared.Infrastructure.Factories;

namespace Scribble.Authors.Web.Consumers;

public class IdentityUserRegisteredConsumer : IConsumer<IdentityUserRegisteredContract>
{
    private readonly IUnitOfWorkFactory _factory;

    public IdentityUserRegisteredConsumer(IUnitOfWorkFactory factory) => _factory = factory;

    public async Task Consume(ConsumeContext<IdentityUserRegisteredContract> context)
    {
        using var unitOfWork = await _factory.CreateAsync(true, context.CancellationToken)
            .ConfigureAwait(false);

        await unitOfWork.ExecuteAsync(new CreateAuthorDbCommand(new { context.Message.Id }),
                context.CancellationToken)
            .ConfigureAwait(false);
        
        unitOfWork.Commit();
    }
}

public class IdentityUserRegisteredConsumerDefinition : ConsumerDefinition<IdentityUserRegisteredConsumer>
{
    public IdentityUserRegisteredConsumerDefinition()
    {
        EndpointName = "create-author-consumer";
        ConcurrentMessageLimit = 8;
    }

    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<IdentityUserRegisteredConsumer> consumerConfigurator)
    {
        endpointConfigurator.UseMessageRetry(i => i.Intervals(100, 200, 500, 800, 1000));
        endpointConfigurator.UseInMemoryOutbox();
    }
}
