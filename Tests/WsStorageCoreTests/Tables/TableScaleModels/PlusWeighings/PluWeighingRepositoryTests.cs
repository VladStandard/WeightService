using WsStorageCore.Entities.SchemaScale.PlusWeightings;

namespace WsStorageCoreTests.Tables.TableScaleModels.PlusWeighings;

[TestFixture]
public sealed class PluWeighingRepositoryTests : TableRepositoryTests
{
    private WsSqlPluWeighingRepository PluWeighingRepository { get; } = new();
    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(WsSqlEntityBase.ChangeDt)).Descending;

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluWeighingEntity> items = PluWeighingRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }
}