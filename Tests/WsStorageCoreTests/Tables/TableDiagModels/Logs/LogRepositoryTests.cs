namespace WsStorageCoreTests.Tables.TableDiagModels.Logs;

[TestFixture]
public sealed class LogRepositoryTests : TableRepositoryTests
{
    private WsSqlLogRepository LogRepository { get; set; } = new();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlLogModel> items = LogRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
}