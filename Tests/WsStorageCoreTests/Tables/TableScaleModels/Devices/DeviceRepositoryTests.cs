using MDSoft.NetUtils;
using WsStorageCore.Entities.SchemaScale.Devices;
namespace WsStorageCoreTests.Tables.TableScaleModels.Devices;

[TestFixture]
public sealed class DeviceRepositoryTests : TableRepositoryTests
{
    private WsSqlDeviceRepository DeviceRepository { get; } = new();

    [Test, Order(1)]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<WsSqlDeviceEntity> items = DeviceRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }

    [Test, Order(2)]
    public void GetOrCreate()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            string pcName = MdNetUtils.GetLocalDeviceName(false);
            WsSqlDeviceEntity device = DeviceRepository.GetItemByNameOrCreate(pcName);
            WsSqlDeviceEntity deviceByUid = DeviceRepository.GetItemByUid(device.IdentityValueUid);

            Assert.That(device.IsExists, Is.True);
            Assert.That(deviceByUid.IsExists, Is.True);

            TestContext.WriteLine($"Success created/updated: {device.Name}");
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }

    [Test, Order(3)]
    public void GetByUid()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            string pcName = MdNetUtils.GetLocalDeviceName(false);
            WsSqlDeviceEntity deviceByName = DeviceRepository.GetItemByName(pcName);
            Guid uid = deviceByName.IdentityValueUid;
            WsSqlDeviceEntity deviceByUid = DeviceRepository.GetItemByUid(uid);

            Assert.That(deviceByUid.IsExists, Is.True);
            Assert.That(deviceByUid.IdentityValueUid, Is.EqualTo(uid));

            TestContext.WriteLine($"Get item success: {deviceByUid.IdentityValueUid}");
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }

    [Test, Order(4)]
    public void GetNewItem()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlDeviceEntity device = DeviceRepository.GetNewItem();
            Assert.That(device.IsNew, Is.True);
            TestContext.WriteLine($"New item: {device.IdentityValueUid}");
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
}