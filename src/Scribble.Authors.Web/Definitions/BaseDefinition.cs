using Calabonga.AspNetCore.AppDefinitions;
using MediatR;

namespace Scribble.Authors.Web.Definitions;

public class BaseDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddControllers();

        services.AddMediatR(typeof(Program));

        services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });
    }
}