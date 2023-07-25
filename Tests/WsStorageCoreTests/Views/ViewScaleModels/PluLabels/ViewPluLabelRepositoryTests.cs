using WsStorageCore.Views.ViewScaleModels.PluLabels;

namespace WsStorageCoreTests.Views.ViewScaleModels.PluLabels;

[TestFixture]
public sealed class ViewPluLabelRepositoryTests : ViewRepositoryTests
{
    private IViewPluLabelRepository PluLabelRepository { get; } = new WsSqlViewPluLabelRepository();

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