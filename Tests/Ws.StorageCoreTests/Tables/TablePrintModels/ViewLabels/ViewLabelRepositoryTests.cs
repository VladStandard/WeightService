using Ws.StorageCore.Entities.SchemaPrint.ViewLabels;

namespace Ws.StorageCoreTests.Tables.TablePrintModels.ViewLabels;

[TestFixture]
public sealed class ViewLabelRepositoryTests : TableRepositoryTests
{
    private SqlViewLabelRepository LabelRepository { get; set; } = new();
    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(SqlEntityBase.CreateDt)).Descending;

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            List<SqlViewLabel> items = LabelRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }
}