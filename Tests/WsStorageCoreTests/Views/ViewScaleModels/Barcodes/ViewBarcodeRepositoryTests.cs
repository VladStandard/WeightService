using WsStorageCore.Views.ViewScaleModels.Barcodes;

namespace WsStorageCoreTests.Views.ViewScaleModels.Barcodes;

[TestFixture]
public sealed class ViewBarcodesRepositoryTests : ViewRepositoryTests
{
    private IViewBarcodeRepository ViewBarcodeRepository { get; } = new SqlViewBarcodeRepository();

    protected override IResolveConstraint SortOrderValue => Is
        .Ordered.By(nameof(SqlViewBarcodeModel.CreateDt)).Descending;

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IList<SqlViewBarcodeModel> items = ViewBarcodeRepository.GetList(SqlCrudConfig);
            PrintViewRecords(items);
        }, false);
    }
}