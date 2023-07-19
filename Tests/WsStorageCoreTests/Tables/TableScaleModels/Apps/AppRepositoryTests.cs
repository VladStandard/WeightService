namespace WsStorageCoreTests.Tables.TableScaleModels.Apps;

[TestFixture]
public sealed class AppRepositoryTests : TableRepositoryTests
{
    private WsSqlAppRepository AppRepository { get; set; } = new();
    
    [Test, Order(1)]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlAppModel> items = AppRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
    
    [Test, Order(2)]
    public void GetOrCreate()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlAppModel access = AppRepository.GetItemByNameOrCreate(nameof(WsStorageCoreTests));
            WsSqlAppModel accessByUid = AppRepository.GetItemByUid(access.IdentityValueUid);
            Assert.That(access.IsExists, Is.True);
            Assert.That(accessByUid.IsExists, Is.True);
            TestContext.WriteLine($"Success created/updated: {access.Name} / {access.IdentityValueUid}");
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
    
    [Test, Order(3)]
    public void GetByUid()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlAppModel appByName = AppRepository.GetItemByName(nameof(WsStorageCoreTests));
            WsSqlAppModel appByUid= AppRepository.GetItemByUid(appByName.IdentityValueUid);
            Assert.That(appByUid.IsExists, Is.True);
            TestContext.WriteLine($"Get item success: {appByUid.IdentityValueUid}");
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
    
    [Test, Order(4)]
    public void GetNewItem()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlAppModel app = AppRepository.GetNewItem();
            Assert.That(app.IsNotExists, Is.True);
            TestContext.WriteLine($"New item: {app.IdentityValueUid}");
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
}