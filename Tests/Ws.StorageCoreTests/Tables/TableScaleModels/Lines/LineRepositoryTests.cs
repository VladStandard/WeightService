using Ws.StorageCore.Entities.SchemaScale.Scales;

namespace Ws.StorageCoreTests.Tables.TableScaleModels.Lines;

[TestFixture]
public sealed class LineRepositoryTests : TableRepositoryTests
{
    private SqlLineRepository LineRepository { get; } = new();
    private SqlLineEntity GetFirstLineModel()
    {
        return LineRepository.GetEnumerable(SqlCrudConfig).First();
    }

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
    public void GetById()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            SqlLineEntity oldLine = GetFirstLineModel();
            SqlLineEntity lineById = LineRepository.GetItemById(oldLine.IdentityValueId);

            Assert.That(lineById.IsExists, Is.True);
            Assert.That(lineById.IdentityValueId, Is.EqualTo(oldLine.IdentityValueId));

            TestContext.WriteLine($"Get item success: {lineById.Description}: {lineById.IdentityValueId}");
        }, false);
    }

    [Test, Order(4)]
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