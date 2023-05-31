using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Antelcat.Extensions;

public static class LinqExtension
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (var item in source) action(item);
    }
}