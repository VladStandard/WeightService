using WsStorageCore.Entities.SchemaScale.BarCodes;

namespace WsStorageCoreTests.Tables.TableScaleModels.BarCodes;

[TestFixture]
public sealed class BarCodeRepositoryTests : TableRepositoryTests
{
    private SqlBarcodeRepository BarcodeRepository { get; } = new();

    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(SqlEntityBase.ChangeDt)).Descending;

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            List<SqlBarCodeEntity> items = BarcodeRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }
}