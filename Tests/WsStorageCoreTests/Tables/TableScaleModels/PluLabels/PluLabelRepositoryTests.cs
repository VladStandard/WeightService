using WsStorageCore.Entities.SchemaScale.PlusLabels;

namespace WsStorageCoreTests.Tables.TableScaleModels.PluLabels;

[TestFixture]
public sealed class PluLabelRepositoryTests : TableRepositoryTests
{
    private WsSqlPluLabelRepository PluLabelRepository { get; set; } = new();
    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(WsSqlTableBase.ChangeDt)).Descending;

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluLabelEntity> items = PluLabelRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }
}