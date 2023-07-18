namespace WsStorageCoreTests.Tables.TableScaleModels.ProductionFacilities;

// TODO: ProductionFacilityRepository GetList

[TestFixture]
public sealed class ProductionFacilitiesRepositoryTests : TableRepositoryTests
{
    private WsSqlProductionFacilityRepository ProductionFacilityRepository { get; set; } = new();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
        }, false, DefaultPublishTypes);
    }
}