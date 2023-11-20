using Antelcat.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Antelcat.Extensions;

public static partial class ServiceExtension
{
    /// <summary>
    /// 配置Jwt行为，可解析出 <see cref="JwtConfigure{TIdentity}"/>
    /// </summary>
    /// <param name="services">服务容器</param>
    /// <param name="scheme">校验的scheme</param>
    /// <param name="configure">Jwt基础配置</param>
    public static IServiceCollection ConfigureJwt(
        this IServiceCollection services,
        string scheme = JwtBearerDefaults.AuthenticationScheme,
        Action<JwtConfigure>? configure = null)
    {
        if (services.FirstOrDefault(x =>
                    x.ImplementationInstance is JwtConfigureFactory)?
                .ImplementationInstance is not JwtConfigureFactory factory)
        {
            factory = new JwtConfigureFactory();
            services.AddSingleton(factory).AddSingleton(typeof(JwtConfigure<>));
        }
        else
        {
            if (factory.Configs.ContainsKey(scheme)) return services;
        }
        
        var config = new JwtConfigure();
        configure?.Invoke(config);
        factory.Configs[scheme] = config;
        services.AddAuthentication()
            .AddJwtBearer(scheme, o =>
            {
                o.IncludeErrorDetails       = true;
                o.TokenValidationParameters = config.Parameters;
                o.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context => config.OnReceived(context),
                    OnTokenValidated  = validation => config.OnValidated(validation),
                    OnForbidden = async context =>
                    {
                        if (config.OnForbidden == null) return;
                        context.Response.Clear();
                        context.Response.Headers.Clear();
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode  = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync(config.OnForbidden(context));
                    },
                    OnChallenge = async context =>
                    {
                        if (config.OnChallenge == null) return;
                        context.HandleResponse();
                        context.Response.Clear();
                        context.Response.Headers.Clear();
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode  = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync(config.OnChallenge(context));
                    }
                };
            });
        return services;
      
    }
}