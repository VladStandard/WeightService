namespace WsStorageCoreTests.Tables.TableScaleFkModels.DeviceTypesFks;

public sealed class DeviceTypeFkRepositoryTests : TableRepositoryTests
{
    private WsSqlDeviceTypeFkRepository DeviceTypeFkRepository { get; set; } = new();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlDeviceTypeFkModel> items = DeviceTypeFkRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
}