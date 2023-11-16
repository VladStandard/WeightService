using System.Reflection.Emit;
using WsDataCore.Settings.Helpers;

namespace Ws.DataCoreTests.Helpers;

[TestFixture]
public sealed class AppHelperTests
{
    private WsAppVersionHelper App => WsAppVersionHelper.Instance;

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
        AssemblyName assemblyName = new() {Name = "MyAssembly", Version = new(0, 1, 5, 123) };
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
}
