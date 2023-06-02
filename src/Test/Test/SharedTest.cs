using System.ComponentModel;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Antelcat.Extensions;
using Antelcat.Implements.Converters;
using Antelcat.Shared.Extensions.DebugExtension;
using NUnit.Framework;

namespace Antelcat.Shared.Test
{
    class SharedTest
    {
        private TypeConverter Converter;
        [SetUp]
        public void Setup()
        {
            Converter = new StringToFloatConverter();
        }

        private const int Times = 1000;

        [Test]
        public void TestInline()
        {
            var times = Times;
            var watch = new Stopwatch();
            watch.Start();
            while (times -- > 0)
            {
                "123".Inline(1, "", 3f);
            }
            watch.Stop();
            Console.WriteLine($"Inline   Cost : {watch.ElapsedTicks}");
            
            times = Times;
            watch = new Stopwatch();
            watch.Start();
            while (times -- > 0)
            {
                "123".NoInline(1, "", 3f);
            }
            watch.Stop();
            Console.WriteLine($"Noinline Cost : {watch.ElapsedTicks}");
            
            Console.WriteLine();
        }

        [Test]
        public async Task MultipleTest()
        {
            var i = 0;
            while (i++ < 3)
            {
                await 1000;
                Console.WriteLine($"预热{i}s");
            }

            var times = 5;
            while (times-- > 0)
            {
                TestInline();
            }
        }
    }

    public static class Extension
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void NoInline<T>(this T instance, int i1, string i2, float i3)
        {
            i3 = i2.Length + i1;
            new StackTrace().GetFrames();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Inline<T>(this T instance, int i1, string i2, float i3)
        {
            i3 = i2.Length + i1;
            new StackTrace().GetFrames();
        }
    }
}