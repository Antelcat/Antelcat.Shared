#if !NET && !NETSTANDARD
using System;
using System.Collections.Generic;
#nullable enable
#endif
using System.ComponentModel;
using Antelcat.Implements.Converters;

namespace Antelcat.Extensions;

public static partial class TypeExtension
{
    public static TypeConverter GetConverter(this Type type, Type toType) =>
        type == toType
            ? NoneConverter.Instance
            : type == typeof(string)
                ? StringValueConverters.FindByType(toType, out var ret) ? ret : new StringConverter()
                : throw new NotSupportedException("Not support this type yet");

    public static object? ConvertTo(this TypeConverter converter, object value) =>
        converter.ConvertTo(null, null, value, null!);

    public static object? ConvertFrom(this TypeConverter converter, object value) =>
        converter.ConvertFrom(null!, null!, value);
}