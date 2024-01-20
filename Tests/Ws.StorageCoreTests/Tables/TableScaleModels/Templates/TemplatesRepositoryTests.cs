using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Models.Entities.SchemaScale;
using Ws.StorageCore.Entities.Scales.Templates;

namespace Ws.StorageCoreTests.Tables.TableScaleModels.Templates;

[TestFixture]
public sealed class TemplatesRepositoryTests : TableRepositoryTests
{
    private SqlTemplateRepository TemplateRepository { get; } = new();
    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(TemplateEntity.Title)).Ascending;

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            List<TemplateEntity> items = TemplateRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        });
    }
}