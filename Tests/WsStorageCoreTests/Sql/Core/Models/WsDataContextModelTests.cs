// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsDataCore.Enums;
using MDSoft.NetUtils;
using WsStorageCore.TableScaleModels.Devices;

namespace WsStorageCoreTests.Sql.Core.Models;

[TestFixture]
public sealed class WsDataContextModelTests
{
    [Test]
    public void Is_my_device_exists()
    {
        WsTestsUtils.DataCore.AssertAction(() =>
        {
            string deviceName1 = MdNetUtils.GetLocalDeviceName(false);
            string deviceName2 = MdNetUtils.GetLocalDeviceName(false);

            DeviceModel device1 = WsTestsUtils.DataCore.DataContext.GetItemDeviceNotNullable(deviceName1);
            DeviceModel device2 = WsTestsUtils.DataCore.DataContext.GetItemDeviceOrCreateNew(deviceName2);
            TestContext.WriteLine($"{nameof(device1)}: {device1}");
            TestContext.WriteLine($"{nameof(device2)}: {device2}");

            Assert.IsTrue(device1.IsExists);
            Assert.IsTrue(device2.IsExists);
        }, false, new() { WsConfiguration.DevelopVS, WsConfiguration.ReleaseVS });
    }
}