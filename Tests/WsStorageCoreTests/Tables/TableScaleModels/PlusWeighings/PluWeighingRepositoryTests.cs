using WsStorageCore.Tables.TableScaleModels.PlusWeighings;
using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableScaleModels.PlusWeighings;

[TestFixture]
public sealed class PluWeighingRepositoryTests : TableRepositoryTests
{
    private WsSqlPluWeighingRepository PluWeighingRepository { get; set; } = new();
    
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