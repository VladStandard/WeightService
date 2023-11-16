using WsStorageCore.Entities.SchemaRef.WorkShops;

namespace WsStorageCoreTests.Tables.TableScaleModels.WorkShops;

[TestFixture]
public sealed class WorkShopRepositoryTests : TableRepositoryTests
{
    private SqlWorkShopRepository WorkShopRepository { get; } = new();

    [Test, Order(1)]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<SqlWorkShopEntity> items = WorkShopRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }

    [Test, Order(2)]
    public void GetNewItem()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            SqlWorkShopEntity app = WorkShopRepository.GetNewItem();
            Assert.That(app.IsNew, Is.True);
            TestContext.WriteLine($"New item: {app.IdentityValueUid}");
        }, false);
    }
}