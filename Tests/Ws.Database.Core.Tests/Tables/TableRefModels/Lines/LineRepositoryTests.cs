using Ws.Database.Core.Entities.Ref.Lines;
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
        AssertAction(() =>
        {
            IEnumerable<LineEntity> items = LineRepository.GetAll();
            ParseRecords(items);
        });
    }

    [Test, Order(3)]
    public void GetItemByDevice()
    {
        AssertAction(() =>
        {
            LineEntity lineByDevice = LineRepository.GetItemByPcName("PC473");
            Assert.That(lineByDevice.IsExists, Is.True);
            TestContext.WriteLine(lineByDevice);
        });
    }
}