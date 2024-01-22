using Ws.Database.Core.Entities;
using Ws.Domain.Models.Entities;

namespace Ws.StorageCoreTests.Views;

[TestFixture]
public sealed class ViewDbFileSizeInfoRepositoryTest : ViewRepositoryTests
{
    private SqlViewDbFileSizeRepository DbFileSizeRepository { get; } = new();

    protected override CollectionOrderedConstraint SortOrderValue =>
        Is.Ordered.By(nameof(DbFileSizeInfoEntity.SizeMb)).Descending
            .Then.By(nameof(DbFileSizeInfoEntity.FileName)).Ascending;

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            List<DbFileSizeInfoEntity> items = DbFileSizeRepository.GetList();
            foreach (DbFileSizeInfoEntity info in items)
            {
                TestContext.WriteLine($"{info.FileName}: {info.DbFillSize}%");
                Assert.That(info.SizeMb, Is.LessThan(10240));
            }
        });
    }
}