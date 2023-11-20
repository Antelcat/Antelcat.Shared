using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Antelcat.Extensions;

public static class JwtExtension
{
    public static TIdentity FromToken<TIdentity>(this TIdentity identity, string token)
        where TIdentity : class => new JwtSecurityToken(token)
        .Claims
        .Aggregate(identity, ClaimExtension<TIdentity>.SetFromClaim);

    internal static TIdentity FromClaims<TIdentity>(this TIdentity identity, IEnumerable<Claim> claims)
        where TIdentity : class =>
        ClaimExtension<TIdentity>.FromClaims(identity, claims);

    internal static IEnumerable<Claim> GetClaims<TIdentity>(this TIdentity identity)
        where TIdentity : class =>
        ClaimExtension<TIdentity>.GetClaims(identity);

}