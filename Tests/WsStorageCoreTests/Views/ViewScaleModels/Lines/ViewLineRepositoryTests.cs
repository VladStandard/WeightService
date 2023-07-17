using WsStorageCore.Views.ViewScaleModels.Lines;

namespace WsStorageCoreTests.Views.ViewScaleModels.Lines;

[TestFixture]
public sealed class ViewLinesRepositoryTests : ViewRepositoryTests
{
    private IViewLineRepository ViewLineRepository = WsSqlViewLineRepository.Instance;

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewLineModel> items = ViewLineRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
}