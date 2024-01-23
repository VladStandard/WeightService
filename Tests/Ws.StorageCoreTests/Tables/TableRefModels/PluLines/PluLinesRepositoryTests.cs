using Ws.Database.Core.Entities.Ref.PlusLines;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.StorageCoreTests.Tables.TableRefModels.PluLines;

[TestFixture]
public sealed class PluLinesRepositoryTests : TableRepositoryTests
{
    private SqlPluLineRepository PluLineRepository { get; } = new();

    protected override IResolveConstraint SortOrderValue =>
        Is.Ordered.Using(
            (IComparer<PluLineEntity>)Comparer<PluLineEntity>.Create((x, y) =>
                x.Plu.Number.CompareTo(y.Plu.Number))).Ascending;

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<PluLineEntity> items = PluLineRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        });
    }
}