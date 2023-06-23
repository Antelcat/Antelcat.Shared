using System;
using System.Threading;
using System.Threading.Tasks;
using Antelcat.Extensions;
using NUnit.Framework;

namespace Antelcat.Shared.Test.NET_Framework
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            ((Action)(() => { })).Run();
            new Action(() => { }).Run();
        }

        [Test]
        public void Test1()
        {
        }
    }
}