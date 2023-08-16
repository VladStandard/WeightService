namespace WsStorageCoreTests.Tables.TableScaleModels.ProductionFacilities;

[TestFixture]
public sealed class ProductionFacilitiesRepositoryTests : TableRepositoryTests
{
    private WsSqlAreaRepository AreaRepository { get; set; } = new();

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<WsSqlProductionFacilityModel> items = AreaRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }
}