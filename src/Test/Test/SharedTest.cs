using System.ComponentModel;
using System;
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

        [Test]
        public void Test1()
        {
            var str = Converter.ConvertTo("1.11");
            this.PrintRuntime();
            Console.WriteLine(str);
            Assert.That(str,Is.TypeOf<float>());
        }
    }

   
}