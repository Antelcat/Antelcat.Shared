﻿#if !NET && !NETSTANDARD
using System;
using System.Collections.Generic;
using System.Linq;
#endif
using System.Reflection;
using System.Runtime.Serialization;
using Antelcat.Extensions;
using Antelcat.Structs;
using Microsoft.Extensions.DependencyInjection;

namespace Antelcat.Implements.Services;
#nullable enable
using SetterCache = Tuple<Type, Setter<object, object>>;

public abstract class ProxiedServiceProvider
    : IServiceProvider, ISupportRequiredService
{
    public object GetRequiredService(Type serviceType) =>
        GetService(serviceType)
        ?? throw new SerializationException($"Unable to resolve service : [ {serviceType} ]");

    protected void Autowired(object target, IEnumerable<SetterCache> mapper) =>
        mapper.ForEach(x =>
        {
            var dependency = ProvideDependency(x.Item1);
            if (dependency != null) x.Item2.Invoke(ref target!, dependency);
        });

    public abstract object? GetService(Type serviceType);

    protected abstract object? ProvideDependency(Type dependencyType);
}

public abstract class CachedAutowiredServiceProvider<TAttribute>
    : ProxiedServiceProvider
    where TAttribute : Attribute
{

    #region Caches

    /// <summary>
    /// 共享的缓存数据
    /// </summary>
    internal ServiceInfos SharedInfos { get; }

    private const BindingFlags Flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

    /// <summary>
    /// 判断实现类型是否需要自动注入,如果需要则将Mapper添加到缓存中
    /// </summary>
    /// <param name="implementType"></param>
    /// <returns></returns>
    protected bool NeedAutowired(Type implementType) => SharedInfos.GetStat(implementType).NeedAutowired;

    private static ImplementInfo CreateStat(IReflect implementType)
    {
        var props = GetProps(implementType).ToList();
        var fields = GetFields(implementType).ToList();
        var need = props.Any() || fields.Any();
        var stat = new ImplementInfo
        {
            NeedAutowired = need
        };
        if (!need) return stat;
        stat.Mappers = props
            .Select(static x => new SetterCache(x.PropertyType, x.CreateSetter()))
            .Concat(fields.Select(static x => new SetterCache(x.FieldType, x.CreateSetter())))
            .ToList();
        return stat;
    }
    private static IEnumerable<PropertyInfo> GetProps(IReflect implementType) => implementType
        .GetProperties(Flags)
        .Where(static x => x.CanWrite && x.GetCustomAttribute<TAttribute>() != null);

    private static IEnumerable<FieldInfo> GetFields(IReflect implementType) => implementType
        .GetFields(Flags)
        .Where(static x => x.GetCustomAttribute<TAttribute>() != null);

    #endregion

    protected IServiceProvider ServiceProvider => SharedInfos.ServiceProvider;
    protected CachedAutowiredServiceProvider(IServiceProvider serviceProvider, ServiceInfos? serviceInfos = null) =>
        SharedInfos = serviceInfos ?? new ServiceInfos(serviceProvider, CreateStat);
    
    protected void Autowired(object target)
    {
        var type = target.GetType();
        var mapper = SharedInfos.GetStat(type);
        if (!mapper.NeedAutowired) return;
        Autowired(target, mapper.Mappers!);
    }
}

/// <summary>
/// 针对 <see cref="ServiceLifetime.Transient"/> 生命周期的依赖构造的Provider, 不需要考虑连续依赖的问题
/// </summary>
/// <typeparam name="TAttribute"></typeparam>
public class TransientAutowiredServiceProvider<TAttribute>
    : CachedAutowiredServiceProvider<TAttribute>
    where TAttribute : Attribute
{
    public TransientAutowiredServiceProvider(IServiceProvider serviceProvider) 
        : base(serviceProvider) { }

    public override object? GetService(Type serviceType)
    {
        var impl = ServiceProvider.GetService(serviceType);
        if (impl == null || !NeedAutowired(impl.GetType())) return impl;
        Autowired(impl);
        return impl;
    }

    protected override object? ProvideDependency(Type dependencyType) =>
        ServiceProvider.GetService(dependencyType);
}

