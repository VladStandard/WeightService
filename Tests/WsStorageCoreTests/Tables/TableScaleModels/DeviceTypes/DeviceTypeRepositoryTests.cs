namespace WsStorageCoreTests.Tables.TableScaleModels.DeviceTypes;

[TestFixture]
public sealed class DeviceTypeRepositoryTests : TableRepositoryTests
{
    private WsSqlDeviceRepository DeviceRepository { get; set; } = new();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlDeviceModel> items = DeviceRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
}