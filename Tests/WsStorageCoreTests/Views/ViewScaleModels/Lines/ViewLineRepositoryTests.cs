using WsStorageCore.Views.ViewScaleModels.Lines;

namespace WsStorageCoreTests.Views.ViewScaleModels.Lines;

[TestFixture]
public sealed class ViewLinesRepositoryTests : ViewRepositoryTests
{
    private IViewLineRepository ViewLineRepository { get; } = new WsSqlViewLineRepository();

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewLineModel> items = ViewLineRepository.GetList(SqlCrudConfig);
            PrintViewRecords(items);
        }, false, DefaultPublishTypes);
    }
}