// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using NUnit.Framework;
using System.Diagnostics;
using WeightCore.Helpers;

namespace WeightCoreTests.Helpers
{
    internal class CollectionsHelperTests
    {
        private CollectionsHelper Collections { get; set; } = CollectionsHelper.Instance;

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
        public void GetDriverFileName_AreEqual()
        {
            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
            TestContext.WriteLine($@"{nameof(GetDriverFileName_AreEqual)} start.");
            Stopwatch stopwatch = Stopwatch.StartNew();

            string actual = Collections.GetDriverFileName(ShareEnums.WinVersion.Win10x64);
            Assert.AreEqual("VCP_V1.5.0_Setup_W8_x64_64bits.exe", actual);
            TestContext.WriteLine();

            actual = Collections.GetDriverFileName(ShareEnums.WinVersion.Win10x32);
            Assert.AreEqual("VCP_V1.5.0_Setup_W8_x86_32bits.exe", actual);

            actual = Collections.GetDriverFileName(ShareEnums.WinVersion.Win7x64);
            Assert.AreEqual("VCP_V1.5.0_Setup_W7_x64_64bits.exe", actual);

            actual = Collections.GetDriverFileName(ShareEnums.WinVersion.Win7x32);
            Assert.AreEqual("VCP_V1.5.0_Setup_W7_x86_32bits.exe", actual);

            TestContext.WriteLine($@"{nameof(GetDriverFileName_AreEqual)} complete. Elapsed time: {stopwatch.Elapsed}");
            stopwatch.Stop();
        }
    }
}
