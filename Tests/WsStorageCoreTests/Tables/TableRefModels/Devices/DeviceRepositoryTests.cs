using MDSoft.NetUtils;
using WsStorageCore.Entities.SchemaRef.Hosts;

namespace WsStorageCoreTests.Tables.TableRefModels.Devices;

[TestFixture]
public sealed class DeviceRepositoryTests : TableRepositoryTests
{
    private WsSqlHostRepository HostRepository { get; } = new();

    [Test, Order(1)]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<WsSqlHostEntity> items = HostRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }

    [Test, Order(2)]
    public void GetOrCreate()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            string pcName = MdNetUtils.GetLocalDeviceName(false);
            WsSqlHostEntity host = HostRepository.GetItemByNameOrCreate(pcName);
            WsSqlHostEntity hostByUid = HostRepository.GetItemByUid(host.IdentityValueUid);

            Assert.That(host.IsExists, Is.True);
            Assert.That(hostByUid.IsExists, Is.True);

            TestContext.WriteLine($"Success created/updated: {host.Name}");
        }, false, new() { WsEnumConfiguration.DevelopVs, WsEnumConfiguration.ReleaseVs });
    }

    [Test, Order(3)]
    public void GetByUid()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            string pcName = MdNetUtils.GetLocalDeviceName(false);
            WsSqlHostEntity hostByName = HostRepository.GetItemByName(pcName);
            Guid uid = hostByName.IdentityValueUid;
            WsSqlHostEntity hostByUid = HostRepository.GetItemByUid(uid);

            Assert.That(hostByUid.IsExists, Is.True);
            Assert.That(hostByUid.IdentityValueUid, Is.EqualTo(uid));

            TestContext.WriteLine($"Get item success: {hostByUid.IdentityValueUid}");
        }, false, new() { WsEnumConfiguration.DevelopVs, WsEnumConfiguration.ReleaseVs });
    }

    [Test, Order(4)]
    public void GetNewItem()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlHostEntity host = HostRepository.GetNewItem();
            Assert.That(host.IsNew, Is.True);
            TestContext.WriteLine($"New item: {host.IdentityValueUid}");
        }, false, new() { WsEnumConfiguration.DevelopVs, WsEnumConfiguration.ReleaseVs });
    }
}