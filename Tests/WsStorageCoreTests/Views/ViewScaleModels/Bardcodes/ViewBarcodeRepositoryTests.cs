using WsStorageCore.Views.ViewScaleModels.Barcodes;

namespace WsStorageCoreTests.Views.ViewScaleModels.Bardcodes;

[TestFixture]
public sealed class ViewBarcodesRepositoryTests
{
    private WsSqlViewBarcodeRepository ViewBarcodeRepository = WsSqlViewBarcodeRepository.Instance;
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlCrudConfigModel sqlConfig = new WsSqlCrudConfigModel() { SelectTopRowsCount = 10 };
            List<WsSqlViewBarcodeModel> items = ViewBarcodeRepository.GetList(sqlConfig);
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
}