using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableScaleModels.ProductionFacilities;

// TODO: ProductionFacilityRepository GetList

[TestFixture]
public sealed class ProductionFacilitiesRepositoryTests : TableRepositoryTests
{
    private WsSqlAreaRepository AreaRepository  { get; set; } = new();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlProductionFacilityModel> items = AreaRepository.GetList(SqlCrudConfig);
            WsTestsUtils.DataTests.ParseRecords(items);
        }, false, DefaultPublishTypes);
    }
}