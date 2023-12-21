using Ws.StorageCore.Entities.SchemaPrint.Pallets;

namespace Ws.StorageCoreTests.Tables.TablePrintModels.Pallets;

[TestFixture]
public sealed class PalletRepositoryTests : TableRepositoryTests
{
    private SqlPalletEntity PluWeighingRepository { get; } = new();
    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(SqlEntityBase.ChangeDt)).Descending;

    [Test]
    public void GetList()
    {
        // TestsUtils.DataTests.AssertAction(() =>
        // {
        //     IEnumerable<SqlPluWeighingEntity> items = PluWeighingRepository.GetList(SqlCrudConfig);
        //     ParseRecords(items);
        // }, false);
    }
}