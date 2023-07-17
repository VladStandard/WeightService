using WsStorageCore.Views.ViewRefModels.PluStorageMethods;

namespace WsStorageCoreTests.Views.ViewRefModels.PluStorageMethods;

public class ViewPluStorageRepository : ViewRepositoryTests
{
    private IViewStorageMethodsRepository ViewPluStorageMethodRepository = WsSqlViewPluStorageMethodRepository.Instance;

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewPluStorageMethodModel> items = ViewPluStorageMethodRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
}