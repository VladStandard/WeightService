namespace WsStorageCoreTests.Tables.TableScaleModels.PluScales;

[TestFixture]
public sealed class PluScalesRepositoryTests : TableRepositoryTests
{
    private WsSqlPluLineRepository PluLineRepository  { get; set; } = new();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluScaleModel> items = PluLineRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
}