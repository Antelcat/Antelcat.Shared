using System.Net.Mime;
using Antelcat.Server.Configs;
using Antelcat.Server.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Antelcat.Server.Filters;

[Serializable]
public class ExceptionHandlerFilter(ILogger<ExceptionHandlerFilter> logger, AntelcatFilterConfig config)
    : AntelcatFilter(config), IAsyncExceptionFilter
{
    private readonly ILogger<ExceptionHandlerFilter> logger = logger ?? throw new ArgumentNullException(nameof(logger));

    private readonly Func<Exception, string> stackTraceFormatter = config.OutputTrace
        ? static exception => exception.ToString()
        : static _ => "Internal Server Error";

    public async Task OnExceptionAsync(ExceptionContext context)
    {
        if (context.HttpContext.Response.HasStarted) return;
        switch (context.Exception)
        {
            case RejectException exception:
                await Handle(context.HttpContext, exception);
                break;
            default:
                await Handle(context.HttpContext, context.Exception);
                break;
        }

        await context.HttpContext.Response.CompleteAsync();

        logger.LogError("{Exception}", context.Exception);
    }

    private static async Task Handle(HttpContext context, RejectException exception)
    {
        context.Response.StatusCode = exception.StatusCode;
        switch (exception.Data)
        {
            case null:
                context.Response.ContentType = MediaTypeNames.Text.Plain;
                await context.Response.WriteAsync(string.Empty);
                return;
            case string content:
                await context.Response.WriteAsync(content);
                return;
            case not null when exception.Data.GetType().IsPrimitive:
                context.Response.ContentType = MediaTypeNames.Text.Plain;
                await context.Response.WriteAsync($"{exception.Data}");
                return;
            default:
                context.Response.ContentType = MediaTypeNames.Application.Json;
                await context.Response.WriteAsJsonAsync(exception.Data);
                return;
        }

    }

    private async Task Handle(HttpContext context, Exception exception)
    {
        context.Response.Clear();
        context.Response.Headers.Clear();
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsync(stackTraceFormatter(exception));
    }
}