using Ws.Domain.Models.Entities;
using Ws.StorageCore.Entities;

namespace Ws.StorageCoreTests.Views;

[TestFixture]
public sealed class ViewDbFileSizeInfoRepositoryTest : ViewRepositoryTests
{
    private SqlViewDbFileSizeRepository DbFileSizeRepository { get; } = new();

    protected override CollectionOrderedConstraint SortOrderValue =>
        Is.Ordered.By(nameof(WsSqlViewDbFileSizeInfoModel.SizeMb)).Descending
            .Then.By(nameof(WsSqlViewDbFileSizeInfoModel.FileName)).Ascending;

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewDbFileSizeInfoModel> items = DbFileSizeRepository.GetList();
            foreach (WsSqlViewDbFileSizeInfoModel info in items)
            {
                TestContext.WriteLine($"{info.FileName}: {info.DbFillSize}%");
                Assert.That(info.SizeMb, Is.LessThan(10240));
            }
        });
    }
}