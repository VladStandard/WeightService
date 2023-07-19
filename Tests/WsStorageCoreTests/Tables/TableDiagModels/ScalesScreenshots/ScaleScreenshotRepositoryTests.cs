using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableDiagModels.ScalesScreenshots;

[TestFixture]
public sealed class ScaleScreenshotRepositoryTests : TableRepositoryTests
{
    private WsSqlScaleScreenshotRepository ScaleScreenshotRepository  { get; set; } = new();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlScaleScreenShotModel> items = ScaleScreenshotRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
}