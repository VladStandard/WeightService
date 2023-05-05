// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using MDSoft.NetUtils;

namespace WsStorageCoreTests.Sql.Core.Models;

[TestFixture]
public sealed class WsDataContextModelTests
{
    [Test]
    public void Is_my_device_exists()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            string deviceName1 = MdNetUtils.GetLocalDeviceName(false);
            string deviceName2 = MdNetUtils.GetLocalDeviceName(false);

            WsSqlDeviceModel device1 = WsTestsUtils.DataTests.ContextManager.ContextItem.GetItemDeviceNotNullable(deviceName1);
            WsSqlDeviceModel device2 = WsTestsUtils.DataTests.ContextManager.ContextItem.GetItemDeviceOrCreateNew(deviceName2);
            TestContext.WriteLine($"{nameof(device1)}: {device1}");
            TestContext.WriteLine($"{nameof(device2)}: {device2}");

            Assert.IsTrue(device1.IsExists);
            Assert.IsTrue(device2.IsExists);
        }, false, new() { WsConfiguration.DevelopVS, WsConfiguration.ReleaseVS });
    }
}