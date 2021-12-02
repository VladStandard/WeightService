// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NUnit.Framework;
using ScalesCore.Helpers;
using System.Diagnostics;

namespace ScalesCoreTests.Helpers
{
    internal class HardwareHelperTests
    {
        private HardwareHelper Hard { get; set; } = HardwareHelper.Instance;

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
        public void SearchingDriver_AreEqual()
        {
            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
            TestContext.WriteLine($@"{nameof(SearchingDriver_AreEqual)} start.");
            Stopwatch sw = Stopwatch.StartNew();

            Assert.AreEqual(true, true);

            sw.Stop();
            TestContext.WriteLine($@"{nameof(SearchingDriver_AreEqual)} complete. Elapsed time: {sw.Elapsed}");
        }
    }
}
