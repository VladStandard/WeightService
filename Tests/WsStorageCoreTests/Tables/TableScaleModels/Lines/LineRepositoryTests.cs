namespace WsStorageCoreTests.Tables.TableScaleModels.Lines;

[TestFixture]
public sealed class LineRepositoryTests : TableRepositoryTests
{
    private WsSqlLineRepository LineRepository { get; } = new();
    private WsSqlScaleModel GetFirstLineModel()
    {
        return LineRepository.GetEnumerable(SqlCrudConfig).First();
    }

    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(WsSqlTableBase.Description)).Ascending;

    [Test, Order(1)]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<WsSqlScaleModel> items = LineRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }

    [Test, Order(3)]
    public void GetById()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlScaleModel oldLine = GetFirstLineModel();
            WsSqlScaleModel lineById = LineRepository.GetItemById(oldLine.IdentityValueId);

            Assert.That(lineById.IsExists, Is.True);
            Assert.That(lineById.IdentityValueId, Is.EqualTo(oldLine.IdentityValueId));

            TestContext.WriteLine($"Get item success: {lineById.Description}: {lineById.IdentityValueId}");
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }

    [Test, Order(4)]
    public void GetItemByDevice()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            SqlCrudConfig.SelectTopRowsCount = 1;
            WsSqlScaleModel oldScale = LineRepository.GetEnumerable(SqlCrudConfig).First();

            WsSqlScaleModel lineByDevice = LineRepository.GetItemByDevice(oldScale.Device);

            Assert.That(lineByDevice.IsExists, Is.True);
            Assert.That(lineByDevice.IdentityValueId, Is.EqualTo(oldScale.IdentityValueId));
            
            TestContext.WriteLine(lineByDevice);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
}