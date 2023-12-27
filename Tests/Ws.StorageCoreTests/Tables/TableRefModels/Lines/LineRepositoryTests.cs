using Ws.StorageCore.Entities.SchemaRef.Lines;

namespace Ws.StorageCoreTests.Tables.TableRefModels.Lines;

[TestFixture]
public sealed class LineRepositoryTests : TableRepositoryTests
{
    private SqlLineRepository LineRepository { get; } = new();

    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(SqlEntityBase.Description)).Ascending;

    [Test, Order(1)]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<SqlLineEntity> items = LineRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }

    [Test, Order(3)]
    public void GetItemByDevice()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            SqlCrudConfig.SelectTopRowsCount = 1;
            SqlLineEntity oldLine = LineRepository.GetEnumerable(SqlCrudConfig).First();

            SqlLineEntity lineByDevice = LineRepository.GetItemByHost(oldLine.Host);

            Assert.That(lineByDevice.IsExists, Is.True);
            Assert.That(lineByDevice.IdentityValueId, Is.EqualTo(oldLine.IdentityValueId));
            
            TestContext.WriteLine(lineByDevice);
        }, false);
    }
}