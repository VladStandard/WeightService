using Ws.StorageCore.Entities.SchemaRef1c.Boxes;

namespace Ws.StorageCoreTests.Tables.TableRef1cModels.Boxes;

[TestFixture]
public sealed class BoxRepositoryTests : TableRepositoryTests
{
    private SqlBoxRepository BoxRepository { get; } = new();

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<SqlBoxEntity> items = BoxRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }
}