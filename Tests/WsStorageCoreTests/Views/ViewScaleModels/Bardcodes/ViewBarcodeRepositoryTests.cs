using WsStorageCore.Views.ViewScaleModels.Barcodes;

namespace WsStorageCoreTests.Views.ViewScaleModels.Bardcodes;

[TestFixture]
public sealed class ViewBarcodesRepositoryTests : ViewRepositoryTests
{
    private IViewBarcodeRepository ViewBarcodeRepository { get; } = new WsSqlViewBarcodeRepository();

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewBarcodeModel> items = ViewBarcodeRepository.GetList(SqlCrudConfig);
            WsTestsUtils.DataTests.PrintViewRecords(items);
        }, false, DefaultPublishTypes);
    }
}