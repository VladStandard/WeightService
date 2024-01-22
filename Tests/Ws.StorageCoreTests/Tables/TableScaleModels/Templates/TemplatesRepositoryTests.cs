using Ws.Domain.Models.Entities.Scale;
using Ws.Database.Core.Entities.Scales.Templates;

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
            IEnumerable<TemplateEntity> items = TemplateRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        });
    }
}