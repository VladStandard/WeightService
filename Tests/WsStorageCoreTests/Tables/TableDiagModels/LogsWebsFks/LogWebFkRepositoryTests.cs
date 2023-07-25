using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableDiagModels.LogsWebsFks;

[TestFixture]
public sealed class LogWebFkRepositoryTests : TableRepositoryTests
{
    private WsSqlLogWebFkRepository LogWebFkRepository { get; set; } = new();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlLogWebFkModel> items = LogWebFkRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultPublishTypes);
    }
}