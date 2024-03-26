using Ws.Database.Nhibernate.Entities.Ref1c.Boxes;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.StorageCoreTests.Tables.TableRef1cModels.Boxes;

[TestFixture]
public sealed class BoxRepositoryTests : TableRepositoryTests
{
    private SqlBoxRepository BoxRepository { get; } = new();
    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(BoxEntity.Weight)).Ascending;

    [Test]
    public void GetList()
    {
        AssertAction(() =>
        {
            IEnumerable<BoxEntity> items = BoxRepository.GetAll();
            ParseRecords(items);
        });
    }
}