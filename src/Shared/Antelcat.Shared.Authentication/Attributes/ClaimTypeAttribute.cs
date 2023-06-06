#if !NET && !NETSTANDARD
using System;
#endif
namespace Antelcat.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class ClaimTypeAttribute : Attribute
{
    public readonly string? Type;

    public ClaimTypeAttribute(string? type = null)
    {
        Type = type;
    }
}