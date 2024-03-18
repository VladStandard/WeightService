using Ws.Database.Nhibernate.Entities.Ref.Lines;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.StorageCoreTests.Tables.TableRefModels.Lines;

[TestFixture]
public sealed class LineRepositoryTests : TableRepositoryTests
{
    private SqlLineRepository LineRepository { get; } = new();
    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(LineEntity.Warehouse.Name)).Ascending;


    [Test, Order(1)]
    public void GetList()
    {
        AssertAction(() => {
            IEnumerable<LineEntity> items = LineRepository.GetAll();
            ParseRecords(items);
        });
    }
}