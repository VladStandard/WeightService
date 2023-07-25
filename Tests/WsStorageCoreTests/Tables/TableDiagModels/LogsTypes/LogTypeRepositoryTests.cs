using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableDiagModels.LogsTypes;

[TestFixture]
public sealed class LogTypesRepositoryTests : TableRepositoryTests
{
    private WsSqlLogTypeRepository LogTypeRepository { get; set; } = new();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlLogTypeModel> items = LogTypeRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            ParseRecords(items);
        }, false, DefaultPublishTypes);
    }
}