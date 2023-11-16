using WsStorageCore.Views.ViewScaleModels.PluLabels;

namespace WsStorageCoreTests.Views.ViewScaleModels.PluLabels;

[TestFixture]
public sealed class ViewPluLabelRepositoryTests : ViewRepositoryTests
{
    private IViewPluLabelRepository PluLabelRepository { get; } = new SqlViewPluLabelRepository();

    protected override IResolveConstraint SortOrderValue => Is
        .Ordered.By(nameof(SqlViewPluLabelModel.CreateDt)).Descending;


    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<SqlViewPluLabelModel> items = PluLabelRepository.GetList(SqlCrudConfig);
            PrintViewRecords(items);
        }, false);
    }
}