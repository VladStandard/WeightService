// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using MDSoft.Wmi.Helpers;

namespace MDSoft.WmiTests;

[TestFixture]
internal class PluginMemoryTests
{
    #region Public and private fields, properties, constructor

    private MdWmiHelper Wmi => MdWmiHelper.Instance;

    #endregion

    #region Public and private methods

    [Test]
    public void WsWmiWin32Memory_GetWin32OperatingSystemMemory_MoreThanZero()
    {
        Assert.DoesNotThrow(() =>
        {
            MdWmiWinMemoryModel getWmi = Wmi.GetWin32OperatingSystemMemory();
            TestContext.WriteLine($"{nameof(getWmi.FreeVirtual)}: {getWmi.FreeVirtual}");
            TestContext.WriteLine($"{nameof(getWmi.FreePhysical)}: {getWmi.FreePhysical}");
            TestContext.WriteLine($"{nameof(getWmi.TotalVirtual)}: {getWmi.TotalVirtual}");
            TestContext.WriteLine($"{nameof(getWmi.TotalPhysical)}: {getWmi.TotalPhysical}");
            Assert.IsTrue(getWmi.FreeVirtual > 0);
            Assert.IsTrue(getWmi.FreePhysical > 0);
            Assert.IsTrue(getWmi.TotalVirtual > 0);
            Assert.IsTrue(getWmi.TotalPhysical > 0);
        });
    }

    #endregion
}