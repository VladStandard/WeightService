using Ws.Domain.Models.Entities.Ref;
using Ws.StorageCore.Entities.Ref.ProductionSites;

namespace Ws.StorageCoreTests.Tables.TableRefModels.ProductionFacilities;

[TestFixture]
public sealed class ProductionFacilitiesRepositoryTests : TableRepositoryTests
{
    private SqlProductionSiteRepository ProductionSiteRepository { get; set; } = new();

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<ProductionSiteEntity> items = ProductionSiteRepository.GetEnumerable();
            ParseRecords(items);
        });
    }
}