public class AutowiredServiceProvider<TAttribute> 
    : CachedAutowiredServiceProvider<TAttribute>
    where TAttribute : Attribute
{
    internal AutowiredServiceProvider(IServiceProvider serviceProvider, ServiceInfos serviceInfos)
        : base(serviceProvider, serviceInfos) { }

    private AutowiredServiceProvider(IServiceProvider serviceProvider,
        Func<Dictionary<Type, ServiceLifetime>> serviceLifetimes) : base(serviceProvider) =>
        SharedInfos.ServiceLifetimes = new Lazy<Dictionary<Type, ServiceLifetime>>(serviceLifetimes);

    public AutowiredServiceProvider(IServiceProvider serviceProvider, IServiceCollection collection)
        : this(serviceProvider, () => collection
            .Aggregate(new Dictionary<Type, ServiceLifetime>(), (d, s) =>
            {
#if NET || NETSTANDARD
                d.TryAdd(s.ServiceType, s.Lifetime);
#else
                if(!d.ContainsKey(s.ServiceType)){
                    d.Add(s.ServiceType, s.Lifetime);
                }
#endif
                return d;
            }))
    {
    }

    public override object? GetService(Type serviceType)
    {
        var impl = ServiceProvider.GetService(serviceType);
        if (SharedInfos.NoNeedAutowired(serviceType, impl)) return impl;
        return impl switch
        {
            IServiceScopeFactory factory => new AutowiredServiceScopeFactory(factory,
                s => new AutowiredServiceProvider<TAttribute>(s, SharedInfos.CreateScope(s))),
            IEnumerable<object> collections => GetServicesInternal(collections, serviceType),
            _ => GetServiceInternal(impl!, serviceType)
        };
    }

    private object GetServicesInternal(IEnumerable<object> targets, Type serviceType)
    {
        var enumerable = targets as object[] ?? targets.ToArray();
        if (!enumerable.Any()) return targets;
        var type = serviceType;
        if (!TryGetServiceLifetime(type, out var lifetime))
        {
            var types = serviceType.GenericTypeArguments;
            if (types.Length == 0) throw new ArgumentException($"Service type {serviceType} has no generic type");
            type = types[0];
            if (!TryGetServiceLifetime(type, out lifetime))
            {
                return targets;
            }
        }

        switch (lifetime)
        {
            case ServiceLifetime.Singleton:
                SharedInfos.ResolvedSingletons.Add(serviceType);
                break;
            case ServiceLifetime.Scoped:
                SharedInfos.ResolvedScopes.Add(serviceType);
                break;
        }

        enumerable.ForEach(Autowired);
        return targets;
    }

    private object? GetServiceInternal(object instance, Type serviceType) =>
        TryGetServiceLifetime(serviceType, out var lifetime)
            ? AutowiredService(instance, serviceType, lifetime)
            : instance;


    private object? GetServiceDependency(object? target, Type serviceType, ServiceLifetime lifetime) =>
        SharedInfos.NoNeedAutowired(serviceType, target)
            ? target
            : AutowiredService(target!, serviceType, lifetime);

    private object AutowiredService(object target, Type serviceType, ServiceLifetime lifetime)
    {
        switch (lifetime)
        {
            case ServiceLifetime.Singleton:
                SharedInfos.ResolvedSingletons.Add(serviceType);
                break;
            case ServiceLifetime.Scoped:
                SharedInfos.ResolvedScopes.Add(serviceType);
                break;
        }

        Autowired(target);
        return target;
    }

    protected override object? ProvideDependency(Type dependencyType)
    {
        var dep = ServiceProvider.GetService(dependencyType);
        return dep switch
        {
            null => null,
            IEnumerable<object> collection => GetServicesInternal(collection, dependencyType),
            _ => TryGetServiceLifetime(dependencyType, out var targetLifetime)
                ? GetServiceDependency(dep, dependencyType, targetLifetime)
                : dep
        };
    }

    private bool TryGetServiceLifetime(Type serviceType, out ServiceLifetime serviceLifetime) =>
        SharedInfos.ServiceLifetimes!.Value.TryGetValue(serviceType, out serviceLifetime)
        || serviceType.IsGenericType
        && SharedInfos.ServiceLifetimes.Value.TryGetValue(serviceType.GetGenericTypeDefinition(),
            out serviceLifetime);
}

public class AutowiredServiceScope : IServiceScope
{
    private readonly IServiceScope proxy;

    public AutowiredServiceScope(IServiceScope scope, Func<IServiceProvider, IServiceProvider> provider)
    {
        proxy = scope;
        ServiceProvider = provider(scope.ServiceProvider);
    }

    public void Dispose() => proxy.Dispose();

    public IServiceProvider ServiceProvider { get; }
}