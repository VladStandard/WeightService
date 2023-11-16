using Ws.StorageCore.Entities.SchemaScale.Versions;

namespace Ws.StorageCoreTests.Tables.TableScaleModels.Versions;

[TestFixture]
public sealed class VersionRepositoryTests : TableRepositoryTests
{
    private SqlVersionRepository VersionRepository { get; } = new();
    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(SqlVersionEntity.Version)).Descending;

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            List<SqlVersionEntity> items = VersionRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }
}