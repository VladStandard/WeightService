using Ws.Database.Core.Entities.Scales.Templates;
using Ws.Domain.Models.Entities.Scale;

namespace Ws.StorageCoreTests.Tables.TableScaleModels.Templates;

[TestFixture]
public sealed class TemplatesRepositoryTests : TableRepositoryTests
{
    private SqlTemplateRepository TemplateRepository { get; } = new();
    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(TemplateEntity.Name)).Ascending;

    [Test]
    public void GetList()
    {
        // AssertAction(() =>
        // {
        //     IEnumerable<TemplateEntity> items = TemplateRepository.GetList(SqlCrudConfig);
        //     ParseRecords(items);
        // });
    }
}