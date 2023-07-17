using WsStorageCore.Views.ViewScaleModels.PluWeightings;

namespace WsStorageCoreTests.Views.ViewScaleModels.PluWeightings;

[TestFixture]
public sealed class ViewPluWeightingRepositoryTests
{
    private WsSqlViewPluWeightingRepository ViewPluWeightingRepository = WsSqlViewPluWeightingRepository.Instance;
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlCrudConfigModel sqlConfig = new WsSqlCrudConfigModel() { SelectTopRowsCount = 10 };
            List<WsSqlViewPluWeightingModel> items = ViewPluWeightingRepository.GetList(sqlConfig);
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
}