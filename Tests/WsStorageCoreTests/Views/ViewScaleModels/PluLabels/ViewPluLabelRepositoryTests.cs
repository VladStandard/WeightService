using NUnit.Framework.Constraints;
using WsStorageCore.Views.ViewScaleModels.PluLabels;

namespace WsStorageCoreTests.Views.ViewScaleModels.PluLabels;

[TestFixture]
public sealed class ViewPluLabelRepositoryTests : ViewRepositoryTests
{
    private IViewPluLabelRepository PluLabelRepository { get; } = new WsSqlViewPluLabelRepository();
    
    protected override IResolveConstraint SortOrderValue => Is
        .Ordered.By(nameof(WsSqlViewPluLabelModel.CreateDt)).Descending;

    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewPluLabelModel> items = PluLabelRepository.GetList(SqlCrudConfig);
            PrintViewRecords(items);
        }, false, DefaultPublishTypes);
    }
}