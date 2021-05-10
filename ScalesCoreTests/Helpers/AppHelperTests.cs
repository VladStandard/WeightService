// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NUnit.Framework;
using ScalesCore.Models;
using ScalesCore.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace ScalesCoreTests.Helpers
{
    internal class AppHelperTests
    {
        #region Private fields and properties

        // Помощник приложения.
        private readonly AppHelper _app = AppHelper.Instance;

        #endregion

        #region Setup & teardown

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

        #endregion

        #region Public methods

        [Test]
        public void GetCurrentVersionSubString_Execute_DoesNotThrow()
        {
            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
            TestContext.WriteLine($@"{nameof(GetCurrentVersionSubString_Execute_DoesNotThrow)} start.");
            var sw = Stopwatch.StartNew();

            var version = "0.1.5.123";
            var actual = string.Empty;
            Assert.DoesNotThrow(() => actual = _app.GetCurrentVersionSubString(version));
            Assert.AreEqual("0.1.5", actual);

            sw.Stop();
            TestContext.WriteLine($@"{nameof(GetCurrentVersionSubString_Execute_DoesNotThrow)} complete. Elapsed time: {sw.Elapsed}");
        }

        [Test]
        public void GetCurrentVersion_Execute_Default()
        {
            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
            TestContext.WriteLine($@"{nameof(GetCurrentVersion_Execute_Default)} start.");
            var sw = Stopwatch.StartNew();

            var result = string.Empty;
            foreach (EnumStringFormat strFormat in Enum.GetValues(typeof(EnumStringFormat)))
            {
                foreach (EnumVerCountDigits countDigits in Enum.GetValues(typeof(EnumVerCountDigits)))
                {
                    Assert.DoesNotThrow(() => result = _app.GetCurrentVersion(countDigits, null));
                    TestContext.WriteLine($@"_app.GetCurrentVersion({countDigits}, null) = {result}");
                    Assert.DoesNotThrow(() => result = _app.GetCurrentVersion(countDigits, new List<EnumStringFormat>()));
                    TestContext.WriteLine($@"_app.GetCurrentVersion({countDigits}, new List<EnumStringFormat>()) = {result}");
                }
            }

            sw.Stop();
            TestContext.WriteLine($@"{nameof(GetCurrentVersion_Execute_Default)} complete. Elapsed time: {sw.Elapsed}");
        }

        [Test]
        public void GetCurrentVersion_Execute_DoesNotThrow()
        {
            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
            TestContext.WriteLine($@"{nameof(GetCurrentVersion_Execute_DoesNotThrow)} start.");
            var sw = Stopwatch.StartNew();

            var result = string.Empty;

            var strFormats = new List<EnumStringFormat>() { EnumStringFormat.Use2, EnumStringFormat.Use2, EnumStringFormat.Use3 };
            foreach (EnumVerCountDigits countDigits in Enum.GetValues(typeof(EnumVerCountDigits)))
            {
                Assert.DoesNotThrow(() => result = _app.GetCurrentVersion(countDigits, strFormats));
                TestContext.WriteLine($@"_app.GetCurrentVersion({countDigits}, {strFormats}) = {result}");
            }

            sw.Stop();
            TestContext.WriteLine($@"{nameof(GetCurrentVersion_Execute_DoesNotThrow)} complete. Elapsed time: {sw.Elapsed}");
        }

        [Test]
        public void GetCurrentVersion_Execute_AreEqual()
        {
            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
            TestContext.WriteLine($@"{nameof(GetCurrentVersion_Execute_AreEqual)} start.");
            var sw = Stopwatch.StartNew();

            var result = string.Empty;
            var version = new Version(0, 1, 5, 123);

            var strFormats = new List<EnumStringFormat>() { EnumStringFormat.Use2, EnumStringFormat.Use2, EnumStringFormat.Use2 };
            foreach (EnumVerCountDigits countDigits in Enum.GetValues(typeof(EnumVerCountDigits)))
            {
                Assert.DoesNotThrow(() => result = _app.GetCurrentVersion(countDigits, strFormats, version));
                TestContext.WriteLine($@"_app.GetCurrentVersion({countDigits}, {strFormats}) = {result}");
                if (countDigits == EnumVerCountDigits.Use1)
                    Assert.AreEqual("00", result);
                if (countDigits == EnumVerCountDigits.Use2)
                    Assert.AreEqual("00.01", result);
                if (countDigits == EnumVerCountDigits.Use3)
                    Assert.AreEqual("00.01.05", result);
                if (countDigits == EnumVerCountDigits.Use4)
                    Assert.AreEqual("00.01.05.123", result);
            }

            strFormats = new List<EnumStringFormat>() { EnumStringFormat.Use1, EnumStringFormat.Use1, EnumStringFormat.Use1 };
            foreach (EnumVerCountDigits countDigits in Enum.GetValues(typeof(EnumVerCountDigits)))
            {
                Assert.DoesNotThrow(() => result = _app.GetCurrentVersion(countDigits, strFormats, version));
                TestContext.WriteLine($@"_app.GetCurrentVersion({countDigits}, {strFormats}) = {result}");
                if (countDigits == EnumVerCountDigits.Use1)
                    Assert.AreEqual("0", result);
                if (countDigits == EnumVerCountDigits.Use2)
                    Assert.AreEqual("0.1", result);
                if (countDigits == EnumVerCountDigits.Use3)
                    Assert.AreEqual("0.1.5", result);
                if (countDigits == EnumVerCountDigits.Use4)
                    Assert.AreEqual("0.1.5.123", result);
            }

            sw.Stop();
            TestContext.WriteLine($@"{nameof(GetCurrentVersion_Execute_AreEqual)} complete. Elapsed time: {sw.Elapsed}");
        }

        [Test]
        public void SetNewSize_Execute_Does()
        {
            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
            TestContext.WriteLine($@"{nameof(GetCurrentVersion_Execute_AreEqual)} start.");
            var sw = Stopwatch.StartNew();

            Assert.DoesNotThrow(() => _app.SetNewSize(null));
            Assert.DoesNotThrow(() => _app.SetNewSize(null, FormStartPosition.CenterParent));
            Assert.DoesNotThrow(() => _app.SetNewSize(new Form()));
            Assert.DoesNotThrow(() => _app.SetNewSize(new Form(), FormStartPosition.CenterParent));

            sw.Stop();
            TestContext.WriteLine($@"{nameof(GetCurrentVersion_Execute_AreEqual)} complete. Elapsed time: {sw.Elapsed}");
        }

        #endregion
    }
}
