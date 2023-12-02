using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using Antelcat.Extensions;
using Antelcat.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Antelcat.Utils;


public class JwtConfigure
{
    internal JwtConfigure()
    {
        Secret = Guid.NewGuid().ToString();
    }

    public string Secret { set => SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(value)); }

    private SecurityKey? SecurityKey
    {
        get => securityKey;
        set
        {
            securityKey = value;
            credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        }
    }
    private SecurityKey? securityKey;
    private SigningCredentials? credentials;

    public Func<MessageReceivedContext, Task>       OnReceived  { get; set; } = _ => Task.CompletedTask;
    public Func<TokenValidatedContext, Task>        OnValidated { get; set; } = _ => Task.CompletedTask;
    public Func<ForbiddenContext, string>?          OnForbidden { get; set; }
    public Func<JwtBearerChallengeContext, string>? OnFailed { get; set; }
    
    
    internal JwtSecurityToken GetToken(IEnumerable<Claim?> claims) => new(
        Parameters.ValidIssuer,
        Parameters.ValidAudience,
        claims,
        signingCredentials: credentials,
        notBefore: DateTime.Now);

    public TokenValidationParameters Parameters =>
        parameters ??= new TokenValidationParameters
        {
            ValidIssuer = Assembly.GetExecutingAssembly().GetName().Name,
            ValidAudience = Assembly.GetExecutingAssembly().GetName().Name,
            ValidateIssuer = true,
            IssuerSigningKey = SecurityKey,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero
        };
    private TokenValidationParameters? parameters;

    internal readonly JwtSecurityTokenHandler Handler = new();
}

[Serializable]
public class JwtConfigure<TIdentity>(JwtConfigureFactory factory) where TIdentity : IClaimSerializable
{
    private (string scheme, JwtConfigure configure)? cache;

    public string CreateToken(TIdentity source, string scheme = JwtBearerDefaults.AuthenticationScheme)
    {
        if (cache?.scheme == scheme)
        {
            var conf = cache.Value.configure;
            return conf.Handler.WriteToken(conf.GetToken(source.GetClaims()));
        }

        if (!factory.Configs.TryGetValue(scheme, out var configure))
            throw new ArgumentOutOfRangeException($"Scheme {scheme} not configured");

        cache = (scheme, configure);
        return configure.Handler.WriteToken(configure.GetToken(source.GetClaims()));
    }
}