// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsDataCoreTests.Helpers;

[TestFixture]
public sealed class AppHelperTests
{
    #region Private fields and properties

    private AppHelper App => AppHelper.Instance;

    #endregion

    #region Public methods

    [Test]
    public void AppHelper_GetCurrentVersionSubString_DoesNotThrow()
    {
        string version = "0.1.5.123";
        string actual = string.Empty;
        Assert.DoesNotThrow(() => actual = App.GetCurrentVersionSubString(version));
        Assert.AreEqual("0.1.5", actual);
    }

    [Test]
    public void AppHelper_GetCurrentVersion_Default()
    {
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
    }

    [Test]
    public void AppHelper_GetCurrentVersion_DoesNotThrow()
    {
        string result = string.Empty;

        List<AppVerStringFormatEnum> strFormats = new() { AppVerStringFormatEnum.Use2, AppVerStringFormatEnum.Use2, AppVerStringFormatEnum.Use3 };
        foreach (AppVerCountDigitsEnum countDigits in Enum.GetValues(typeof(AppVerCountDigitsEnum)))
        {
            Assert.DoesNotThrow(() => result = App.GetCurrentVersion(countDigits, strFormats));
            TestContext.WriteLine($@"_app.GetCurrentVersion({countDigits}, {strFormats}) = {result}");
        }
    }

    [Test]
    public void AppHelper_GetCurrentVersion_AreEqual()
    {
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
    }

    #endregion
}
