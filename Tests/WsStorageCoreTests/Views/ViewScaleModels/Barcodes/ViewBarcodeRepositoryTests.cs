using NUnit.Framework.Constraints;
using WsStorageCore.Views.ViewScaleModels.Barcodes;

namespace WsStorageCoreTests.Views.ViewScaleModels.Barcodes;

[TestFixture]
public sealed class ViewBarcodesRepositoryTests : ViewRepositoryTests
{
    private IViewBarcodeRepository ViewBarcodeRepository { get; } = new WsSqlViewBarcodeRepository();
    
    protected override IResolveConstraint SortOrderValue => Is
        .Ordered.By(nameof(WsSqlViewBarcodeModel.CreateDt)).Descending;

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewBarcodeModel> items = ViewBarcodeRepository.GetList(SqlCrudConfig);
            PrintViewRecords(items);
        }, false, DefaultPublishTypes);
    }
}