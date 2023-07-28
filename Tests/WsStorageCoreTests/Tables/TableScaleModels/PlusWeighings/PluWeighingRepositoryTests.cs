using NUnit.Framework.Constraints;
using WsStorageCore.Tables.TableScaleModels.PlusWeighings;
using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableScaleModels.PlusWeighings;

[TestFixture]
public sealed class PluWeighingRepositoryTests : TableRepositoryTests
{
    private WsSqlPluWeighingRepository PluWeighingRepository { get; } = new();
    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(WsSqlTableBase.ChangeDt)).Descending;

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluWeighingModel> items = PluWeighingRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultPublishTypes);
    }
}