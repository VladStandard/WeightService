using WsStorageCore.Entities.SchemaScale.Apps;

namespace WsStorageCoreTests.Tables.TableScaleModels.Apps;

[TestFixture]
public sealed class AppRepositoryTests : TableRepositoryTests
{
    private SqlAppRepository AppRepository { get; } = new();

    [Test, Order(1)]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<SqlAppEntity> items = AppRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }

    [Test, Order(2)]
    public void GetOrCreate()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            SqlAppEntity access = AppRepository.GetItemByNameOrCreate(nameof(WsStorageCoreTests));
            SqlAppEntity accessByUid = AppRepository.GetItemByUid(access.IdentityValueUid);
            Assert.That(access.IsExists, Is.True);
            Assert.That(accessByUid.IsExists, Is.True);
            TestContext.WriteLine($"Success created/updated: {access.Name} / {access.IdentityValueUid}");
        }, false);
    }

    [Test, Order(3)]
    public void GetByUid()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            SqlAppEntity appByName = AppRepository.GetItemByName(nameof(WsStorageCoreTests));
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
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            SqlAppEntity app = AppRepository.GetNewItem();
            Assert.That(app.IsNew, Is.True);
            TestContext.WriteLine($"New item: {app.IdentityValueUid}");
        }, false);
    }
}