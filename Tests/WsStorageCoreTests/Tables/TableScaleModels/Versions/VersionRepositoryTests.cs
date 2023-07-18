namespace WsStorageCoreTests.Tables.TableScaleModels.Versions;

[TestFixture]
public sealed class VersionRepositoryTests : TableRepositoryTests
{
    private WsSqlVersionRepository VersionRepository  { get; set; } = new();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlVersionModel> items = VersionRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
}