using MDSoft.NetUtils;
using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableScaleModels.Devices;

[TestFixture]
public sealed class DeviceRepositoryTests : TableRepositoryTests
{
    private WsSqlDeviceRepository DeviceRepository { get; set; } = new();
    
    [Test, Order(1)]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlDeviceModel> items = DeviceRepository.GetList(SqlCrudConfig);
            WsTestsUtils.DataTests.ParseRecords(items);
        }, false, DefaultPublishTypes);
    }
    
    [Test, Order(2)]
    public void GetOrCreate()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            string pcName = MdNetUtils.GetLocalDeviceName(false);
            WsSqlDeviceModel device = DeviceRepository.GetItemByNameOrCreate(pcName);
            WsSqlDeviceModel deviceByUid = DeviceRepository.GetItemByUid(device.IdentityValueUid);
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
            WsSqlDeviceModel deviceByName = DeviceRepository.GetItemByName(pcName);
            Guid uid = deviceByName.IdentityValueUid;
            WsSqlDeviceModel deviceByUid= DeviceRepository.GetItemByUid(uid);
            
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
            WsSqlDeviceModel device = DeviceRepository.GetNewItem();
            Assert.That(device.IsNotExists, Is.True);
            TestContext.WriteLine($"New item: {device.IdentityValueUid}");
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
    
}