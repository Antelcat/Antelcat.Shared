#if !NET && !NETSTANDARD
using System;
using System.Collections.Generic;
using System.Linq;
#endif
using System.ComponentModel;
using System.Reflection;
using System.Security.Claims;
using Antelcat.Attributes;

namespace Antelcat.Extensions;

public static class ClaimExtension<TIdentity>
{
    private static readonly Dictionary<string,
        Tuple<Getter<TIdentity, object>,
            Setter<TIdentity, object>,
            TypeConverter>> Props;

    static ClaimExtension()
    {
        Props = typeof(TIdentity)
            .GetProperties()
            .Where(static x => x is { CanRead: true, CanWrite: true })
            .ToDictionary(
                static p => p.GetCustomAttribute<ClaimTypeAttribute>()?.Type ?? p.Name,
                static p => new Tuple<
                    Getter<TIdentity, object>,
                    Setter<TIdentity, object>,
                    TypeConverter>(
                    p.CreateGetter<TIdentity, object>(),
                    p.CreateSetter<TIdentity, object>(),
                    typeof(string).GetConverter(p.PropertyType)));
    }
    public static TIdentity SetFromClaim(TIdentity identity, Claim claim)
    {
        if (!Props.TryGetValue(claim.Type, out var tuple)) return identity;
        tuple.Item2.Invoke(ref identity!, tuple.Item3.ConvertTo(claim.Value));
        return identity;
    }
 
    
    public static IEnumerable<Claim> GetClaims(TIdentity identity) =>
        Props.Select(x => new Claim(x.Key,
            (string?)x.Value.Item3.ConvertFrom(
                x.Value.Item1.Invoke(identity)!) ?? string.Empty));
    
    public static TIdentity FromClaims(TIdentity identity, IEnumerable<Claim> claims) =>
        claims.Aggregate(identity, SetFromClaim);
}