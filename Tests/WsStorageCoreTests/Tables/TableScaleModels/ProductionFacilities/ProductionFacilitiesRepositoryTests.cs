using WsStorageCore.Entities.SchemaRef.ProductionSites;

namespace WsStorageCoreTests.Tables.TableScaleModels.ProductionFacilities;

[TestFixture]
public sealed class ProductionFacilitiesRepositoryTests : TableRepositoryTests
{
    private WsSqlProductionSiteRepository ProductionSiteRepository { get; set; } = new();

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<WsSqlProductionSiteEntity> items = ProductionSiteRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }
}