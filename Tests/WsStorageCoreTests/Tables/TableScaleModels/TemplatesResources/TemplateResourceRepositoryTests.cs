namespace WsStorageCoreTests.Tables.TableScaleModels.TemplatesResources;

[TestFixture]
public sealed class TemplateResourceRepositoryTests : TableRepositoryTests
{
    private WsSqlTemplateResourceRepository TemplateResourceRepository { get; set; } = new();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlTemplateResourceModel> items = TemplateResourceRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
}