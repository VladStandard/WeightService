using WsStorageCore.Entities.SchemaScale.Scales;

namespace WsStorageCoreTests.Tables.TableScaleModels.Lines;

[TestFixture]
public sealed class LineRepositoryTests : TableRepositoryTests
{
    private WsSqlLineRepository LineRepository { get; } = new();
    private WsSqlScaleEntity GetFirstLineModel()
    {
        return LineRepository.GetEnumerable(SqlCrudConfig).First();
    }

    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(WsSqlEntityBase.Description)).Ascending;

    [Test, Order(1)]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<WsSqlScaleEntity> items = LineRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }

    [Test, Order(3)]
    public void GetById()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlScaleEntity oldLine = GetFirstLineModel();
            WsSqlScaleEntity lineById = LineRepository.GetItemById(oldLine.IdentityValueId);

            Assert.That(lineById.IsExists, Is.True);
            Assert.That(lineById.IdentityValueId, Is.EqualTo(oldLine.IdentityValueId));

            TestContext.WriteLine($"Get item success: {lineById.Description}: {lineById.IdentityValueId}");
        }, false);
    }

    [Test, Order(4)]
    public void GetItemByDevice()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            SqlCrudConfig.SelectTopRowsCount = 1;
            WsSqlScaleEntity oldScale = LineRepository.GetEnumerable(SqlCrudConfig).First();

            WsSqlScaleEntity lineByDevice = LineRepository.GetItemByHost(oldScale.Host);

            Assert.That(lineByDevice.IsExists, Is.True);
            Assert.That(lineByDevice.IdentityValueId, Is.EqualTo(oldScale.IdentityValueId));
            
            TestContext.WriteLine(lineByDevice);
        }, false);
    }
}