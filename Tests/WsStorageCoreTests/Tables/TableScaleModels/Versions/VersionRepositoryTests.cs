using WsStorageCore.Entities.SchemaScale.Versions;

namespace WsStorageCoreTests.Tables.TableScaleModels.Versions;

[TestFixture]
public sealed class VersionRepositoryTests : TableRepositoryTests
{
    private SqlVersionRepository VersionRepository { get; } = new();
    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(SqlVersionEntity.Version)).Descending;

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<SqlVersionEntity> items = VersionRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }
}