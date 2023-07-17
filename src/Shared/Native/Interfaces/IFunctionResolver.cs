using System.Diagnostics.CodeAnalysis;
using System;

namespace Antelcat.Shared.NET.Interfaces;

public interface IFunctionResolver
{
    bool TryGetFunctionDelegate<T>(string libraryPath, string functionName, 
#if NET || NETSTANDARD
        [NotNullWhen(true)] 
#endif
        out T? handler)
        where T : Delegate;

    T GetFunctionDelegate<T>(string libraryPath, string functionName, bool throwOnError = true) where T : Delegate;
}