using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Models.Entities.SchemaScale;
using Ws.StorageCore.Entities.Scales.PlusNestingFks;

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
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<PluNestingEntity> items = PluNestingFkRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        });
    }
    
}