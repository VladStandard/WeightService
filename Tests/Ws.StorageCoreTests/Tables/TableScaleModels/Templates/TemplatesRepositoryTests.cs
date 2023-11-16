using Ws.StorageCore.Entities.SchemaScale.Templates;

namespace Ws.StorageCoreTests.Tables.TableScaleModels.Templates;

[TestFixture]
public sealed class TemplatesRepositoryTests : TableRepositoryTests
{
    private SqlTemplateRepository TemplateRepository { get; } = new();
    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(SqlTemplateEntity.Title)).Ascending;

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            List<SqlTemplateEntity> items = TemplateRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }
}