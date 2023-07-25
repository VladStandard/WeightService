using WsStorageCore.Views.ViewScaleModels.PluWeightings;

namespace WsStorageCoreTests.Views.ViewScaleModels.PluWeightings;

[TestFixture]
public sealed class ViewPluWeightingRepositoryTests : ViewRepositoryTests
{
    private IViewPluWeightingRepository ViewPluWeightingRepository { get; } = new WsSqlViewPluWeightingRepository();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewPluWeightingModel> items = ViewPluWeightingRepository.GetList(SqlCrudConfig);
            PrintViewRecords(items);
        }, false, DefaultPublishTypes);
    }
}