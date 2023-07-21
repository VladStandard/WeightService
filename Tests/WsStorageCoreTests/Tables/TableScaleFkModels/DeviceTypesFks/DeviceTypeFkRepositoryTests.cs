using WsStorageCoreTests.Tables.Common;

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
            WsTestsUtils.DataTests.ParseRecords(items);
        }, false, DefaultPublishTypes);
    }
}