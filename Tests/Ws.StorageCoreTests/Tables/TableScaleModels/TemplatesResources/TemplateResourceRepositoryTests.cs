using Ws.StorageCore.Entities.SchemaScale.TemplatesResources;

namespace Ws.StorageCoreTests.Tables.TableScaleModels.TemplatesResources;

[TestFixture]
public sealed class TemplateResourceRepositoryTests : TableRepositoryTests
{
    private SqlTemplateResourceRepository TemplateResourceRepository { get; } = new();

    protected override IResolveConstraint SortOrderValue =>
        Is.Ordered.By(nameof(SqlEntityBase.Name)).Ascending.Then.By(nameof(SqlTemplateResourceEntity.Type)).Ascending;

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<SqlTemplateResourceEntity> items = TemplateResourceRepository.GetList();
            ParseRecords(items);
        });
    }
}