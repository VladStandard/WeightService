using WsStorageCore.Entities.SchemaScale.Apps;
namespace WsStorageCoreTests.Tables.TableScaleModels.Apps;

[TestFixture]
public sealed class AppRepositoryTests : TableRepositoryTests
{
    private WsSqlAppRepository AppRepository { get; } = new();

    [Test, Order(1)]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlAppEntity> items = AppRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }

    [Test, Order(2)]
    public void GetOrCreate()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlAppEntity access = AppRepository.GetItemByNameOrCreate(nameof(WsStorageCoreTests));
            WsSqlAppEntity accessByUid = AppRepository.GetItemByUid(access.IdentityValueUid);
            Assert.That(access.IsExists, Is.True);
            Assert.That(accessByUid.IsExists, Is.True);
            TestContext.WriteLine($"Success created/updated: {access.Name} / {access.IdentityValueUid}");
        }, false, new() { WsEnumConfiguration.DevelopVs, WsEnumConfiguration.ReleaseVs });
    }

    [Test, Order(3)]
    public void GetByUid()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlAppEntity appByName = AppRepository.GetItemByName(nameof(WsStorageCoreTests));
            Guid uid = appByName.IdentityValueUid;
            WsSqlAppEntity appByUid = AppRepository.GetItemByUid(uid);

            Assert.That(appByUid.IsExists, Is.True);
            Assert.That(appByUid.IdentityValueUid, Is.EqualTo(uid));

            TestContext.WriteLine($"Get item success: {appByUid.IdentityValueUid}");
        }, false, new() { WsEnumConfiguration.DevelopVs, WsEnumConfiguration.ReleaseVs });
    }

    [Test, Order(4)]
    public void GetNewItem()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlAppEntity app = AppRepository.GetNewItem();
            Assert.That(app.IsNew, Is.True);
            TestContext.WriteLine($"New item: {app.IdentityValueUid}");
        }, false, new() { WsEnumConfiguration.DevelopVs, WsEnumConfiguration.ReleaseVs });
    }
}