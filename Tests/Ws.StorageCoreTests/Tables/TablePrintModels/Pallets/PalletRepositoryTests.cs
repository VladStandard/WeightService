using Ws.StorageCore.Entities.SchemaPrint.Pallets;

namespace Ws.StorageCoreTests.Tables.TablePrintModels.Pallets;

[TestFixture]
public sealed class PalletRepositoryTests : TableRepositoryTests
{
    private SqlPalletRepository PalletRepository { get; } = new();
    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(SqlEntityBase.CreateDt)).Descending;

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<SqlPalletEntity> items = PalletRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }
}