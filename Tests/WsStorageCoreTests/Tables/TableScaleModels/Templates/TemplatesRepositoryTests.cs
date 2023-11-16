using WsStorageCore.Entities.SchemaScale.Templates;

namespace WsStorageCoreTests.Tables.TableScaleModels.Templates;

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