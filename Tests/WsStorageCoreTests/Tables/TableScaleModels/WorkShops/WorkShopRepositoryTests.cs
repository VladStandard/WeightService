namespace WsStorageCoreTests.Tables.TableScaleModels.WorkShops;

[TestFixture]
public sealed class WorkShopRepositoryTests : TableRepositoryTests
{
    private WsSqlWorkShopRepository WorkShopRepository { get; set; } = new();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlWorkShopModel> items = WorkShopRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
}