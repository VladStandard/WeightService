using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusGroupsFks;

public sealed class PluGroupFkRepositoryTests : TableRepositoryTests
{
    private WsSqlPluGroupFkRepository PluGroupFkRepository { get; set; } = new();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluGroupFkModel> items = PluGroupFkRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultPublishTypes);
    }
}