using Ws.Domain.Models.Entities.Print;
using Ws.StorageCore.Entities.Print.Pallets;

namespace Ws.StorageCoreTests.Tables.TablePrintModels.Pallets;

[TestFixture]
public sealed class PalletRepositoryTests : TableRepositoryTests
{
    private SqlPalletRepository PalletRepository { get; } = new();
    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(PalletEntity.CreateDt)).Descending;

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<PalletEntity> items = PalletRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        });
    }
}