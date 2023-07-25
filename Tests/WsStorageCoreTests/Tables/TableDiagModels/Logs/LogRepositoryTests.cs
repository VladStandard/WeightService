using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableDiagModels.Logs;

[TestFixture]
public sealed class LogRepositoryTests : TableRepositoryTests
{
    private WsSqlLogRepository LogRepository { get; set; } = new();

    private WsSqlLogModel GetFirstLogModel()
    {
        SqlCrudConfig.SelectTopRowsCount = 1;
        return LogRepository.GetList(SqlCrudConfig).First();
    }
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlLogModel> items = LogRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            ParseRecords(items);
        }, false, DefaultPublishTypes);
    }
    
    [Test]
    public void GetItemByUid()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlLogModel oldLog = GetFirstLogModel();
            WsSqlLogModel logByUid = LogRepository.GetItemByUid(oldLog.IdentityValueUid);
            
            Assert.That(logByUid.IsExists, Is.True);
            Assert.That(logByUid, Is.EqualTo(oldLog));
            
            TestContext.WriteLine($"Get item success: {logByUid.IdentityValueUid}");
        }, false, DefaultPublishTypes);
    }
}