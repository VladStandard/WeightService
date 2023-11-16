using Ws.StorageCore.Entities.SchemaScale.PlusWeightings;

namespace Ws.StorageCoreTests.Tables.TableScaleModels.PlusWeighings;

[TestFixture]
public sealed class PluWeighingRepositoryTests : TableRepositoryTests
{
    private SqlPluWeighingRepository PluWeighingRepository { get; } = new();
    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(SqlEntityBase.ChangeDt)).Descending;

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            List<SqlPluWeighingEntity> items = PluWeighingRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }
}