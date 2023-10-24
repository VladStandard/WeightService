using WsStorageCore.Entities.SchemaScale.Devices;
using WsStorageCore.Entities.SchemaScale.DeviceTypesFks;
namespace WsStorageCoreTests.Tables.TableScaleFkModels.DeviceTypesFks;

[TestFixture]
public sealed class DeviceTypeFkRepositoryTests : TableRepositoryTests
{
    private WsSqlDeviceTypeFkRepository DeviceTypeFkRepository { get; } = new();

    private WsSqlDeviceTypeFkEntity GetFirstDeviceTypeFk()
    {
        SqlCrudConfig.SelectTopRowsCount = 1;
        return DeviceTypeFkRepository.GetList(SqlCrudConfig).First();
    }

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlDeviceTypeFkEntity> items = DeviceTypeFkRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }

    [Test]
    public void GetItemByDevice()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlDeviceTypeFkEntity oldDeviceTypeFk = GetFirstDeviceTypeFk();
            WsSqlDeviceEntity device = oldDeviceTypeFk.Device;
            WsSqlDeviceTypeFkEntity deviceLinesByDevice = DeviceTypeFkRepository.GetItemByDevice(device);

            Assert.That(deviceLinesByDevice.IsExists, Is.True);
            Assert.That(deviceLinesByDevice, Is.EqualTo(oldDeviceTypeFk));

            TestContext.WriteLine(deviceLinesByDevice);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
}