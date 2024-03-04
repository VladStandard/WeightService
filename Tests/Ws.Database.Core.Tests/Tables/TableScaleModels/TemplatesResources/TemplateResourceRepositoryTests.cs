using Ws.Database.Core.Entities.Ref.ZplResources;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.StorageCoreTests.Tables.TableScaleModels.TemplatesResources;

[TestFixture]
public sealed class TemplateResourceRepositoryTests : TableRepositoryTests
{
    private SqlZplResourceRepository ZplResourceRepository { get; } = new();

    protected override IResolveConstraint SortOrderValue =>
        Is.Ordered.By(nameof(ZplResourceEntity.Name)).Ascending.Then.By(nameof(ZplResourceEntity.Name))
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