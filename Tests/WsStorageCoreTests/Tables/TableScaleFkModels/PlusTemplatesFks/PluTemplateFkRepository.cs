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
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
}