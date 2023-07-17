#if !NET && !NETSTANDARD
using System;
using System.Collections.Generic;
#else
using System.Diagnostics.CodeAnalysis;
#endif
using System.Reflection;
using System.Runtime.InteropServices;
using Antelcat.Shared.NET.Interfaces;

namespace Antelcat.Implements;
#nullable enable
public abstract class FunctionResolverBase : IFunctionResolver
{
    private readonly Dictionary<string, IntPtr> loadedLibraries = new();
    private readonly object locker = new();

    private static readonly Func<IntPtr, Type, object> GetDelegateForFunctionPointerInternal =
        (Func<IntPtr, Type, object>)typeof(Marshal)
            .GetMethod(nameof(GetDelegateForFunctionPointerInternal),
                BindingFlags.Static | BindingFlags.NonPublic)!
            .CreateDelegate(typeof(Func<IntPtr, Type, object>));
    
    public bool TryGetFunctionDelegate<T>(string libraryPath, string functionName,
#if NET || NETSTANDARD
        [NotNullWhen(true)] 
#endif
        out T? handler)
        where T : Delegate
    {
        handler = null;
        var handle = GetOrLoadLibrary(libraryPath, false);
        if (handle == IntPtr.Zero) return false;
        var address = FindFunctionPointer(handle, functionName);
        if (address == IntPtr.Zero) return false;
        handler = (T)GetDelegateForFunctionPointerInternal(address,typeof(T));
        return true;
    }

    public T? GetFunctionDelegate<T>(string libraryPath, string functionName, bool throwOnError = true) 
        where T : Delegate =>
        GetFunctionDelegate<T>(GetOrLoadLibrary(libraryPath, throwOnError), functionName, throwOnError);

    private T? GetFunctionDelegate<T>(IntPtr nativeLibraryHandle, string functionName, bool throwOnError)
        where T : Delegate
    {
        var functionPointer = FindFunctionPointer(nativeLibraryHandle, functionName);
        if (functionPointer != IntPtr.Zero) return (T)GetDelegateForFunctionPointerInternal(functionPointer, typeof(T));
        if (throwOnError) throw new EntryPointNotFoundException($"Could not find the entrypoint for {functionName}.");
        return null;
    }

    private IntPtr GetOrLoadLibrary(string libraryPath, bool throwOnError)
    {
        // ReSharper disable once InconsistentlySynchronizedField
        if (loadedLibraries.TryGetValue(libraryPath, out var ptr)) return ptr;
        lock (locker)
        {
            if (loadedLibraries.TryGetValue(libraryPath, out ptr)) return ptr;
            ptr = LoadNativeLibrary(libraryPath);
            if (ptr != IntPtr.Zero) loadedLibraries.Add(libraryPath, ptr);
            else if (throwOnError) throw new DllNotFoundException($"Could not found dll locates at : {libraryPath}");
            return ptr;
        }
    }

    protected abstract IntPtr LoadNativeLibrary(string libraryName);
    protected abstract IntPtr FindFunctionPointer(IntPtr nativeLibraryHandle, string functionName);
}
