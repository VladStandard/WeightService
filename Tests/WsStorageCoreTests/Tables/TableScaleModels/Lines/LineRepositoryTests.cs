using NUnit.Framework.Constraints;

namespace WsStorageCoreTests.Tables.TableScaleModels.Lines;

[TestFixture]
public sealed class LineRepositoryTests : TableRepositoryTests
{
    private WsSqlLineRepository LineRepository { get; } = new();
    private WsSqlDeviceLineFkRepository DeviceLineFkRepository { get; } = new();

    private WsSqlScaleModel GetFirstLineModel()
    {
        return LineRepository.GetList(SqlCrudConfig).First();
    }

    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(WsSqlTableBase.Description)).Ascending;

    [Test, Order(1)]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlScaleModel> items = LineRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultPublishTypes);
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
            WsSqlDeviceScaleFkModel devicesScale = DeviceLineFkRepository.GetList(SqlCrudConfig).First();
            WsSqlDeviceModel device = devicesScale.Device;
            WsSqlScaleModel oldLine = devicesScale.Scale;

            WsSqlScaleModel lineByDevice = LineRepository.GetItemByDevice(device);

            Assert.That(lineByDevice.IsNotNew, Is.True);
            Assert.That(lineByDevice, Is.EqualTo(oldLine));

            TestContext.WriteLine(lineByDevice);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
}