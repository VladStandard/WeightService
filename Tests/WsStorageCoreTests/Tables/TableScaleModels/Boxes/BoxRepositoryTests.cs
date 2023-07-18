namespace WsStorageCoreTests.Tables.TableScaleModels.Boxes;

[TestFixture]
public sealed class BoxRepositoryTests : TableRepositoryTests
{
    private WsSqlBoxRepository BoxRepository { get; set; } = new();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlBoxModel> items = BoxRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
}