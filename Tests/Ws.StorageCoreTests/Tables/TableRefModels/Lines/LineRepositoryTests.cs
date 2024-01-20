using Ws.StorageCore.Entities.SchemaRef.Lines;

namespace Ws.StorageCoreTests.Tables.TableRefModels.Lines;

[TestFixture]
public sealed class LineRepositoryTests : TableRepositoryTests
{
    private SqlLineRepository LineRepository { get; } = new();

    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(SqlEntityBase.Name)).Ascending;

    [Test, Order(1)]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<SqlLineEntity> items = LineRepository.GetAll();
            ParseRecords(items);
        });
    }

    [Test, Order(3)]
    public void GetItemByDevice()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            SqlLineEntity lineByDevice = LineRepository.GetItemByPcName("PC473");
            Assert.That(lineByDevice.IsExists, Is.True);
            TestContext.WriteLine(lineByDevice);
        });
    }
}