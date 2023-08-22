using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mime;
using Antelcat.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Antelcat.Extensions;

public static partial class ServiceExtension
{
    public static IServiceCollection ConfigureCookie<TIdentity>(
        this IServiceCollection services,
        string scheme = CookieAuthenticationDefaults.AuthenticationScheme,
        Action<CookieBuilder>? configure = null,
        Func<TIdentity,  CookieValidatePrincipalContext, Task>? validation = null,
        Func<RedirectContext<CookieAuthenticationOptions>, string>? denied = null,
        Func<RedirectContext<CookieAuthenticationOptions>, string>? failed = null)
        where TIdentity : class
    {
        services
            .AddAuthentication()
            .AddCookie(scheme,o =>
            {
                o.Cookie.SameSite = SameSiteMode.None;
                o.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                configure?.Invoke(o.Cookie);
                o.Events = new CookieAuthenticationEvents
                {
                    OnValidatePrincipal =
                        validation == null
                            ? static _ => Task.CompletedTask
                            : async context =>
                            {
                                await validation.Invoke(typeof(TIdentity).RawInstance<TIdentity>()
                                    .FromClaims(context.HttpContext.User.Claims), context);
                            },
                    OnRedirectToAccessDenied = async context =>
                    {
                        if (denied == null) return;
                        context.Response.Clear();
                        context.Response.Headers.Clear();
                        context.Response.ContentType = MediaTypeNames.Application.Json;
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync(denied(context));
                    },
                    OnRedirectToLogin = async context =>
                    {
                        if (failed == null) return;
                        context.Response.Clear();
                        context.Response.Headers.Clear();
                        context.Response.ContentType = MediaTypeNames.Application.Json;
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync(failed(context));
                    }
                };
            });
        return services;
    }
}