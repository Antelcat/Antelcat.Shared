using Antelcat.Server.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Antelcat.Server.Extensions;

public partial class ServiceExtension
{
    public static IMvcBuilder AddAntelcatControllers(this IServiceCollection collection)
    {
        return collection.AddControllers(x =>
        {
            x.Filters.Add<ExceptionHandlerFilter>();
        });
    }
}