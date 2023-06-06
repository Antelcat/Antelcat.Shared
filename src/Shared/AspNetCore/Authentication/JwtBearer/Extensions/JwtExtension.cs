using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using Antelcat.Attributes;

namespace Antelcat.Extensions;

public static class JwtExtension
{
    public static TIdentity? FromToken<TIdentity>(this TIdentity identity, string token) where TIdentity : class
    {
        try
        {
            return new JwtSecurityToken(token)
                .Claims
                .Aggregate(identity, ClaimExtension<TIdentity>.SetFromClaim);
        }
        catch
        {
            return null;
        }
    }
    public static TIdentity FromClaims<TIdentity>(this TIdentity identity, IEnumerable<Claim> claims)
        where TIdentity : class =>
        ClaimExtension<TIdentity>.FromClaims(identity, claims);

    public static IEnumerable<Claim> GetClaims<TIdentity>(this TIdentity identity)
        where TIdentity : class =>
        ClaimExtension<TIdentity>.GetClaims(identity);

}