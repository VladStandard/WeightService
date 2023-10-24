using WsStorageCore.Entities.SchemaScale.TemplatesResources;
namespace WsStorageCoreTests.Tables.TableScaleModels.TemplatesResources;

[TestFixture]
public sealed class TemplateResourceRepositoryTests : TableRepositoryTests
{
    private WsSqlTemplateResourceRepository TemplateResourceRepository { get; } = new();

    protected override IResolveConstraint SortOrderValue =>
        Is.Ordered.By(nameof(WsSqlTableBase.Name)).Ascending.Then.By(nameof(WsSqlTemplateResourceEntity.Type)).Ascending;

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlTemplateResourceEntity> items = TemplateResourceRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }
}