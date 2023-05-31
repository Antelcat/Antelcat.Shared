using System.Reflection.Metadata.Ecma335;
using Antelcat.Structs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace Antelcat.Implements.Services;

public abstract class ControllerActivatorBase<TServiceProvider> : IControllerActivator 
    where TServiceProvider : IServiceProvider
{
    protected abstract TServiceProvider ProvideService(IServiceProvider provider);
    public object Create(ControllerContext? context)
    {
        if (context == null)
            throw new ArgumentNullException($"{nameof(ControllerContext)} is null");
        var provider = context.HttpContext.RequestServices;
        if (provider is not TServiceProvider)
            provider = ProvideService(context.HttpContext.RequestServices);
        return provider.GetRequiredService(context.ActionDescriptor.ControllerTypeInfo.AsType());
    }
    public void Release(ControllerContext? context, object controller)
    {
        if (context == null) throw new ArgumentNullException($"{nameof(ControllerContext)} is null");
        switch (controller)
        {
            case null:
                throw new ArgumentNullException(nameof(controller));
            case IDisposable disposable:
                disposable.Dispose();
                break;
        }
    }
}

public class AutowiredControllerActivator<TAttribute> 
    : ControllerActivatorBase<AutowiredServiceProvider<TAttribute>> 
    where TAttribute : Attribute
{
    private readonly IServiceCollection collection;
    private AutowiredServiceProvider<TAttribute>? serviceProvider;
    public AutowiredControllerActivator(IServiceCollection collection) => this.collection = collection;

    protected override AutowiredServiceProvider<TAttribute> ProvideService(
        IServiceProvider provider)
    {
        if (serviceProvider == null)
        {
            serviceProvider = new AutowiredServiceProvider<TAttribute>(provider, collection);
        }
        else
        {
            serviceProvider.SharedInfos.ServiceProvider = provider;
        }

        return serviceProvider;
    }
}

