namespace WsStorageCoreTests.Tables.TableScaleModels.TasksTypes;

[TestFixture]
public sealed class TaskTypeRepositoryTests : TableRepositoryTests
{
    private WsSqlTaskTypeRepository TaskTypeRepository { get; } = new();

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlTaskTypeModel> items = TaskTypeRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }
}