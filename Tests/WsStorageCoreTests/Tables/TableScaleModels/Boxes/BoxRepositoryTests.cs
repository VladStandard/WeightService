using WsStorageCore.Entities.SchemaRef1c.Boxes;

namespace WsStorageCoreTests.Tables.TableScaleModels.Boxes;

[TestFixture]
public sealed class BoxRepositoryTests : TableRepositoryTests
{
    private SqlBoxRepository BoxRepository { get; } = new();

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<SqlBoxEntity> items = BoxRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }
}