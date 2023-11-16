using WsStorageCore.Entities.SchemaScale.PlusLabels;

namespace WsStorageCoreTests.Tables.TableScaleModels.PluLabels;

[TestFixture]
public sealed class PluLabelRepositoryTests : TableRepositoryTests
{
    private SqlPluLabelRepository PluLabelRepository { get; set; } = new();
    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(SqlEntityBase.ChangeDt)).Descending;

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<SqlPluLabelEntity> items = PluLabelRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }
}