// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NUnit.Framework;
using ScalesCore.Helpers;
using ScalesCore.Models;
using System;
using System.Diagnostics;

namespace ScalesCoreTests.Helpers
{
    internal class WinHelperTests
    {
        // Помощник Windows.
        private readonly WinHelper _winHelp = WinHelper.Instance;

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
        public void SearchingSoftware_AreEqual_Empty()
        {
            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
            TestContext.WriteLine($@"{nameof(SearchingSoftware_AreEqual_Empty)} start.");
            var sw = Stopwatch.StartNew();

            foreach (EnumWinProvider provider in Enum.GetValues(typeof(EnumWinProvider)))
            {
                foreach (EnumStringTemplate template in Enum.GetValues(typeof(EnumStringTemplate)))
                {
                    var actual = _winHelp.SearchingSoftware(EnumWinProvider.Alias, "Unknown Software", template);
                    var expected = new ResultWmiSoftware();
                    TestContext.WriteLine($@"actual = {actual}");
                    Assert.AreEqual(expected.ToString(), actual.ToString());
                }
            }

            sw.Stop();
            TestContext.WriteLine($@"{nameof(SearchingSoftware_AreEqual_Empty)} complete. Elapsed time: {sw.Elapsed}");
        }

        [Test]
        public void SearchingSoftware_AreEqual_FromRegistry()
        {
            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
            TestContext.WriteLine($@"{nameof(SearchingSoftware_AreEqual_FromRegistry)} start.");
            var sw = Stopwatch.StartNew();

            var actual = _winHelp.SearchingSoftware(EnumWinProvider.Registry, "Microsoft .NET Framework", EnumStringTemplate.StartsWith);
            TestContext.WriteLine($"actual: {actual}");
            TestContext.WriteLine($"actual.Name: {actual.Name}");
            Assert.AreEqual("Microsoft Corporation", actual.Vendor);

            sw.Stop();
            TestContext.WriteLine($@"{nameof(SearchingSoftware_AreEqual_FromRegistry)} complete. Elapsed time: {sw.Elapsed}");
        }
    }
}
