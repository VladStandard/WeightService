using Ws.StorageCore.Views.ViewScaleModels.Barcodes;

namespace Ws.StorageCoreTests.Views.ViewScaleModels.Barcodes;

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