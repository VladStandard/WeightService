using NUnit.Framework.Constraints;
using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableScaleFkModels.DeviceTypesFks;

[TestFixture]
public sealed class DeviceTypeFkRepositoryTests : TableRepositoryTests
{
    private WsSqlDeviceTypeFkRepository DeviceTypeFkRepository { get; } = new();

    private WsSqlDeviceTypeFkModel GetFirstDeviceTypeFk()
    {
        SqlCrudConfig.SelectTopRowsCount = 1;
        return DeviceTypeFkRepository.GetList(SqlCrudConfig).First();
    }

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlDeviceTypeFkModel> items = DeviceTypeFkRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultPublishTypes);
    }

    [Test]
    public void GetItemByDevice()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlDeviceTypeFkModel oldDeviceTypeFk = GetFirstDeviceTypeFk();
            WsSqlDeviceModel device = oldDeviceTypeFk.Device;
            WsSqlDeviceTypeFkModel deviceLinesByDevice = DeviceTypeFkRepository.GetItemByDevice(device);

            Assert.That(deviceLinesByDevice.IsNotNew, Is.True);
            Assert.That(deviceLinesByDevice, Is.EqualTo(oldDeviceTypeFk));

            TestContext.WriteLine(deviceLinesByDevice);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
}