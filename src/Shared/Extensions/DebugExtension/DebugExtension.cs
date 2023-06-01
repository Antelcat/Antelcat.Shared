#if !NET && !NETSTANDARD
using System;
#endif
using System.Diagnostics;

namespace Antelcat.Shared.Extensions.DebugExtension
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