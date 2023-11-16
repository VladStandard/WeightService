using Ws.StorageCoreTests.Tables.Common;
using Ws.StorageCore.Entities.SchemaRef.ProductionSites;

namespace Ws.StorageCoreTests.Tables.TableScaleModels.ProductionFacilities;

[TestFixture]
public sealed class ProductionFacilitiesRepositoryTests : TableRepositoryTests
{
    private SqlProductionSiteRepository ProductionSiteRepository { get; set; } = new();

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<SqlProductionSiteEntity> items = ProductionSiteRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }
}