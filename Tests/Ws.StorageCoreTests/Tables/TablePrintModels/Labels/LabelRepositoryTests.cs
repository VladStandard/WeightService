using Ws.StorageCore.Entities.SchemaPrint.Labels;

namespace Ws.StorageCoreTests.Tables.TablePrintModels.Labels;

[TestFixture]
public sealed class LabelRepositoryTests : TableRepositoryTests
{
    private SqlLabelRepository LabelRepository { get; set; } = new();
    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(SqlEntityBase.CreateDt)).Descending;

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            // List<SqlLabelEntity> items = LabelRepository.GetList(SqlCrudConfig);
            // ParseRecords(items);
        }, false);
    }
}