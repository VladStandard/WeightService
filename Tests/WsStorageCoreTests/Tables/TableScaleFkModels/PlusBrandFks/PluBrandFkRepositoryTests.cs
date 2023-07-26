using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusBrandFks;

[TestFixture]
public sealed class PluBrandFkRepositoryTests : TableRepositoryTests
{
    private WsSqlPluBrandFkRepository PluBrandFkRepository { get; } = new();
    
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