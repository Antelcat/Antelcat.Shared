using Antelcat.Attributes;
using Antelcat.Implements.Services;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace Antelcat.Extensions;

public static partial class ServiceExtension
{
    /// <summary>
    /// 使用 <see cref="AutowiredServiceProviderFactory{AutowiredAttribute}"/> 作为服务生成工厂， 
    /// 实现自动注入携带 <see cref="AutowiredAttribute"/> 注解的属性和字段
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IHostBuilder UseAutowiredServiceProviderFactory(this IHostBuilder builder)
        => builder.UseServiceProviderFactory(new AutowiredServiceProviderFactory<AutowiredAttribute>(
            ServiceCollectionContainerBuilderExtensions.BuildServiceProvider));

    /// <summary>
    /// 使用 <see cref="AutowiredServiceProviderFactory{TAttribute}"/> 作为服务生成工厂
    /// </summary>
    /// <typeparam name="TAttribute">属性</typeparam>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IHostBuilder UseAutowiredServiceProviderFactory<TAttribute>(this IHostBuilder builder)
        where TAttribute : Attribute
        => builder.UseServiceProviderFactory(new AutowiredServiceProviderFactory<TAttribute>(
            ServiceCollectionContainerBuilderExtensions.BuildServiceProvider));

    /// <summary>
    /// 将 <see cref="IControllerActivator"/> 的实现替换为 <see cref="AutowiredControllerActivator{AutowiredAttribute}"/> ,
    /// 且应当在 <see cref="MvcCoreMvcBuilderExtensions.AddControllersAsServices"/> 之后调用
    /// </summary>
    /// <param name="collection"></param>
    /// <returns></returns>
    public static IServiceCollection UseAutowiredControllers(this IMvcBuilder collection)
        => collection.Services.Replace(ServiceDescriptor
            .Transient<IControllerActivator, AutowiredControllerActivator<AutowiredAttribute>>());

    /// <summary>
    /// 将 <see cref="IControllerActivator"/> 的实现替换为 <see cref="AutowiredControllerActivator{TAttribute}"/> ,
    /// 且应当在 <see cref="MvcCoreMvcBuilderExtensions.AddControllersAsServices"/> 之后调用
    /// </summary>
    /// <param name="collection"></param>
    /// <typeparam name="TAttribute"></typeparam>
    /// <returns></returns>
    public static IServiceCollection UseAutowiredControllers<TAttribute>(this IMvcBuilder collection)
        where TAttribute : Attribute
        => collection.Services.Replace(ServiceDescriptor
            .Transient<IControllerActivator, AutowiredControllerActivator<TAttribute>>());
}