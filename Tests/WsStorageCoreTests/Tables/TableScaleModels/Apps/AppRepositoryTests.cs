namespace WsStorageCoreTests.Tables.TableScaleModels.Apps;

[TestFixture]
public sealed class AppRepositoryTests : TableRepositoryTests
{
    private WsSqlAppRepository AppRepository { get; set; } = new();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlAppModel> items = AppRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
}