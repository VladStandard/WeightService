using WsStorageCore.Tables.TableScaleFkModels.PlusFks;
using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusFks;

public sealed class PluFkRepositoryTests : TableRepositoryTests
{
    private WsSqlPluFkRepository PluFkRepository { get; set; } = new();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluFkModel> items = PluFkRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultPublishTypes);
    }
}