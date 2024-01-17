using Microsoft.AspNetCore.Http;

namespace Antelcat.Server.Configs;

public class AntelcatFilterConfig
{
    public bool OutputTrace { get; set; }

    public AntelcatFilterConfig RegisterExceptionHandler<TException>(Action<TException, HttpResponse> handler)
        where TException : Exception
    {
        ExceptionHandlers[typeof(TException)] = (exception, response) => handler((TException)exception, response);
        return this;
    }

    public AntelcatFilterConfig RegisterExceptionHandler(Type exceptionType, Action<Exception, HttpResponse> handler)
    {
        ExceptionHandlers[exceptionType] = (exception, response) => handler((Exception)exception, response);
        return this;
    }

    internal void ExecuteHandler(Exception exception, HttpResponse response)
    {
        if (ExceptionHandlers.TryGetValue(exception.GetType(), out var handler)) handler.Invoke(exception, response);
    }
    
    internal Dictionary<Type, Action<object, HttpResponse>> ExceptionHandlers { get; } = new();

    internal static readonly AntelcatFilterConfig Default = new();
}