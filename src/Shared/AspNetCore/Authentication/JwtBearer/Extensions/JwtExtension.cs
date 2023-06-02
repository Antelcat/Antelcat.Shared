using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Antelcat.Extensions;

public static class JwtExtension<TIdentity> where TIdentity : class
{
    static JwtExtension()
    {
        var identityType = typeof(TIdentity);
            
        var props = identityType.GetProperties();
        ReadableProps = props
            .Where(static x => x.CanRead)
            .ToDictionary(
                static p => p.Name,
                static p => new Tuple<Getter<TIdentity, object>, TypeConverter>(
                    p.CreateGetter<TIdentity, object>(),
                    typeof(string).GetConverter(p.PropertyType)));

        WritableProps = props.Where(static x => x.CanWrite)
            .ToDictionary(
                static p => p.Name,
                static p => new Tuple<Setter<TIdentity, object>, TypeConverter>(
                    p.CreateSetter<TIdentity, object>(),
                    typeof(string).GetConverter(p.PropertyType)));
    }

    private static readonly IDictionary<string, Tuple<Getter<TIdentity, object>, TypeConverter>> ReadableProps;
    private static readonly IDictionary<string, Tuple<Setter<TIdentity, object>, TypeConverter>> WritableProps;
    private static TIdentity SetFromClaim(TIdentity identity, Claim claim)
    {
        if (!WritableProps.TryGetValue(claim.Type, out var tuple)) return identity;
        tuple.Item1.Invoke(ref identity!, tuple.Item2.ConvertTo(claim.Value));
        return identity;
    }
    public static TIdentity? FromToken(TIdentity identity, string token)
    {
        try
        {
            return new JwtSecurityToken(token)
                .Claims
                .Aggregate(identity, SetFromClaim);
        }
        catch
        {
            return default;
        }
    }
    public static IEnumerable<Claim> GetClaims(TIdentity identity) =>
        ReadableProps
            .Select(x =>
                new Claim(x.Key, 
                    (string?)x.Value.Item2.ConvertFrom(x.Value.Item1.Invoke(identity)!) ?? string.Empty));

    public static TIdentity FromClaims(TIdentity identity, IEnumerable<Claim> claims) =>
        claims.Aggregate(identity, SetFromClaim);
}

public static class JwtExtension
{
    public static TIdentity FromClaims<TIdentity>(this TIdentity identity, IEnumerable<Claim> claims)
        where TIdentity : class =>
        JwtExtension<TIdentity>.FromClaims(identity, claims);

    public static IEnumerable<Claim> GetClaims<TIdentity>(this TIdentity identity)
        where TIdentity : class =>
        JwtExtension<TIdentity>.GetClaims(identity);

    public static TIdentity? FromToken<TIdentity>(this TIdentity identity, string token)
        where TIdentity : class => 
        JwtExtension<TIdentity>.FromToken(identity, token);
  
}