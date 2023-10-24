using WsStorageCore.Entities.SchemaScale.DeviceTypes;
namespace WsStorageCoreTests.Tables.TableScaleModels.DeviceTypes;

[TestFixture]
public sealed class DeviceTypeRepositoryTests : TableRepositoryTests
{
    private WsSqlDeviceTypeRepository DeviceTypeRepository { get; } = new();

    private static IEnumerable<string> GetDeviceTypesEnums() => new List<string>()
    {
        "Monoblock",
        "Print TSC",
        "Print Zebra",
        "Scale Massa-K",
        "SQL-Server",
        "Web-Server",
        "Computer",
    };

    [Test, Order(1)]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlDeviceTypeModel> items = DeviceTypeRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }

    [Test, Order(2)]
    public void GetOrCreate()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            foreach (string deviceTypeName in GetDeviceTypesEnums())
            {
                WsSqlDeviceTypeModel deviceType = DeviceTypeRepository.GetItemByNameOrCreate(deviceTypeName);
                WsSqlDeviceTypeModel deviceTypeByUid = DeviceTypeRepository.GetItemByUid(deviceType.IdentityValueUid);

                Assert.That(deviceType.IsExists, Is.True);
                Assert.That(deviceTypeByUid.IsExists, Is.True);

                TestContext.WriteLine($"Success created/updated: {deviceType.Name}");
            }
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }

    [Test, Order(3)]
    public void GetByUid()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlDeviceTypeModel deviceByName = DeviceTypeRepository.GetItemByName("Monoblock");
            Guid uid = deviceByName.IdentityValueUid;
            WsSqlDeviceTypeModel deviceByUid = DeviceTypeRepository.GetItemByUid(uid);

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
            WsSqlDeviceTypeModel device = DeviceTypeRepository.GetNewItem();
            Assert.That(device.IsNew, Is.True);
            TestContext.WriteLine($"New item: {device.IdentityValueUid}");
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
}