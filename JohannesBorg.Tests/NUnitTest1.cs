using FluentAssertions;
using NUnit.Framework;

namespace JohannesBorg.Tests
{
    [TestFixture]
    public class NUnitTest1
    {
        [Test]
        public void TestMethod1()
        {
            new Class1().Should().NotBeNull();
        }
    }
}