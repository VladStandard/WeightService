namespace WsStorageCoreTests.Tables.TableDiagModels.ScalesScreenshots;

[TestFixture]
public sealed class ScaleScreenshotRepositoryTests : TableRepositoryTests
{
    private WsSqlScaleScreenshotRepository ScaleScreenshotRepository { get; set; } = new();
    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(WsSqlTableBase.ChangeDt)).Descending;

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlScaleScreenShotModel> items = ScaleScreenshotRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultPublishTypes);
    }
}