using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusGroupsFks;

[TestFixture]
public sealed class PluGroupFkRepositoryTests : TableRepositoryTests
{
    private WsSqlPluGroupFkRepository PluGroupFkRepository { get; } = new();

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