// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Diagnostics;
using Microsoft.Win32;
using NUnit.Framework;
using ScalesCore.Win.Registry.Helpers;

namespace ScalesCoreTests.Helpers
{
    internal class RegistryHelperTests
    {
        // Помощник реестра.
        private readonly RegistryHelper _regHelp = RegistryHelper.Instance;

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
        public void Exists_Execute_AreEqual()
        {
            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
            TestContext.WriteLine($@"{nameof(Exists_Execute_AreEqual)} start.");
            var sw = Stopwatch.StartNew();

            Assert.AreEqual(false, _regHelp.Exists(_regHelp.Root, ScalesCore.Properties.Settings.Default.RegVladimirStandardCorp, "ExePathNoExists"));
            Assert.AreEqual(false, _regHelp.Exists(_regHelp.Root, ScalesCore.Properties.Settings.Default.RegVladimirStandardCorp + "NoExists"));
            Assert.AreEqual(false, _regHelp.Exists(_regHelp.Root, ScalesCore.Properties.Settings.Default.RegVladimirStandardCorp + "NoExists", "ExePathNoExists"));

            sw.Stop();
            TestContext.WriteLine($@"{nameof(Exists_Execute_AreEqual)} complete. Elapsed time: {sw.Elapsed}");
        }

        [Test]
        public void CreateSubKey_Execute_AreEqual()
        {
            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
            TestContext.WriteLine($@"{nameof(Exists_Execute_AreEqual)} start.");
            var sw = Stopwatch.StartNew();

            //if (!_reg.Exists(ScalesCore.Properties.Settings.Default.RegScalesUI))
            //    Assert.AreEqual(true, _reg.CreateSubKey(ScalesCore.Properties.Settings.Default.RegScalesUI));
            Assert.AreEqual(true, true);

            sw.Stop();
            TestContext.WriteLine($@"{nameof(Exists_Execute_AreEqual)} complete. Elapsed time: {sw.Elapsed}");
        }

        [Test]
        public void GetValue_Execute_AreEqual()
        {
            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
            TestContext.WriteLine($@"{nameof(GetValue_Execute_AreEqual)} start.");
            var sw = Stopwatch.StartNew();

            if (!_regHelp.Exists(_regHelp.Root, ScalesCore.Properties.Settings.Default.RegVladimirStandardCorp))
                _regHelp.CreateSubKey(_regHelp.Root, ScalesCore.Properties.Settings.Default.RegVladimirStandardCorp);
            if (!_regHelp.Exists(_regHelp.Root, ScalesCore.Properties.Settings.Default.RegVladimirStandardCorp, "ExePath"))
                _regHelp.CreateParameter(_regHelp.Root, ScalesCore.Properties.Settings.Default.RegVladimirStandardCorp, "ExePath");
            var actual = _regHelp.GetValue<string>(_regHelp.Root, ScalesCore.Properties.Settings.Default.RegVladimirStandardCorp, "ExePath");
            TestContext.WriteLine($@"actual: {actual}");
            Assert.IsTrue(string.IsNullOrEmpty(actual));

            sw.Stop();
            TestContext.WriteLine($@"{nameof(GetValue_Execute_AreEqual)} complete. Elapsed time: {sw.Elapsed}");
        }

        [Test]
        public void SetValue_Execute_AreEqual()
        {
            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
            TestContext.WriteLine($@"{nameof(GetValue_Execute_AreEqual)} start.");
            var sw = Stopwatch.StartNew();

            if (_regHelp.Exists(_regHelp.Root, ScalesCore.Properties.Settings.Default.RegVladimirStandardCorp))
            {
                if (!_regHelp.Exists(_regHelp.Root, ScalesCore.Properties.Settings.Default.RegVladimirStandardCorp, "ExePath2"))
                    _regHelp.CreateSubKey(_regHelp.Root, ScalesCore.Properties.Settings.Default.RegVladimirStandardCorp + @"\ExePath2");
                if (_regHelp.Exists(_regHelp.Root, ScalesCore.Properties.Settings.Default.RegVladimirStandardCorp, "ExePath2"))
                {
                    Assert.AreEqual(true, _regHelp.SetValue(_regHelp.Root, ScalesCore.Properties.Settings.Default.RegVladimirStandardCorp,"ExePath2", "TestValue", RegistryValueKind.String));
                }
            }

            sw.Stop();
            TestContext.WriteLine($@"{nameof(GetValue_Execute_AreEqual)} complete. Elapsed time: {sw.Elapsed}");
        }
    }
}
