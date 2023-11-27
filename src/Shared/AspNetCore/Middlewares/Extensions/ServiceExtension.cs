using Antelcat.Server.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Antelcat.Server.Extensions;

public partial class ServiceExtension
{
    public static IMvcBuilder AddAntelcatFilters(this IMvcBuilder builder)
    {
        return builder.AddMvcOptions(x =>
        {
            x.Filters.Add<ExceptionHandlerFilter>();
        });
    }
}