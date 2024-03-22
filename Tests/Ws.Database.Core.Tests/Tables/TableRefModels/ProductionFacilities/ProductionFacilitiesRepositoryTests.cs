using Ws.Database.Nhibernate.Entities.Ref.ProductionSites;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.StorageCoreTests.Tables.TableRefModels.ProductionFacilities;

[TestFixture]
public sealed class ProductionFacilitiesRepositoryTests : TableRepositoryTests
{
    private SqlProductionSiteRepository ProductionSiteRepository { get; } = new();

    [Test]
    public void GetList()
    {
        AssertAction(() =>
        {
            IEnumerable<ProductionSiteEntity> items = ProductionSiteRepository.GetAll();
            ParseRecords(items);
        });
    }
}