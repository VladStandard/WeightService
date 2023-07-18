namespace WsStorageCoreTests.Tables.TableScaleModels.Access;

[TestFixture]
public sealed class AccessRepositoryTests : TableRepositoryTests
{
    private WsSqlAccessRepository AccessRepository { get; set; } = new();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlAccessModel> items = AccessRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
}