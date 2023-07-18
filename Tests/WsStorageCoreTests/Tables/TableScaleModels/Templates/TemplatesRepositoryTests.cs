namespace WsStorageCoreTests.Tables.TableScaleModels.Templates;

[TestFixture]
public sealed class TemplatesRepositoryTests : TableRepositoryTests
{
    private WsSqlTemplateRepository TemplateRepository { get; set; } = new();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlTemplateModel> items = TemplateRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
}