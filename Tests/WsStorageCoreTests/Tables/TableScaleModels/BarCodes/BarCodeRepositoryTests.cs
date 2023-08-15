namespace WsStorageCoreTests.Tables.TableScaleModels.BarCodes;

[TestFixture]
public sealed class BarCodeRepositoryTests : TableRepositoryTests
{
    private WsSqlBarcodeRepository BarcodeRepository { get; } = new();

    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(WsSqlTableBase.ChangeDt)).Descending;

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlBarCodeModel> items = BarcodeRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }
}