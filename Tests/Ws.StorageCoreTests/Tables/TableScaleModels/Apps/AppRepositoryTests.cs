using Ws.StorageCoreTests.Tables.Common;
using Ws.StorageCore.Entities.SchemaScale.Apps;

namespace Ws.StorageCoreTests.Tables.TableScaleModels.Apps;

[TestFixture]
public sealed class AppRepositoryTests : TableRepositoryTests
{
    private SqlAppRepository AppRepository { get; } = new();

    [Test, Order(1)]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            List<SqlAppEntity> items = AppRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }

    [Test, Order(2)]
    public void GetOrCreate()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            SqlAppEntity access = AppRepository.GetItemByNameOrCreate(nameof(StorageCoreTests));
            SqlAppEntity accessByUid = AppRepository.GetItemByUid(access.IdentityValueUid);
            Assert.That(access.IsExists, Is.True);
            Assert.That(accessByUid.IsExists, Is.True);
            TestContext.WriteLine($"Success created/updated: {access.Name} / {access.IdentityValueUid}");
        }, false);
    }

    [Test, Order(3)]
    public void GetByUid()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            SqlAppEntity appByName = AppRepository.GetItemByName(nameof(StorageCoreTests));
            Guid uid = appByName.IdentityValueUid;
            SqlAppEntity appByUid = AppRepository.GetItemByUid(uid);

            Assert.That(appByUid.IsExists, Is.True);
            Assert.That(appByUid.IdentityValueUid, Is.EqualTo(uid));

            TestContext.WriteLine($"Get item success: {appByUid.IdentityValueUid}");
        }, false);
    }

    [Test, Order(4)]
    public void GetNewItem()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            SqlAppEntity app = AppRepository.GetNewItem();
            Assert.That(app.IsNew, Is.True);
            TestContext.WriteLine($"New item: {app.IdentityValueUid}");
        }, false);
    }
}