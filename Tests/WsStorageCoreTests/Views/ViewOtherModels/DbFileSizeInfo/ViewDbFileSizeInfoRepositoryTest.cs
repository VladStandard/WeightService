using WsStorageCore.Views.ViewOtherModels.DbFileSizeInfo;

namespace WsStorageCoreTests.Views.ViewOtherModels.DbFileSizeInfo;

[TestFixture]
public sealed class ViewDbFileSizeInfoRepositoryTest : ViewRepositoryTests
{
    private IViewDbFileSizeRepository DbFileSizeRepository { get; } = new SqlViewDbFileSizeRepository();

    protected override CollectionOrderedConstraint SortOrderValue =>
        Is.Ordered.By(nameof(WsSqlViewDbFileSizeInfoModel.SizeMb)).Descending
            .Then.By(nameof(WsSqlViewDbFileSizeInfoModel.FileName)).Ascending;

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewDbFileSizeInfoModel> items = DbFileSizeRepository.GetList();
            foreach (WsSqlViewDbFileSizeInfoModel info in items)
            {
                TestContext.WriteLine($"{info.FileName}: {info.DbFillSize}%");
                Assert.That(info.SizeMb, Is.LessThan(10240));
            }
        }, false);
    }
}