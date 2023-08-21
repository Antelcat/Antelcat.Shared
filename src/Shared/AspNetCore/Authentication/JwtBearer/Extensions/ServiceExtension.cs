using System.IdentityModel.Tokens.Jwt;
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
    /// <param name="received">预处理</param>
    /// <param name="validation">Jwt验证通过二级校验</param>
    /// <param name="denied">权限禁止返回报文处理</param>
    /// <param name="failed">校验失败的返回报文处理</param>
    /// <typeparam name="TIdentity">验证关联的身份模型</typeparam>
    public static IServiceCollection ConfigureJwt<TIdentity>(
        this IServiceCollection services,
        string scheme = JwtBearerDefaults.AuthenticationScheme,
        Action<JwtConfigure<TIdentity>>? configure = null,
        Func<MessageReceivedContext,Task>? received = null,
        Func<TIdentity, TokenValidatedContext, Task>? validation = null,
        Func<ForbiddenContext, string>? denied = null,
        Func<JwtBearerChallengeContext, string>? failed = null)
        where TIdentity : class
    {
        var config = new JwtConfigure<TIdentity>();
        configure?.Invoke(config);
        services
            .AddSingleton(config)
            .AddAuthentication()
            .AddJwtBearer(scheme,o =>
            {
                o.IncludeErrorDetails = true;
                o.TokenValidationParameters = config.Parameters;
                o.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context => received?.Invoke(context) ?? Task.CompletedTask,
                    OnTokenValidated = validation == null
                        ? static _ => Task.CompletedTask
                        : async context =>
                        {
                            var token = (context.SecurityToken as JwtSecurityToken)!.RawData;
                            var identity = typeof(TIdentity).RawInstance<TIdentity>().FromToken(token);
                            if (identity == null)
                            {
                                context.Fail(
                                    new NullReferenceException($"Cannot resolve {typeof(TIdentity)} from token"));
                            }
                            else
                            {
                                await validation.Invoke(identity, context);
                            }
                        },
                    OnForbidden = async context =>
                    {
                        if (denied == null) return;
                        context.Response.Clear();
                        context.Response.Headers.Clear();
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync(denied(context));
                    },
             
                    OnChallenge = async context =>
                    {
                        if (failed == null) return;
                        context.HandleResponse();
                        context.Response.Clear();
                        context.Response.Headers.Clear();
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync(failed(context));
                    }
                };
            });
        return services;
    }
}