// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore;
using DataShareCore;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.IO;

namespace WeightCoreTests.Helpers
{
    internal class SettingsHelperTests
    {
        private SettingsHelper Settings { get; set; } = SettingsHelper.Instance;

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
        public void SetupDirs_AreEqual()
        {
            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
            TestContext.WriteLine($@"{nameof(SetupDirs_AreEqual)} start.");
            Stopwatch sw = Stopwatch.StartNew();

            bool actual = Settings.SetupAndCheckDirs(@"c:\Program Files (x86)\VladimirStandardCorp\ScalesUI", ProjectsEnums.SilentUI.True,
                ShareEnums.Lang.Russian);
            Assert.AreEqual(
                Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\VladimirStandardCorp\ScalesUI\"), actual);

            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\VladimirStandardCorp\ScalesUI\"))
            {
                TestContext.WriteLine("DirMain: " + Settings.DirMain);
                Assert.AreEqual(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\VladimirStandardCorp\ScalesUI\", Settings.DirMain);

                TestContext.WriteLine("DirDocs: " + Settings.DirDocs);
                Assert.AreEqual(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\VladimirStandardCorp\ScalesUI\Docs", Settings.DirDocs);

                TestContext.WriteLine("DirDrivers: " + Settings.DirDrivers);
                Assert.AreEqual(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\VladimirStandardCorp\ScalesUI\Drivers", Settings.DirDrivers);

                TestContext.WriteLine("DirFonts: " + Settings.DirFonts);
                Assert.AreEqual(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\VladimirStandardCorp\ScalesUI\frx", Settings.DirFonts);

                TestContext.WriteLine("DirManuals: " + Settings.DirManuals);
                Assert.AreEqual(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\VladimirStandardCorp\ScalesUI\Manuals", Settings.DirManuals);
            }

            sw.Stop();
            TestContext.WriteLine($@"{nameof(SetupDirs_AreEqual)} complete. Elapsed time: {sw.Elapsed}");
        }
    }
}
