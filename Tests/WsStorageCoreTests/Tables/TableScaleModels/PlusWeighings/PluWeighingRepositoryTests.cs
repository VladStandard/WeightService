using WsStorageCore.Entities.SchemaScale.PlusWeightings;

namespace WsStorageCoreTests.Tables.TableScaleModels.PlusWeighings;

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