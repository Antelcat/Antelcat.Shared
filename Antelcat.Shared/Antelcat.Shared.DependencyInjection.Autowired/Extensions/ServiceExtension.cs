using Microsoft.Extensions.DependencyInjection;
using Antelcat.Implements.Services;
using Antelcat.Attributes;
namespace Antelcat.Extensions;

public static partial class ServiceExtension
{
    /// <summary>
    /// 创建用于解析 <see cref="AutowiredAttribute"/> 的 <see cref="AutowiredServiceProvider{AutowiredAttribute}"/>
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IServiceProvider BuildAutowiredServiceProvider(this IServiceCollection collection,
        Func<IServiceCollection, IServiceProvider> builder)
        => new AutowiredServiceProvider<AutowiredAttribute>(builder(collection), collection);

    /// <summary>
    /// 创建用于解析 <see cref="TAttribute"/> 的 <see cref="AutowiredServiceProvider{TAttribute}"/>
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="builder"></param>
    /// <typeparam name="TAttribute"></typeparam>
    /// <returns></returns>
    public static IServiceProvider BuildAutowiredServiceProvider<TAttribute>(this IServiceCollection collection,
        Func<IServiceCollection, IServiceProvider> builder) where TAttribute : Attribute
        => new AutowiredServiceProvider<TAttribute>(builder(collection), collection);
}