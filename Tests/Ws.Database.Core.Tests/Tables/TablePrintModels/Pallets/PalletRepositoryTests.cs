using Ws.Database.Nhibernate.Entities.Print.Pallets;
using Ws.Domain.Models.Entities.Print;

namespace Ws.StorageCoreTests.Tables.TablePrintModels.Pallets;

[TestFixture]
public sealed class PalletRepositoryTests : TableRepositoryTests
{
    private SqlPalletRepository PalletRepository { get; } = new();
    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(PalletEntity.CreateDt)).Descending;

    [Test]
    public void GetList()
    {
        // AssertAction(() =>
        // {
        //     // IEnumerable<PalletEntity> items = PalletRepository.GetList(SqlCrudConfig);
        //     ParseRecords(items);
        // });
    }
}