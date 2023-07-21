using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusTemplatesFks;

public sealed class PluTemplateFkRepositoryTests : TableRepositoryTests
{
    private WsSqlPluTemplateFkRepository PluTemplateFkRepository  { get; set; } = new();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluTemplateFkModel> items = PluTemplateFkRepository.GetList(SqlCrudConfig);
            WsTestsUtils.DataTests.ParseRecords(items);
        }, false, DefaultPublishTypes);
    }
}