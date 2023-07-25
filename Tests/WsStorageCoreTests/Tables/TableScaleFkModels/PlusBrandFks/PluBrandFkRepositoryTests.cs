using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusBrandFks;

public sealed class PluBrandFkRepositoryTests : TableRepositoryTests
{
    private WsSqlPluBrandFkRepository PluBrandFkRepository { get; set; } = new();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluBrandFkModel> items = PluBrandFkRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultPublishTypes);
    }
}