using Ws.StorageCore.Entities.SchemaRef1c.Clips;

namespace Ws.StorageCoreTests.Tables.TableScaleModels.Clips;

[TestFixture]
public sealed class ClipRepositoryTests : TableRepositoryTests
{
    private SqlClipRepository ClipRepository { get; } = new();

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<SqlClipEntity> items = ClipRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }
}