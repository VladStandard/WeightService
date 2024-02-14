using Ws.Database.Core.Entities.Ref.ZplResources;
using Ws.Domain.Models.Entities.Scale;

namespace Ws.StorageCoreTests.Tables.TableScaleModels.TemplatesResources;

[TestFixture]
public sealed class TemplateResourceRepositoryTests : TableRepositoryTests
{
    private SqlZplResourceRepository ZplResourceRepository { get; } = new();

    protected override IResolveConstraint SortOrderValue =>
        Is.Ordered.By(nameof(TemplateResourceEntity.Name)).Ascending.Then.By(nameof(TemplateResourceEntity.Name))
            .Ascending;

    [Test]
    public void GetList()
    {
        // AssertAction(() =>
        // {
        //     IEnumerable<TemplateResourceEntity> items = TemplateResourceRepository.GetList();
        //     ParseRecords(items);
        // });
    }
}