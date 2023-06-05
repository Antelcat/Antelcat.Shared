#if !NET && !NETSTANDARD
using System;
#endif
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Antelcat.Extensions.DebugExtension
{
    public static class DebugExtension
    {
            public const string Runtime =
#if NET
                    "NET";
#elif NETSTANDARD
                    "NET Standard";
#else
                    "NET Framework";
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Debug<T>(this T instance)
        {
#if DEBUG
            Debugger.Break();
#endif
            return instance;
        }

        public static void PrintRuntime<T>(this T _) => Console.WriteLine($"This is [{Runtime}]");
    }
}