// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using DataCore.Models;
using WeightCore.Helpers;

namespace WeightCoreTests.Helpers
{
    internal class AppHelperTests
    {
        #region Private fields and properties

        private AppHelper App { get; set; } = AppHelper.Instance;

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
            Stopwatch stopwatch = Stopwatch.StartNew();

            string version = "0.1.5.123";
            string actual = string.Empty;
            Assert.DoesNotThrow(() => actual = App.GetCurrentVersionSubString(version));
            Assert.AreEqual("0.1.5", actual);

            TestContext.WriteLine($@"{nameof(GetCurrentVersionSubString_Execute_DoesNotThrow)} complete. Elapsed time: {stopwatch.Elapsed}");
            stopwatch.Stop();
        }

        [Test]
        public void GetCurrentVersion_Execute_Default()
        {
            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
            TestContext.WriteLine($@"{nameof(GetCurrentVersion_Execute_Default)} start.");
            Stopwatch stopwatch = Stopwatch.StartNew();

            string result = string.Empty;
            foreach (AppVerStringFormatEnum strFormat in Enum.GetValues(typeof(AppVerStringFormatEnum)))
            {
                foreach (AppVerCountDigitsEnum countDigits in Enum.GetValues(typeof(AppVerCountDigitsEnum)))
                {
                    Assert.DoesNotThrow(() => result = App.GetCurrentVersion(countDigits, null));
                    TestContext.WriteLine($@"_app.GetCurrentVersion({countDigits}, null) = {result}");
                    Assert.DoesNotThrow(() => result = App.GetCurrentVersion(countDigits, new List<AppVerStringFormatEnum>()));
                    TestContext.WriteLine($@"_app.GetCurrentVersion({countDigits}, new List<AppVerStringFormat>()) = {result}");
                }
            }

            TestContext.WriteLine($@"{nameof(GetCurrentVersion_Execute_Default)} complete. Elapsed time: {stopwatch.Elapsed}");
            stopwatch.Stop();
        }

        [Test]
        public void GetCurrentVersion_Execute_DoesNotThrow()
        {
            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
            TestContext.WriteLine($@"{nameof(GetCurrentVersion_Execute_DoesNotThrow)} start.");
            Stopwatch stopwatch = Stopwatch.StartNew();

            string result = string.Empty;

            List<AppVerStringFormatEnum> strFormats = new() { AppVerStringFormatEnum.Use2, AppVerStringFormatEnum.Use2, AppVerStringFormatEnum.Use3 };
            foreach (AppVerCountDigitsEnum countDigits in Enum.GetValues(typeof(AppVerCountDigitsEnum)))
            {
                Assert.DoesNotThrow(() => result = App.GetCurrentVersion(countDigits, strFormats));
                TestContext.WriteLine($@"_app.GetCurrentVersion({countDigits}, {strFormats}) = {result}");
            }

            TestContext.WriteLine($@"{nameof(GetCurrentVersion_Execute_DoesNotThrow)} complete. Elapsed time: {stopwatch.Elapsed}");
            stopwatch.Stop();
        }

        [Test]
        public void GetCurrentVersion_Execute_AreEqual()
        {
            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
            TestContext.WriteLine($@"{nameof(GetCurrentVersion_Execute_AreEqual)} start.");
            Stopwatch stopwatch = Stopwatch.StartNew();

            string result = string.Empty;
            Version version = new(0, 1, 5, 123);

            List<AppVerStringFormatEnum> strFormats = new() { AppVerStringFormatEnum.Use2, AppVerStringFormatEnum.Use2, AppVerStringFormatEnum.Use2 };
            foreach (AppVerCountDigitsEnum countDigits in Enum.GetValues(typeof(AppVerCountDigitsEnum)))
            {
                Assert.DoesNotThrow(() => result = App.GetCurrentVersion(countDigits, strFormats, version));
                TestContext.WriteLine($@"_app.GetCurrentVersion({countDigits}, {strFormats}) = {result}");
                if (countDigits == AppVerCountDigitsEnum.Use1)
                    Assert.AreEqual("00", result);
                if (countDigits == AppVerCountDigitsEnum.Use2)
                    Assert.AreEqual("00.01", result);
                if (countDigits == AppVerCountDigitsEnum.Use3)
                    Assert.AreEqual("00.01.05", result);
                if (countDigits == AppVerCountDigitsEnum.Use4)
                    Assert.AreEqual("00.01.05.123", result);
            }

            strFormats = new List<AppVerStringFormatEnum>() { AppVerStringFormatEnum.Use1, AppVerStringFormatEnum.Use1, AppVerStringFormatEnum.Use1 };
            foreach (AppVerCountDigitsEnum countDigits in Enum.GetValues(typeof(AppVerCountDigitsEnum)))
            {
                Assert.DoesNotThrow(() => result = App.GetCurrentVersion(countDigits, strFormats, version));
                TestContext.WriteLine($@"_app.GetCurrentVersion({countDigits}, {strFormats}) = {result}");
                if (countDigits == AppVerCountDigitsEnum.Use1)
                    Assert.AreEqual("0", result);
                if (countDigits == AppVerCountDigitsEnum.Use2)
                    Assert.AreEqual("0.1", result);
                if (countDigits == AppVerCountDigitsEnum.Use3)
                    Assert.AreEqual("0.1.5", result);
                if (countDigits == AppVerCountDigitsEnum.Use4)
                    Assert.AreEqual("0.1.5.123", result);
            }

            TestContext.WriteLine($@"{nameof(GetCurrentVersion_Execute_AreEqual)} complete. Elapsed time: {stopwatch.Elapsed}");
            stopwatch.Stop();
        }

        [Test]
        public void SetNewSize_Execute_Does()
        {
            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
            TestContext.WriteLine($@"{nameof(GetCurrentVersion_Execute_AreEqual)} start.");
            Stopwatch stopwatch = Stopwatch.StartNew();

            Assert.DoesNotThrow(() => App.SetNewSize(null));
            Assert.DoesNotThrow(() => App.SetNewSize(null, FormStartPosition.CenterParent));
            Assert.DoesNotThrow(() => App.SetNewSize(new Form()));
            Assert.DoesNotThrow(() => App.SetNewSize(new Form(), FormStartPosition.CenterParent));

            TestContext.WriteLine($@"{nameof(GetCurrentVersion_Execute_AreEqual)} complete. Elapsed time: {stopwatch.Elapsed}");
            stopwatch.Stop();
        }

        #endregion
    }
}
