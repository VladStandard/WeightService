using WsStorageCore.Views.ViewDiagModels.TableSize;

namespace WsStorageCoreTests.Views.ViewDiagModels.TableSize;

[TestFixture]
public sealed class ViewTableSizeRepositoryTests : ViewRepositoryTests
{
    private IViewTableSizeRepository ViewTableSizeRepository { get; } = new WsSqlViewTableSizeRepository();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewTableSizeModel> items = ViewTableSizeRepository.GetList(SqlCrudConfig);
            PrintViewRecords(items);
        }, false, DefaultPublishTypes);
    }
}