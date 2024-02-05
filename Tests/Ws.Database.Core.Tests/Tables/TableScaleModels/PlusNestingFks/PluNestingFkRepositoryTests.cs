using Ws.Database.Core.Entities.Scales.PlusNestingFks;
using Ws.Domain.Models.Entities.Scale;

namespace Ws.StorageCoreTests.Tables.TableScaleModels.PlusNestingFks;

[TestFixture]
public sealed class PluNestingFkRepositoryTests : TableRepositoryTests
{
    private SqlPluNestingFkRepository PluNestingFkRepository { get; } = new();

    protected override IResolveConstraint SortOrderValue =>
        Is.Ordered.Using((IComparer<PluNestingEntity>)Comparer<PluNestingEntity>.Create((x, y) =>
            x.Plu.Number.CompareTo(y.Plu.Number))).Ascending;

    [Test]
    public void GetList()
    {
        // AssertAction(() =>
        // {
        //     IEnumerable<PluNestingEntity> items = PluNestingFkRepository.GetEnumerable(SqlCrudConfig);
        //     ParseRecords(items);
        // });
    }
    
}