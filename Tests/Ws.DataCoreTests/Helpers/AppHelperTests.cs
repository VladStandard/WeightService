using System.Reflection.Emit;
using Ws.DataCore.Settings;

namespace Ws.DataCoreTests.Helpers;

[TestFixture]
public sealed class AppHelperTests
{
    private AppVersionHelper App => AppVersionHelper.Instance;

    [Test]
    public void AppHelper_GetCurrentVersionSubString_DoesNotThrow()
    {
        const string version = "0.1.5.123";
        string actual = string.Empty;
        Assert.DoesNotThrow(() => actual = AppVersionHelper.GetCurrentVersionSubString(version));
        Assert.AreEqual("0.1.5", actual);
    }

    [Test]
    public void AppHelper_GetCurrentVersion_AreEqual()
    {
        AssemblyName assemblyName = new() {Name = "MyAssembly", Version = new(0, 1, 5, 123) };
        AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);

        string result = string.Empty;
        foreach (EnumAppVerCountDigits countDigits in Enum.GetValues(typeof(EnumAppVerCountDigits)))
        {
            Assert.DoesNotThrow(() => result = App.GetCurrentVersion(assemblyBuilder, countDigits));
            TestContext.WriteLine($@"_app.GetCurrentVersion({countDigits}) = {result}");
            if (countDigits == EnumAppVerCountDigits.Use1)
                Assert.AreEqual("0", result);
            else if (countDigits == EnumAppVerCountDigits.Use2)
                Assert.AreEqual("0.1", result);
            else if (countDigits == EnumAppVerCountDigits.Use3)
                Assert.AreEqual("0.1.5", result);
            else if (countDigits == EnumAppVerCountDigits.Use4)
                Assert.AreEqual("0.1.5.123", result);
        }
    }
}
