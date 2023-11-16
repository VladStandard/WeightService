using MDSoft.NetUtils;
using Ws.StorageCore.Entities.SchemaRef.Hosts;

namespace Ws.StorageCoreTests.Tables.TableRefModels.Devices;

[TestFixture]
public sealed class DeviceRepositoryTests : TableRepositoryTests
{
    private SqlHostRepository HostRepository { get; } = new();

    [Test, Order(1)]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<SqlHostEntity> items = HostRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }

    [Test, Order(2)]
    public void GetOrCreate()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            string pcName = MdNetUtils.GetLocalDeviceName(false);
            SqlHostEntity host = HostRepository.GetItemByNameOrCreate(pcName);
            SqlHostEntity hostByUid = HostRepository.GetItemByUid(host.IdentityValueUid);

            Assert.That(host.IsExists, Is.True);
            Assert.That(hostByUid.IsExists, Is.True);

            TestContext.WriteLine($"Success created/updated: {host.Name}");
        }, false);
    }

    [Test, Order(3)]
    public void GetByUid()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            string pcName = MdNetUtils.GetLocalDeviceName(false);
            SqlHostEntity hostByName = HostRepository.GetItemByName(pcName);
            Guid uid = hostByName.IdentityValueUid;
            SqlHostEntity hostByUid = HostRepository.GetItemByUid(uid);

            Assert.That(hostByUid.IsExists, Is.True);
            Assert.That(hostByUid.IdentityValueUid, Is.EqualTo(uid));

            TestContext.WriteLine($"Get item success: {hostByUid.IdentityValueUid}");
        }, false);
    }

    [Test, Order(4)]
    public void GetNewItem()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            SqlHostEntity host = HostRepository.GetNewItem();
            Assert.That(host.IsNew, Is.True);
            TestContext.WriteLine($"New item: {host.IdentityValueUid}");
        }, false);
    }
}