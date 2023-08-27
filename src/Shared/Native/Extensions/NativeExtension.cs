using System.Runtime.InteropServices;
using Antelcat.Implements;
using Antelcat.Interfaces;
#if !NET && !NETSTANDARD
using System;
using System.IO;
#else
using System.Diagnostics.CodeAnalysis;
#endif

#nullable enable
namespace Antelcat.Extensions;

public static class NativeExtension
{
    private static readonly IFunctionResolver Resolver;

    static NativeExtension()
    {
        Resolver = Environment.OSVersion.Platform switch
        {
            PlatformID.Unix => new LinuxFunctionResolver(),
            PlatformID.MacOSX => new MacFunctionResolver(),
            _ => new WindowsFunctionResolver(),
        };
    }

    public static T GetFunctionDelegate<T>(string libraryPath, string functionName)
        where T : Delegate =>
        Resolver.GetFunctionDelegate<T>(libraryPath, functionName);

    public static T GetFunctionDelegate<T>(this FileInfo fileInfo, string functionName)
        where T : Delegate =>
        GetFunctionDelegate<T>(fileInfo.FullName, functionName);

    public static bool TryGetFunctionDelegate<T>(string libraryPath, string functionName,
#if NET || NETSTANDARD
        [NotNullWhen(true)]
#endif
        out T? handler)
        where T : Delegate =>
        Resolver.TryGetFunctionDelegate(libraryPath, functionName, out handler);

    public static bool TryGetFunctionDelegate<T>(this FileInfo fileInfo, string functionName,
#if NET || NETSTANDARD
        [NotNullWhen(true)]
#endif
        out T? handler)
        where T : Delegate =>
        TryGetFunctionDelegate(fileInfo.FullName, functionName, out handler);


    public static IntPtr ToPointer(this Delegate @delegate) => Marshal.GetFunctionPointerForDelegate(@delegate);

    public static TDelegate ToDelegate<TDelegate>(this IntPtr pointer) =>
        Marshal.GetDelegateForFunctionPointer<TDelegate>(pointer);
}