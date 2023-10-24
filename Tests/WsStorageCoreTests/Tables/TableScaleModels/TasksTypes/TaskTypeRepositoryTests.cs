using WsStorageCore.Entities.SchemaScale.TasksTypes;
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
            List<WsSqlTaskTypeEntity> items = TaskTypeRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }
}