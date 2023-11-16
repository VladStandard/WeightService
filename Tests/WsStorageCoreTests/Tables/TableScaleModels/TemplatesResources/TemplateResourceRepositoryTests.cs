using WsStorageCore.Entities.SchemaScale.TemplatesResources;

namespace WsStorageCoreTests.Tables.TableScaleModels.TemplatesResources;

[TestFixture]
public sealed class TemplateResourceRepositoryTests : TableRepositoryTests
{
    private SqlTemplateResourceRepository TemplateResourceRepository { get; } = new();

    protected override IResolveConstraint SortOrderValue =>
        Is.Ordered.By(nameof(SqlEntityBase.Name)).Ascending.Then.By(nameof(SqlTemplateResourceEntity.Type)).Ascending;

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<SqlTemplateResourceEntity> items = TemplateResourceRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }
}