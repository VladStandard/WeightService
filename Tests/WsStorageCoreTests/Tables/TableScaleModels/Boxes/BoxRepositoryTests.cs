using WsStorageCore.Entities.SchemaRef1c.Boxes;

namespace WsStorageCoreTests.Tables.TableScaleModels.Boxes;

[TestFixture]
public sealed class BoxRepositoryTests : TableRepositoryTests
{
    private WsSqlBoxRepository BoxRepository { get; } = new();

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<WsSqlBoxEntity> items = BoxRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }
}