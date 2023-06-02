using System.IdentityModel.Tokens.Jwt;
using Antelcat.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Antelcat.Extensions;

public static partial class ServiceExtension
{
    public static void ConfigureJwt<TIdentity>(
        this IServiceCollection services,
        Action<JwtConfigure<TIdentity>>? configure = null,
        Func<TIdentity, TokenValidatedContext, Task>? validation = null,
        Func<JwtBearerChallengeContext, string>? failed = null)
        where TIdentity : class, new()
    {
        var config = new JwtConfigure<TIdentity>();
        configure?.Invoke(config);
        services
            .AddSingleton(config)
            .AddAuthentication(static options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.IncludeErrorDetails = true;
                o.TokenValidationParameters = config.Parameters;
                o.Events = new JwtBearerEvents
                {
                    OnTokenValidated = validation == null
                        ? static _ => Task.CompletedTask
                        : async context =>
                        {
                            var token = (context.SecurityToken as JwtSecurityToken)!.RawData;
                            var identity = new TIdentity().FromToken(token);
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
    }

}