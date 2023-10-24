using WsStorageCore.Entities.SchemaRef.WorkShops;

namespace WsStorageCoreTests.Tables.TableScaleModels.WorkShops;

[TestFixture]
public sealed class WorkShopRepositoryTests : TableRepositoryTests
{
    private WsSqlWorkShopRepository WorkShopRepository { get; } = new();

    [Test, Order(1)]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<WsSqlWorkShopEntity> items = WorkShopRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }

    [Test, Order(2)]
    public void GetNewItem()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlWorkShopEntity app = WorkShopRepository.GetNewItem();
            Assert.That(app.IsNew, Is.True);
            TestContext.WriteLine($"New item: {app.IdentityValueUid}");
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
}