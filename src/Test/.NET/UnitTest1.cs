using System.ComponentModel;
using Antelcat.Extensions;

namespace Antelcat.Shared.Test;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var s = "a".ToUpperCamelCase();

        Assert.Pass();
    }
}