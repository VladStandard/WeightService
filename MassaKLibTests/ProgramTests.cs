using NUnit.Framework;
using System.Diagnostics;
	
namespace Namespace.Tests
{
    [TestFixture]
    public class ProgramTests
    {
        /// <summary>
        /// Setup private fields.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
            TestContext.WriteLine($@"{nameof(Setup)} start.");
            // 
            TestContext.WriteLine($@"{nameof(Setup)} complete.");
        }

        /// <summary>
        /// Reset private fields to default state.
        /// </summary>
        [TearDown]
        public void Teardown()
        {
            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
            TestContext.WriteLine($@"{nameof(Teardown)} start.");
            // 
            TestContext.WriteLine($@"{nameof(Teardown)} complete.");
            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
        }

        [Test]
        public void Foo_AreEqual()
        {
            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
            TestContext.WriteLine($@"{nameof(Foo_AreEqual)} start.");
            var sw = Stopwatch.StartNew();

            int actual = default;
            // 
            int expected = default;
            TestContext.WriteLine($"actual/expected: {actual}");
            Assert.AreEqual(expected, actual);

            sw.Stop();
            TestContext.WriteLine($@"{nameof(Foo_AreEqual)} complete. Elapsed time: {sw.Elapsed}");
        }
    }
}