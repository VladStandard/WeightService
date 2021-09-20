// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace ScalesCoreTests.Helpers
{
    internal class SettingsHelperTests
    {
        // Помощник настроек.
        private readonly SettingsHelper _settingsHelp = SettingsHelper.Instance;

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
            var sw = Stopwatch.StartNew();

            var actual = _settingsHelp.SetupAndCheckDirs(@"c:\Program Files (x86)\VladimirStandardCorp\ScalesUI", ProjectsEnums.SilentUI.True, 
                ShareEnums.Lang.Russian);
            Assert.AreEqual(
                Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\VladimirStandardCorp\ScalesUI\"), actual);

            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\VladimirStandardCorp\ScalesUI\"))
            {
                TestContext.WriteLine("DirMain: " + _settingsHelp.DirMain);
                Assert.AreEqual(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\VladimirStandardCorp\ScalesUI\", _settingsHelp.DirMain);

                TestContext.WriteLine("DirDocs: " + _settingsHelp.DirDocs);
                Assert.AreEqual(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\VladimirStandardCorp\ScalesUI\Docs", _settingsHelp.DirDocs);

                TestContext.WriteLine("DirDrivers: " + _settingsHelp.DirDrivers);
                Assert.AreEqual(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\VladimirStandardCorp\ScalesUI\Drivers", _settingsHelp.DirDrivers);

                TestContext.WriteLine("DirFonts: " + _settingsHelp.DirFonts);
                Assert.AreEqual(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\VladimirStandardCorp\ScalesUI\frx", _settingsHelp.DirFonts);

                TestContext.WriteLine("DirManuals: " + _settingsHelp.DirManuals);
                Assert.AreEqual(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\VladimirStandardCorp\ScalesUI\Manuals", _settingsHelp.DirManuals);
            }

            sw.Stop();
            TestContext.WriteLine($@"{nameof(SetupDirs_AreEqual)} complete. Elapsed time: {sw.Elapsed}");
        }
    }
}
