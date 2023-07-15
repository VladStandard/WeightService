// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Reflection.Emit;
using WsDataCore.Settings.Helpers;

namespace WsDataCoreTests.Helpers;

[TestFixture]
public sealed class AppHelperTests
{
    #region Private fields and properties

    private WsAppVersionHelper App => WsAppVersionHelper.Instance;

    #endregion

    #region Public methods

    [Test]
    public void AppHelper_GetCurrentVersionSubString_DoesNotThrow()
    {
        const string version = "0.1.5.123";
        string actual = string.Empty;
        Assert.DoesNotThrow(() => actual = WsAppVersionHelper.GetCurrentVersionSubString(version));
        Assert.AreEqual("0.1.5", actual);
    }

    [Test]
    public void AppHelper_GetCurrentVersion_AreEqual()
    {
        AssemblyName assemblyName = new AssemblyName() {Name = "MyAssembly", Version = new(0, 1, 5, 123) };
        AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);

        string result = string.Empty;
        foreach (WsEnumAppVerCountDigits countDigits in Enum.GetValues(typeof(WsEnumAppVerCountDigits)))
        {
            Assert.DoesNotThrow(() => result = App.GetCurrentVersion(assemblyBuilder, countDigits));
            TestContext.WriteLine($@"_app.GetCurrentVersion({countDigits}) = {result}");
            if (countDigits == WsEnumAppVerCountDigits.Use1)
                Assert.AreEqual("0", result);
            else if (countDigits == WsEnumAppVerCountDigits.Use2)
                Assert.AreEqual("0.1", result);
            else if (countDigits == WsEnumAppVerCountDigits.Use3)
                Assert.AreEqual("0.1.5", result);
            else if (countDigits == WsEnumAppVerCountDigits.Use4)
                Assert.AreEqual("0.1.5.123", result);
        }
    }

    #endregion
}
