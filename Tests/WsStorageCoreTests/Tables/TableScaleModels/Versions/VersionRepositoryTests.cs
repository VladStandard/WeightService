using WsStorageCore.Entities.SchemaScale.Versions;
namespace WsStorageCoreTests.Tables.TableScaleModels.Versions;

[TestFixture]
public sealed class VersionRepositoryTests : TableRepositoryTests
{
    private WsSqlVersionRepository VersionRepository { get; } = new();
    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(WsSqlVersionEntity.Version)).Descending;

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlVersionEntity> items = VersionRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }
}