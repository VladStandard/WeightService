using WsStorageCore.Entities.SchemaScale.Templates;
namespace WsStorageCoreTests.Tables.TableScaleModels.Templates;

[TestFixture]
public sealed class TemplatesRepositoryTests : TableRepositoryTests
{
    private WsSqlTemplateRepository TemplateRepository { get; } = new();
    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(WsSqlTemplateEntity.Title)).Ascending;

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlTemplateEntity> items = TemplateRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }
}