using WsStorageCore.Views.ViewScaleModels.PluWeightings;

namespace WsStorageCoreTests.Views.ViewScaleModels.PluWeightings;

[TestFixture]
public sealed class ViewPluWeightingRepositoryTests : ViewRepositoryTests
{
    private IViewPluWeightingRepository ViewPluWeightingRepository = WsSqlViewPluWeightingRepository.Instance;
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewPluWeightingModel> items = ViewPluWeightingRepository.GetList(SqlCrudConfig);
            WsTestsUtils.DataTests.PrintViewRecords(items);
        }, false, DefaultPublishTypes);
    }
}